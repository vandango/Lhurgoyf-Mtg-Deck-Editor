using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Lhurgoyf.Event;
using Toenda.Lhurgoyf.Data;

namespace Toenda.Lhurgoyf.Utility {
	public class ArchetypeIdentify {
		public static List<string> Identify(Deck deck) {
			List<Deck> templateList = DeckArchetypeTemplates.Instance.Data;

			List<ArchetypeDeckCard> checkList = new List<ArchetypeDeckCard>();
			List<string> archetypes = new List<string>();

			foreach(Deck tmpl in templateList) {
				checkList.Clear();

				List<string> additionalColors = new List<string>();

				foreach(ArchetypeDeckCard card in tmpl.CardList) {
					short res = CheckCardIsInDeck(deck, card);

					if(res > 0) {
						if(card.AdditionalDeckColor != null
						&& res == 1) {
							string color = card.AdditionalDeckColor.Symbol.ToString().ToLower();

							if(!additionalColors.Contains(color)) {
								additionalColors.Add(color);
							}
						}

						checkList.Add(card);
					}
					//if(res == 1) {
					//    checkList.Add(card);
					//}
					//else if(res == 2
					//&& checkList.Count > 0) {
					//    checkList.Add(card);
					//}
				}

				if(checkList.Count == tmpl.CardList.Count) {
					string deckName = tmpl.Name;

					if(deckName.Contains("{x}")
					&& additionalColors.Count > 0) {
						string addColors = "";

						foreach(string color in additionalColors) {
							addColors += color.ToLower();
						}

						deckName = deckName.Replace("{x}", addColors);
					}
					else {
						deckName = deckName.Replace("{x}", "");
					}

					archetypes.Add(deckName);
				}
			}

			return archetypes;
		}

		private static short CheckCardIsInDeck(Deck deck, ArchetypeDeckCard templateCard) {
			if(deck != null) {
				foreach(DeckCard card in deck.CardList) {
					//if(templateCard.IsOptional) {
					//    //return 2;

					//    if(card.Name.Equals(templateCard.Name)
					//    && card.Amount >= templateCard.Amount
					//    && card.Sideboard == templateCard.Sideboard) {
					//        return 1;
					//    }
					//    else {
					//    }
					//}
					//else {

					if(card.Name.Equals(templateCard.Name)
					&& card.Amount >= templateCard.Amount
					&& card.Sideboard == templateCard.Sideboard) {
						return 1;
					}

					//}
				}

				if(templateCard.IsOptional) {
					return 2;
				}
				else {
					return 0;
				}
			}

			return 0;
		}
	}
}
