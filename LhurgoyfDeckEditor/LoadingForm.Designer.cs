﻿namespace Toenda.LhurgoyfDeckEditor {
	partial class LoadingForm {
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
			this.pbLoad = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// pbLoad
			// 
			this.pbLoad.Location = new System.Drawing.Point(12, 12);
			this.pbLoad.Name = "pbLoad";
			this.pbLoad.Size = new System.Drawing.Size(300, 23);
			this.pbLoad.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbLoad.TabIndex = 0;
			// 
			// LoadingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(328, 47);
			this.Controls.Add(this.pbLoad);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LoadingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "LoadingForm";
			this.Shown += new System.EventHandler(this.LoadingForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbLoad;
	}
}