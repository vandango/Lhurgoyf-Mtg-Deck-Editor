using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Foundation.Data;
using System.Data;
using System.Configuration;
using Toenda.Foundation;
using Toenda.Lhurgoyf.Data.Import;

namespace Toenda.Lhurgoyf.Data {
	public static class CardBase {
		public delegate void LoaderEventHandler(LoaderEventArgs args);
		public static event LoaderEventHandler LoaderResponse;

		public delegate void LoaderFinishEventhandler(LoaderFinishEventArgs args);
		public static event LoaderFinishEventhandler LoaderFinish;

		public static bool IsInitialized { get; private set; }
		public static List<Card> Cards { get; private set; }
		public static List<Edition> Editions { get; private set; }
		public static int MaxItems { get; private set; }

		public static ImportSource ImportSource { get; set; }

		static CardBase() {
			Cards = new List<Card>();
			Editions = new List<Edition>();

			IsInitialized = false;

			if(ImportSource == null) {
				ImportSource = Import.ImportSource.Cockatrice;
			}

			switch(ImportSource) {
				case ImportSource.Cockatrice:
					MaxItems = CockatriceImport.MaxItems;
					break;

				case ImportSource.MagiccardsInfo:
					MaxItems = 0;
					break;

				default:
					break;
			}
		}

		public static void LoadFromImportSource() {
			switch(ImportSource) {
				case ImportSource.Cockatrice:
					CockatriceImport.LoaderResponse += new CockatriceImport.LoaderEventHandler(InternalLoaderResponse);
					CockatriceImport.LoaderFinish += new CockatriceImport.LoaderFinishEventhandler(InternalLoaderFinish);

					CockatriceImport.Run();
					break;

				case ImportSource.MagiccardsInfo:
					MagiccardsInfoImport.Run();
					break;
				
				default:
					break;
			}

			IsInitialized = true;
		}

		static void InternalLoaderResponse(LoaderEventArgs args) {
			if(LoaderResponse != null) {
				LoaderResponse(args);
			}
		}

		static void InternalLoaderFinish(LoaderFinishEventArgs args) {
			if(LoaderFinish != null) {
				LoaderFinish(args);
			}
		}

		public static void LoadFromDatabase() {
			// load abilities
			List<Ability> abilities = new List<Ability>();

			using(DAL dal = DAL.FromConfiguration("sqlite")) {
				IDbCommand cmd = dal.CreateCommand();
				cmd.CommandText = "SELECT * FROM Abilities";

				dal.OpenConnection();

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						Ability item = new Ability();

						long id = reader.GetSafeValue<long>("Id");
						item.Id = Convert.ToInt32(id);

						long cardId = reader.GetSafeValue<long>("CardId");
						item.CardId = Convert.ToInt32(cardId);

						item.Name = reader.GetSafeValue<string>("Name");
						item.Description = reader.GetSafeValue<string>("Description");
						item.ActivationCost = reader.GetSafeValue<string>("ActivationCost");
						item.TapToActivate = reader.GetSafeValue<bool>("TapToActivate");

						abilities.Add(item);
					}
				}
			}

			using(DAL dal = DAL.FromConfiguration("sqlite")) {
				IDbCommand cmd = dal.CreateCommand();
				cmd.CommandText = @"
					SELECT c.Id, c.Name, CastingCost, Type
						,Color, Flavour, Abilities, Rarity, Artist
						,e.Name AS EditionName, e.Id AS EditionId, Shortname
					FROM Cards AS c
					INNER JOIN Cards2Editions AS c2e ON c.Id = c2e.CardId
					INNER JOIN Editions AS e ON c2e.EditionId = e.Id
				";

				dal.OpenConnection();

				using(IDataReader reader = dal.ExecuteQueryForDataReader(cmd)) {
					while(reader.Read()) {
						Card card = new Card();

						long cardId = reader.GetSafeValue<long>("Id");
						card.Id = Convert.ToInt32(cardId);

						card.Name = reader.GetSafeValue<string>("Name");
						card.CastingCost = reader.GetSafeValue<string>("CastingCost");
						card.Type = reader.GetSafeValue<string>("Type");

						card.Color = new List<CardColor>();
						//card.Color = "";
						string colors = reader.GetSafeValue<string>("Color");
						foreach(char color in colors) {
							card.Color.Add(new CardColor() { Symbol = color.ToString().ToEnum<ColorSymbol>() });
							//card.Color += color;
						}

						card.Flavour = reader.GetSafeValue<string>("Flavour");
						card.Rarity = reader.GetSafeValue<string>("Rarity").ToEnum<Rarity>();
						card.Artist = reader.GetSafeValue<string>("Artist");
						card.PowerThoughness = reader.GetSafeValue<string>("PT");
						card.Text = reader.GetSafeValue<string>("Text");

						// edition
						Edition edition = new Edition();
						long editionId = reader.GetSafeValue<long>("EditionId");
						edition.Id = Convert.ToInt32(editionId);
						edition.Name = reader.GetSafeValue<string>("EditionName");
						edition.Shortname = reader.GetSafeValue<string>("Shortname");

						var edi =
							from item in Editions
							where item.Id == edition.Id
							&& item.Name == edition.Name
							&& item.Shortname == edition.Shortname
							select item;

						if(edi.Count() == 0
						) {
							edition.Cards.Add(card);

							Editions.Add(edition);
						}
						else {
							((Edition)edi).Cards.Add(card);
						}

						var ediPics =
							from item in card.EditionPictures
							where item.Key.Id == edition.Id
							&& item.Key.Name == edition.Name
							&& item.Key.Shortname == edition.Shortname
							select item;

						if(ediPics.Count() == 0) {
							card.EditionPictures.Add(edition, null);
						}
						else {
							card.EditionPictures[edition] = null;
						}

						//// load abilities
						//var abi = from item in abilities
						//          where item.CardId == card.Id
						//          select item;

						//foreach(Ability item in abi) {
						//    card.Abilities.Add(item);
						//}

						Cards.Add(card);
					}
				}
			}

			IsInitialized = true;
		}
	}
}
