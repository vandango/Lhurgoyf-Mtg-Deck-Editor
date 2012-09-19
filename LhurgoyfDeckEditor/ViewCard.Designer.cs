namespace Toenda.LhurgoyfDeckEditor {
	partial class ViewCard {
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
			this.gbCard = new System.Windows.Forms.GroupBox();
			this.btnTransform = new System.Windows.Forms.Button();
			this.txtFlavourText = new System.Windows.Forms.TextBox();
			this.txtText = new System.Windows.Forms.TextBox();
			this.lblPowerToughness = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblArtist = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lbCollections = new System.Windows.Forms.ListBox();
			this.lblColor = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblRarity = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblType = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblCastingCost = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cardImage = new System.Windows.Forms.PictureBox();
			this.gbCard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cardImage)).BeginInit();
			this.SuspendLayout();
			// 
			// gbCard
			// 
			this.gbCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbCard.Controls.Add(this.btnTransform);
			this.gbCard.Controls.Add(this.txtFlavourText);
			this.gbCard.Controls.Add(this.txtText);
			this.gbCard.Controls.Add(this.lblPowerToughness);
			this.gbCard.Controls.Add(this.label13);
			this.gbCard.Controls.Add(this.lblArtist);
			this.gbCard.Controls.Add(this.label11);
			this.gbCard.Controls.Add(this.label9);
			this.gbCard.Controls.Add(this.label8);
			this.gbCard.Controls.Add(this.label2);
			this.gbCard.Controls.Add(this.lbCollections);
			this.gbCard.Controls.Add(this.lblColor);
			this.gbCard.Controls.Add(this.label7);
			this.gbCard.Controls.Add(this.lblRarity);
			this.gbCard.Controls.Add(this.label5);
			this.gbCard.Controls.Add(this.lblType);
			this.gbCard.Controls.Add(this.label4);
			this.gbCard.Controls.Add(this.lblCastingCost);
			this.gbCard.Controls.Add(this.label3);
			this.gbCard.Controls.Add(this.lblName);
			this.gbCard.Controls.Add(this.label1);
			this.gbCard.Controls.Add(this.cardImage);
			this.gbCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbCard.Location = new System.Drawing.Point(12, 12);
			this.gbCard.Name = "gbCard";
			this.gbCard.Size = new System.Drawing.Size(519, 345);
			this.gbCard.TabIndex = 1;
			this.gbCard.TabStop = false;
			this.gbCard.Text = "Name of the card";
			// 
			// btnTransform
			// 
			this.btnTransform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTransform.Location = new System.Drawing.Point(453, 0);
			this.btnTransform.Name = "btnTransform";
			this.btnTransform.Size = new System.Drawing.Size(60, 23);
			this.btnTransform.TabIndex = 26;
			this.btnTransform.Text = "Tansform";
			this.btnTransform.UseVisualStyleBackColor = true;
			this.btnTransform.Visible = false;
			this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
			// 
			// txtFlavourText
			// 
			this.txtFlavourText.BackColor = System.Drawing.SystemColors.Control;
			this.txtFlavourText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFlavourText.Location = new System.Drawing.Point(323, 224);
			this.txtFlavourText.Multiline = true;
			this.txtFlavourText.Name = "txtFlavourText";
			this.txtFlavourText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFlavourText.Size = new System.Drawing.Size(190, 39);
			this.txtFlavourText.TabIndex = 25;
			this.txtFlavourText.Text = "Flavour Text";
			// 
			// txtText
			// 
			this.txtText.BackColor = System.Drawing.SystemColors.Control;
			this.txtText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtText.Location = new System.Drawing.Point(322, 140);
			this.txtText.Multiline = true;
			this.txtText.Name = "txtText";
			this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtText.Size = new System.Drawing.Size(191, 81);
			this.txtText.TabIndex = 24;
			this.txtText.Text = "Text";
			// 
			// lblPowerToughness
			// 
			this.lblPowerToughness.AutoSize = true;
			this.lblPowerToughness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPowerToughness.Location = new System.Drawing.Point(322, 124);
			this.lblPowerToughness.Name = "lblPowerToughness";
			this.lblPowerToughness.Size = new System.Drawing.Size(101, 13);
			this.lblPowerToughness.TabIndex = 21;
			this.lblPowerToughness.Text = "Power / Toughness";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(236, 124);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(41, 13);
			this.label13.TabIndex = 20;
			this.label13.Text = "P / T:";
			// 
			// lblArtist
			// 
			this.lblArtist.AutoSize = true;
			this.lblArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblArtist.Location = new System.Drawing.Point(322, 108);
			this.lblArtist.Name = "lblArtist";
			this.lblArtist.Size = new System.Drawing.Size(30, 13);
			this.lblArtist.TabIndex = 19;
			this.lblArtist.Text = "Artist";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(235, 108);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 13);
			this.label11.TabIndex = 18;
			this.label11.Text = "Artist:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(236, 224);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(82, 13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Flavour Text:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(235, 140);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(36, 13);
			this.label8.TabIndex = 14;
			this.label8.Text = "Text:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(235, 269);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Editions:";
			// 
			// lbCollections
			// 
			this.lbCollections.BackColor = System.Drawing.SystemColors.Control;
			this.lbCollections.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbCollections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbCollections.FormattingEnabled = true;
			this.lbCollections.Location = new System.Drawing.Point(325, 269);
			this.lbCollections.Name = "lbCollections";
			this.lbCollections.Size = new System.Drawing.Size(188, 65);
			this.lbCollections.TabIndex = 12;
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColor.Location = new System.Drawing.Point(322, 76);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(31, 13);
			this.lblColor.TabIndex = 11;
			this.lblColor.Text = "Color";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(235, 76);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Color:";
			// 
			// lblRarity
			// 
			this.lblRarity.AutoSize = true;
			this.lblRarity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRarity.Location = new System.Drawing.Point(322, 92);
			this.lblRarity.Name = "lblRarity";
			this.lblRarity.Size = new System.Drawing.Size(34, 13);
			this.lblRarity.TabIndex = 9;
			this.lblRarity.Text = "Rarity";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(235, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Rarity:";
			// 
			// lblType
			// 
			this.lblType.AutoSize = true;
			this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblType.Location = new System.Drawing.Point(322, 60);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(31, 13);
			this.lblType.TabIndex = 7;
			this.lblType.Text = "Type";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(235, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Type:";
			// 
			// lblCastingCost
			// 
			this.lblCastingCost.AutoSize = true;
			this.lblCastingCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCastingCost.Location = new System.Drawing.Point(322, 44);
			this.lblCastingCost.Name = "lblCastingCost";
			this.lblCastingCost.Size = new System.Drawing.Size(65, 13);
			this.lblCastingCost.TabIndex = 5;
			this.lblCastingCost.Text = "Casting cost";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(235, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Casting cost:";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(322, 28);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(89, 13);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Name of the card";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(235, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			// 
			// cardImage
			// 
			this.cardImage.Location = new System.Drawing.Point(6, 28);
			this.cardImage.Name = "cardImage";
			this.cardImage.Size = new System.Drawing.Size(223, 310);
			this.cardImage.TabIndex = 1;
			this.cardImage.TabStop = false;
			this.cardImage.Click += new System.EventHandler(this.cardImage_Click);
			// 
			// ViewCard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(544, 369);
			this.Controls.Add(this.gbCard);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ViewCard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ViewCard";
			this.Click += new System.EventHandler(this.ViewCard_Click);
			this.gbCard.ResumeLayout(false);
			this.gbCard.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cardImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbCard;
		private System.Windows.Forms.PictureBox cardImage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblCastingCost;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblRarity;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lbCollections;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblArtist;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblPowerToughness;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtFlavourText;
		private System.Windows.Forms.TextBox txtText;
		private System.Windows.Forms.Button btnTransform;

	}
}