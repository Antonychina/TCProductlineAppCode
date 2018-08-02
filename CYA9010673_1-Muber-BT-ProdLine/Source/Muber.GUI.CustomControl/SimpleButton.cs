using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mubea.GUI.CustomControl
{
	public partial class SimpleButton : Button
	{
		/// <summary>
		/// 控件的状态。
		/// </summary>
		enum ControlState
		{
			/// <summary>
			///  正常。
			/// </summary>
			Normal,
			/// <summary>
			/// 鼠标进入。
			/// </summary>
			Hover,
			/// <summary>
			/// 鼠标按下。
			/// </summary>
			Pressed,
			/// <summary>
			/// 获得焦点。
			/// </summary>
			Focused,
		}

		private const float _OffsetX = 5.0f;
		private const float _OffsetY = 5.0f;

		private GraphicsPath _path;
		private GraphicsPath _borderPath;

		private ControlState _controlState;

		public SimpleButton()
		{
			InitializeComponent();

			SetStyle(ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer |
					ControlStyles.ResizeRedraw |
					ControlStyles.SupportsTransparentBackColor, 
					true);

			this.ForeColor = Color.White;
			this.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		}

		public SimpleButton(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			RectangleF rect = ClientRectangle;
			rect.Inflate(-0.5f, -0.5f);
			_path = CommonGraphic.CreateRoundRectPath(rect, _OffsetX, _OffsetY);

			rect.Inflate(-1f, -1f);
			_borderPath = CommonGraphic.CreateRoundRectPath(rect, _OffsetX, _OffsetY);

		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
		//	base.OnPaintBackground(pevent);	//绘制按钮背景，透明效果

			Graphics g = pevent.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;


			if (_path == null)
			{
				_path = CommonGraphic.CreateRoundRectPath(ClientRectangle, _OffsetX, _OffsetY);
			}

			////////////////////////////////////////绘制控件背景色/////////////////////////////////////////
			Color colorUp = Color.FromArgb(69, 162, 217);
			Color colorDown = Color.FromArgb(68, 161, 218);

			switch (_controlState)
			{
				case ControlState.Hover:
					colorUp = CommonGraphic.ColorOffset(colorUp, 0.2f);
					colorDown = CommonGraphic.ColorOffset(colorDown, 0.2f);
					break;
				case ControlState.Pressed:
					colorUp = CommonGraphic.ColorOffset(colorUp, -0.2f);
					colorDown = CommonGraphic.ColorOffset(colorDown, -0.2f);
					break;
				default:
					break;
			}

			LinearGradientBrush lgb = new LinearGradientBrush(ClientRectangle,
								  colorUp,
								  colorDown,
								  90f);
			g.FillPath(lgb, _path);

			////////////////////////////////////////绘制空间边框/////////////////////////////////////////
			Color borderColor = CommonGraphic.ColorOffset(colorDown, -0.2f);
			Pen pen = new Pen(borderColor, 1.0f);
			g.DrawPath(pen, _path);

			pen.Color = CommonGraphic.ColorOffset(colorDown, 0.1f);
			g.DrawPath(pen, _borderPath);

			////////////////////////////////////////绘制文字/////////////////////////////////////////
			Rectangle txtRect = ClientRectangle;
			if (this.BackgroundImage != null)
			{
				float height = this.ClientRectangle.Height * 0.8f;
				float width = height * this.BackgroundImage.Width / this.BackgroundImage.Height;

				g.DrawImage(this.BackgroundImage, 5, (ClientRectangle.Height - height) / 2.0f, width, height);

				txtRect.Offset((int)height, 0);
				txtRect.Width -= (int)height;
			}
			TextRenderer.DrawText(g,
								this.Text,
								this.Font,
								txtRect,
								this.ForeColor,
								TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_controlState = ControlState.Hover;
			this.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_controlState = ControlState.Normal;
			Console.WriteLine(_controlState);
			this.Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left/* && e.Clicks == 1*/)
			{
				_controlState = ControlState.Pressed;
				this.Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left/* && e.Clicks == 1*/)
			{
				if (ClientRectangle.Contains(e.Location))
				{
					_controlState = ControlState.Hover;
				}
				else
				{
					_controlState = ControlState.Normal;
				}
				this.Invalidate();
			}
		}

		
	}
}
