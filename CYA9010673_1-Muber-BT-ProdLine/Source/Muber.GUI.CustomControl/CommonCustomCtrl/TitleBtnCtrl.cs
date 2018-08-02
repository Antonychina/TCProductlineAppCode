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
	public struct TitleBtnItem
	{
		/// <summary>
		/// 显示文字
		/// </summary>
		public string Text;

		/// <summary>
		/// 背景图片
		/// </summary>
		public Image BackgroundImage;

		/// <summary>
		/// 选中后的背景图片
		/// </summary>
		public Image SelectedBKImage;
	}

	public class TitleBtnCtrl : BaseCustomCtrl
	{
		private const float OffsetX = 20f;
		private const float OffsetY = 20f;

		private GraphicsPath _totalPath;

		private enum ButtonPos
		{
			First = 0,
			Middle,
			Last
		}

		public TitleBtnCtrl()
		{
			_gpBtnArr = new List<GraphicButton>();

			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();

			this.ForeColor = Color.White;
            //this.ForeColor = Color.FromArgb(0, 0, 64);
			this.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.ForeColor = Color.FromArgb(0, 0, 64);
            //this.BackColor = Color.White;
		}

		public void InitCtrl(List<TitleBtnItem> items)
		{
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
				float width = (float)ClientRectangle.Width / _gpBtnArr.Count;
			
				for (int i = 0; i < _gpBtnArr.Count; i++)
				{
					RectangleF rect = new RectangleF();
					rect.X = i * width;
					rect.Width = width;
					rect.Y = (float)ClientRectangle.Top;
					rect.Height = (float)ClientRectangle.Height;

					_gpBtnArr[i].BtnRect = rect;

					_gpBtnArr[i].BtnPath = new GraphicsPath();
					_gpBtnArr[i].BtnPath.AddRectangle(rect);

					_gpBtnArr[i].BtnRegion = new Region(_gpBtnArr[i].BtnPath);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
// 			BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
// 			BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
// 			Graphics g = myBuffer.Graphics;

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;


			g.Clear(this.BackColor); 

			Rectangle rect = ClientRectangle;

			if (_totalPath == null)
			{
				_totalPath = CommonGraphic.CreateRoundRectPath(rect, OffsetX, OffsetY); 
			}
         
			Color colorDown = BackColor;
			Color colorUp = BackColor;
			
			//绘制控件背景色
			SolidBrush lgb = new SolidBrush(BackColor);
			g.FillPath(lgb, _totalPath);

			if (_gpBtnArr.Count > 0)
			{
				Pen splitPenMain = new Pen(CommonGraphic.ColorOffset(BackColor, -0.2f), 1f);
				Pen splitPenSub = new Pen(CommonGraphic.ColorOffset(BackColor, 0.2f), 1f);

				Color pressedColor = CommonGraphic.ColorOffset(BackColor, -0.2f);

				Image bkImage = null;
				
				for (int i = 0; i < _gpBtnArr.Count; i++)
				{
					bkImage = _gpBtnArr[i].BackgroundImage;

					if (_gpBtnArr[i].Pressed)
					{
						using (SolidBrush btnbsh = new SolidBrush(pressedColor))
						{
							g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);
						}

						if (_gpBtnArr[i].SelectedBKImage != null)
						{
							bkImage = _gpBtnArr[i].SelectedBKImage;
						}
					}
					else if (_gpBtnArr[i].MouseHover)
					{
						using (SolidBrush btnbsh = new SolidBrush(CommonGraphic.ColorOffset(this.BackColor, -0.05f)/*CommonGraphic.ColorOffset(Color.White, -240, 0, 0, 0)*/))
						{
							g.FillPath(btnbsh, _gpBtnArr[i].BtnPath);
						}
					}

					if (i < _gpBtnArr.Count - 1
						&& i != this.MouseHoverIndex - 1
						&& i != this.MouseHoverIndex
						&& i != this.PressedIndex - 1
						&& i != this.PressedIndex)
					{
						CommonGraphic.DrawLine(g, splitPenMain,
							_gpBtnArr[i].BtnRect.Right,
							_gpBtnArr[i].BtnRect.Top + 6f,
							_gpBtnArr[i].BtnRect.Right,
							_gpBtnArr[i].BtnRect.Bottom - 6f);

						CommonGraphic.DrawLine(g, splitPenSub,
							_gpBtnArr[i].BtnRect.Right + 1f,
							_gpBtnArr[i].BtnRect.Top + 6f,
							_gpBtnArr[i].BtnRect.Right + 1f,
							_gpBtnArr[i].BtnRect.Bottom - 6f);
					}

					Rectangle txtRect = Rectangle.Round(_gpBtnArr[i].BtnRect);

					if (bkImage != null)
					{
						SizeF fontSize = g.MeasureString(_gpBtnArr[i].Text, this.Font);

						float height = (float)Math.Round(_gpBtnArr[i].BtnRect.Height * 0.76f, 1);		//图片高度;
						float width = (float)Math.Round(height * bkImage.Width / bkImage.Height);		//图片宽度;
						float textWidth = fontSize.Width * 1.3f;										//文字宽度;

						float imageLeftOffset = (_gpBtnArr[i].BtnRect.Width - (width + textWidth)) / 2f;	//图片相对于按钮左边偏移

						g.DrawImage(bkImage,
									_gpBtnArr[i].BtnRect.Left + imageLeftOffset, 
									_gpBtnArr[i].BtnRect.Top + (_gpBtnArr[i].BtnRect.Height - height) / 2.0f, 
									width, height);

						txtRect.Offset((int)(imageLeftOffset + width), 0);
						txtRect.Width = (int)textWidth;
					}

					TextRenderer.DrawText(g,
										_gpBtnArr[i].Text,
										this.Font,
										txtRect,
										this.ForeColor,
										TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

				}				
			}

// 			myBuffer.Render(e.Graphics);  //呈现图像至关联的Graphics  
// 			myBuffer.Dispose();
// 			g.Dispose();  
		}

		private GraphicsPath CreateButtonPath(RectangleF rect, PointF pt, ButtonPos pos)
		{
			GraphicsPath path = new GraphicsPath();

			float offsetX = pt.X;
			float offsetY = pt.Y;

			path.StartFigure();

			if (pos == ButtonPos.First)
			{
				path.AddArc(rect.Left, rect.Bottom - offsetY, offsetX, offsetY, 90, 90);			//左下角;
				path.AddArc(rect.Left, rect.Top, offsetX, offsetY, 180, 90);						//左上角;

				PointF[] pts = 
				{
					new PointF(rect.Left + offsetX / 2.0f, rect.Bottom),
					new PointF(rect.Right, rect.Bottom),
					new PointF(rect.Right, rect.Top),
					new PointF(rect.Left + offsetX / 2.0f, rect.Top)
				};
				path.AddPolygon(pts);
			}
			else if (pos == ButtonPos.Last)
			{
				path.AddArc(rect.Right - offsetX, rect.Top, offsetX, offsetY, 270, 90);				//右上角;
				path.AddArc(rect.Right - offsetX, rect.Bottom - offsetY, offsetX, offsetY, 0, 90);	//右下角;

				PointF[] pts = 
				{
					new PointF(rect.Right - offsetX / 2.0f, rect.Bottom),
					new PointF(rect.Left, rect.Bottom),
					new PointF(rect.Left, rect.Top),
					new PointF(rect.Right-offsetX / 2.0f, rect.Top)
				};
				path.AddPolygon(pts);
			}
			else
			{
				path.AddRectangle(rect);
			}

			path.CloseFigure();

			return path;
		}
	}
}
