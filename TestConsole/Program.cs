using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Toenda.Foundation;
using Toenda.Lhurgoyf;
using Toenda.Lhurgoyf.Utility;

namespace TestConsole {
	class Program {
		static void Main(string[] args) {
			Toenda.Lhurgoyf.Data.CardBase.ImportSource = Toenda.Lhurgoyf.Data.Import.ImportSource.Cockatrice;
			Toenda.Lhurgoyf.Data.CardBase.LoadFromImportSource();
			//Toenda.Lhurgoyf.Data.CardBase.LoadFromDatabase();

			//Toenda.Foundation.Data.ListQuery<Card> query = new Toenda.Foundation.Data.ListQuery<Card>(
			//    Toenda.Lhurgoyf.Data.CardBase.Cards
			//);

			//List<Card> foundEntries = query.ExecuteQuery("Type like '%goyf%' AND Color.Symbol == 'G'");

			//foreach(Card card in foundEntries) {
			//    string colors = "";

			//    foreach(CardColor color in card.Color) {
			//        colors += color.Symbol;
			//        colors += ", ";
			//    }

			//    if(!colors.IsNullOrTrimmedEmpty()) {
			//        colors = colors.Substring(0, colors.Length - 2);
			//        colors = " (" + colors + ")";
			//    }

			//    string editions = "";

			//    foreach(KeyValuePair<Edition, Uri> pair in card.EditionPictures) {
			//        editions += pair.Key.Shortname;
			//        editions += ", ";
			//    }

			//    if(!editions.IsNullOrTrimmedEmpty()) {
			//        editions = editions.Substring(0, editions.Length - 2);
			//        editions = " (" + editions + ")";
			//    }

			//    Console.WriteLine("{0} - {1} - {2}", card.Name, colors, editions);
			//}



			//bool repeat = true;

			//while(repeat) {
			//    Console.WriteLine("");
			//    Console.WriteLine("Lhurgoyf Console");
			//    for(int i = 0; i < Console.WindowWidth; i++) {
			//        Console.Write("-");
			//    }
			//    Console.WriteLine();
			//    Console.WriteLine();
			//    Console.Write("Enter searchword: ");
			//    string searchWord = Console.ReadLine();

			//    List<Card> foundEntries = CardFinder.Find(searchWord);

			//    foreach(Card card in foundEntries) {
			//        string editions = "";

			//        foreach(KeyValuePair<Edition, EditionImage> pair in card.EditionPictures) {
			//            editions += pair.Key.Shortname;
			//            editions += ", ";
			//        }

			//        if(!editions.IsNullOrTrimmedEmpty()) {
			//            editions = editions.Substring(0, editions.Length - 2);
			//            editions = " (" + editions + ")";
			//        }

			//        Console.WriteLine("{0} {1}", card.Name, editions);
			//    }

			//    Console.WriteLine();
			//    for(int i = 0; i < Console.WindowWidth; i++) {
			//        Console.Write("-");
			//    }
			//    Console.WriteLine();
			//    Console.Write("Try a new search? (y/n) ");
			//    if(Console.ReadLine() == "y") {
			//        repeat = true;
			//        Console.Clear();
			//    }
			//    else {
			//        repeat = false;
			//    }
			//}





			string filename = @"C:\Users\vandango\Dropbox\MTG\Type 1.5 - Legacy\GW Maverick.dec";
			string filename2 = @"C:\Users\vandango\Dropbox\MTG\Type 1.5 - Legacy\GWr Punishing Maverick.dec";

			filename = @"C:\Users\vandango\Dropbox\MTG\Type 1.5 - Legacy\GW Maverick.dec";

			FileHandler fileLoader = new ApprenticeFileHandler();

			Deck deck = fileLoader.Load(filename);

			List<string> archeTypes = ArchetypeIdentify.Identify(deck);

			if(archeTypes != null
			&& archeTypes.Count == 1) {
				deck.ArcheType = archeTypes[0];
			}
			else if(archeTypes != null
			&& archeTypes.Count > 1) {
				bool isFirst = true;

				foreach(string type in archeTypes) {
					if(!isFirst) {
						deck.ArcheType += ", ";
					}

					deck.ArcheType += type;
				}
			}

			Console.WriteLine("Deck");
			Console.WriteLine("  Name  : {0}", deck.Name);
			Console.WriteLine("  Author: {0}", deck.Author);
			Console.WriteLine("  Type  : {0}", deck.ArcheType);
			Console.WriteLine("Cards");
			Console.WriteLine("  Mainboard");

			foreach(DeckCard card in deck.CardList.Where(item => item.Sideboard == false)) {
				Console.WriteLine(
					"    {0} {1}",
					card.Amount,
					card.Name
				);
			}

			Console.WriteLine("  Sideboard");

			foreach(DeckCard card in deck.CardList.Where(item => item.Sideboard == true)) {
				Console.WriteLine(
					"    {0} {1}",
					card.Amount,
					card.Name
				);
			}




			for(int i = 0; i < Console.WindowWidth; i++) {
				Console.Write("-");
			}
			Console.WriteLine();
			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();
		}
	}
}
