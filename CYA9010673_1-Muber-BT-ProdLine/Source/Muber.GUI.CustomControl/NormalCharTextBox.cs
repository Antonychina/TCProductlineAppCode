using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mubea.GUI.CustomControl
{
	public class NormalCharTextBox : TextBox
	{
		private const int WM_PASTEDATA = 0x0302;
		private const int WM_CHAR = 0x0102;

		private HashSet<char> _unallowedCharSet;
		private uint _maxInputLength;
		private ToolTip _tip;

		/// <summary>
		/// MaxInputLength
		/// </summary>
		[Category("Custom")]
		[Description("Set/Get Max Input Length.")]
		[DefaultValue(30)]
		public uint MaxInputLength
		{
			get { return _maxInputLength; }
			set { _maxInputLength = value; }
		}

		#region public method

		/// <summary>
		/// constructor
		/// </summary>
		public NormalCharTextBox()
		{
			_unallowedCharSet = new HashSet<char>() { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '|', '/', '?' };
			_maxInputLength = 30;

			_tip = new ToolTip();
		}

		/// <summary>
		/// Add UnallowedChar
		/// </summary>
		/// <param name="c"></param>
		public void AddUnallowedChar(char c)
		{
			_unallowedCharSet.Add(c);
		}

		/// <summary>
		/// Remove UnallowedChar
		/// </summary>
		/// <param name="c"></param>
		public void RemoveUnallowedChar(char c)
		{
			_unallowedCharSet.Remove(c);
		}

		#endregion public method

		#region override method

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			if (_unallowedCharSet.Contains(e.KeyChar))
			{
				e.Handled = true;

				ShowToolTip("不允许输入特殊字符！");

				return;
			}
			else if (e.KeyChar == 0x08)		//'b' 退格键(按delete键不会进入OnKeyPress消息，所以这里不考虑)
			{

			}
			else
			{
				string proText = this.Text.Remove(this.SelectionStart, this.SelectionLength);

				proText = proText.Insert(this.SelectionStart, e.KeyChar.ToString());

				if (proText.Length > MaxInputLength)
				{
					e.Handled = true;

					string sTip = string.Format("输入字符个数不能超过{0}个！", MaxInputLength);
					ShowToolTip(sTip);

					return;
				}
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys)Shortcut.CtrlV)
			{
				SendCharFromClipboard();		//把粘贴板每个字分拆，发送给自己;
				return true;
			}
			else if (keyData == (Keys)Shortcut.CtrlC)
			{
				Clipboard.SetText(this.SelectedText);
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		//捕捉粘贴板数据;
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_PASTEDATA)			//包括Ctrl+V, Shift+Ins和鼠标右键的粘贴;
			{
				SendCharFromClipboard();		//把粘贴板每个字分拆，发送给自己;
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		#endregion override method

		/// <summary>
		/// 显示报警提示
		/// </summary>
		/// <param name="sTip"></param>
		private void ShowToolTip(string sTip)
		{
			System.Drawing.Point pt = new System.Drawing.Point(0, 0);
			pt.Offset(0, this.Height);

			_tip.UseFading = true;		//逐渐消失效果
			_tip.RemoveAll();

			_tip.ForeColor = System.Drawing.Color.Red;
			_tip.Show(sTip, this, pt, 1000);
		}

		/// <summary>
		/// 把粘贴板每个字分拆，发送给自己
		/// </summary>
		private void SendCharFromClipboard()
		{
			foreach (char c in Clipboard.GetText())
			{
				Message msg = new Message();
				msg.HWnd = Handle;
				msg.Msg = WM_CHAR;
				msg.WParam = (IntPtr)c;
				msg.LParam = IntPtr.Zero;
				base.WndProc(ref msg);
			}
		}
	}
}
