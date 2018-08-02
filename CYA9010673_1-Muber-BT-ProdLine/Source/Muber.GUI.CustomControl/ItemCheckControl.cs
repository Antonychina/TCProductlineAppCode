using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Mubea.GUI.CustomControl
{
	public partial class ItemCheckControl : BaseUserCtrl
	{
		private int _columnNum = 8;
		[DefaultValue(8)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public int ColumnNum
		{
			get 
			{
				if (_columnNum < 1)
				{
					return 1;
				}

				return _columnNum; 
			}
			set 
			{
				if (value < 2)
				{
					_columnNum = 2;
				}
				else
				{
					_columnNum = value;
				}
			}
		}

		private int _itemCount = 40;

		[Browsable(false)]
		[Category("Custom Appearance")]
		public int ItemWidth { get; private set; }

		[DefaultValue(70)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public int ItemHeight { get; set; }

		[DefaultValue(false)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public bool IsShowCalibStatus { get; set; }

		[DefaultValue(null)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Image CalibrateImage { get; set; }

		[DefaultValue(null)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Image UnCalibrateImage { get; set; }

		[DefaultValue("Green")]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Color SelectedColor { get; set; }

		[DefaultValue("SkyBlue")]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Color DownloadColor { get; set; }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}

		public ItemCheckControl()
		{
			ColumnNum = 8;
			ItemHeight = 70;
			IsShowCalibStatus = false;

			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();

			this.AutoScroll = true;
			MultiSelect = true;
		}

		#region public Methods
		public void InitCtrl(List<string> itemArr)
		{
			int rowCount = (itemArr.Count - 1) / ColumnNum + 1;
			_itemCount = rowCount * ColumnNum;

			int ctrlHeight = rowCount * ItemHeight;
			this.AutoScrollMinSize = new Size(0, ctrlHeight);

			ItemWidth = this.ClientRectangle.Width / ColumnNum;

			_gpBtnArr.Clear();

			int col = 0;
			int row = 0;
			for (int i = 0; i < _itemCount; i++)
			{
				col = i % ColumnNum;
				row = i / ColumnNum;

				RectangleF rect = new RectangleF();
				rect.X = col * ItemWidth;
				rect.Width = ItemWidth;
				rect.Y = row * ItemHeight;
				rect.Height = ItemHeight;
				rect.Inflate(-5, -5);

				ItemButton gpBtn = new ItemButton();

				gpBtn.BtnRect = rect;

				rect.Inflate(-15, -15);
				rect.Width = rect.Height;
				rect.Offset(-5, 0);
				gpBtn.CalibImageRect = rect;

				gpBtn.BtnPath = new GraphicsPath();
				gpBtn.BtnPath.AddRectangle(gpBtn.BtnRect);

				gpBtn.BtnRegion = new Region(gpBtn.BtnPath);

				if (i < itemArr.Count)
				{
					gpBtn.Text = itemArr[i];
					gpBtn.Enabled = true;
				}
				else
				{
					gpBtn.Enabled = false;
				}

				_gpBtnArr.Add(gpBtn);
			}
		}

		public void SetItemText(int index, string text)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			_gpBtnArr[index].Text = text;

			this.Invalidate();
		}

		public void SetItemCheckState(int index, bool bChecked)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			_gpBtnArr[index].Selected = bChecked;

			this.Invalidate();
		}

		public void SetItemCheckState(List<string> itemArr, bool bChecked)
		{
			int index = -1;
			foreach (var val in itemArr)
			{
				index = _gpBtnArr.FindIndex(r => r.Text == val);
				if (index != -1)
				{
					_gpBtnArr[index].Selected = bChecked;
				}
			}

			this.Invalidate();
		}

        public void SetItemCheckState(string item, bool bChecked)
        {
            int index = _gpBtnArr.FindIndex(r => r.Text == item);
            if (index != -1)
            {
                _gpBtnArr[index].Selected = bChecked;
            }

            this.Invalidate();
        }

		public void SetItemCheckState(bool bChecked)
		{
			foreach (var val in _gpBtnArr)
			{
				val.Selected = bChecked;
			}

			this.Invalidate();
		}

		public void SetItemDownloadState(int index, bool download)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			ItemButton itemBtn = _gpBtnArr[index] as ItemButton;
			itemBtn.Download = download;

			this.Invalidate();
		}

		public void SetItemDownloadState(List<string> itemArr, bool download)
		{
			int index = -1;
			foreach (var val in itemArr)
			{
				index = _gpBtnArr.FindIndex(r => r.Text == val);
				if (index != -1)
				{
					ItemButton itemBtn = _gpBtnArr[index] as ItemButton;
					itemBtn.Download = download;
				}
			}

			this.Invalidate();
		}

		public void SetItemDownloadState(bool download)
		{
			foreach (var val in _gpBtnArr)
			{
				ItemButton itemBtn = val as ItemButton;
				itemBtn.Download = download;
			}

			this.Invalidate();
		}

		public void SetItemCalibState(int index, bool bCalibrated)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			ItemButton itemBtn = _gpBtnArr[index] as ItemButton;
			itemBtn.IsCalibrated = bCalibrated;

			this.Invalidate();
		}

		public void SetItemCalibState(List<string> itemArr, bool bCalibrated)
		{
			int index = -1;
			foreach (var val in itemArr)
			{
				index = _gpBtnArr.FindIndex(r => r.Text == val);
				if (index != -1)
				{
					ItemButton itemBtn = _gpBtnArr[index] as ItemButton;
					itemBtn.IsCalibrated = bCalibrated;
				}
			}
		}

		public void SetItemCalibState(bool bCalibrated)
		{
			foreach (var val in _gpBtnArr)
			{
				ItemButton itemBtn = val as ItemButton;
				itemBtn.IsCalibrated = bCalibrated;
			}

			this.Invalidate();
		}

		public void EnableItem(int index, bool bEnable)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			_gpBtnArr[index].Enabled = bEnable;

			this.Invalidate();
		}


		public void EnableItem(List<string> itemArr, bool bEnable)
		{
			int index = -1;
			foreach (var val in itemArr)
			{
				index = _gpBtnArr.FindIndex(r => r.Text == val);
				if (index != -1)
				{
					_gpBtnArr[index].Enabled = bEnable;
				}
			}

			this.Invalidate();
		}

		public void EnableItem(bool bEnable)
		{
			foreach (var val in _gpBtnArr)
			{
				val.Enabled = bEnable;
				val.Selected = false;
			}

			this.Invalidate();
		}

		public List<string> GetCheckedItem()
		{
			List<string> itemArr = new List<string>();

			foreach (var val in _gpBtnArr)
			{
				ItemButton itemBtn = val as ItemButton;
				if (itemBtn.Selected && !itemBtn.Download && itemBtn.Enabled)
				{
					itemArr.Add(val.Text);
				}
			}

			return itemArr;
		}

        public List<int> GetCheckedItemID()
        {
            List<int> itemArr = new List<int>();

            for (int i = 0; i < _gpBtnArr.Count; i++)
            {
                if (_gpBtnArr[i].Selected)
                {
                    itemArr.Add(i);
                }
            }
            return itemArr;
        }

		public List<string> GetDownloadItem()
		{
			List<string> itemArr = new List<string>();

			foreach (var val in _gpBtnArr)
			{
				ItemButton itemBtn = val as ItemButton;
				if (itemBtn.Download)
				{
					itemArr.Add(val.Text);
				}
			}

			return itemArr;
		}

		public int GetCheckedItemCount()
		{
			int count = 0;
			foreach (var val in _gpBtnArr)
			{
				if (val.Selected)
				{
					count++;
				}
			}

			return count;
		}

		public string GetItem(int index)
		{
			Debug.Assert(index > -1 && index < _gpBtnArr.Count);

			return _gpBtnArr[index].Text;
		}

		public void Clear()
		{
			_gpBtnArr.ForEach(r =>
				{
					ItemButton itemBtn = r as ItemButton;
					itemBtn.Selected = false;
					itemBtn.Download = false;
				});
            this.Invalidate();
		}

		#endregion
		
		#region Protected Overridden Methods
		
		protected override void OnScroll(ScrollEventArgs se)
		{
			this.Invalidate();

			base.OnScroll(se);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.Invalidate();

			base.OnMouseWheel(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;

			g.Clear(this.BackColor);

			Color bkColor = Color.White;
			if (!this.Enabled)
			{
				bkColor = Color.FromArgb(244, 244, 244);
			}
			using (SolidBrush backBsh = new SolidBrush(bkColor))
			{
				g.FillRectangle(backBsh, this.ClientRectangle);
			}

			using (Pen pen = new Pen(Color.Black))
			{
				g.DrawRectangle(pen, this.ClientRectangle);
			}

			g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

			if (_gpBtnArr.Count > 0)
			{
				for (int i = 0; i < _gpBtnArr.Count; i++)
				{
					ItemButton itemBtn = _gpBtnArr[i] as ItemButton;

					Color txtColor = Color.Black;
					Rectangle txtRect = Rectangle.Round(itemBtn.BtnRect);
					txtRect.Offset(this.AutoScrollPosition);

					Pen borderPen = new Pen(Color.Black,1f);

					if (itemBtn.Enabled)
					{
						if (itemBtn.Download)
						{
							txtColor = BackColor;

							using (SolidBrush btnbsh = new SolidBrush(DownloadColor))
							{
								g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);
							}
						}
						else
						{
							if (itemBtn.Selected)
							{
								txtColor = BackColor;

								using (SolidBrush btnbsh = new SolidBrush(SelectedColor))
								{
									g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);
								}
							}
							else if (_gpBtnArr[i].MouseHover)
							{
								borderPen.Color = Color.Green;
								borderPen.Width = 2f;
							}
						}
						
					}
					else
					{
						using (SolidBrush btnbsh = new SolidBrush(Color.LightGray))
						{
							g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);
						}
					}

					CommonGraphic.DrawRectangle(g, borderPen,
												_gpBtnArr[i].BtnRect.X,
												_gpBtnArr[i].BtnRect.Y,
												_gpBtnArr[i].BtnRect.Width,
												_gpBtnArr[i].BtnRect.Height);

					if (IsShowCalibStatus)
					{
						if (itemBtn.IsCalibrated && CalibrateImage != null)
						{
							g.DrawImage(CalibrateImage, itemBtn.CalibImageRect);
						}
						else if (!itemBtn.IsCalibrated && UnCalibrateImage != null)
						{
							g.DrawImage(UnCalibrateImage, itemBtn.CalibImageRect);
						}
					}

					TextRenderer.DrawText(g,
										_gpBtnArr[i].Text,
										this.Font,
										txtRect,
										txtColor,
										TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

				}
			}

			g.ResetTransform();
		}

		#endregion Protected Overridden Methods
	}

	internal class ItemButton : GraphicButton
	{
		private bool _bCalibrated = false;
		public bool IsCalibrated
		{
			get { return _bCalibrated; }
			set 
			{ 
				_bCalibrated = value;
				_bEnable = value;
			}
		}

		public RectangleF CalibImageRect { get; set; }

		private bool _bDownload = false;
		public bool Download
		{
			get { return _bDownload; }
			set { _bDownload = value; }
		}
	}
}
