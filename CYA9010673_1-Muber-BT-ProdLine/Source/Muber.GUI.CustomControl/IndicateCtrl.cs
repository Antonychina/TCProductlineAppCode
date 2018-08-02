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
	public enum Shape
	{
		Rectangle = 0,		//方形
		Oval				//圆形
	}

	public class IndicateCtrl : PictureBox
	{
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
		}

		public bool ShowBorder { get; set; }

		private Shape _shape = Shape.Rectangle;
		public Shape Shape
		{
			get { return _shape; }
			set { _shape = value; }
		}

		public IndicateCtrl()
		{
			ShowBorder = true;

			SetStyle(ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer |
					ControlStyles.ResizeRedraw |
					ControlStyles.SupportsTransparentBackColor,
					true);
		}

		protected override void  OnPaint(PaintEventArgs pe)
		{
 			//base.OnPaint(pe);

			pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			pe.Graphics.CompositingQuality = CompositingQuality.HighQuality;

			pe.Graphics.Clear(this.Parent.BackColor);

			using (SolidBrush bsh = new SolidBrush(this.BackColor))
			{
				if (Shape == Shape.Rectangle)
				{
					pe.Graphics.FillRectangle(bsh, this.ClientRectangle);
				}
				else if (Shape == Shape.Oval)
				{
					pe.Graphics.FillEllipse(bsh, this.ClientRectangle);
				}
			}

			if (ShowBorder)
			{
				Rectangle rect = this.ClientRectangle;
				rect.Inflate(-1, -1);

				using (Pen pen = new Pen(Color.Black,1.5f))
				{
					if (Shape == Shape.Rectangle)
					{
						pe.Graphics.DrawRectangle(pen, rect);
					}
					else if (Shape == Shape.Oval)
					{
						pe.Graphics.DrawEllipse(pen, rect);
					}
				}
			}
			
		}
	}
}
