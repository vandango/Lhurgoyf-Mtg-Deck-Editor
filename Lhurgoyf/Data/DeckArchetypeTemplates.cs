using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Data {
	public class DeckArchetypeTemplates {
		public static DeckArchetypeTemplates Instance = new DeckArchetypeTemplates();

		public List<Deck> Data { get; private set; }

		public DeckArchetypeTemplates() {
			this.Data = new List<Deck>();

			this.Init();
		}

		private void Init() {
			if(this.Data != null) {
				// load template files
				DirectoryInfo dir = new DirectoryInfo("data/templates/");

				if(!dir.Exists) {
					dir.Create();
				}

				foreach(FileInfo file in dir.GetFiles("*.dectpl")) {
					List<string> lines = new List<string>(File.ReadAllLines(file.FullName));

					Deck deck = new Deck();

					foreach(string line in lines) {
						string correctLine = line;

						// kill spaces and tabs inside of the string
						correctLine = correctLine.Replace("\t", " ").Trim();

						// name
						if(line.StartsWith("// NAME:")) {
							string name = correctLine.Substring(correctLine.IndexOf("NAME") + 4)
								.Replace(":", "")
								.Trim();

							deck.Name = name;
						}

						// format
						if(line.StartsWith("// FORMAT:")) {
							deck.Type = new TournamentType();

							string format = correctLine.Substring(correctLine.IndexOf("FORMAT") + 6)
								.Replace(":", "")
								.Trim();

							string formatId = "";
							string formatDesc = "";

							if(format.Contains("(")
							&& format.Contains(")")) {
								formatId = format.Substring(0, format.IndexOf("("));
								formatDesc = format.Substring(format.IndexOf("("))
									.Replace("(", "")
									.Replace(")", "")
									.Trim();
							}
							else {
								formatId = format;
							}

							if(!formatDesc.IsNullOrTrimmedEmpty()) {
								deck.Type.TypeSubFormat = formatDesc;
							}

							switch(formatId.ToLower()) {
								case "standard":
								case "t2":
									deck.Type.Type = TournamentFormat.T2;
									break;

								case "legacy":
								case "t1.5":
								case "t15":
								case "t1_5":
								case "t1-5":
									deck.Type.Type = TournamentFormat.T1_5;
									break;

								case "vintage":
								case "t1":
									deck.Type.Type = TournamentFormat.T1;
									break;

								case "extended":
								case "t1.x":
								case "t1x":
								case "t1_x":
								case "t1-x":
									deck.Type.Type = TournamentFormat.T1_x;
									break;

								case "highlander":
									deck.Type.Type = TournamentFormat.Highlander;
									break;

								case "commander":
								case "edh":
									deck.Type.Type = TournamentFormat.Commander;
									break;

								case "modern":
									deck.Type.Type = TournamentFormat.Modern;
									break;

								case "block":
									deck.Type.Type = TournamentFormat.Block;
									break;

								case "casual":
								default:
									deck.Type.Type = TournamentFormat.Casual;
									break;
							}
						}

						// load cards
						if(!correctLine.IsNullOrTrimmedEmpty()
						&& !correctLine.StartsWith("//")) {
							string amount = "";
							string name = "";

							CardColor addColor = null;

							bool isOptional = false;

							// isOptional
							if(correctLine.StartsWith("opt: ")) {
								correctLine = correctLine.Replace("opt: ", "");
								isOptional = true;
							}

							// red
							if(correctLine.StartsWith("R: ")) {
								correctLine = correctLine.Replace("R: ", "");

								addColor = new CardColor();
								addColor.Symbol = ColorSymbol.R;

								isOptional = true;
							}

							// green
							if(correctLine.StartsWith("G: ")) {
								correctLine = correctLine.Replace("G: ", "");

								addColor = new CardColor();
								addColor.Symbol = ColorSymbol.G;

								isOptional = true;
							}

							// blue
							if(correctLine.StartsWith("U: ")) {
								correctLine = correctLine.Replace("U: ", "");

								addColor = new CardColor();
								addColor.Symbol = ColorSymbol.U;

								isOptional = true;
							}

							// white
							if(correctLine.StartsWith("W: ")) {
								correctLine = correctLine.Replace("W: ", "");

								addColor = new CardColor();
								addColor.Symbol = ColorSymbol.W;

								isOptional = true;
							}

							// black
							if(correctLine.StartsWith("B: ")) {
								correctLine = correctLine.Replace("B: ", "");

								addColor = new CardColor();
								addColor.Symbol = ColorSymbol.B;

								isOptional = true;
							}

							// cardname
							if(correctLine[0].ToString().IsNumeric()) {
								amount = correctLine.Substring(0, correctLine.IndexOf(" ")).Trim();
								name = correctLine.Substring(correctLine.IndexOf(" ")).Trim();
							}
							else {
								amount = "1";
								name = correctLine.Trim();
							}

							ArchetypeDeckCard dc = new ArchetypeDeckCard();
							dc.Amount = amount.ToInt32();
							dc.IsOptional = isOptional;
							dc.AdditionalDeckColor = addColor;

							// the card object will be laoded by setting the name (part of the property)
							dc.Name = name;

							deck.CardList.Add(dc);
						}
					}

					// add to template list
					this.Data.Add(deck);
				}
			}
		}
	}
}
