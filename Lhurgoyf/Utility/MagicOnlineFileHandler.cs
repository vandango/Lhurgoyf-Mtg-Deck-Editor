using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Utility {
	public class MagicOnlineFileHandler : FileHandler {
		public override string Generate(Deck deck) {
			List<DeckCard> mainboardList = new List<DeckCard>();
			List<DeckCard> sideboardList = new List<DeckCard>();

			foreach(DeckCard card in deck.CardList) {
				if(card.Sideboard) {
					sideboardList.Add(card);
				}
				else {
					mainboardList.Add(card);
				}
			}

			// generate mainboard
			StringBuilder maindeck = new StringBuilder();

			foreach(DeckCard card in mainboardList) {
				maindeck.Append(card.Amount.ToString());
				maindeck.Append(" ");
				maindeck.Append(card.Card.Name);
				maindeck.AppendLine();
			}

			// generate sideboard
			StringBuilder sideboard = new StringBuilder();

			if(sideboardList.Count > 0) {
				sideboard.AppendLine("Sideboard");

				foreach(DeckCard card in sideboardList) {
					sideboard.Append(card.Amount.ToString());
					sideboard.Append(" ");
					sideboard.Append(card.Card.Name);
					sideboard.AppendLine();
				}
			}

			// generate full file content
			StringBuilder full = new StringBuilder();

			full.Append(maindeck.ToString());
			full.Append(sideboard.ToString());

			return full.ToString();
		}

		public override Deck Load(string filename) {
			if(File.Exists(filename)) {
				StreamReader reader = new StreamReader(filename);

				List<string> list = new List<string>();

				while(!reader.EndOfStream) {
					list.Add(reader.ReadLine());
				}

				reader.Close();
				reader.Dispose();

				Deck deck = new Deck();
				deck.Filename = filename;
				deck.Name = Path.GetFileNameWithoutExtension(filename);

				bool isSideboard = false;

				foreach(string line in list) {
					if(!line.IsNullOrTrimmedEmpty()
					&& !line.StartsWith("//")) {
						string correctLine = line;

						// kill spaces and tabs inside of the string
						correctLine = correctLine.Replace("\t", " ").Trim();

						// sb?
						if(correctLine.ToLower().Equals("sideboard")) {
							isSideboard = true;
						}

						if(correctLine.StartsWith("SB:")) {
							correctLine = correctLine.Replace("SB:", "").Trim();
							isSideboard = true;
						}

						// add
						if(!correctLine.ToLower().Equals("sideboard")) {
							string amount = "";
							string name = "";

							if(correctLine[0].ToString().IsNumeric()) {
								amount = correctLine.Substring(0, correctLine.IndexOf(" ")).Trim();
								name = correctLine.Substring(correctLine.IndexOf(" ")).Trim();
							}
							else {
								amount = "1";
								name = correctLine.Trim();
							}

							DeckCard dc = new DeckCard();
							dc.Amount = amount.ToInt32();

							// the card object will be laoded by setting the name (part of the property)
							dc.Name = name;

							dc.Sideboard = isSideboard;

							deck.CardList.Add(dc);
						}
					}
				}

				return deck;
			}

			return null;
		}
	}
}
