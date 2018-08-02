namespace Mubea.AutoTest.GUI
{
	partial class SpMessageBoxDlg
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
            this.textBox_Syslog = new System.Windows.Forms.TextBox();
            this.labMsg = new System.Windows.Forms.Label();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Syslog
            // 
            this.textBox_Syslog.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Syslog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Syslog.Location = new System.Drawing.Point(14, 48);
            this.textBox_Syslog.MaxLength = 327671;
            this.textBox_Syslog.Multiline = true;
            this.textBox_Syslog.Name = "textBox_Syslog";
            this.textBox_Syslog.ReadOnly = true;
            this.textBox_Syslog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Syslog.Size = new System.Drawing.Size(626, 270);
            this.textBox_Syslog.TabIndex = 2;
            this.textBox_Syslog.Visible = false;
            // 
            // labMsg
            // 
            this.labMsg.Location = new System.Drawing.Point(14, 48);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(425, 104);
            this.labMsg.TabIndex = 1;
            this.labMsg.Text = "message";
            this.labMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn3
            // 
            this.btn3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn3.Location = new System.Drawing.Point(309, 166);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(131, 38);
            this.btn3.TabIndex = 0;
            this.btn3.Text = "Cancal_button";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn2
            // 
            this.btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2.Location = new System.Drawing.Point(161, 166);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(131, 38);
            this.btn2.TabIndex = 0;
            this.btn2.Text = "Cancal_button";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn1
            // 
            this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn1.Location = new System.Drawing.Point(14, 166);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(131, 38);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "Cancal_button";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn_Click);
            // 
            // SpMessageBoxDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 217);
            this.Controls.Add(this.textBox_Syslog);
            this.Controls.Add(this.labMsg);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Name = "SpMessageBoxDlg";
            this.Padding = new System.Windows.Forms.Padding(11, 48, 11, 10);
            this.Text = "SpMessageBox";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn1;
		private System.Windows.Forms.Button btn2;
		private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.TextBox textBox_Syslog;
	}
}