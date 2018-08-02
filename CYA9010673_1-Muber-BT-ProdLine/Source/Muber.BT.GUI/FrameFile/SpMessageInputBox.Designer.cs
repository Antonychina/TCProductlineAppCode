namespace Mubea.AutoTest.GUI
{
    partial class SpMessageInputBox
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
            this.Label_PourTimes = new System.Windows.Forms.Label();
            this.inPutpasswd_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label_PourTimes
            // 
            this.Label_PourTimes.AutoSize = true;
            this.Label_PourTimes.Location = new System.Drawing.Point(53, 106);
            this.Label_PourTimes.Name = "Label_PourTimes";
            this.Label_PourTimes.Size = new System.Drawing.Size(152, 25);
            this.Label_PourTimes.TabIndex = 4;
            this.Label_PourTimes.Text = "Input Password:";
            // 
            // inPutpasswd_textBox
            // 
            this.inPutpasswd_textBox.Location = new System.Drawing.Point(253, 103);
            this.inPutpasswd_textBox.Name = "inPutpasswd_textBox";
            this.inPutpasswd_textBox.Size = new System.Drawing.Size(128, 30);
            this.inPutpasswd_textBox.TabIndex = 0;
            this.inPutpasswd_textBox.UseSystemPasswordChar = true;
            // 
            // SpMessageInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.ClientSize = new System.Drawing.Size(454, 217);
            this.Controls.Add(this.inPutpasswd_textBox);
            this.Controls.Add(this.Label_PourTimes);
            this.Name = "SpMessageInputBox";
            this.Padding = new System.Windows.Forms.Padding(12, 46, 12, 10);
            this.Controls.SetChildIndex(this.Label_PourTimes, 0);
            this.Controls.SetChildIndex(this.inPutpasswd_textBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_PourTimes;
        private System.Windows.Forms.TextBox inPutpasswd_textBox;

    }
}