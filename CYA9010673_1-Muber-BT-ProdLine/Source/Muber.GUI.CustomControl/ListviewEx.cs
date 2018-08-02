using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace Mubea.GUI.CustomControl
{
	public enum EditColumnStyle
	{
		IntTextBox = 1,
		DoubleTextBox = 4,
		StringTextBox = 5,
		CombBox = 6,
		DateTimePicker = 7
	}

    ///   <summary>   
    ///   CListView   的摘要说明。   
    ///   </summary>   
	public class ListViewEx : ListView
	{
		/// <summary>
		/// 列控件字典
		/// </summary>
		private Dictionary<int, Control> _colCtrlDic;

		/// <summary>
		/// 当前正在编辑的控件
		/// </summary>
		private Control _curEditCtrl = null;

		public ListViewEx()
		{
			this.GridLines = true;
			this.FullRowSelect = true;

			_colCtrlDic = new Dictionary<int, Control>();
		}


		/// <summary>
		/// 设置可编辑列类型
		/// </summary>
		/// <param name="index">列索引</param>
		/// <param name="colStyle">列类型</param>
		public bool SetEditColumnStyle(int index, EditColumnStyle colStyle)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				return false;
			}
			else
			{
				switch (colStyle)
				{
					case EditColumnStyle.StringTextBox:
						NormalCharTextBox txtBox = new NormalCharTextBox();
						InitTextBox(txtBox);

						_colCtrlDic.Add(index, txtBox);
						break;

					case EditColumnStyle.IntTextBox:
						IntTextBox txtIntBox = new IntTextBox();
						InitTextBox(txtIntBox);

						_colCtrlDic.Add(index, txtIntBox);
						break;

					case EditColumnStyle.DoubleTextBox:
						DoubleTextBox txtDoubleBox = new DoubleTextBox();
						InitTextBox(txtDoubleBox);

						_colCtrlDic.Add(index, txtDoubleBox);
						break;

					case EditColumnStyle.CombBox:
						ComboBox cmbBox = new ComboBox();
						cmbBox.Visible = false;
						cmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
						cmbBox.FlatStyle = FlatStyle.Standard;
						cmbBox.Leave += new EventHandler(combBox_Leave);
						this.Controls.Add(cmbBox);
						_colCtrlDic.Add(index, cmbBox);
						break;

					case EditColumnStyle.DateTimePicker:
						DateTimePicker dtp = new DateTimePicker();
						dtp.Format = DateTimePickerFormat.Custom;
						dtp.Visible = false;
						dtp.Leave += new EventHandler(dateTime_Leave);
						this.Controls.Add(dtp);
						_colCtrlDic.Add(index, dtp);
						break;

					default:
						break;
				}

				return true;
			}
		}

		/// <summary>
		/// 设置某列ComboBox控件的Items
		/// </summary>
		/// <param name="index">列索引</param>
		/// <param name="strItems">元素数组</param>
		/// <returns>是否设置成功</returns>
		public bool SetCombBoxColumnItems(int index, string[] strItems)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is ComboBox)
				{
					ComboBox cmb = _colCtrlDic[index] as ComboBox;
					cmb.Items.Clear();
					cmb.Items.AddRange(strItems);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 设置numberTextBox的范围
		/// </summary>
		/// <param name="index">列索引</param>
		/// <param name="minVal">最小值</param>
		/// <param name="maxVal">最大值</param>
		/// <returns>是否设置成功</returns>
		public bool SetTextBoxColumnRange(int index, double minVal, double maxVal)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is IntTextBox)
				{
					IntTextBox txtBox = _colCtrlDic[index] as IntTextBox;
					txtBox.SetRange((int)minVal, (int)maxVal);

					return true;
				}
				else if (_colCtrlDic[index] is DoubleTextBox)
				{
					DoubleTextBox txtBox = _colCtrlDic[index] as DoubleTextBox;
					txtBox.SetRange(minVal, maxVal);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 设置时间显示格式
		/// </summary>
		/// <param name="index">列号</param>
		/// <param name="format">格式</param>
		/// <returns>是否设置成功</returns>
		public bool SetDateTimeColumnFormat(int index, string format)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is DateTimePicker)
				{
					DateTimePicker dtp = _colCtrlDic[index] as DateTimePicker;
					dtp.Format = DateTimePickerFormat.Custom;

					dtp.CustomFormat = format;

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 设置最大输入字符长度
		/// </summary>
		/// <param name="index">列号</param>
		/// <param name="maxLen">最大输入长度</param>
		/// <returns></returns>
		public bool SetStringColumnLength(int index, uint maxLen)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is NormalCharTextBox)
				{
					NormalCharTextBox norTxtCtrl = _colCtrlDic[index] as NormalCharTextBox;

					norTxtCtrl.MaxInputLength = maxLen;

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 添加限制输入字符
		/// </summary>
		/// <param name="index">列号</param>
		/// <param name="c">限制字符</param>
		/// <returns></returns>
		public bool AddStringColumnUnallowedChar(int index, char c)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is NormalCharTextBox)
				{
					NormalCharTextBox norTxtCtrl = _colCtrlDic[index] as NormalCharTextBox;

					norTxtCtrl.AddUnallowedChar(c);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 删除限制输入字符
		/// </summary>
		/// <param name="index">列号</param>
		/// <param name="c">限制字符</param>
		/// <returns></returns>
		public bool RemoveStringColumnUnallowedChar(int index, char c)
		{
			Debug.Assert(index > -1 && index < this.Columns.Count);

			if (_colCtrlDic.ContainsKey(index))
			{
				if (_colCtrlDic[index] is NormalCharTextBox)
				{
					NormalCharTextBox norTxtCtrl = _colCtrlDic[index] as NormalCharTextBox;

					norTxtCtrl.RemoveUnallowedChar(c);

					return true;
				}
			}

			return false;
		}

		private void ShowTextBox(TextBox txtBox, ListViewItem.ListViewSubItem subItem, HorizontalAlignment align)
		{
			Rectangle rect = subItem.Bounds;

			txtBox.Bounds = new Rectangle(rect.Left + 1, rect.Top, rect.Width - 1, rect.Height - 1);
			txtBox.BorderStyle = BorderStyle.None;
			
			txtBox.Text = subItem.Text;
			txtBox.TextAlign = align;
			txtBox.Tag = subItem;

			txtBox.Visible = true;	
			txtBox.BringToFront();
			txtBox.Select();

			_curEditCtrl = txtBox;
		}

		private void ShowCombBox(ComboBox cmbBox, ListViewItem.ListViewSubItem subItem)
		{
			Rectangle rect = subItem.Bounds;
			cmbBox.Bounds = new Rectangle(rect.Left + 1, rect.Top, rect.Width - 1, rect.Height - 1);
			
			cmbBox.SelectedIndex = cmbBox.FindStringExact(subItem.Text);
			cmbBox.Tag = subItem;

			cmbBox.Visible = true;			
			cmbBox.BringToFront();
			cmbBox.Select();

			_curEditCtrl = cmbBox;
		}

		private void ShowDateTimePicker(DateTimePicker dtp, ListViewItem.ListViewSubItem subItem)
		{
			Rectangle rect = subItem.Bounds;
			dtp.Bounds = new Rectangle(rect.Left + 1, rect.Top, rect.Width - 1, rect.Height - 1);

			dtp.Tag = subItem;

			DateTime time;
			if (DateTime.TryParse(subItem.Text, out time))
			{
				dtp.Value = time;
			}
			else
			{
				dtp.Value = DateTime.Now;
			}
			
			dtp.Visible = true;
			dtp.BringToFront();
			dtp.Focus();

			_curEditCtrl = dtp;
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			foreach (Control ctrl in this.Controls)
			{
				ctrl.Visible = false;
			}

			base.OnSelectedIndexChanged(e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
            //If _colCtrlDic is null do nothing and return
            if (_colCtrlDic.Count < 1)
                return;

			Point tmpPoint = this.PointToClient(Cursor.Position);
			ListViewItem.ListViewSubItem subitem = this.HitTest(tmpPoint).SubItem;
			ListViewItem item = this.HitTest(tmpPoint).Item;

			if (subitem != null)
			{
				int index = item.SubItems.IndexOf(subitem);
				if (index != -1)
				{
					if (_colCtrlDic.ContainsKey(index))
					{
						Control ctrl = _colCtrlDic[index];

						if (ctrl is TextBox)
						{
							HorizontalAlignment align = this.Columns[index].TextAlign;
							ShowTextBox(ctrl as TextBox, subitem, align);
						}
						else if (ctrl is ComboBox)
						{
							ShowCombBox(ctrl as ComboBox, subitem);
						}
						else if (ctrl is DateTimePicker)
						{
							ShowDateTimePicker(ctrl as DateTimePicker, subitem);
						}
					}
				}
			}

			base.OnDoubleClick(e);
		}

		protected override void WndProc(ref Message m)
		{
			//msg=0x115   (WM_VSCROLL)     
			//msg=0x114   (WM_HSCROLL)   
			if (m.Msg == 0x115 || m.Msg == 0x114)
			{
				if (_curEditCtrl != null)
				{
					return;
				}
			}
			base.WndProc(ref m);
		}

		private void InitTextBox(TextBox txtBox)
		{
			txtBox.Multiline = true;
			txtBox.Visible = false;
			txtBox.Validating += new System.ComponentModel.CancelEventHandler(txtBox_Validating);

			this.Controls.Add(txtBox);
		}

		private void txtBox_Validating(object sender, CancelEventArgs e)
		{
			if (!e.Cancel)
			{
				TextBox txtBox = sender as TextBox;

				txtBox.Visible = false;
				(txtBox.Tag as ListViewItem.ListViewSubItem).Text = txtBox.Text;

				_curEditCtrl = null;
			}
			else
			{
			}
		}

		private void combBox_Leave(object sender, EventArgs e)
		{
			ComboBox cmbBox = sender as ComboBox;

			cmbBox.Visible = false;
			(cmbBox.Tag as ListViewItem.ListViewSubItem).Text = cmbBox.Text;
			cmbBox.SelectedIndex = -1;

			_curEditCtrl = null;
		}

		private void dateTime_Leave(object sender, EventArgs e)
		{
			DateTimePicker dtp = sender as DateTimePicker;
			dtp.Visible = false;
			(dtp.Tag as ListViewItem.ListViewSubItem).Text = dtp.Value.ToString(dtp.CustomFormat);

			_curEditCtrl = null;
		}

        public void ClearDictionary()
        {
            _colCtrlDic.Clear();
        }
	}
}
