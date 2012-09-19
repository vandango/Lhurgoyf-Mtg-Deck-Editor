using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Foundation;
using Toenda.Lhurgoyf.Utility;

namespace Toenda.Lhurgoyf {
	public class DeckCard {
		//public string Name { get; set; }
		public Card Card { get; set; }
		public int Amount { get; set; }
		public bool Sideboard { get; set; }
		public string EditionShortName { get; set; }

		public string Name {
			get {
				if(this.Card == null) {
					return "";
				}
				else {
					return this.Card.Name;
				}
			}
			set {
				if(this.Card == null) {
					List<Card> foundCards;

					if(this.EditionShortName.IsNullOrTrimmedEmpty()) {
						foundCards = CardFinder.FindByName(value);
					}
					else {
						foundCards = CardFinder.FindByNameAndEdition(value, this.EditionShortName);
					}

					if(foundCards != null
					&& foundCards.Count > 0) {
						this.Card = foundCards[0];
					}
					else {
						this.Card = new Card();
						this.Card.Name = value;
					}
				}
				else {
					List<Card> foundCards;

					if(this.EditionShortName.IsNullOrTrimmedEmpty()) {
						foundCards = CardFinder.FindByName(value);
					}
					else {
						foundCards = CardFinder.FindByNameAndEdition(value, this.EditionShortName);
					}

					if(foundCards != null
					&& foundCards.Count > 0) {
						this.Card = foundCards[0];
					}
					else {
						this.Card.Name = value;
					}
				}
			}
		}

		public override string ToString() {
			StringBuilder str = new StringBuilder();

			if(this.Sideboard) {
				str.Append("Sideboard: ");
			}

			str.Append(this.Amount.ToString());
			str.Append(" ");
			str.Append(this.Card.ToString());

			return str.ToString();
		}
	}
}
