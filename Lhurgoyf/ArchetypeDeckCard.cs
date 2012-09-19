using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf {
	public class ArchetypeDeckCard : DeckCard {
		public bool IsOptional { get; set; }
		public CardColor AdditionalDeckColor { get; set; }

		public override string ToString() {
			StringBuilder str = new StringBuilder();

			if(this.Sideboard) {
				str.Append("Sideboard: ");
			}

			if(this.IsOptional) {
				str.Append("[Optional] ");
			}

			str.Append(this.Amount.ToString());
			str.Append(" ");
			str.Append(this.Card.ToString());

			if(this.AdditionalDeckColor != null) {
				str.Append(" (Color: ");
				str.Append(this.AdditionalDeckColor.Name);
				str.Append(")");
			}

			return str.ToString();
		}
	}
}
