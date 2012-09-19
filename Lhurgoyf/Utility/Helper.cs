using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Utility {
	public static class Helper {
		public static string CreateImageFilename(string editionShortName, string cardName) {
			return "img\\" + editionShortName + "\\"
				+ cardName
					.Replace("//", "+")
					.Replace(":", "-")
					.Replace("\"", "'")
					.Replace("?", "")
					.Replace("/", "-")
				+ ".jpg";
		}
	}
}
