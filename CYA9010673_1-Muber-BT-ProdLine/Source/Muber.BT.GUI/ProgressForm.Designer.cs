namespace Mubea.AutoTest.GUI
{
	partial class ProgressForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.prgBar = new System.Windows.Forms.ProgressBar();
			this.labNote = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// prgBar
			// 
			this.prgBar.Location = new System.Drawing.Point(16, 149);
			this.prgBar.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.prgBar.Name = "prgBar";
			this.prgBar.Size = new System.Drawing.Size(760, 47);
			this.prgBar.TabIndex = 1;
			// 
			// labNote
			// 
			this.labNote.AutoSize = true;
			this.labNote.Location = new System.Drawing.Point(16, 102);
			this.labNote.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.labNote.Name = "labNote";
			this.labNote.Size = new System.Drawing.Size(78, 26);
			this.labNote.TabIndex = 0;
			this.labNote.Text = "label1";
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 274);
			this.Controls.Add(this.prgBar);
			this.Controls.Add(this.labNote);
			this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressForm";
			this.Text = "运行进度";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
			this.Load += new System.EventHandler(this.ProgressForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labNote;
		private System.Windows.Forms.ProgressBar prgBar;
	}
}