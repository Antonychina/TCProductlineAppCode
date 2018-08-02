using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mubea.AutoTest.GUI
{
	public partial class SpMessageBoxDlg : BaseDialog
	{
		private int _minlabMsgWidth = 390;
		private int _maxlabMsgWidth = 700;
		private int _minlabMsgHeight = 108;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color _defaultColor = Color.FromArgb(57, 179, 215);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color _errorColor = Color.FromArgb(210, 50, 45);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color _warningColor = Color.FromArgb(237, 156, 40);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color _success = Color.FromArgb(0, 170, 173);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color _question = Color.FromArgb(71, 164, 71);

		public SpMessageBoxDlg()
		{
			InitializeComponent();
		}

		public void ArrangeApperance(String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
		{
			this.Text = title;
			labMsg.Text = message;

			ReSizeDlg(message);

			DisplayBtnText(buttons);

			SetTitelColor(icon);

			SetDefaultButton(defaultbutton);
		}

        public void ArrangeApperanceModeless(String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            this.Text = title;
            this.textBox_Syslog.Text = message;

            ReSizeModelessDlg();

            DisplayBtnText(buttons);

            SetTitelColor(icon);

            SetDefaultButton(defaultbutton);
        }

        public void UpdateDlgMessage(String message)
        {
            textBox_Syslog.Text = textBox_Syslog.Text + Environment.NewLine + message;
        }


		private void ReSizeDlg(String message)
		{
			Graphics graphics = labMsg.CreateGraphics();
			SizeF sizeF = graphics.MeasureString(message, Font, _maxlabMsgWidth);
			graphics.Dispose();

			int width = (int)sizeF.Width;

			if (width < _minlabMsgWidth)
			{
				labMsg.Size = new System.Drawing.Size(_minlabMsgWidth, _minlabMsgHeight);
			}
			else 
			{
				int height = (int)(sizeF.Height + 1);
				height = Math.Max(height, _minlabMsgHeight);

				labMsg.Size = new System.Drawing.Size(_maxlabMsgWidth, height);
			}

			this.Width = labMsg.Width + labMsg.Padding.Left * 2 + this.Padding.Left * 2;
			this.Height = labMsg.Height + labMsg.Location.Y + 68;
		}

        public void ReSizeModelessDlg()
        {
            this.Width = 650;
            this.Height = 400;
            this.labMsg.Visible = false;
            this.textBox_Syslog.Visible = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
        }

		private void DisplayBtnText(MessageBoxButtons buttons)
		{
			switch (buttons)
			{
				case MessageBoxButtons.OK:
					EnableButton(btn1);

                    btn1.Text = "确定";
					btn1.Location = btn3.Location;

					btn1.Tag = DialogResult.OK;

					EnableButton(btn2, false);
					EnableButton(btn3, false);
					break;
				case MessageBoxButtons.OKCancel:
					EnableButton(btn1);

                    btn1.Text = "确定";
					btn1.Location = btn2.Location;
					btn1.Tag = DialogResult.OK;

					EnableButton(btn2);

					btn2.Text = "取消";
					btn2.Location = btn3.Location;
					btn2.Tag = DialogResult.Cancel;

					EnableButton(btn3, false);
					break;
				case MessageBoxButtons.RetryCancel:
					EnableButton(btn1);

					btn1.Text = "重试";
					btn1.Location = btn2.Location;
					btn1.Tag = DialogResult.Retry;

					EnableButton(btn2);

                    btn2.Text = "取消";
					btn2.Location = btn3.Location;
					btn2.Tag = DialogResult.Cancel;

					EnableButton(btn3, false);
					break;
				case MessageBoxButtons.YesNo:
					EnableButton(btn1);

					btn1.Text = "是";
					btn1.Location = btn2.Location;
					btn1.Tag = DialogResult.Yes;

					EnableButton(btn2);

					btn2.Text = "否";
					btn2.Location = btn3.Location;
					btn2.Tag = DialogResult.No;

					EnableButton(btn3, false);
					break;
				case MessageBoxButtons.YesNoCancel:
					EnableButton(btn1);

                    btn1.Text = "是";
					btn1.Tag = DialogResult.Yes;

					EnableButton(btn2);

                    btn2.Text = "否";
					btn2.Tag = DialogResult.No;

					EnableButton(btn3);

                    btn3.Text = "取消";
					btn3.Tag = DialogResult.Cancel;

					break;
				case MessageBoxButtons.AbortRetryIgnore:
					EnableButton(btn1);

					btn1.Text = "停止";
					btn1.Tag = DialogResult.Abort;

					EnableButton(btn2);

                    btn2.Text = "重试";
					btn2.Tag = DialogResult.Retry;

					EnableButton(btn3);

                    btn3.Text = "忽略";
					btn3.Tag = DialogResult.Ignore;

					break;
				default: break;
			}
		}

		private void EnableButton(Button button, bool enabled = true)
		{
			button.Enabled = enabled; 
			button.Visible = enabled;
		}

		public void SetTitelColor(MessageBoxIcon icon)
		{
			switch (icon)
			{
				case MessageBoxIcon.Error:
					this.TitleBackColor = _errorColor; break;
				case MessageBoxIcon.Warning:
					this.TitleBackColor = _warningColor; break;
				case MessageBoxIcon.Information:
					this.TitleBackColor = _defaultColor;
					break;
				case MessageBoxIcon.Question:
					this.TitleBackColor = _question; break;
				default:
					this.TitleBackColor = _success; break;
			}
		}

		public void SetDefaultButton(MessageBoxDefaultButton defaultbutton)
		{
			switch (defaultbutton)
			{
				case MessageBoxDefaultButton.Button1:
					if (btn1.Enabled)
					{
						AcceptButton = btn1;
						btn1.Focus();
					}
					
					break;
				case MessageBoxDefaultButton.Button2:
					if (btn2.Enabled)
					{
						AcceptButton = btn2;
						btn2.Focus();
					}
					
					break;
				case MessageBoxDefaultButton.Button3:
					if (btn3.Enabled)
					{
						AcceptButton = btn3;
						btn3.Focus();
					}
                    break;
                case (MessageBoxDefaultButton)99:
                    if (btn1.Enabled)
                    {
                        AcceptButton = btn1;
                        btn1.Focus();
                    }
                    btn1.Text = "确定";
					break;
				default: break;
			}
		}

		private void btn_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;

			this.DialogResult = (DialogResult)btn.Tag;
            if (btn.Text == "确定")
            {
                SystemLog.AlarmMessageCount = 0;
                //SideBarHighLight.UpdateUnreadMessageCount(0);
            }
			this.Close();
		}
	}
}
