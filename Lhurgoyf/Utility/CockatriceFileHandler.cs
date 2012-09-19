using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Utility {
	public class CockatriceFileHandler : FileHandler {
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

			// generate header
			StringBuilder header = new StringBuilder();

			if(!deck.Name.IsNullOrTrimmedEmpty()
			|| !deck.Author.IsNullOrTrimmedEmpty()) {
				header.Append("\t<deckname>");
			}

			if(!deck.Name.IsNullOrTrimmedEmpty()) {
				header.Append(deck.Name);
			}
			else {
				header.Append(Path.GetFileNameWithoutExtension(deck.Filename));
			}

			if(!deck.Author.IsNullOrTrimmedEmpty()) {
				header.Append(" by ");
				header.Append(deck.Author);
			}

			if(header.Length > 0) {
				header.AppendLine("</deckname>");
			}

			if(!deck.Comment.IsNullOrTrimmedEmpty()) {
				header.Append("\t<comments>");
				header.Append(deck.Comment);
				header.Append(" - ");
				header.Append("Exported by Lhurgoyf Deck Library");
				header.AppendLine("</comments>");
			}
			else {
				header.Append("\t<comments>Exported by Lhurgoyf Deck Library</comments>");
			}

			// generate sideboard
			StringBuilder maindeck = new StringBuilder();

			maindeck.AppendLine("\t<zone name=\"main\">");

			foreach(DeckCard card in mainboardList) {
				maindeck.Append("\t\t<card number=\"");
				maindeck.Append(card.Amount.ToString());
				maindeck.Append("\" name=\"");
				maindeck.Append(card.Card.Name);
				maindeck.Append("\"/>");
				maindeck.AppendLine();
			}

			maindeck.AppendLine("\t</zone>");

			// generate sideboard
			StringBuilder sideboard = new StringBuilder();

			sideboard.AppendLine("\t<zone name=\"side\">");

			foreach(DeckCard card in sideboardList) {
				sideboard.Append("\t\t<card number=\"");
				sideboard.Append(card.Amount.ToString());
				sideboard.Append("\" name=\"");
				sideboard.Append(card.Card.Name);
				sideboard.Append("\"/>");
				sideboard.AppendLine();
			}

			sideboard.AppendLine("\t</zone>");

			// generate full file content
			StringBuilder full = new StringBuilder();

			full.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			full.AppendLine("<cockatrice_deck version=\"1\">");
			full.Append(header.ToString());
			full.Append(maindeck.ToString());
			full.Append(sideboard.ToString());
			full.AppendLine("</cockatrice_deck>");

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

				// TODO: Implement XML Document, currently line parsing is enough

				bool isSideboard = false;

				foreach(string line in list) {
					string correctLine = line;

					// kill spaces and tabs inside of the string
					correctLine = correctLine.Replace("\t", " ").Trim();
					
					// name
					if(correctLine.Contains("<deckname>")) {
						string nameLine = correctLine.Substring(correctLine.IndexOf("<deckname>") + 10)
							.Trim();
						
						nameLine = nameLine.Substring(0, nameLine.IndexOf("</deckname>"));

						if(nameLine.Contains(" by ")) {
							string[] splitted = nameLine.Split(new string[] { " by " }, StringSplitOptions.RemoveEmptyEntries);

							deck.Name = splitted[0].Trim();

							if(splitted.Length > 1) {
								deck.Author = splitted[1].Trim();
							}
						}
						else {
							deck.Name = nameLine;
						}
					}

					// comment
					if(correctLine.Contains("<comments>")) {
						string nameLine = correctLine.Substring(correctLine.IndexOf("<comments>") + 10)
							.Trim();

						nameLine = nameLine.Substring(0, nameLine.IndexOf("</comments>"));

						deck.Comment = nameLine;
					} 

					// sideboard
					if(correctLine.Contains("<zone name=\"side\">")) {
						isSideboard = true;
					}

					// add card
					if(correctLine.Contains("<card ")) {
						string amount = "";
						string name = "";


						string content = correctLine
							.Replace("<card number=\"", "")
							.Replace("\" name=\"", "|")
							.Replace("\"/>", "");

						List<string> numberAndName = content.Split("|");

						if(numberAndName.Count == 2) {
							amount = numberAndName[0].Trim();
							name = numberAndName[1].Trim();
						}
						else if(numberAndName.Count == 1) {
							amount = "1";
							name = correctLine.Trim();
						}
						else {
							amount = "1";
							name = "Plains";
						}

						DeckCard dc = new DeckCard();
						dc.Amount = amount.ToInt32();

						// the card object will be laoded by setting the name (part of the property)
						dc.Name = name;

						dc.Sideboard = isSideboard;

						deck.CardList.Add(dc);
					}
				}

				return deck;
			}

			return null;
		}
	}
}
