using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toenda.Lhurgoyf.Data;

namespace Toenda.LhurgoyfDeckEditor {
	public partial class SearchForm : Form {
		public SearchForm() {
			InitializeComponent();

			this.InitControls();
		}

		private void InitControls() {
			// load special type
			var specialTypes =
				from item in CardBase.Cards
				orderby item.SpecialType
				group item by item.SpecialType;

			this.cbSpecialType.Items.Add("");

			foreach(var specialType in specialTypes) {
				this.cbSpecialType.Items.Add(specialType.Key);
			}
		}
	}
}
