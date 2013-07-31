using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Event {
	public class TournamentEvent {
		public string Name { get; set; }
		public List<EventDeck> DeckList { get; set; }

		public TournamentEvent() {
			this.DeckList = new List<EventDeck>();
		}
	}
}
