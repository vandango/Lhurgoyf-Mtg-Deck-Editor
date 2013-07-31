using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Utility {
	public class MagicWorkstationFileHandler : FileHandler {
		public override string Generate(Deck deck) {
			List<DeckCard> creatureList = new List<DeckCard>();
			List<DeckCard> spellList = new List<DeckCard>();
			List<DeckCard> landList = new List<DeckCard>();
			List<DeckCard> sideboardList = new List<DeckCard>();

			foreach(DeckCard card in deck.CardList) {
				if(card.Sideboard) {
					sideboardList.Add(card);
				}
				else {
					switch(card.Card.MainType) {
						case "Creature":
							creatureList.Add(card);
							break;

						case "Land":
							landList.Add(card);
							break;

						default:
							spellList.Add(card);
							break;
					}
				}
			}

			// generate header
			StringBuilder header = new StringBuilder();

			header.AppendLine("// Deck file for Magic Workstation (http://www.magicworkstation.com)");

			if(!deck.Name.IsNullOrTrimmedEmpty()
			|| !deck.Author.IsNullOrTrimmedEmpty()) {
				header.Append("// NAME: ");
			}

			if(!deck.Name.IsNullOrTrimmedEmpty()) {
				header.Append(deck.Name);
			}
			else {
				header.Append(Path.GetFileNameWithoutExtension(deck.Filename));
			}

			if(!deck.ArcheType.IsNullOrTrimmedEmpty()) {
				header.Append(" (");
				header.Append(deck.ArcheType);
				header.Append(") ");
			}

			if(!deck.Author.IsNullOrTrimmedEmpty()) {
				header.Append(" by ");
				header.Append(deck.Author);
			}

			if(!deck.Name.IsNullOrTrimmedEmpty()
			|| !deck.ArcheType.IsNullOrTrimmedEmpty()
			|| !deck.Author.IsNullOrTrimmedEmpty()) {
				header.AppendLine();
			}

			header.AppendLine("// Exported by Lhurgoyf Deck Library");

			if(!deck.Comment.IsNullOrTrimmedEmpty()) {
				header.Append("// COMMENT: ");
				header.AppendLine(deck.Comment);
			}

			// generate sideboard
			StringBuilder maindeck = new StringBuilder();

			if(creatureList.Count > 0) {
				maindeck.AppendLine("// Creatures");

				foreach(DeckCard card in creatureList.OrderBy(item => item.Card.ConvertedCastingCost)) {
					maindeck.Append("    ");
					maindeck.Append(card.Amount.ToString());
					maindeck.Append(" [");
					maindeck.Append(card.Card.FirstEditionFromList.Shortname);
					maindeck.Append("] ");
					maindeck.Append(card.Card.Name);
					maindeck.AppendLine();
				}

				maindeck.AppendLine();
			}

			if(spellList.Count > 0) {
				maindeck.AppendLine("// Spells");

				foreach(DeckCard card in spellList.OrderBy(item => item.Card.ConvertedCastingCost)) {
					maindeck.Append("    ");
					maindeck.Append(card.Amount.ToString());
					maindeck.Append(" [");
					maindeck.Append(card.Card.FirstEditionFromList.Shortname);
					maindeck.Append("] ");
					maindeck.Append(card.Card.Name);
					maindeck.AppendLine();
				}

				maindeck.AppendLine();
			}

			if(landList.Count > 0) {
				maindeck.AppendLine("// Lands");

				foreach(DeckCard card in landList.OrderBy(item => item.Card.ConvertedCastingCost)) {
					maindeck.Append("    ");
					maindeck.Append(card.Amount.ToString());
					maindeck.Append(" [");
					maindeck.Append(card.Card.FirstEditionFromList.Shortname);
					maindeck.Append("] ");
					maindeck.Append(card.Card.Name);
					maindeck.AppendLine();
				}

				//maindeck.AppendLine();
			}

			// generate sideboard
			StringBuilder sideboard = new StringBuilder();

			if(sideboardList.Count > 0) {
				sideboard.AppendLine("// Sideboard");

				foreach(DeckCard card in sideboardList.OrderBy(item => item.Card.ConvertedCastingCost)) {
					sideboard.Append("SB: ");
					sideboard.Append(card.Amount.ToString());
					sideboard.Append(" [");
					sideboard.Append(card.Card.FirstEditionFromList.Shortname);
					sideboard.Append("] ");
					sideboard.Append(card.Card.Name);
					sideboard.AppendLine();
				}
			}

			// generate full file content
			StringBuilder full = new StringBuilder();

			full.Append(header.ToString());
			full.AppendLine();
			full.Append(maindeck.ToString());
			full.AppendLine();
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
					string correctLine = line;

					// kill spaces and tabs inside of the string
					correctLine = correctLine.Replace("\t", " ").Trim();

					// name
					if(correctLine.Contains("//")
					&& correctLine.ToLower().Contains("name")) {
						string nameLine = correctLine.Substring(correctLine.IndexOf("NAME") + 4)
							.Replace(":", "")
							.Trim();

						if(correctLine.Contains(" by ")) {
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
					if(correctLine.Contains("//")
					&& correctLine.ToLower().Contains("comment")) {
						string nameLine = correctLine.Substring(correctLine.IndexOf("COMMENT") + 7)
							.Replace(":", "")
							.Trim();

						deck.Comment = nameLine;
					}

					// sb?
					if(correctLine.Contains("//")
					&& correctLine.ToLower().Contains("sideboard")) {
						isSideboard = true;
					}

					// add
					if(!correctLine.IsNullOrTrimmedEmpty()
					&& !correctLine.StartsWith("//")) {
						string amount = "";
						string name = "";

						if(correctLine.StartsWith("SB: ")) {
							correctLine = correctLine.Replace("SB: ", "");
							isSideboard = true;
						}

						if(correctLine[0].ToString().IsNumeric()) {
							amount = correctLine.Substring(0, correctLine.IndexOf(" ")).Trim();
							name = correctLine.Substring(correctLine.IndexOf(" ")).Trim();
						}
						else {
							amount = "1";
							name = correctLine.Trim();
						}

						string editionShortName = "";

						if(name.Contains("[")
						&& name.Contains("]")) {
							editionShortName = name.Substring(0, name.IndexOf("] ") + 1)
								.Replace("[", "")
								.Replace("]", "")
								.Trim();

							name = name.Substring(name.IndexOf("] ") + 1).Trim();
						}

						if(name.Contains("(")
						&& name.Contains(")")) {
							name = name.Substring(0, name.IndexOf("("))
								.Trim();
						}

						DeckCard dc = new DeckCard();
						dc.Amount = amount.ToInt32();
						dc.EditionShortName = editionShortName;

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
