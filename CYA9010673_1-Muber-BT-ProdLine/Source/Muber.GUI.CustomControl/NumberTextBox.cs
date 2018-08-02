using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Mubea.GUI.CustomControl
{
	public abstract class NumberTextBox<T> : TextBox
		where T : struct
	{
		[Category("Custom")]
		[Description("Set/Get Max Vaule.")]
		[DefaultValue(0)]
		public T MaxValue { get; set; }

		[Category("Custom")]
		[Description("Set/Get Min Vaule.")]
		[DefaultValue(0)]
		public T MinValue { get; set; }

		private ToolTip _tip = new ToolTip();

		public NumberTextBox()
		{
			this.Text = "0";

			if (typeof(T) != typeof(Int32)
				&& typeof(T) != typeof(Int64)
				&& typeof(T) != typeof(Double)
				&& typeof(T) != typeof(Single))
			{
				throw new Exception("Invalid Type");
			}
		}

		public void SetRange(T minValue, T maxValue)
		{
			MinValue = minValue;
			MaxValue = maxValue;
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			if (e.KeyChar == '-')
			{
				string input = this.Text.Substring(0, this.SelectionStart);
				if (input.Length != 0)	//负号位置不在首位;
				{
					e.Handled = true;
					return;
				}
			}
			else if (e.KeyChar == 0x08)		//'b' 退格键(按delete键不会进入OnKeyPress消息，所以这里不考虑)
			{

			}
			else if (e.KeyChar == '.')
			{
				if(typeof(T) == typeof(double)
					|| typeof(T) == typeof(float))
				{
					string pattern = @"^-?\d+$";

					string input = this.Text.Substring(0, this.SelectionStart);
					if (!Regex.IsMatch(input, pattern))
					{
						e.Handled = true;
						return;
					}
				}
				else
				{
					e.Handled = true;
					return;
				}
			}
			else if (!char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
				return;
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
                if (this.SelectedText == string.Empty)
                    return false;
				Clipboard.SetText(this.SelectedText);
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		const int WM_PASTEDATA = 0x0302;
		const int WM_CHAR = 0x0102;
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

		//把粘贴板每个字分拆，发送给自己;
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

		protected override void OnValidating(CancelEventArgs e)
		{
			if (!IsValid())
			{
				e.Cancel = true;

				this.SelectAll();

				ShowToolTip("请输入有效的数字！");
			}
			else if (!CheckRange())
			{
				e.Cancel = true;

				this.SelectAll();

				string sTip = string.Format("请输入{0}～{1}之间的数!", MinValue, MaxValue);
				ShowToolTip(sTip);
			}

			base.OnValidating(e);
		}

		protected bool IsValid()
		{
			string pattern = "";

			if(typeof(T) == typeof(double)
				|| typeof(T) == typeof(float))
			{
				pattern = @"^(-?\d+)(\.\d+)?$";
			}
			else
			{
				pattern = @"^-?\d+$";
			}
            if (this.Text == "")
                return true;
            else
			    return Regex.IsMatch(this.Text, pattern);				
		}

		protected virtual bool CheckRange()
		{
			return true;
		}

		private void ShowToolTip(string sTip)
		{
// 			Graphics graphics = CreateGraphics();
// 			SizeF sizeF = graphics.MeasureString(this.Text, this.Font);  

			Point pt = new Point(0, 0);
			pt.Offset(0, this.Height);

// 			if (this.TextAlign == HorizontalAlignment.Left)
// 			{
// 				pt.Offset((int)sizeF.Width - 20, -this.Size.Height - 10);
// 			}
// 			else if (this.TextAlign == HorizontalAlignment.Center)
// 			{
// 				int offsetX = this.Size.Width / 2 + (int)(sizeF.Width / 2) - 20;
// 				pt.Offset(offsetX, -this.Size.Height - 10);
// 			}
// 			else
// 			{
// 				int offsetX = this.Size.Width - (int)(sizeF.Width) - 20;
// 				pt.Offset(offsetX, -this.Size.Height - 10);
// 			}

// 			_tip.IsBalloon = true;
// 			_tip.UseAnimation = true;
			_tip.UseFading = true;
			//tip.AutoPopDelay = 1000;
			_tip.RemoveAll();
			//_tip.ToolTipTitle = "超范围提示";
			//_tip.ToolTipIcon = ToolTipIcon.Warning;

			_tip.Show(sTip, this, pt, 1000);
		}
	}

	public class IntTextBox : NumberTextBox<int>
	{
		public IntTextBox()
		{
			MaxValue = int.MaxValue;
			MinValue = int.MinValue;
		}

		[Category("Custom")]
		[Description("Get Int Vaule.")]
		public int Value
		{
			get 
			{
				return int.Parse(this.Text);
			}
		}

		protected override bool CheckRange()
		{
			if (this.Text.Length == 0)
			{
				return true;
			}

			int val = int.Parse(this.Text);

			if(val < MinValue
				|| val > MaxValue)
			{
				return false;
			}

			return true;
		}
	}

	public class DoubleTextBox : NumberTextBox<double>
	{
		public DoubleTextBox()
		{
			MaxValue = double.MaxValue;
			MinValue = double.MinValue;
		}

		[Category("Custom")]
		[Description("Get double Vaule.")]
		public double Value
		{
			get
			{
				return double.Parse(this.Text);
			}
		}

		protected override bool CheckRange()
		{
			if (this.Text.Length == 0)
			{
				return true;
			}

			double val = double.Parse(this.Text);

			if (val < MinValue
				|| val > MaxValue)
			{
				return false;
			}

			return true;
		}
	}
}
