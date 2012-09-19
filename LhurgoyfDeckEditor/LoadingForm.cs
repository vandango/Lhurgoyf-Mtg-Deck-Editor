using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toenda.Lhurgoyf.Data.Import;
using Toenda.Lhurgoyf.Data;
using Toenda.Lhurgoyf.Utility;
using Toenda.Lhurgoyf;
using Toenda.Foundation;
using System.IO;

namespace Toenda.LhurgoyfDeckEditor {
	public partial class LoadingForm : Form {
		private string _filename = null;

		public LoadingForm(string filename) {
			InitializeComponent();

			this._filename = filename;

			this.DialogResult = System.Windows.Forms.DialogResult.Ignore;

			this.pbLoad.Step = 1;
			this.pbLoad.Value = 0;
			this.pbLoad.Minimum = 0;

			if(this._filename.IsNullOrTrimmedEmpty()) {
				CardBase.ImportSource = ImportSource.Cockatrice;

				CardBase.LoaderResponse += new CardBase.LoaderEventHandler(CardBase_LoaderResponse);
				CardBase.LoaderFinish += new CardBase.LoaderFinishEventhandler(CardBase_LoaderFinish);

				this.pbLoad.Maximum = CardBase.MaxItems + 1;
			}
			else {
				this.pbLoad.Maximum = 6;
				//this.pbLoad.Style = ProgressBarStyle.Marquee;
			}
		}

		void CardBase_LoaderResponse(LoaderEventArgs args) {
			this.pbLoad.PerformStep();
			//this.PerformProgressBarStep();
		}

		void CardBase_LoaderFinish(LoaderFinishEventArgs args) {
			this.pbLoad.Value = CardBase.MaxItems;

			this.DialogResult = System.Windows.Forms.DialogResult.OK;

			MainForm mainForm = (MainForm)this.Owner;
			mainForm.BringToFront();

			//this.Hide();
		}

		private void LoadingForm_Shown(object sender, EventArgs e) {
			MainForm mainForm = (MainForm)this.Owner;

			if(this._filename.IsNullOrTrimmedEmpty()) {
				CardBase.LoadFromImportSource();

				mainForm.LoadDataToTreeView("");
				
				this.PerformProgressBarStep();
			}
			else {
				this.pbLoad.Refresh();
				this.pbLoad.Update();

				//this.pbLoad.MarqueeAnimationSpeed = 100;

				string ext = Path.GetExtension(this._filename);
				
				this.PerformProgressBarStep();

				FileHandler fileLoader = FileHandler.GetFileHandlerByExtension(ext);
				
				this.PerformProgressBarStep();

				Deck deck = fileLoader.Load(this._filename);
				
				this.PerformProgressBarStep();

				List<string> archeTypes = ArchetypeIdentify.Identify(deck);
				
				this.PerformProgressBarStep();

				mainForm.LoadDeckToDeckViewer(deck, archeTypes);
				
				this.PerformProgressBarStep();

				mainForm.LoadRecentFiles(this._filename);

				this.PerformProgressBarStep();

				this.DialogResult = System.Windows.Forms.DialogResult.OK;

				this.Hide();
			}
		}

		public void PerformProgressBarStep() {
			this.pbLoad.PerformStep();
			this.pbLoad.Refresh();
			this.pbLoad.Update();
		}
	}
}
