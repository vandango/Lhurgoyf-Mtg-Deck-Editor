using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Toenda.Foundation;
using Toenda.Lhurgoyf;
using Toenda.LhurgoyfDeckEditor.UserControls;
using Toenda.Lhurgoyf.Utility;
using Toenda.Foundation.Drawing;
using Toenda.Lhurgoyf.Worker;

namespace Toenda.LhurgoyfDeckEditor {
	public partial class ViewCard : Form {
		private Bitmap _transformedImage;
		private Bitmap _mainImage;
		private bool _normalVisible = true;

		public ViewCard(Card card) {
			InitializeComponent();

			EditionImage firstImage = card.FirstEditionImageFromList;

			string transformedFilename = "";
			Card transformed = null;

			if(card.IsTransformable) {
				this.btnTransform.Visible = true;

				transformed = card.TransformedCard;

				transformedFilename = Helper.CreateImageFilename(
					transformed.FirstEditionFromList.Shortname,
					transformed.Name
				);
			}

			string filename = Helper.CreateImageFilename(
				card.FirstEditionFromList.Shortname,
				card.Name
			);

			if(!File.Exists(filename)) {
				if(!Directory.Exists("img\\" + firstImage.Edition.Shortname + "\\")) {
					Directory.CreateDirectory("img\\" + firstImage.Edition.Shortname + "\\");
				}

				ImageLoader.DownloadImage(firstImage.Url.AbsoluteUri, filename, card.MainType);
			}

			if(!transformedFilename.IsNullOrTrimmedEmpty()
			&& transformed != null) {
				if(!File.Exists(transformedFilename)) {
					if(!Directory.Exists("img\\" + transformed.FirstEditionImageFromList.Edition.Shortname + "\\")) {
						Directory.CreateDirectory("img\\" + transformed.FirstEditionImageFromList.Edition.Shortname + "\\");
					}

					ImageLoader.DownloadImage(
						transformed.FirstEditionImageFromList.Url.AbsoluteUri,
						transformedFilename,
						card.MainType
					);
				}

				this._transformedImage = new Bitmap(transformedFilename);
			}

			/*
			 * LOAD IMAGE & DATA
			 */
			this._mainImage = new Bitmap(filename);

			this.cardImage.Image = this._mainImage;

			this.gbCard.Text = card.Name + " (" + card.MainType + ")";
			this.lblName.Text = card.Name;
			this.lblCastingCost.Text = card.CastingCost 
				+ (card.CastingCost.IsNullOrTrimmedEmpty() ? "" : " (" + card.ConvertedCastingCost + ")");
			this.lblType.Text = card.Type 
				+ (card.SpecialType != "[Unknown special type]" ? " (" + card.SpecialType + ")" : "");
			this.lblColor.Text = card.GetColorString();
			this.lblRarity.Text = card.Rarity.ToString();
			this.lblArtist.Text = card.Artist;
			this.lblPowerToughness.Text = card.PowerThoughness;
			this.txtText.Text = card.CreatableMana.Count.ToString() + card.Text;
			this.txtFlavourText.Text = card.Flavour;

			foreach(KeyValuePair<Edition, EditionImage> pair in card.EditionPictures) {
				this.lbCollections.Items.Add(pair.Key.ToString());
			}

			this.txtText.BringToFront();
			this.txtFlavourText.BringToFront();
		}

		private void ViewCard_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void clickPanel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void cardImage_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnTransform_Click(object sender, EventArgs e) {
			if(this._normalVisible) {
				this.cardImage.Image = this._transformedImage;
				this._normalVisible = false;
			}
			else {
				this.cardImage.Image = this._mainImage;
				this._normalVisible = true;
			}
		}
	}
}
