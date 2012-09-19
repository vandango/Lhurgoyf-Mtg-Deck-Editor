using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using Toenda.Foundation;
using Toenda.Foundation.Drawing;
using Toenda.Lhurgoyf;
using Toenda.Lhurgoyf.Data;
using Toenda.Lhurgoyf.Data.Import;
using Toenda.Lhurgoyf.Worker;
using Toenda.Lhurgoyf.Utility;
using Toenda.Foundation.Utility;

namespace Toenda.LhurgoyfDeckEditor {
	public partial class MainForm : Form {
		private Thread _imageLoaderThread;
		private ImageLoader _imageLoader;
		private ImageGenerator _imageGenerator;

		public MainForm() {
			InitializeComponent();

			this.deckLayout.MouseWheel += new MouseEventHandler(deckLayout_MouseWheel);
			this.cmsDeck.Opening += new CancelEventHandler(cmsDeck_Opening);

			this._imageGenerator = new ImageGenerator();

			if(!Directory.Exists("data")) {
				Directory.CreateDirectory("data");
			}

			/*
			 * Open File Dialog
			 */
			StringBuilder str = new StringBuilder();
			str.Append("All supported dec files|*.dec;*.mwDeck;*.cod;*.txt");
			str.Append("|Apprentice File (.dec)|*.dec");
			str.Append("|Magic Workstation File (.mwDeck)|*.mwDeck");
			str.Append("|Cockatrice File (.cod)|*.cod");
			str.Append("|Magic Online File (.txt)|*.txt");

			this.openFileDialog.FileName = "";
			this.openFileDialog.Multiselect = false;
			this.openFileDialog.Filter = str.ToString();

			/*
			 * Save File Dialog
			 */
			str = new StringBuilder();
			str.Append("Apprentice File (.dec)|*.dec");
			str.Append("|MWS File (.mwDeck)|*.mwDeck");
			str.Append("|Cockatrice File (.cod)|*.cod");
			str.Append("|Magic Online File (.txt)|*.txt");

			this.saveFileDialog.Filter = str.ToString();

			this.saveFileDialog.DefaultExt = ".dec";
			this.saveFileDialog.AddExtension = true;
			this.saveFileDialog.AutoUpgradeEnabled = true;
			this.saveFileDialog.CheckPathExists = true;
			this.saveFileDialog.CreatePrompt = true;
			this.saveFileDialog.OverwritePrompt = true;

			/*
			 * Deck Layout
			 */
			this.deckLayout.ShowGroups = true;
			//this.deckLayout.View = View.LargeIcon;
			this.deckLayout.View = View.Tile;
			//this.deckLayout.View = View.List;
			//this.deckLayout.TileSize = new Size(168, 67);

			/*
			 * Menu Items
			 */
			this.closeToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Enabled = false;
			this.saveAsToolStripMenuItem.Enabled = false;

			this.LoadRecentFiles("");
		}

		#region Mousewheel on Deck

		private bool __controlPressed = false;

		private void deckLayout_KeyDown(object sender, KeyEventArgs e) {
			this.__controlPressed = e.Control;
		}

		private void deckLayout_KeyUp(object sender, KeyEventArgs e) {
			this.__controlPressed = false;
		}

		private void deckLayout_MouseLeave(object sender, EventArgs e) {
			this.__controlPressed = false;
		}

		private void deckLayout_MouseWheel(object sender, MouseEventArgs e) {
			if(this.__controlPressed) {
				int delta = e.Delta;

				ListViewItem item = this.deckLayout.GetItemAt(e.X, e.Y);

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;
					if(delta == 120) {
						this.IncreaseAmount(card);
					}
					else if(delta == -120) {
						this.DecreaseAmount(card, false);
					}
				}
			}
		}

		#endregion

		#region DragDrop from Deck to TreeView

