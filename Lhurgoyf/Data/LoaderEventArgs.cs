using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Data {
	public class LoaderEventArgs : EventArgs {
		public Card Card { get; set; }
		public Edition Edition { get; set; }
		public int Index { get; set; }
		public int MaxIndex { get; set; }

		public LoaderEventArgs(Card card, Edition edition, int index, int maxIndex) {
			this.Card = card;
			this.Edition = edition;
			this.Index = index;
			this.MaxIndex = maxIndex;
		}
	}
}
