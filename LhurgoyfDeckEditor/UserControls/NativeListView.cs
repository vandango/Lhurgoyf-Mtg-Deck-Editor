using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Toenda.LhurgoyfDeckEditor.UserControls {
	public class NativeListView : System.Windows.Forms.ListView {
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
											string pszSubIdList);

		protected override void CreateHandle() {
			base.CreateHandle();

			SetWindowTheme(this.Handle, "explorer", null);
		}

		protected override void DestroyHandle() {
			base.DestroyHandle();
		}
	}
}
