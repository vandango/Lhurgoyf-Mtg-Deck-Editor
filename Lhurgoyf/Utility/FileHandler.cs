using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toenda.Lhurgoyf.Utility {
	public abstract class FileHandler {
		public abstract Deck Load(string filename);
		public abstract string Generate(Deck deck);

		public static FileHandler GetFileHandlerByExtension(string extension) {
			FileHandler fileLoader;

			switch(extension.ToLower()) {
				case ".cod":
					fileLoader = new CockatriceFileHandler();
					break;

				case ".txt":
					fileLoader = new MagicOnlineFileHandler();
					break;

				case ".mwdeck":
					fileLoader = new MagicWorkstationFileHandler();
					break;

				case ".dec":
				default:
					fileLoader = new ApprenticeFileHandler();
					break;
			}

			return fileLoader;
		}
	}
}
