using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf {
	/// <summary>
	/// Creates an object of type mana item
	/// </summary>
	public class ManaItem {
		/// <summary>
		/// Gets or sets the amount of mana
		/// </summary>
		public int Amount { get; set; }

		/// <summary>
		/// Gets or sets the type of mana
		/// </summary>
		public ManaSymbol Type { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="ManaItem"/>
		/// </summary>
		public ManaItem() {
			this.Name = "C";
		}

		/// <summary>
		/// Creates a new instance of <see cref="ManaItem"/>
		/// </summary>
		/// <param name="value">Describes different types of mana to initialize the item.</param>
		public ManaItem(string value) {
			this.Name = value;
		}

		/// <summary>
		/// Returns a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return this.Name;
		}

		/// <summary>
		/// Gets or sets the item and initializes it via a describing string
		/// </summary>
		private string Name {
			get {
				switch(this.Type) {
					case ManaSymbol.W:
						return this.Amount.ToString() + " White";

					case ManaSymbol.U:
						return this.Amount.ToString() + " Blue";

					case ManaSymbol.B:
						return this.Amount.ToString() + " Black";

					case ManaSymbol.G:
						return this.Amount.ToString() + " Green";

					case ManaSymbol.R:
						return this.Amount.ToString() + " Red";

					case ManaSymbol.C:
						return this.Amount.ToString() + " Colorless";

					case ManaSymbol.A:
						return this.Amount.ToString() + " Any mana";
				}

				return "";
			}
			set {
				string val = value.ToLower();
				string symbol = "";
				string amount = "";
				bool valuesAreSet = false;

				if(!val.Contains(" ")
				&& val.IsAlpha()) {
					if(val.Length == 1) {
						this.Amount = 1;

						if(val == "x") {
							val = "c";
						}

						this.Type = val.ToUpper().ToEnum<ManaSymbol>();
						valuesAreSet = true;
					}
					else {
						amount = "1";
						symbol = val;
					}
				}
				else {
					if(val.Contains(" ")) {
						List<string> tmp = val.Split(" ");

						amount = tmp[0];
						symbol = tmp[1];
					}
					else {
						foreach(char c in val) {
							if(c.ToString().IsNumeric()) {
								amount += c.ToString();
							}
							else {
								symbol += c.ToString();
							}
						}
					}
				}

				if(!valuesAreSet) {
					if(!amount.IsNullOrTrimmedEmpty()) {
						this.Amount = amount.ToInt32();
					}

					switch(symbol) {
						case "white":
							this.Type = ManaSymbol.W;
							break;

						case "blue":
							this.Type = ManaSymbol.U;
							break;

						case "black":
							this.Type = ManaSymbol.B;
							break;

						case "green":
							this.Type = ManaSymbol.G;
							break;

						case "red":
							this.Type = ManaSymbol.R;
							break;

						case "colorless":
							this.Type = ManaSymbol.C;
							break;

						case "any":
						case "all":
						default:
							this.Type = ManaSymbol.A;
							break;
					}
				}
			}
		}
	}
}