		private void tvCards_DragEnter(object sender, DragEventArgs e) {
			if(e.Data.GetDataPresent(typeof(List<Card>))) {
				e.Effect = DragDropEffects.Copy;
			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}

		private void tvCards_DragDrop(object sender, DragEventArgs e) {
			if(sender is TreeView) {
				if(e.Data.GetDataPresent(typeof(List<Card>))) {
					List<Card> cards = (List<Card>)e.Data.GetData(typeof(List<Card>));

					foreach(Card card in cards) {
						//this.DecreaseAmount(card, true);
						this.RemoveCardFromDeck(card);
					}
				}
			}
		}

		private void deckLayout_DragLeave(object sender, EventArgs e) {
			// leave???
		}

		private void deckLayout_ItemDrag(object sender, ItemDragEventArgs e) {
			if(e.Button == System.Windows.Forms.MouseButtons.Left) {
				List<Card> cards = new List<Card>();

				foreach(ListViewItem item in this.deckLayout.SelectedItems) {
					if(item.Tag != null) {
						cards.Add(((DeckCard)item.Tag).Card);
					}
				}

				//ListViewItem item = (ListViewItem)e.Item;

				//DeckCard card = (DeckCard)item.Tag;

				DoDragDrop(cards, DragDropEffects.Copy);
			}
		}

		#endregion

		#region DragDrop from TreeView to Deck

		private void deckLayout_DragEnter(object sender, DragEventArgs e) {
			if(e.Data.GetDataPresent(typeof(Card))) {
				e.Effect = DragDropEffects.Copy;
			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}

		private void deckLayout_DragDrop(object sender, DragEventArgs e) {
			if(sender is ListView) {
				if(e.Data.GetDataPresent(typeof(Card))) {
					Card card = (Card)e.Data.GetData(typeof(Card));

					//ListViewItem item = this.deckLayout.FindNearestItem(SearchDirectionHint.Left, e.Y, e.Y);

					ListViewItem item = null;
					ListViewGroup group = null;

					if(item != null) {
						group = item.Group;
					}

					bool isSideboard = false;

					if(group != null
					&& group.Name == "Sideboard") {
						isSideboard = true;
					}

					this.AddCardToDeck(card, 1, isSideboard, false);
				}
			}
		}

		private void tvCards_DragLeave(object sender, EventArgs e) {
			//
		}

		private void tvCards_ItemDrag(object sender, ItemDragEventArgs e) {
			if(e.Button == System.Windows.Forms.MouseButtons.Left) {
				TreeNode node = (TreeNode)e.Item;

				if(node.Parent != null) {
					DoDragDrop((Card)node.Tag, DragDropEffects.Copy);
				}
			}
		}

		#endregion

		#region Events

		private void cmsDeck_Opening(object sender, CancelEventArgs e) {
			ToolStripItem item = this.cmsDeck.Items["moveToSideboardToolStripMenuItem"];

			if(this.deckLayout.SelectedItems.Count > 0
			&& this.deckLayout.SelectedItems.Count == 1) {
				string groupName = ((ListView)((ContextMenuStrip)sender).SourceControl).SelectedItems[0].Group.Name;

				if(groupName.Equals("Sideboard")) {
					item.Text = "Move to Mainboard";
				}
				else {
					item.Text = "Move to Sideboard";
				}
			}
			else {
				item.Text = "Move to Sideboard";
			}
		}

		private void moveToSideboardToolStripMenuItem_Click(object sender, EventArgs e) {
			if(this.deckLayout.SelectedItems.Count > 0
			&& this.deckLayout.SelectedItems.Count == 1) {
				ListViewItem item = this.deckLayout.SelectedItems[0];

				if(item != null
				&& item.Tag != null) {
					DeckCard card = (DeckCard)item.Tag;

					// to sideboard
					if(((ToolStripMenuItem)sender).Text.Contains("Sideboard")) {
						this.RemoveCardFromDeck(card.Card);
						this.AddCardToDeck(card.Card, card.Amount, true, false);
					}
					// to mainboard
					else {
						this.RemoveCardFromDeck(card.Card);
						this.AddCardToDeck(card.Card, card.Amount, false, false);
					}

					this.deckLayout.Refresh();
					this.deckLayout.Update();
				}
			}
		}

		private void removeCardToolStripMenuItem_Click(object sender, EventArgs e) {
			if(this.deckLayout.SelectedItems.Count > 0) {
				ListViewItem item = this.deckLayout.SelectedItems[0];

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;
					this.RemoveCardFromDeck(card);
				}
			}
		}

		private void amountToolStripMenuItem_Click(object sender, EventArgs e) {
			// implement plus
			if(this.deckLayout.SelectedItems.Count > 0) {
				ListViewItem item = this.deckLayout.SelectedItems[0];

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;

					this.IncreaseAmount(card);
				}
			}
		}

