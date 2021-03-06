﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Toenda.LhurgoyfDeckEditor.UserControls {
	public class TransparentPanel : Panel {
		public TransparentPanel() {
		}

		protected void TickHandler(object sender, EventArgs e) {
			this.InvalidateEx();
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT

				return cp;
			}
		}

		protected void InvalidateEx() {
			if(Parent == null) {
				return;
			}

			Rectangle rc = new Rectangle(this.Location, this.Size);

			Parent.Invalidate(rc, true);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//do not allow the background to be painted 
		}
	}
}
