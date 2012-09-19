using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Data {
	public class LoaderFinishEventArgs : EventArgs {
		public string Text { get; set; }

		public LoaderFinishEventArgs(string text) {
			this.Text = text;
		}
	}
}
