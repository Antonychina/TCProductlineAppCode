using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Mubea.GUI.CustomControl
{
	public partial class InchingBtnCtrl : BaseCustomCtrl
	{
		public int MicroSteps
		{
			get { return SelectedIndex + 1; }
		}

		private Rectangle _txtRect;

		public InchingBtnCtrl()
		{
			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);
		}

		public void InitCtrl(uint btnNums)
		{
			Debug.Assert(btnNums > 0);

			_gpBtnArr.Clear();

			float startHeight = ClientRectangle.Bottom * 0.7f;
			float k = (startHeight - ClientRectangle.Top) / (float)(ClientRectangle.Left - ClientRectangle.Right);

			_txtRect = ClientRectangle;
			_txtRect.Height /= 3;

			float btnWidth = this.Width / (float)btnNums;

			for (int i = 0; i < btnNums;i++ )
			{
				RectangleF rect = new RectangleF();
				rect.X = i * btnWidth;
				rect.Width = btnWidth;

				rect.Y = (rect.X - ClientRectangle.Left) * k + startHeight; 
				rect.Height = ClientRectangle.Height - rect.Y;

				rect.Inflate(-2f, 0);
				CommonGraphic.ConvertRect(ref rect);

				if (rect.Bottom > ClientRectangle.Bottom)
				{
					rect.Height -= 1f;
				}

				GraphicButton gpBtn = new GraphicButton();
				gpBtn.BtnRect = rect;
				gpBtn.BtnPath = new GraphicsPath();
				gpBtn.BtnPath.AddRectangle(rect);
				gpBtn.BtnRegion = new Region(gpBtn.BtnPath);

				_gpBtnArr.Add(gpBtn);
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


			Color clr1 = Color.FromArgb(235, 242, 252);
			Color clr2 = Color.FromArgb(235, 242, 252);
			for (int i = SelectedIndex + 1; i < _gpBtnArr.Count; i++)
			{
				LinearGradientBrush bsh = new LinearGradientBrush(_gpBtnArr[i].BtnRect, clr1, clr2, 90f);
				g.FillPath(bsh, _gpBtnArr[i].BtnPath);
			}

			if (SelectedIndex > -1)
			{
				clr1 = Color.FromArgb(21, 166, 251);
				clr2 = CommonGraphic.ColorOffset(clr1, -0.1f);

				for (int i = 0; i <= SelectedIndex; i++)
				{
					LinearGradientBrush bsh = new LinearGradientBrush(_gpBtnArr[i].BtnRect, clr1, clr2, 90f);
					g.FillPath(bsh, _gpBtnArr[i].BtnPath);
				}
			}

			using (Pen pen = new Pen(Color.FromArgb(91, 159, 238)))
			{
				foreach (var btn in _gpBtnArr)
				{
					g.DrawPath(pen, btn.BtnPath);
				}
			}

			TextRenderer.DrawText(g,
									MicroSteps.ToString(),
									this.Font,
									_txtRect,
									Color.Black,
									TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

// 			myBuffer.Render(e.Graphics);  //呈现图像至关联的Graphics  
// 			myBuffer.Dispose();
// 			g.Dispose(); 
		}
	}
}
