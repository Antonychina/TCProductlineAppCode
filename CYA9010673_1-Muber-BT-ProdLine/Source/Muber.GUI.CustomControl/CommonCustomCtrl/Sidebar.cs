using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Mubea.GUI.CustomControl
{
	public partial class Sidebar : BaseCustomCtrl
	{
		private GraphicsPath _totalPath;
		private Rectangle _timeRect;
		private Dictionary<int, byte> _highLightDic;
		private Font _numFont;
		private System.Timers.Timer _timer;

		/// <summary>
		/// 选中后的显示颜色
		/// </summary>
		public Color SelectedColor { get; set; }

		/// <summary>
		/// 是否显示系统时间
		/// </summary>
		public bool EnableDisplayTime { get; set; }

		/// <summary>
		/// 系统时间显示格式
		/// </summary>
		public string TimeFormat { get; set; }

		/// <summary>
		/// 显示时间字体
		/// </summary>
		public Font TimeFont { get; set; }

		public Sidebar()
		{
			EnableCancelSelectSelf = false;

			_gpBtnArr = new List<GraphicButton>();

			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();

			this.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.TimeFont = new System.Drawing.Font("YaHei Consolas Hybrid", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

			_highLightDic = new Dictionary<int, byte>();
			_numFont = new Font("YaHei Consolas Hybrid", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

			_timer = new System.Timers.Timer(5000);

			TimeFormat = "yyyy/MM/dd HH:mm";
		}

		public void InitCtrl(List<TitleBtnItem> items)
		{
			//SelectedColor = Color.FromArgb(0, 0, 64);

            SelectedColor = Color.FromArgb(0, 0, 84);

			_gpBtnArr.Clear();

			foreach (var item in items)
			{
				GraphicButton gpBtn = new GraphicButton();
				gpBtn.Text = item.Text;
				gpBtn.BackgroundImage = item.BackgroundImage;
				gpBtn.SelectedBKImage = item.SelectedBKImage;

				_gpBtnArr.Add(gpBtn);
			}

			if (_gpBtnArr.Count > 0)
			{
				float width = (float)ClientRectangle.Width;
				float height = width/3f;
			
				for (int i = 0; i < _gpBtnArr.Count; i++)
				{
					RectangleF rect = new RectangleF();
					rect.X = 0;
					rect.Width = width;
					rect.Y = i * height;
					rect.Height = height;

					_gpBtnArr[i].BtnRect = rect;

					_gpBtnArr[i].BtnPath = new GraphicsPath();
					_gpBtnArr[i].BtnPath.AddRectangle(rect);

					_gpBtnArr[i].BtnRegion = new Region(_gpBtnArr[i].BtnPath);
				}
			}

			if (EnableDisplayTime)
			{
				_timeRect = new Rectangle(0, this.Height - 80, this.Width, 80);

				_timer.Elapsed += OntimerElapsed;
				_timer.AutoReset = true;
				_timer.Enabled = true;
			}
		}

		public void SetSidebarSelected(int selIndex)
		{
			if(selIndex > -1 && selIndex < _gpBtnArr.Count)
			{
				if (this.SelectedIndex != selIndex)
				{
					if (this.SelectedIndex != -1)
					{
						_gpBtnArr[this.SelectedIndex].Selected = false;
					}

					this.SelectedIndex = selIndex;
					_gpBtnArr[this.SelectedIndex].Selected = true;

				}
				
			}
			
		}

		/// <summary>
		/// 设置高亮显示的数字
		/// </summary>
		/// <param name="index">按钮索引</param>
		/// <param name="num">显示数字</param>
		/// <returns></returns>
		public bool SetHighLightNumber(int index, byte num)
		{
			if (index > -1 && index < _gpBtnArr.Count)
			{
				if (_highLightDic.ContainsKey(index))
				{
					_highLightDic[index] = num;
				}
				else
				{
					_highLightDic.Add(index, num);
				}

				this.Invalidate();

				return true;
			}

			return false;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;

			g.Clear(this.BackColor);

			Rectangle rect = ClientRectangle;

			if (_totalPath == null)
			{
				_totalPath = new GraphicsPath();

				_totalPath.Reset();
				_totalPath.AddRectangle(ClientRectangle);
			}

			SolidBrush bsh = new SolidBrush(BackColor);
			g.FillPath(bsh, _totalPath);

			if (_gpBtnArr.Count > 0)
			{
				Pen splitPenUp = new Pen(CommonGraphic.ColorOffset(BackColor, -0.3f), 1.5f);
				Pen splitPenDown = new Pen(CommonGraphic.ColorOffset(BackColor, 0.2f), 1.5f);

				Image bkImage = null;

				for (int i = 0; i < _gpBtnArr.Count; i++)
				{
					Color txtColor = Color.White;
					Rectangle txtRect = Rectangle.Round(_gpBtnArr[i].BtnRect);

					bkImage = _gpBtnArr[i].BackgroundImage;

					if (_gpBtnArr[i].Selected)
					{
                        txtColor = Color.White;

						if (_gpBtnArr[i].SelectedBKImage != null)
						{
							bkImage = _gpBtnArr[i].SelectedBKImage;
						}

						SolidBrush btnbsh = new SolidBrush(SelectedColor);
						g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);

						Color spiltLine = CommonGraphic.ColorOffset(SelectedColor, -0.3f);

						Pen linePen = new Pen(spiltLine);
						g.DrawLine(linePen,
							_gpBtnArr[i].BtnRect.Right,
							_gpBtnArr[i].BtnRect.Top + 10.0f,
							_gpBtnArr[i].BtnRect.Right,
							_gpBtnArr[i].BtnRect.Bottom - 10.0f);
					}
					else if (_gpBtnArr[i].MouseHover)
					{
						txtRect.Inflate(-5, -5);
						txtRect.Offset(5, 5);
					}

					if (i != this.SelectedIndex - 1
						&& i != this.SelectedIndex)
					{
						CommonGraphic.DrawLine(g, splitPenUp,
							_gpBtnArr[i].BtnRect.Left + 2f,
							_gpBtnArr[i].BtnRect.Bottom,
							_gpBtnArr[i].BtnRect.Right - 2f,
							_gpBtnArr[i].BtnRect.Bottom);

						CommonGraphic.DrawLine(g, splitPenDown,
							_gpBtnArr[i].BtnRect.Left + 2f,
							_gpBtnArr[i].BtnRect.Bottom + 1,
							_gpBtnArr[i].BtnRect.Right - 2f,
							_gpBtnArr[i].BtnRect.Bottom + 1);
					}

					if (bkImage != null)
					{
						float height = _gpBtnArr[i].BtnRect.Height * 0.5f;
						float width = height * bkImage.Width / bkImage.Height;

						g.DrawImage(bkImage,
									_gpBtnArr[i].BtnRect.Left + (_gpBtnArr[i].BtnRect.Height - width) / 2.0f, 
									_gpBtnArr[i].BtnRect.Top + (_gpBtnArr[i].BtnRect.Height - height) / 2.0f, 
									width, height);

						txtRect.Offset((int)_gpBtnArr[i].BtnRect.Height, 0);
						txtRect.Width -= (int)_gpBtnArr[i].BtnRect.Height;
					}

					TextRenderer.DrawText(g,
										_gpBtnArr[i].Text,
										this.Font,
										txtRect,
										txtColor,
										/*TextFormatFlags.HorizontalCenter | */TextFormatFlags.VerticalCenter);

					if (_highLightDic.ContainsKey(i))
					{
						if (_highLightDic[i] > 0)
						{
							Rectangle numRect = new Rectangle();
							numRect.X = (int)_gpBtnArr[i].BtnRect.Right - 35;
							numRect.Y = (int)_gpBtnArr[i].BtnRect.Top + (int)_gpBtnArr[i].BtnRect.Height / 2 - 15;
							numRect.Width = 30;
							numRect.Height = 30;

							bsh.Color = Color.Red;
							g.FillEllipse(bsh, numRect);

							numRect.Offset(0, -2);
							TextRenderer.DrawText(g,
										_highLightDic[i].ToString(),
										_numFont,
										numRect,
										Color.White,
										TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
						}
					}
				}

				if (EnableDisplayTime)
				{
					TextRenderer.DrawText(g,
									DateTime.Now.ToString(TimeFormat),
									this.TimeFont,
									_timeRect,
									Color.White,
									TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				}

			}

		}

		private void OntimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (this.IsHandleCreated)
			{
				this.BeginInvoke((EventHandler)(delegate { this.Invalidate(_timeRect); }));
			}
		}
	}
}