		private void amountToolStripMenuItem1_Click(object sender, EventArgs e) {
			// implement minus
			if(this.deckLayout.SelectedItems.Count > 0) {
				ListViewItem item = this.deckLayout.SelectedItems[0];

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;

					this.DecreaseAmount(card, false);
				}
			}
		}

		private void viewCardToolStripMenuItem1_Click(object sender, EventArgs e) {
			if(this.deckLayout.SelectedItems.Count > 0) {
				ListViewItem item = this.deckLayout.SelectedItems[0];

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;

					this.OpenCardViewer(card, new Point());
				}
			}
		}

		private void addToDeckToolStripMenuItem_Click(object sender, EventArgs e) {
			TreeNode node = this.tvCards.SelectedNode;

			if(node != null
			&& node.Parent != null) {
				Card card = (Card)node.Tag;

				this.AddCardToDeck(card, 1, false, false);

				this.deckLayout.Refresh();
				this.deckLayout.Update();
			}
		}

		private void addToSideboardToolStripMenuItem_Click(object sender, EventArgs e) {
			TreeNode node = this.tvCards.SelectedNode;

			if(node != null
			&& node.Parent != null) {
				Card card = (Card)node.Tag;

				this.AddCardToDeck(card, 1, true, false);

				this.deckLayout.Refresh();
				this.deckLayout.Update();
			}
		}

		private void viewCardToolStripMenuItem_Click(object sender, EventArgs e) {
			TreeNode node = this.tvCards.SelectedNode;

			if(node != null
			&& node.Parent != null) {
				Card card = (Card)node.Tag;

				this.OpenCardViewer(card, new Point());
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
			this.deckLayout.Clear();

			this.closeToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Enabled = false;
			this.saveAsToolStripMenuItem.Enabled = false;

			Deck deck = (Deck)this.deckLayout.Tag;

			this.LoadRecentFiles(deck.Filename);
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			this.SaveFile(false);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
			this.SaveFile(true);
		}

		private void item_Click(object sender, EventArgs e) {
			if(CardBase.IsInitialized) {
				// check if there is already a deck
				// and save it
				if(this.deckLayout.Items.Count > 0
				&& this.deckLayout.Groups.Count == 4) {
					if(MessageBox.Show(
						"There is a deck already loaded. You want to save it first?",
						"Lhurgoyf Deck Editor",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Exclamation
					) == System.Windows.Forms.DialogResult.Yes) {
						this.SaveFile(true);
					}
				}

				// load deck
				string filename = ((ToolStripMenuItem)sender).Tag.ToString();
				string ext = Path.GetExtension(filename);

				LoadingForm loadingForm = new LoadingForm(filename);
				if(loadingForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
					//FileHandler fileLoader = FileHandler.GetFileHandlerByExtension(ext);

					//Deck deck = fileLoader.Load(filename);
					//List<string> archeTypes = ArchetypeIdentify.Identify(deck);

					//this.LoadDeckToDeckViewer(deck, archeTypes);

					//this.LoadRecentFiles(filename);
				}
			}
			else {
				MessageBox.Show(
					"Please load a card database first.",
					"Lhurgoyf Deck Editor", 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Exclamation
				);
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			if(CardBase.IsInitialized) {
				// check if there is already a deck
				// and save it
				if(this.deckLayout.Items.Count > 0
				&& this.deckLayout.Groups.Count == 4) {
					if(MessageBox.Show(
						"There is a deck already loaded. You want to save it first?",
						"Lhurgoyf Deck Editor",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Exclamation
					) == System.Windows.Forms.DialogResult.Yes) {
						this.SaveFile(true);
					}
				}
				
				// load deck
				if(this.openFileDialog.ShowDialog() == DialogResult.OK) {
					string ext = Path.GetExtension(this.openFileDialog.FileName).ToLower();

					if(ext == ".dec"
					|| ext == ".mwdeck"
					|| ext == ".cod"
					|| ext == ".txt") {
						string filename = this.openFileDialog.FileName;

						LoadingForm loadingForm = new LoadingForm(filename);
						if(loadingForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
							//FileHandler fileLoader = FileHandler.GetFileHandlerByExtension(ext);

							//Deck deck = fileLoader.Load(filename);
							//List<string> archeTypes = ArchetypeIdentify.Identify(deck);

							//this.LoadDeckToDeckViewer(deck, archeTypes);

							//this.LoadRecentFiles(filename);
						}
					}
					else {
						MessageBox.Show("Wrong format!", "Lhurgoyf");
					}
				}
			}
			else {
				MessageBox.Show(
					"Please load a card database first.",
					"Lhurgoyf Deck Editor",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation
				);
			}
		}

		private void MainForm_Shown(object sender, EventArgs e) {
			this.Refresh();
			this.Update();

			this.Initialize();

			this._imageLoaderThread = new Thread(new ThreadStart(this._imageLoader.Run));
			this._imageLoaderThread.Start();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			Configuration.Save();

			this._imageLoaderThread.Abort();

			this.deckLayout.Dispose();

			this._imageLoader = null;
			this._imageGenerator = null;

			CardBase.Editions.Clear();
			CardBase.Cards.Clear();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this._imageLoaderThread.Abort();

			Configuration.Save();

			Application.Exit();
		}

		private void deckLayout_MouseDown(object sender, MouseEventArgs e) {
			if(e.Button == System.Windows.Forms.MouseButtons.Middle) {
				ListViewItem item = this.deckLayout.GetItemAt(e.X, e.Y);

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;

					this.OpenCardViewer(card, new Point(e.X, e.Y));
				}
			}
		}

		private void deckLayout_MouseClick(object sender, MouseEventArgs e) {
			if(e.Button == System.Windows.Forms.MouseButtons.Middle) {
				ListViewItem item = this.deckLayout.GetItemAt(e.X, e.Y);

				if(item != null) {
					Card card = ((DeckCard)item.Tag).Card;

					this.OpenCardViewer(card, new Point(e.X, e.Y));
				}
			}
		}

		private void deckLayout_MouseDoubleClick(object sender, MouseEventArgs e) {
			//ListViewItem item = this.deckLayout.FindNearestItem(
			//    SearchDirectionHint.Down,
			//    e.X,
			//    e.Y
			//);

			ListViewItem item = this.deckLayout.GetItemAt(e.X, e.Y);

			if(item != null) {
				Card card = ((DeckCard)item.Tag).Card;

				this.OpenCardViewer(card, new Point(e.X, e.Y));
			}
		}

		private void tvCards_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			TreeNode checkNode = this.tvCards.GetNodeAt(e.X, e.Y);

			if(e.Node.Parent != null
			&& this.tvCards.SelectedNode != null
			&& e.Node == checkNode) {
				Card card = (Card)e.Node.Tag;

				this.OpenCardViewer(card, new Point(e.X, e.Y));
			}
			else {
				//
			}
		}

		private void tvCards_MouseDown(object sender, MouseEventArgs e) {
			TreeNode checkNode = this.tvCards.GetNodeAt(e.X, e.Y);

			if(e.Button == System.Windows.Forms.MouseButtons.Middle
			&& checkNode.Parent != null) {
				Card card = (Card)checkNode.Tag;

				this.OpenCardViewer(card, new Point(e.X, e.Y));
			}
			else {
				//
			}
		}

		private void tvCards_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
			TreeNode checkNode = this.tvCards.GetNodeAt(e.X, e.Y);

			if(e.Button == System.Windows.Forms.MouseButtons.Middle
			&& e.Node.Parent != null
			&& this.tvCards.SelectedNode != null
			&& e.Node == checkNode) {
				Card card = (Card)e.Node.Tag;

				this.OpenCardViewer(card, new Point(e.X, e.Y));
			}
			else {
				//
			}
		}

		private void imageLoader_ImageLoaderResponse(ImageLoaderEventArgs args) {
			this.SetToolStripProgressBar(args.Index);
			this.SetToolStripLabelText(args.Card.Name + " (" + args.Edition.Shortname + ") loaded.");
			this.SetToolStripLabelTimeText(args.TotalTime, args.LoadingTime);
		}

		private void imageLoader_ImageLoaderFinish(ImageLoaderFinishEventArgs args) {
			this.SetToolStripProgressBar(this._imageLoader.ItemsToLoad);
			this.SetToolStripLabelText("All images loaded!");
			this.SetToolStripVisibility(false);
		}

		private void btnSearch_Click(object sender, EventArgs e) {
			if(this.txtSearch.Text.Length > 0) {
				this.Search(this.txtSearch.Text);
			}
			else {
				this.LoadDataToTreeView("");
			}
		}

		private void btnExtSearch_Click(object sender, EventArgs e) {
			// open new window

			// search: and, or, not

			// TODO: implement extended search

			SearchForm form = new SearchForm();

			if(form.ShowDialog() == DialogResult.OK) {
			}
		}

		private void txtSearch_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == 13) {
				if(this.txtSearch.Text.Length > 0) {
					this.Search(this.txtSearch.Text);
				}
				else {
					this.LoadDataToTreeView("");
				}
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			AboutBox aboutBox = new AboutBox();

			aboutBox.Show(this);
		}

		#endregion

		#region Private

		private void OpenCardViewer(Card card, Point point) {
			if(card.MainType == "Plane") {
				ViewCardBig form = new ViewCardBig(card);

				form.StartPosition = FormStartPosition.CenterParent;

				//if(point.IsEmpty) {
				//    form.StartPosition = FormStartPosition.CenterParent;
				//}
				//else {
				//    form.StartPosition = FormStartPosition.WindowsDefaultLocation;

				//    Point p = new Point(
				//        point.X + this.Location.X,
				//        point.Y + this.Location.Y
				//    );

				//    form.Location = p;
				//}

				if(form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				}
			}
			else {
				ViewCard form = new ViewCard(card);

				form.StartPosition = FormStartPosition.CenterParent;

				//if(point == null) {
				//    form.StartPosition = FormStartPosition.CenterParent;
				//}
				//else {
				//    form.StartPosition = FormStartPosition.WindowsDefaultLocation;

				//    Point p = new Point(
				//        point.X + this.Location.X,
				//        point.Y + this.Location.Y
				//    );

				//    form.Location = p;
				//}

				if(form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				}
			}
		}

		private void DecreaseAmount(Card card, bool removeCard) {
			ListViewItem toBeRemoved = null;

			foreach(ListViewItem item in this.deckLayout.Items) {
				DeckCard cd = (DeckCard)item.Tag;

				if(cd != null
				&& cd.Card.Name.Equals(card.Name)) {
					string number = item.Text.Substring(0, item.Text.IndexOf(" ")).Trim();

					if(number.IsNumeric()) {
						int amount = number.ToInt32();

						// remove a card
						if(amount > 1) {
							amount--;

							cd.Amount = amount;
							item.Text = amount.ToString() + " " + cd.Card.Name;
						}
						// remove tha whole card
						else if(amount == 1) {
							toBeRemoved = item;
						}

						this.SetDeckCardAmountLabel();
					}
				}
			}

			if(toBeRemoved != null
			&& removeCard) {
				ListViewGroup group = toBeRemoved.Group;

				if(group.Header.Contains("(")
				&& group.Header.Contains(")")) {
					// ({0} cards)
					string name = group.Header.Substring(0, group.Header.IndexOf("(")).Trim();
					string number = group.Header.Substring(
						group.Header.IndexOf("(") + 1,
						group.Header.IndexOf("cards)") - group.Header.IndexOf("(") - 1
					).Trim();

					int newNumber = number.ToInt32() - 1;

					if(newNumber < 0) {
						newNumber = 0;
					}

					group.Header = name + " (" + newNumber.ToString() + " cards)";
				}

				this.deckLayout.Items.Remove(toBeRemoved);

				if(this.deckLayout.Items.Count == 0) {
					this.deckLayout.Groups.Clear();
				}
			}
		}

		private void IncreaseAmount(Card card) {
			foreach(ListViewItem item in this.deckLayout.Items) {
				DeckCard cd = (DeckCard)item.Tag;

				if(cd != null
				&& cd.Card.Name.Equals(card.Name)) {
					string number = item.Text.Substring(0, item.Text.IndexOf(" ")).Trim();

					if(number.IsNumeric()) {
						int amount = number.ToInt32();

						amount++;

						cd.Amount = amount;
						item.Text = amount.ToString() + " " + cd.Card.Name;
					}

					this.SetDeckCardAmountLabel();
				}
			}
		}

		private void RemoveCardFromDeck(Card card) {
			ListViewItem toBeRemoved = null;

			foreach(ListViewItem item in this.deckLayout.Items) {
				DeckCard cd = (DeckCard)item.Tag;

				if(cd != null
				&& cd.Card.Name.Equals(card.Name)) {
					toBeRemoved = item;
				}
			}

			if(toBeRemoved != null) {
				ListViewGroup group = toBeRemoved.Group;

				if(group.Header.Contains("(")
				&& group.Header.Contains(")")) {
					// ({0} cards)
					string name = group.Header.Substring(0, group.Header.IndexOf("(")).Trim();
					string number = group.Header.Substring(
						group.Header.IndexOf("(") + 1,
						group.Header.IndexOf("cards)") - group.Header.IndexOf("(") - 1
					).Trim();

					int newNumber = number.ToInt32() - 1;

					if(newNumber < 0) {
						newNumber = 0;
					}

					group.Header = name + " (" + newNumber.ToString() + " cards)";
				}

				this.deckLayout.Items.Remove(toBeRemoved);

				if(this.deckLayout.Tag != null) {
					Deck deck = (Deck)this.deckLayout.Tag;

					deck.CardList.Remove((DeckCard)toBeRemoved.Tag);
				}

				if(this.deckLayout.Items.Count == 0) {
					this.deckLayout.Groups.Clear();
				}
			}

			this.SetDeckCardAmountLabel();
		}

		private void AddCardToDeck(Card card, int amount, bool isSideboard, bool withoutCardAmountCounting) {
			DeckCard deckCard = new DeckCard();

			deckCard.Card = card;
			deckCard.Amount = amount;
			deckCard.Sideboard = isSideboard;

			this.AddCardToDeck(deckCard, withoutCardAmountCounting);
		}

		private void AddCardToDeck(DeckCard card, bool withoutCardAmountCounting) {
			// if there are no cards and no 
			// groups in, create new deck
			if(this.deckLayout.Items.Count == 0
			&& this.deckLayout.Groups.Count != 4) {
				this.deckLayout.Groups.Clear();

				this.CreateNewDeck();
			}

			// checked if card exist
			bool cardIsIn = false;
			ListViewItem existingItem = null;

			foreach(ListViewItem item in this.deckLayout.Items) {
				DeckCard cd = (DeckCard)item.Tag;

				if(cd != null
				&& cd.Card.Name.Equals(card.Name)) {
					existingItem = item;
					cardIsIn = true;
					break;
				}
			}

			// if card is in, increase amount
			if(cardIsIn
			&& existingItem != null) {
				string number = existingItem.Text[0].ToString();

				if(number.IsNumeric()) {
					int amount = number.ToInt32();
					DeckCard cd = (DeckCard)existingItem.Tag;

					amount++;

					cd.Amount = amount;
					existingItem.Text = amount.ToString() + " " + cd.Card.Name;

					if(!withoutCardAmountCounting) {
						ListViewGroup group = existingItem.Group;

						if(group.Header.Contains("(")
						&& group.Header.Contains(")")) {
							// ({0} cards)
							string name = group.Header.Substring(0, group.Header.IndexOf("(")).Trim();
							string intNumber = group.Header.Substring(
								group.Header.IndexOf("(") + 1,
								group.Header.IndexOf("cards)") - group.Header.IndexOf("(") - 1
							).Trim();

							int newNumber = intNumber.ToInt32() + 1;

							group.Header = name + " (" + newNumber.ToString() + " cards)";
						}
					}
				}
			}
			else {
				ListViewItem listItem = new ListViewItem();

				listItem.Name = card.Card.Name;
				listItem.Text = card.Amount + " " + card.Card.Name;
				listItem.Tag = card;

				if(card.Card.FirstEditionFromList != null) {
					string filename = Toenda.Lhurgoyf.Utility.Helper.CreateImageFilename(
						card.Card.FirstEditionFromList.Shortname,
						card.Card.Name
					);

					if(!File.Exists(filename)) {
						if(!Directory.Exists("img\\" + card.Card.FirstEditionFromList.Shortname + "\\")) {
							Directory.CreateDirectory("img\\" + card.Card.FirstEditionFromList.Shortname + "\\");
						}

						ImageLoader.DownloadImage(
							card.Card.FirstEditionImageFromList.Url.AbsoluteUri,
							filename,
							card.Card.MainType
						);
					}

					Bitmap bmpToShrink = new Bitmap(filename);

					Bitmap bmp = this._imageGenerator.GenerateNewResolution(bmpToShrink, 48);

					this.deckLayout.LargeImageList.Images.Add(card.Card.Name, bmp);
					//this.deckLayout.SmallImageList.Images.Add(card.Card.Name, bmp);
					//this.deckLayout.StateImageList.Images.Add(card.Card.Name, bmp);

					listItem.ImageKey = card.Card.Name;

					ListViewGroup group = null;

					if(card.Sideboard) {
						group = this.deckLayout.Groups["Sideboard"];
					}
					else {
						switch(card.Card.MainType) {
							case "Creature":
								group = this.deckLayout.Groups["Creatures"];
								break;

							case "Land":
								group = this.deckLayout.Groups["Lands"];
								break;

							default:
								group = this.deckLayout.Groups["Spells"];
								break;
						}
					}

					if(!withoutCardAmountCounting) {
						if(group.Header.Contains("(")
						&& group.Header.Contains(")")) {
							// ({0} cards)
							string name = group.Header.Substring(0, group.Header.IndexOf("(")).Trim();
							string number = group.Header.Substring(
								group.Header.IndexOf("(") + 1,
								group.Header.IndexOf("cards)") - group.Header.IndexOf("(") - 1
							).Trim();

							int newNumber = number.ToInt32() + 1;

							group.Header = name + " (" + newNumber.ToString() + " cards)";
						}
					}

					listItem.Group = group;

					this.deckLayout.Items.Add(listItem);

					if(this.deckLayout.Tag != null) {
						Deck deck = (Deck)this.deckLayout.Tag;

						deck.CardList.Add(card);
					}
				}

				this.deckLayout.Refresh();
				this.deckLayout.Update();
			}

			this.SetDeckCardAmountLabel();
		}

		private void CreateNewDeck() {
			Deck deck = new Deck();

			deck.CardList = new List<DeckCard>();

			this.deckLayout.Tag = deck;

			this.deckLayout.LargeImageList = new ImageList();
			//this.deckLayout.SmallImageList = new ImageList();
			//this.deckLayout.StateImageList = new ImageList();

			this.deckLayout.Groups.Add("Creatures", "Creatures ({0} cards)".FormatIt(0));
			this.deckLayout.Groups.Add("Spells", "Spells ({0} cards)".FormatIt(0));
			this.deckLayout.Groups.Add("Lands", "Lands ({0} cards)".FormatIt(0));
			this.deckLayout.Groups.Add("Sideboard", "Sideboard ({0} cards)".FormatIt(0));
		}

		private void SaveFile(bool giveNewName) {
			Deck deck = (Deck)this.deckLayout.Tag;

			deck.CardList.Clear();

			foreach(ListViewItem item in this.deckLayout.Items) {
				DeckCard card = (DeckCard)item.Tag;

				deck.CardList.Add(card);
			}

			deck.Name = this.txtName.Text;
			deck.Author = this.txtAuthor.Text;
			deck.Comment = this.txtComment.Text;

			string buffer = "";
			FileType fileType = FileType.Apprentice;
			string filename = "";

			if(giveNewName
			|| !File.Exists(deck.Filename)) {
				if(!deck.Name.IsNullOrTrimmedEmpty()) {
					this.saveFileDialog.FileName = deck.Name + ".dec";
				}
				else if(!deck.ArcheType.IsNullOrTrimmedEmpty()) {
					this.saveFileDialog.FileName = deck.ArcheType + ".dec";
				}
				else if(deck.Type != null
				&& !deck.Type.Name.IsNullOrTrimmedEmpty()) {
					this.saveFileDialog.FileName = deck.Type.Name + ".dec";
				}
				else {
					this.saveFileDialog.FileName = "NewDeckList.dec";
				}

				if(this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					filename = this.saveFileDialog.FileName;
					
					switch(Path.GetExtension(filename).ToLower()) {
						case ".cod":
							fileType = FileType.Cockatrice;
							break;
						
						case ".mwdeck":
							fileType = FileType.MagicWorkstation;
							break;
						
						case ".txt":
							fileType = FileType.MagicOnline;
							break;

						case ".dec":
						default:
							fileType = FileType.Apprentice;
							break;
					}

					buffer = this.GenerateFileContent(deck, fileType);
				}
			}
			else {
				filename = deck.Filename;

				switch(Path.GetExtension(filename).ToLower()) {
					case ".cod":
						fileType = FileType.Cockatrice;
						break;

					case ".mwdeck":
						fileType = FileType.MagicWorkstation;
						break;

					case ".txt":
						fileType = FileType.MagicOnline;
						break;

					case ".dec":
					default:
						fileType = FileType.Apprentice;
						break;
				}

				buffer = this.GenerateFileContent(deck, fileType);
			}

			if(!filename.IsNullOrTrimmedEmpty()) {
				StreamWriter writer = new StreamWriter(filename, false);
				writer.Write(buffer);
				writer.Flush();
				writer.Close();
				writer.Dispose();

				//if(giveNewName) {
				//    List<string> archeTypes = ArchetypeIdentify.Identify(deck);
				//    this.LoadDeckToDeckViewer(deck, archeTypes);
				//}
			}
		}

		private string GenerateFileContent(Deck deck, FileType fileType) {
			FileHandler fileHandler;

			switch(fileType) {
				case FileType.MagicWorkstation:
					fileHandler = new MagicWorkstationFileHandler();
					break;

				case FileType.Cockatrice:
					fileHandler = new CockatriceFileHandler();
					break;

				case FileType.MagicOnline:
					fileHandler = new MagicOnlineFileHandler();
					break;

				case FileType.Apprentice:
				default:
					fileHandler = new ApprenticeFileHandler();
					break;
			}

			return fileHandler.Generate(deck);
		}

		private void SetDeckCardAmountLabel() {
			int mainboard = 0;
			int sideboard = 0;

			if(this.deckLayout.Tag != null) {
				Deck deck = (Deck)this.deckLayout.Tag;

				if(deck != null) {
					foreach(DeckCard card in deck.CardList) {
						if(card.Sideboard) {
							sideboard += card.Amount;
						}
						else {
							mainboard += card.Amount;
						}
					}
				}
			}

			StringBuilder str = new StringBuilder();
			str.Append("Mainboard: ");
			str.Append(mainboard.ToString());
			str.Append(" cards / ");
			str.Append("Sideboard: ");
			str.Append(sideboard.ToString());
			str.Append(" cards");

			this.lblAmount.Text = str.ToString();
		}

		private void Search(string searchword) {
			this.LoadDataToTreeView(searchword);
		}

		private delegate void SetToolStripProgressBarCallback(int index);
		private delegate void SetToolStripLabelTextCallback(string text);
		private delegate void SetToolStripVisibilityCallback(bool visibile);
		private delegate void SetToolStripLabelTimeTextCallback(TimeSpan totalTime, TimeSpan loadingTime);

		private void SetToolStripLabelTimeText(TimeSpan totalTime, TimeSpan loadingTime) {
			if(this.toolStrip.InvokeRequired) {
				SetToolStripLabelTimeTextCallback d = new SetToolStripLabelTimeTextCallback(SetToolStripLabelTimeText);
				this.Invoke(d, new object[] { totalTime, loadingTime });
			}
			else {
				StringBuilder str = new StringBuilder();

				// Format: 00:00:00 h / 00:00:00 h
				str.Append(Converter.AddLeadingNil(loadingTime.Hours.ToString()));
				str.Append(":");
				str.Append(Converter.AddLeadingNil(loadingTime.Minutes.ToString()));
				str.Append(":");
				str.Append(Converter.AddLeadingNil(loadingTime.Seconds.ToString()));
				str.Append(" h / ");
				str.Append(Converter.AddLeadingNil(totalTime.Hours.ToString()));
				str.Append(":");
				str.Append(Converter.AddLeadingNil(totalTime.Minutes.ToString()));
				str.Append(":");
				str.Append(Converter.AddLeadingNil(totalTime.Seconds.ToString()));
				str.Append(" h");

				this.tslblTime.Text = str.ToString();
				this.toolStrip.Update();
				this.toolStrip.Refresh();
			}
		}

		private void SetToolStripVisibility(bool visibile) {
			if(this.toolStrip.InvokeRequired) {
				SetToolStripVisibilityCallback d = new SetToolStripVisibilityCallback(SetToolStripVisibility);
				this.Invoke(d, new object[] { visibile });
			}
			else {
				this.toolStrip.Visible = visibile;
				this.toolStrip.Update();
				this.toolStrip.Refresh();
			}
		}

		private void SetToolStripProgressBar(int index) {
			if(this.toolStrip.InvokeRequired) {
				SetToolStripProgressBarCallback d = new SetToolStripProgressBarCallback(SetToolStripProgressBar);
				this.Invoke(d, new object[] { index });
			}
			else {
				this.tspbImageLoader.Value = index;
				this.toolStrip.Update();
				this.toolStrip.Refresh();
			}
		}

		private void SetToolStripLabelText(string text) {
			if(this.toolStrip.InvokeRequired) {
				SetToolStripLabelTextCallback d = new SetToolStripLabelTextCallback(SetToolStripLabelText);
				this.Invoke(d, new object[] { text });
			}
			else {
				this.tslblImageLoader.Text = text;
				this.toolStrip.Update();
				this.toolStrip.Refresh();
			}
		}

		private void Initialize() {
			LoadingForm loadingForm = new LoadingForm(null);

			if(loadingForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				//this.LoadDataToTreeView("");

				this._imageLoader = new ImageLoader();

				this.tspbImageLoader.Maximum = this._imageLoader.ItemsToLoad;
				this.tslblImageLoader.Text = "ImageLoader initialized.";

				this._imageLoader.ImageLoaderResponse += new ImageLoader.ImageLoaderEventHandler(imageLoader_ImageLoaderResponse);
				this._imageLoader.ImageLoaderFinish += new ImageLoader.ImageLoaderFinishEventhandler(imageLoader_ImageLoaderFinish);
			}

			loadingForm.Close();
		}

		private TreeNode FindGroupNode(string name) {
			return this.tvCards.Nodes[this.tvCards.Nodes.IndexOfKey(name)];
		}

		#endregion

		#region Public

		public void LoadDataToTreeView(string searchword) {
			List<Card> foundEntries = CardBase.Cards;

			if(!searchword.IsNullOrTrimmedEmpty()) {
				foundEntries = CardFinder.Find(searchword);
			}

			this.tvCards.Nodes.Clear();

			var groups =
				from item in foundEntries
				orderby item.MainType ascending
				group item by item.MainType;

			foreach(var group in groups) {
				TreeNode node = new TreeNode();

				node.Name = group.Key;
				node.Tag = group.Key;
				node.Text = group.Key;

				this.tvCards.Nodes.Add(node);
			}

			var orderedList =
				from item in foundEntries
				orderby item.Name ascending
				select item;

			foreach(var item in orderedList) {
				Card card = (Card)item;

				TreeNode node = this.FindGroupNode(card.MainType);

				TreeNode newNode = new TreeNode();

				newNode.Name = card.Name;
				newNode.Tag = card;
				newNode.Text = card.ToString();

				node.Nodes.Add(newNode);
			}
		}

		public void LoadRecentFiles(string filename) {
			if(!filename.IsNullOrTrimmedEmpty()) {
				if(Configuration.LastFiles.Contains(filename)) {
					Configuration.LastFiles.Remove(filename);
				}

				Configuration.LastFiles.Insert(0, filename);

				if(Configuration.LastFiles.Count > 15) {
					Configuration.LastFiles.RemoveRange(
						15,
						Configuration.LastFiles.Count - 15
					);
				}
			}

			if(Configuration.LastFiles.Count > 0) {
				this.recentFilesToolStripMenuItem.DropDownItems.Clear();

				foreach(string file in Configuration.LastFiles) {
					ToolStripItem item = new ToolStripMenuItem();
					item.Text = Toenda.Foundation.Utility.Text.ShortenString(
						file, 70, StringCutPlaceholderPosition.Middle
					);
					item.Tag = file;
					item.Click += new EventHandler(item_Click);

					this.recentFilesToolStripMenuItem.DropDownItems.Add(item);
				}
			}
			else {
				this.recentFilesToolStripMenuItem.Enabled = false;
			}
		}

		public void LoadDeckToDeckViewer(Deck deck, List<string> archeTypes) {
			this.deckLayout.Clear();

			/*
			 * Create Group Titles
			 */
			int sideboard = 0;
			int creatures = 0;
			int lands = 0;
			int spells = 0;
			int mainboard = 0;

			if(deck != null) {
				foreach(DeckCard card in deck.CardList) {
					if(card.Sideboard) {
						sideboard += card.Amount;
					}
					else {
						switch(card.Card.MainType) {
							case "Creature":
								creatures += card.Amount;
								break;

							case "Land":
								lands += card.Amount;
								break;

							default:
								spells += card.Amount;
								break;
						}

						mainboard += card.Amount;
					}
				}

				this.deckLayout.Groups.Add("Creatures", "Creatures ({0} cards)".FormatIt(creatures));
				this.deckLayout.Groups.Add("Spells", "Spells ({0} cards)".FormatIt(spells));
				this.deckLayout.Groups.Add("Lands", "Lands ({0} cards)".FormatIt(lands));
				this.deckLayout.Groups.Add("Sideboard", "Sideboard ({0} cards)".FormatIt(sideboard));

				/*
				 * Load Deck
				 */
				this.txtName.Text = deck.Name;
				this.txtAuthor.Text = deck.Author;
				this.txtComment.Text = deck.Comment;

				this.lbArchetypes.Items.Clear();

				foreach(string archeType in archeTypes) {
					this.lbArchetypes.Items.Add(archeType);
				}

				var orderedList =
					from item in deck.CardList
					orderby item.Card.ConvertedCastingCost ascending, item.Card.Name ascending
					select item;

				this.deckLayout.LargeImageList = new ImageList();
				//this.deckLayout.SmallImageList = new ImageList();
				//this.deckLayout.StateImageList = new ImageList();

				// -----------------------------

				ListViewItem[] itemList = new ListViewItem[orderedList.Count()];
				int index = 0;

				foreach(var item in orderedList) {
					DeckCard card = (DeckCard)item;

					//this.AddCardToDeck(card, true);

					ListViewItem listItem = new ListViewItem();

					listItem.Name = card.Card.Name;
					listItem.Text = card.Amount + " " + card.Card.Name;
					listItem.Tag = card;

					if(card.Card.FirstEditionFromList != null) {
						string filename = Toenda.Lhurgoyf.Utility.Helper.CreateImageFilename(
							card.Card.FirstEditionFromList.Shortname,
							card.Card.Name
						);

						if(!File.Exists(filename)) {
							if(!Directory.Exists("img\\" + card.Card.FirstEditionFromList.Shortname + "\\")) {
								Directory.CreateDirectory("img\\" + card.Card.FirstEditionFromList.Shortname + "\\");
							}

							ImageLoader.DownloadImage(
								card.Card.FirstEditionImageFromList.Url.AbsoluteUri,
								filename,
								card.Card.MainType
							);
						}

						Bitmap bmpToShrink = new Bitmap(filename);

						Bitmap bmp = this._imageGenerator.GenerateNewResolution(bmpToShrink, 48);

						this.deckLayout.LargeImageList.Images.Add(card.Card.Name, bmp);
						//this.deckLayout.SmallImageList.Images.Add(card.Card.Name, bmp);
						//this.deckLayout.StateImageList.Images.Add(card.Card.Name, bmp);

						listItem.ImageKey = card.Card.Name;
					}

					ListViewGroup group = null;

					if(card.Sideboard) {
						group = this.deckLayout.Groups["Sideboard"];
					}
					else {
						switch(card.Card.MainType) {
							case "Creature":
								group = this.deckLayout.Groups["Creatures"];
								break;

							case "Land":
								group = this.deckLayout.Groups["Lands"];
								break;

							default:
								group = this.deckLayout.Groups["Spells"];
								break;
						}
					}

					listItem.Group = group;

					itemList[index] = listItem;

					index++;
				}

				this.deckLayout.Items.AddRange(itemList);

				this.deckLayout.Refresh();
				this.deckLayout.Update();

				// -----------------------------

				this.closeToolStripMenuItem.Enabled = true;
				this.saveToolStripMenuItem.Enabled = true;
				this.saveAsToolStripMenuItem.Enabled = true;

				this.deckLayout.Tag = deck;

				this.SetDeckCardAmountLabel();
			}
		}

		#endregion
	}
}
