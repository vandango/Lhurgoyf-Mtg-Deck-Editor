using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Toenda.Lhurgoyf {
	public class EditionImage {
		//public Stream BitmapStream { get; set; }

		/// <summary>
		/// Gets or sets the url of the image
		/// </summary>
		public Uri Url { get; set; }

		/// <summary>
		/// Gets or sets the card which is linked to this image
		/// </summary>
		public Card Card { get; set; }

		/// <summary>
		/// Gets or sets the edition which is linked to this image
		/// </summary>
		public Edition Edition { get; set; }

		/// <summary>
		/// Gets or sets the artist of the card image
		/// </summary>
		public string Artist { get; set; }
	}
}
