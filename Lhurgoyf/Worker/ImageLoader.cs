using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Lhurgoyf.Data;
using System.Net;
using System.IO;
using System.Drawing;
using Toenda.Lhurgoyf.Utility;
using Toenda.Foundation.Drawing;
using System.Diagnostics;

namespace Toenda.Lhurgoyf.Worker {
	public class ImageLoader {
		private static object locker = new object();

		public delegate void ImageLoaderEventHandler(ImageLoaderEventArgs args);
		public event ImageLoaderEventHandler ImageLoaderResponse;

		public delegate void ImageLoaderFinishEventhandler(ImageLoaderFinishEventArgs args);
		public event ImageLoaderFinishEventhandler ImageLoaderFinish;

		public int ItemsToLoad { get; set; }
		public int CurrentItemAmount { get; set; }

		public ImageLoader() {
			DirectoryInfo dir = new DirectoryInfo("img");

			if(!dir.Exists) {
				dir.Create();
			}

			this.ItemsToLoad = 0;

			foreach(Card card in CardBase.Cards) {
				foreach(KeyValuePair<Edition, EditionImage> pair in card.EditionPictures) {
					this.ItemsToLoad++;
				}
			}

			this.CurrentItemAmount = dir.GetFiles("*.jpg", SearchOption.AllDirectories).Count();
		}

		public void Run() {
			if(this.CurrentItemAmount != this.ItemsToLoad) {
				int index = 0;
				int currentIndex = 0;
				TimeSpan? totalTime = null;
				TimeSpan totalLoadingTime = new TimeSpan();

				foreach(Card card in CardBase.Cards) {
					foreach(KeyValuePair<Edition, EditionImage> pair in card.EditionPictures) {
						string filename = Helper.CreateImageFilename(pair.Key.Shortname, pair.Value.Card.Name);

						if(!File.Exists(filename)) {
							if(!Directory.Exists("img\\" + pair.Key.Shortname + "\\")) {
								Directory.CreateDirectory("img\\" + pair.Key.Shortname + "\\");
							}

							TimeSpan loadingTime;

							Stopwatch watch = Stopwatch.StartNew();

							DownloadImage(pair.Value.Url.AbsoluteUri, filename, card.MainType);

							watch.Stop();

							currentIndex++;

							// calc laoding time
							loadingTime = watch.Elapsed;
							totalLoadingTime += loadingTime;

							// calc total time
							int imgToLoad = this.ItemsToLoad - this.CurrentItemAmount - currentIndex;
							long ticks = (totalLoadingTime.Ticks / currentIndex);
							totalTime = new TimeSpan(ticks * imgToLoad);

							// raise event
							if(this.ImageLoaderResponse != null) {
								this.ImageLoaderResponse(new ImageLoaderEventArgs(
									pair.Value.Card,
									pair.Value.Edition,
									index, 
									totalTime.GetValueOrDefault(),
									totalLoadingTime
								));
							}
						}

						index++;
					}
				}
			}

			if(this.ImageLoaderFinish != null) {
				this.ImageLoaderFinish(new ImageLoaderFinishEventArgs("All items successfull loaded!"));
			}
		}

		public static void DownloadImage(string url, string targetFilename, string mainType) {
			WebClient wc = new WebClient();

			byte[] originalData = wc.DownloadData(url);
			MemoryStream stream = new MemoryStream(originalData);

			ImageGenerator imageGenerator = new ImageGenerator();

			if(mainType == "Plane") {
				// to 310; 223
				Bitmap bmp2 = imageGenerator.GenerateNewResolution(new Bitmap(stream), 310, 223);

				bmp2.Save(targetFilename);
			}
			else if(mainType == "Vanguard") {
				// to 223; 310
				Bitmap bmp2 = imageGenerator.GenerateNewResolution(new Bitmap(stream), 223, 310);

				bmp2.Save(targetFilename);
			}
			else if(mainType == "Scheme") {
				// to 223; 310
				Bitmap bmp2 = imageGenerator.GenerateNewResolution(new Bitmap(stream), 223, 310);

				bmp2.Save(targetFilename);
			}
			else {
				StreamWriter writer = new StreamWriter(targetFilename);
				writer.BaseStream.Write(originalData, 0, originalData.Length);
				writer.Flush();
				writer.Close();
				writer.Dispose();
			}
		}
	}
}
