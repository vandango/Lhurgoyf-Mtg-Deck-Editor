using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Utility {
	public class ApprenticeFileHandler : FileHandler {
		public override string Generate(Deck deck) {
			List<DeckCard> creatureList = new List<DeckCard>();
			List<DeckCard> spellList = new List<DeckCard>();
			List<DeckCard> planeswalkerList = new List<DeckCard>();
			List<DeckCard> artifactList = new List<DeckCard>();
			List<DeckCard> landList = new List<DeckCard>();
			List<DeckCard> sideboardList = new List<DeckCard>();

			// count cards
			int cardAmount = 0;

			foreach(DeckCard card in deck.CardList) {
				cardAmount += card.Amount;
			}

			// sort cards for highlander decklist
			if(cardAmount >= 9999) {
				// order special type
				var result =
					from item in deck.CardList
					orderby item.Card.SpecialType, item.Card.ConvertedCastingCost
					group item by item.Card.SpecialType;

				List<DeckCard> ungroupedList = new List<DeckCard>();
				Dictionary<string, List<DeckCard>> landDict = new Dictionary<string, List<DeckCard>>();

				foreach(var item in result) {
					if(item.Key != "Zentral Type"
					&& !item.Key.Contains("Land")) {
						Console.WriteLine(item.Key);

						foreach(var sub in item) {
							Console.WriteLine("..." + ((DeckCard)sub).Name);
						}

						Console.WriteLine();
					}
					else if(item.Key.Contains("Land")) {
						landDict.Add(item.Key, item.ToList());
					}
					else {
						ungroupedList.AddRange(item);
					}
				}

				Console.WriteLine("---------------");

				// order main type
				result =
					from item in ungroupedList
					orderby item.Card.MainType
					group item by item.Card.MainType;

				foreach(var item in result) {
					if(item.Key != "Land") {
						Console.WriteLine(item.Key);

						foreach(var sub in item) {
							Console.WriteLine("..." + ((DeckCard)sub).Name);
						}

						Console.WriteLine();
					}
					else {
						landDict.Add(item.Key, item.ToList());
					}
				}

				Console.WriteLine("---------------");

				// order land type
				foreach(KeyValuePair<string, List<DeckCard>> item in landDict) {
					Console.WriteLine(item.Key);
					
					foreach(DeckCard card in item.Value) {
						Console.WriteLine("..." + card.Name);
					}

					Console.WriteLine();
				}

				Console.WriteLine();

				//if(landList.Count > 0) {
				//    int amount = 0;

				//    foreach(DeckCard card in landList) {
				//        amount += card.Amount;
				//    }

				//    maindeck.Append("// Lands (");
				//    maindeck.Append(amount);
				//    maindeck.Append(" cards)");
				//    maindeck.AppendLine();

				//    var orderedList =
				//        from item in landList
				//        orderby item.Card.Type, item.Card.SpecialType
				//        select item;

				//    foreach(DeckCard card in orderedList) {
				//        maindeck.Append(card.Amount.ToString());
				//        maindeck.Append(" ");
				//        maindeck.Append(card.Card.Name);
				//        maindeck.AppendLine();
				//    }
				//}

				// generate full file content
				StringBuilder full = new StringBuilder();

				//full.Append(header.ToString());
				//full.AppendLine();
				//full.Append(maindeck.ToString());
				//full.AppendLine();
				//full.Append(sideboard.ToString());

				return full.ToString();
			}
			// sort cards for normal deck list
			else {
				foreach(DeckCard card in deck.CardList) {
					if(card.Sideboard) {
						sideboardList.Add(card);
					}
					else {
						switch(card.Card.MainType) {
							case "Creature":
								creatureList.Add(card);
								break;

							case "Planeswalker":
								planeswalkerList.Add(card);
								break;

							case "Artifact":
								artifactList.Add(card);
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

				if(header.Length > 0) {
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
					int amount = 0;

					foreach(DeckCard card in creatureList) {
						amount += card.Amount;
					}

					maindeck.Append("// Creatures (");
					maindeck.Append(amount);
					maindeck.Append(" cards)");
					maindeck.AppendLine();

					var orderedList =
						from item in creatureList
						orderby item.Card.SpecialType, item.Card.ConvertedCastingCost
						select item;

					foreach(DeckCard card in orderedList) {
						maindeck.Append(card.Amount.ToString());
						maindeck.Append(" ");
						maindeck.Append(card.Card.Name);
						maindeck.AppendLine();
					}

					maindeck.AppendLine();
				}

				if(spellList.Count > 0) {
					int amount = 0;

					foreach(DeckCard card in spellList) {
						amount += card.Amount;
					}

					maindeck.Append("// Spells (");
					maindeck.Append(amount);
					maindeck.Append(" cards)");
					maindeck.AppendLine();

					var orderedList =
						from item in spellList
						orderby item.Card.ConvertedCastingCost, item.Card.SpecialType
						select item;

					foreach(DeckCard card in orderedList) {
						maindeck.Append(card.Amount.ToString());
						maindeck.Append(" ");
						maindeck.Append(card.Card.Name);
						maindeck.AppendLine();
					}

					maindeck.AppendLine();
				}

				if(planeswalkerList.Count > 0) {
					int amount = 0;

					foreach(DeckCard card in planeswalkerList) {
						amount += card.Amount;
					}

					maindeck.Append("// Planeswalker (");
					maindeck.Append(amount);
					maindeck.Append(" cards)");
					maindeck.AppendLine();

					var orderedList =
						from item in planeswalkerList
						orderby item.Card.ConvertedCastingCost, item.Card.SpecialType
						select item;

					foreach(DeckCard card in orderedList) {
						maindeck.Append(card.Amount.ToString());
						maindeck.Append(" ");
						maindeck.Append(card.Card.Name);
						maindeck.AppendLine();
					}

					maindeck.AppendLine();
				}

				if(artifactList.Count > 0) {
					int amount = 0;

					foreach(DeckCard card in artifactList) {
						amount += card.Amount;
					}

					maindeck.Append("// Artifacts (");
					maindeck.Append(amount);
					maindeck.Append(" cards)");
					maindeck.AppendLine();

					var orderedList =
						from item in artifactList
						orderby item.Card.ConvertedCastingCost, item.Card.SpecialType
						select item;

					foreach(DeckCard card in orderedList) {
						maindeck.Append(card.Amount.ToString());
						maindeck.Append(" ");
						maindeck.Append(card.Card.Name);
						maindeck.AppendLine();
					}

					maindeck.AppendLine();
				}

				if(landList.Count > 0) {
					int amount = 0;

					foreach(DeckCard card in landList) {
						amount += card.Amount;
					}

					maindeck.Append("// Lands (");
					maindeck.Append(amount);
					maindeck.Append(" cards)");
					maindeck.AppendLine();

					var orderedList =
						from item in landList
						orderby item.Card.Type, item.Card.SpecialType
						select item;

					foreach(DeckCard card in orderedList) {
						maindeck.Append(card.Amount.ToString());
						maindeck.Append(" ");
						maindeck.Append(card.Card.Name);
						maindeck.AppendLine();
					}

					//maindeck.AppendLine();
				}

				// generate sideboard
				StringBuilder sideboard = new StringBuilder();

				if(sideboardList.Count > 0) {
					int amount = 0;

					foreach(DeckCard card in sideboardList) {
						amount += card.Amount;
					}

					sideboard.Append("// Sideboard (");
					sideboard.Append(amount);
					sideboard.Append(" cards)");
					sideboard.AppendLine();

					foreach(DeckCard card in sideboardList.OrderBy(item => item.Card.ConvertedCastingCost)) {
						sideboard.Append("SB: ");
						sideboard.Append(card.Amount.ToString());
						sideboard.Append(" ");
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
		}

		public override Deck Load(string filename) {
			if(File.Exists(filename)) {
				StreamReader reader = new StreamReader(filename);

				List<string> list = new List<string>();
				StringBuilder str = new StringBuilder();

				while(!reader.EndOfStream) {
					string line = reader.ReadLine();

					str.Append(line);
					list.Add(line);
				}

				reader.Close();
				reader.Dispose();

				Deck deck = new Deck();
				deck.Filename = filename;
				deck.Name = Path.GetFileNameWithoutExtension(filename);

				bool isOldFormat = !str.ToString().Contains("SB:");
				bool isSideboard = false;

				foreach(string line in list) {
					string correctLine = line;

					// kill spaces and tabs inside of the string
					correctLine = correctLine.Replace("\t", " ").Trim();

					// name
					if(correctLine.Contains("//")
					&& correctLine.Contains("NAME:")) {
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
					if(isOldFormat
					&& correctLine.Contains("//")
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
