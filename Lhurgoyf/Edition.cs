using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf {
	public class Edition {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Shortname { get; set; }
		public string Description { get; set; }
		public List<Card> Cards { get; set; }

		public Edition() {
			this.Cards = new List<Card>();
		}

		public override string ToString() {
			StringBuilder str = new StringBuilder();

			str.Append(this.Name);

			return str.ToString();
		}
	}
}
