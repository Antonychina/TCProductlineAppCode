using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Mubea.GUI.CustomControl
{
	class CommonGraphic
	{
		static public Color BKColor = Color.White/*Color.FromArgb(240, 243, 248)*/;

		static public GraphicsPath CreateRoundRectPath(RectangleF rect, float offsetX, float offsetY)
		{
			GraphicsPath path = new GraphicsPath();

			path.StartFigure();
			path.AddArc(rect.Right - offsetX, rect.Top, offsetX, offsetY, 270, 90);				//右上角;
			path.AddArc(rect.Right - offsetX, rect.Bottom - offsetY, offsetX, offsetY, 0, 90);	//右下角;
			path.AddArc(rect.Left, rect.Bottom - offsetY, offsetX, offsetY, 90, 90);			//左下角;
			path.AddArc(rect.Left, rect.Top, offsetX, offsetY, 180, 90);						//左上角;
			path.CloseFigure();

			return path;
		}

		static public Color ColorOffset(Color colorBase, int a, int r, int g, int b)
		{
			int a0 = colorBase.A;
			int r0 = colorBase.R;
			int g0 = colorBase.G;
			int b0 = colorBase.B;

			if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
			if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
			if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
			if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

			return Color.FromArgb(a, r, g, b);
		}

		static public Color ColorOffset(Color colorBase, float offset)
		{
			int a0 = colorBase.A;
			int r0 = colorBase.R;
			int g0 = colorBase.G;
			int b0 = colorBase.B;

			r0 = Math.Min(255, (int)(r0 * (1 + offset)));
			r0 = Math.Max(0, r0);

			g0 = Math.Min(255, (int)(g0 * (1 + offset)));
			g0 = Math.Max(0, g0);

			b0 = Math.Min(255, (int)(b0 * (1 + offset)));
			b0 = Math.Max(0, b0);

			return Color.FromArgb(a0, r0, g0, b0);
		}

		static public void DrawLine(Graphics g, Pen pen, float x1, float y1, float x2, float y2)
		{
			x1 = (int)(x1 + 0.5f) + 0.5f;
			y1 = (int)(y1 + 0.5f) + 0.5f;
			x2 = (int)(x2 + 0.5f) + 0.5f;
			y2 = (int)(y2 + 0.5f) + 0.5f;

			g.DrawLine(pen, x1, y1, x2, y2);
		}

		static public void DrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height)
		{
			x = (int)(x + 0.5f) + 0.5f;
			y = (int)(y + 0.5f) + 0.5f;
			width = (int)width;
			height = (int)height;

			g.DrawRectangle(pen, x, y, width, height);
		}

		static public void ConvertRect(ref RectangleF rect)
		{
			rect.X = (int)(rect.X + 0.5f) + 0.5f;
			rect.Y = (int)(rect.Y + 0.5f) + 0.5f;
			rect.Width = (int)rect.Width;
			rect.Height = (int)rect.Height;
		}
	}
}
