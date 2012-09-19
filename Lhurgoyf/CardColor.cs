using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf {
	public class CardColor {
		public ColorSymbol Symbol { get; set; }

		public string Name {
			get {
				switch(this.Symbol) {
					case ColorSymbol.W:
						return "White";

					case ColorSymbol.U:
						return "Blue";

					case ColorSymbol.B:
						return "Black";

					case ColorSymbol.G:
						return "Green";

					case ColorSymbol.R:
						return "Red";

					case ColorSymbol.C:
						return "Colorless";

					case ColorSymbol.L:
						return "Land";
				}

				return "";
			}
			set {
				switch(value.ToLower()) {
					case "white":
						this.Symbol = ColorSymbol.W;
						break;

					case "blue":
						this.Symbol = ColorSymbol.U;
						break;

					case "black":
						this.Symbol = ColorSymbol.B;
						break;

					case "green":
						this.Symbol = ColorSymbol.G;
						break;

					case "red":
						this.Symbol = ColorSymbol.R;
						break;

					case "colorless":
						this.Symbol = ColorSymbol.C;
						break;

					case "land":
						this.Symbol = ColorSymbol.L;
						break;
				}
			}
		}
	}
}
