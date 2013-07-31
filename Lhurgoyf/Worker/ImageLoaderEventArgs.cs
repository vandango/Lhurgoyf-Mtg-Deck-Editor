using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Worker {
	public class ImageLoaderEventArgs : EventArgs {
		public Card Card { get; set; }
		public Edition Edition { get; set; }
		public int Index { get; set; }
		public TimeSpan TotalTime { get; set; }
		public TimeSpan LoadingTime { get; set; }

		public ImageLoaderEventArgs(Card card, Edition edition, int index, TimeSpan totalTime, TimeSpan loadingTime) {
			this.Card = card;
			this.Edition = edition;
			this.Index = index;
			this.TotalTime = totalTime;
			this.LoadingTime = loadingTime;
		}
	}
}
