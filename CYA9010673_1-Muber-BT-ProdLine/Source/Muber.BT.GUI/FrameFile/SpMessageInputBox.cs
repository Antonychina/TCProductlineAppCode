using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mubea.AutoTest.GUI
{
    public partial class SpMessageInputBox : SpMessageBoxDlg
    {
        public SpMessageInputBox()
        {
            InitializeComponent();
        }

        public string GetPasswordValue()
        {
            if (this.inPutpasswd_textBox.Text.Trim().Length > 0)
            {
                return this.inPutpasswd_textBox.Text.Trim();
            }
            else
                return string.Empty;
        }

        public void SetInputTextFormat(bool isPasswprd, string message = "Input UserName:", string defaultText = "")
        {
            this.inPutpasswd_textBox.UseSystemPasswordChar = isPasswprd;
            this.inPutpasswd_textBox.Text = defaultText;
            this.Label_PourTimes.Text = isPasswprd ? "Input Password:" : message;

        }
    }
}
