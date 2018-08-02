using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Drawing;

namespace Mubea.AutoTest.GUI
{
	public static class SpMessageBox
	{

        public static SpMessageBoxDlg msgDlg_Modeless;

		/// <summary>
		/// Shows a metro-styles message notification into the specified owner window.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static DialogResult Show(String message)
		{ 
			return Show(message, ""); 
		}

		/// <summary>
		/// Shows a metro-styles message notification into the specified owner window.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		public static DialogResult Show(String message, String title)
		{ 
			return Show(message, title, MessageBoxButtons.OK);
		}

		/// <summary>
		/// Shows a metro-styles message notification into the specified owner window.
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="buttons"></param>
		/// <returns></returns>
		public static DialogResult Show(String message, String title, MessageBoxButtons buttons)
		{
			return Show(message, title, buttons, MessageBoxIcon.None); 
		}

		/// <summary>
		/// Shows a metro-styles message notification into the specified owner window.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="buttons"></param>
		/// <param name="icon"></param>
		/// <returns></returns>
		public static DialogResult Show(String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon)
		{ 
			return Show(message, title, buttons, icon, MessageBoxDefaultButton.Button1); 
		}

		/// <summary>
		/// Shows a metro-styles message notification into the specified owner window.
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="message"></param>
		/// <param name="title"></param>
		/// <param name="buttons"></param>
		/// <param name="icon"></param>
		/// <param name="defaultbutton"></param>
		/// <returns></returns>
		public static DialogResult Show(String message, String title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
		{
			DialogResult _result = DialogResult.None;

			switch (icon)
			{
				case MessageBoxIcon.Error:
					SystemSounds.Hand.Play(); 
					break;
				case MessageBoxIcon.Exclamation:
					SystemSounds.Exclamation.Play(); 
					break;
				case MessageBoxIcon.Question:
					SystemSounds.Beep.Play(); 
					break;
				default:
					SystemSounds.Asterisk.Play(); 
					break;
			}

			SpMessageBoxDlg msgDlg = new SpMessageBoxDlg();
			msgDlg.ArrangeApperance(message, title, buttons, icon, defaultbutton);
            _result = msgDlg.ShowDialog();
			msgDlg.BringToFront();
			return _result;
		}


        public static void ShowModelessDialog(String message, String title)
        {
            SystemSounds.Asterisk.Play();

            if (msgDlg_Modeless == null) //目前无告警提示框存在
            {
                msgDlg_Modeless = new SpMessageBoxDlg();
                msgDlg_Modeless.ArrangeApperanceModeless(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, (MessageBoxDefaultButton)99);

                msgDlg_Modeless.Show();
                msgDlg_Modeless.BringToFront();
            }
            else if (msgDlg_Modeless.IsDisposed) //告警提示框 disposed
            {
                msgDlg_Modeless = new SpMessageBoxDlg();
                msgDlg_Modeless.ArrangeApperanceModeless(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error, (MessageBoxDefaultButton)99);
            
                msgDlg_Modeless.Show();
                msgDlg_Modeless.BringToFront();
            }
            else  //当前有告警提示框存在，告警信息需要累加上去
            {
                if (message!=string.Empty)
                    msgDlg_Modeless.UpdateDlgMessage(message);
                msgDlg_Modeless.Show();
                msgDlg_Modeless.BringToFront();
            }
            return;
        }

        public static void ShowInputDialog(String title, out string inputString, bool isPassword, string message = "用户名:",  string defaultText = "")
        {
            SpMessageInputBox msgDlg_InputBox = new SpMessageInputBox();
            msgDlg_InputBox.SetInputTextFormat(isPassword, message, defaultText);
            msgDlg_InputBox.ArrangeApperance("", title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
           
            msgDlg_InputBox.ShowDialog();
            inputString = msgDlg_InputBox.GetPasswordValue();
            return;
        }
	}
}
