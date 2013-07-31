using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf {
	public class Deck {
		public TournamentType Type { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }
		public string Author { get; set; }
		public string ArcheType { get; set; }
		public List<DeckCard> CardList { get; set; }
		public string Filename { get; set; }

		public Deck() {
			this.CardList = new List<DeckCard>();
		}

		public override string ToString() {
			StringBuilder str = new StringBuilder();

			str.Append(this.Name);
			str.Append(" (");
			str.Append(this.Type.ToString());
			str.Append(")");

			return str.ToString();
		}
	}
}
