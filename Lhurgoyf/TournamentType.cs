using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf {
	public class TournamentType {
		public TournamentFormat Type { get; set; }
		public string TypeSubFormat { get; set; }

		public string Name {
			get {
				switch(this.Type) {
					case TournamentFormat.T1:
						return "Vintage";

					case TournamentFormat.T1_5:
						return "Legacy";

					case TournamentFormat.T1_x:
						return "Extended";

					case TournamentFormat.T2:
						return "Standard";

					case TournamentFormat.Highlander:
						return "Highlander";

					case TournamentFormat.Block:
						return "Block Constructed";

					case TournamentFormat.Casual:
						return "Casual";

					case TournamentFormat.Commander:
						return "Commander";

					case TournamentFormat.Modern:
						return "Modern";
				}

				return "";
			}
			set {
				switch(value.ToLower()) {
					case "vintage":
						this.Type = TournamentFormat.T1;
						break;

					case "legacy":
						this.Type = TournamentFormat.T1_5;
						break;

					case "extended":
						this.Type = TournamentFormat.T1_x;
						break;

					case "standard":
						this.Type = TournamentFormat.T2;
						break;

					case "highlander":
						this.Type = TournamentFormat.Highlander;
						break;

					case "block constructed":
						this.Type = TournamentFormat.Block;
						break;

					case "commander":
						this.Type = TournamentFormat.Commander;
						break;

					case "modern":
						this.Type = TournamentFormat.Modern;
						break;

					case "casual":
					default:
						this.Type = TournamentFormat.Casual;
						break;
				}
			}
		}

		public override string ToString() {
			StringBuilder str = new StringBuilder();

			str.Append(this.Name);

			if(!this.TypeSubFormat.IsNullOrTrimmedEmpty()) {
				str.Append(" (");
				str.Append(this.TypeSubFormat);
				str.Append(")");
			}

			return str.ToString();
		}
	}
}
