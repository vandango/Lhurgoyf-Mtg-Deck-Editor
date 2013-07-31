using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf {
	public class Ability {
		public int Id { get; set; }
		public int CardId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ActivationCost { get; set; }
		public bool TapToActivate { get; set; }
	}
}
