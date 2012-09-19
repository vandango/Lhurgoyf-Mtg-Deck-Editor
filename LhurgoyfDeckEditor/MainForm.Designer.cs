namespace Toenda.LhurgoyfDeckEditor {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tspbImageLoader = new System.Windows.Forms.ToolStripProgressBar();
			this.tslblTime = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tslblImageLoader = new System.Windows.Forms.ToolStripLabel();
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.btnExtSearch = new System.Windows.Forms.Button();
			this.tvCards = new System.Windows.Forms.TreeView();
			this.cmsTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.addToDeckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addToSideboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.splitContainerSub = new System.Windows.Forms.SplitContainer();
			this.lblAmount = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lbArchetypes = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.deckLayout = new Toenda.LhurgoyfDeckEditor.UserControls.NativeListView();
			this.cmsDeck = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewCardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.amountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.amountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.removeCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveToSideboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.cmsTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSub)).BeginInit();
			this.splitContainerSub.Panel1.SuspendLayout();
			this.splitContainerSub.Panel2.SuspendLayout();
			this.splitContainerSub.SuspendLayout();
			this.cmsDeck.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.toolStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainerMain);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(884, 463);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(884, 512);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbImageLoader,
            this.tslblTime,
            this.toolStripSeparator4,
            this.tslblImageLoader});
			this.toolStrip.Location = new System.Drawing.Point(3, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(258, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// tspbImageLoader
			// 
			this.tspbImageLoader.Name = "tspbImageLoader";
			this.tspbImageLoader.Size = new System.Drawing.Size(100, 22);
			// 
			// tslblTime
			// 
			this.tslblTime.Name = "tslblTime";
			this.tslblTime.Size = new System.Drawing.Size(122, 22);
			this.tslblTime.Text = "00:00:00 h / 00:00:00 h";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tslblImageLoader
			// 
			this.tslblImageLoader.Name = "tslblImageLoader";
			this.tslblImageLoader.Size = new System.Drawing.Size(16, 22);
			this.tslblImageLoader.Text = "...";
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.btnExtSearch);
			this.splitContainerMain.Panel1.Controls.Add(this.tvCards);
			this.splitContainerMain.Panel1.Controls.Add(this.btnSearch);
			this.splitContainerMain.Panel1.Controls.Add(this.txtSearch);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.splitContainerSub);
			this.splitContainerMain.Size = new System.Drawing.Size(884, 463);
			this.splitContainerMain.SplitterDistance = 293;
			this.splitContainerMain.TabIndex = 0;
			// 
			// btnExtSearch
			// 
			this.btnExtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExtSearch.Location = new System.Drawing.Point(241, 4);
			this.btnExtSearch.Name = "btnExtSearch";
			this.btnExtSearch.Size = new System.Drawing.Size(49, 20);
			this.btnExtSearch.TabIndex = 3;
			this.btnExtSearch.Text = "Ext";
			this.btnExtSearch.UseVisualStyleBackColor = true;
			this.btnExtSearch.Click += new System.EventHandler(this.btnExtSearch_Click);
			// 
			// tvCards
			// 
			this.tvCards.AllowDrop = true;
			this.tvCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tvCards.ContextMenuStrip = this.cmsTreeView;
			this.tvCards.Indent = 19;
			this.tvCards.Location = new System.Drawing.Point(3, 30);
			this.tvCards.Name = "tvCards";
			this.tvCards.PathSeparator = "|";
			this.tvCards.Size = new System.Drawing.Size(287, 430);
			this.tvCards.TabIndex = 2;
			this.tvCards.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvCards_ItemDrag);
			this.tvCards.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCards_NodeMouseClick);
			this.tvCards.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCards_NodeMouseDoubleClick);
			this.tvCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvCards_DragDrop);
			this.tvCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvCards_DragEnter);
			this.tvCards.DragLeave += new System.EventHandler(this.tvCards_DragLeave);
			this.tvCards.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvCards_MouseDown);
			// 
			// cmsTreeView
			// 
			this.cmsTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewCardToolStripMenuItem,
            this.toolStripSeparator5,
            this.addToDeckToolStripMenuItem,
            this.addToSideboardToolStripMenuItem});
			this.cmsTreeView.Name = "cmsTreeView";
			this.cmsTreeView.Size = new System.Drawing.Size(167, 76);
			// 
			// viewCardToolStripMenuItem
			// 
			this.viewCardToolStripMenuItem.Name = "viewCardToolStripMenuItem";
			this.viewCardToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.viewCardToolStripMenuItem.Text = "View Card";
			this.viewCardToolStripMenuItem.Click += new System.EventHandler(this.viewCardToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(163, 6);
			// 
			// addToDeckToolStripMenuItem
			// 
			this.addToDeckToolStripMenuItem.Name = "addToDeckToolStripMenuItem";
			this.addToDeckToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.addToDeckToolStripMenuItem.Text = "Add to deck";
			this.addToDeckToolStripMenuItem.Click += new System.EventHandler(this.addToDeckToolStripMenuItem_Click);
			// 
			// addToSideboardToolStripMenuItem
			// 
			this.addToSideboardToolStripMenuItem.Name = "addToSideboardToolStripMenuItem";
			this.addToSideboardToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.addToSideboardToolStripMenuItem.Text = "Add to Sideboard";
			this.addToSideboardToolStripMenuItem.Click += new System.EventHandler(this.addToSideboardToolStripMenuItem_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(186, 4);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(49, 20);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point(3, 4);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(177, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
			// 
			// splitContainerSub
			// 
			this.splitContainerSub.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerSub.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerSub.IsSplitterFixed = true;
			this.splitContainerSub.Location = new System.Drawing.Point(0, 0);
			this.splitContainerSub.Name = "splitContainerSub";
			this.splitContainerSub.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerSub.Panel1
			// 
			this.splitContainerSub.Panel1.Controls.Add(this.lblAmount);
			this.splitContainerSub.Panel1.Controls.Add(this.label2);
			this.splitContainerSub.Panel1.Controls.Add(this.txtName);
			this.splitContainerSub.Panel1.Controls.Add(this.txtComment);
			this.splitContainerSub.Panel1.Controls.Add(this.txtAuthor);
			this.splitContainerSub.Panel1.Controls.Add(this.label5);
			this.splitContainerSub.Panel1.Controls.Add(this.label4);
			this.splitContainerSub.Panel1.Controls.Add(this.lbArchetypes);
			this.splitContainerSub.Panel1.Controls.Add(this.label3);
			this.splitContainerSub.Panel1.Controls.Add(this.label1);
			// 
			// splitContainerSub.Panel2
			// 
			this.splitContainerSub.Panel2.Controls.Add(this.deckLayout);
			this.splitContainerSub.Size = new System.Drawing.Size(587, 463);
			this.splitContainerSub.SplitterDistance = 120;
			this.splitContainerSub.TabIndex = 0;
			// 
			// lblAmount
			// 
			this.lblAmount.AutoSize = true;
			this.lblAmount.Location = new System.Drawing.Point(96, 106);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(42, 13);
			this.lblAmount.TabIndex = 10;
			this.lblAmount.Text = "0 cards";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(3, 106);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Card Amount:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(99, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(244, 20);
			this.txtName.TabIndex = 8;
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtComment.Location = new System.Drawing.Point(99, 60);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(485, 35);
			this.txtComment.TabIndex = 7;
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.Location = new System.Drawing.Point(99, 34);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(244, 20);
			this.txtAuthor.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(3, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Comment:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(349, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Archetypes:";
			// 
			// lbArchetypes
			// 
			this.lbArchetypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbArchetypes.FormattingEnabled = true;
			this.lbArchetypes.Location = new System.Drawing.Point(429, 8);
			this.lbArchetypes.Name = "lbArchetypes";
			this.lbArchetypes.Size = new System.Drawing.Size(155, 43);
			this.lbArchetypes.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(3, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Author:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Deckname:";
			// 
			// deckLayout
			// 
			this.deckLayout.AllowDrop = true;
			this.deckLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.deckLayout.ContextMenuStrip = this.cmsDeck;
			this.deckLayout.GridLines = true;
			this.deckLayout.Location = new System.Drawing.Point(3, 3);
			this.deckLayout.Name = "deckLayout";
			this.deckLayout.Size = new System.Drawing.Size(581, 333);
			this.deckLayout.TabIndex = 1;
			this.deckLayout.UseCompatibleStateImageBehavior = false;
			this.deckLayout.View = System.Windows.Forms.View.Tile;
			this.deckLayout.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.deckLayout_ItemDrag);
			this.deckLayout.DragDrop += new System.Windows.Forms.DragEventHandler(this.deckLayout_DragDrop);
			this.deckLayout.DragEnter += new System.Windows.Forms.DragEventHandler(this.deckLayout_DragEnter);
			this.deckLayout.DragLeave += new System.EventHandler(this.deckLayout_DragLeave);
			this.deckLayout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deckLayout_KeyDown);
			this.deckLayout.KeyUp += new System.Windows.Forms.KeyEventHandler(this.deckLayout_KeyUp);
			this.deckLayout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deckLayout_MouseClick);
			this.deckLayout.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.deckLayout_MouseDoubleClick);
			this.deckLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.deckLayout_MouseDown);
			this.deckLayout.MouseLeave += new System.EventHandler(this.deckLayout_MouseLeave);
			// 
			// cmsDeck
			// 
			this.cmsDeck.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewCardToolStripMenuItem1,
            this.toolStripSeparator6,
            this.amountToolStripMenuItem,
            this.amountToolStripMenuItem1,
            this.removeCardToolStripMenuItem,
            this.moveToSideboardToolStripMenuItem});
			this.cmsDeck.Name = "cmsDeck";
			this.cmsDeck.Size = new System.Drawing.Size(175, 120);
			// 
			// viewCardToolStripMenuItem1
			// 
			this.viewCardToolStripMenuItem1.Name = "viewCardToolStripMenuItem1";
			this.viewCardToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
			this.viewCardToolStripMenuItem1.Text = "View Card";
			this.viewCardToolStripMenuItem1.Click += new System.EventHandler(this.viewCardToolStripMenuItem1_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(171, 6);
			// 
			// amountToolStripMenuItem
			// 
			this.amountToolStripMenuItem.Name = "amountToolStripMenuItem";
			this.amountToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.amountToolStripMenuItem.Text = "Amount +";
			this.amountToolStripMenuItem.Click += new System.EventHandler(this.amountToolStripMenuItem_Click);
			// 
			// amountToolStripMenuItem1
			// 
			this.amountToolStripMenuItem1.Name = "amountToolStripMenuItem1";
			this.amountToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
			this.amountToolStripMenuItem1.Text = "Amount -";
			this.amountToolStripMenuItem1.Click += new System.EventHandler(this.amountToolStripMenuItem1_Click);
			// 
			// removeCardToolStripMenuItem
			// 
			this.removeCardToolStripMenuItem.Name = "removeCardToolStripMenuItem";
			this.removeCardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.removeCardToolStripMenuItem.Text = "Remove Card";
			this.removeCardToolStripMenuItem.Click += new System.EventHandler(this.removeCardToolStripMenuItem_Click);
			// 
			// moveToSideboardToolStripMenuItem
			// 
			this.moveToSideboardToolStripMenuItem.Name = "moveToSideboardToolStripMenuItem";
			this.moveToSideboardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.moveToSideboardToolStripMenuItem.Text = "Move to Sideboard";
			this.moveToSideboardToolStripMenuItem.Click += new System.EventHandler(this.moveToSideboardToolStripMenuItem_Click);
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(884, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator3,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.openToolStripMenuItem.Text = "Open File...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// recentFilesToolStripMenuItem
			// 
			this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
			this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.recentFilesToolStripMenuItem.Text = "Recent Files";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.closeToolStripMenuItem.Text = "Close...";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.saveToolStripMenuItem.Text = "Save...";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 512);
			this.Controls.Add(this.toolStripContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lhurgoyf Deck Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel1.PerformLayout();
			this.splitContainerMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
			this.splitContainerMain.ResumeLayout(false);
			this.cmsTreeView.ResumeLayout(false);
			this.splitContainerSub.Panel1.ResumeLayout(false);
			this.splitContainerSub.Panel1.PerformLayout();
			this.splitContainerSub.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSub)).EndInit();
			this.splitContainerSub.ResumeLayout(false);
			this.cmsDeck.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.SplitContainer splitContainerSub;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TreeView tvCards;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripProgressBar tspbImageLoader;
		private System.Windows.Forms.ToolStripLabel tslblImageLoader;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripLabel tslblTime;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private Toenda.LhurgoyfDeckEditor.UserControls.NativeListView deckLayout;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lbArchetypes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnExtSearch;
		private System.Windows.Forms.ContextMenuStrip cmsTreeView;
		private System.Windows.Forms.ToolStripMenuItem addToDeckToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewCardToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem addToSideboardToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip cmsDeck;
		private System.Windows.Forms.ToolStripMenuItem viewCardToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem amountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem amountToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem removeCardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveToSideboardToolStripMenuItem;
		private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.Label label2;
	}
}

