using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Worker {
	public class ImageLoaderFinishEventArgs : EventArgs {
		public string Text { get; set; }

		public ImageLoaderFinishEventArgs(string text) {
			this.Text = text;
		}
	}
}
