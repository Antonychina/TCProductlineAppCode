using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;

namespace Mubea.AutoTest.GUI
{
	public class BaseDialog : BaseViewForm
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		private Color _titleBackColor = Color.FromArgb(37, 40, 45);
		[DefaultValue(typeof(Color), "Black")]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Color TitleBackColor
		{
			get { return _titleBackColor; }
			set { _titleBackColor = value; }
		}

		private Color _titleForeColor = Color.White;
		[DefaultValue("White")]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public Color TitleForeColor
		{
			get { return _titleForeColor; }
			set { _titleForeColor = value; } 
		}

		private Rectangle _titleRect;

		public new Padding Padding
		{
			get { return base.Padding; }
			set
			{
				base.Padding = value;
				_titleRect = new Rectangle(0, 0, this.Width, value.Top - 10);
			}
		}

		private bool _isMovable = true;
		[DefaultValue(true)]
		[Browsable(true)]
		[Category("Custom Appearance")]
		public bool Movable
		{
			get { return _isMovable; }
			set { _isMovable = value; }
		}

		public BaseDialog()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint |
					 ControlStyles.OptimizedDoubleBuffer |
					 ControlStyles.ResizeRedraw |
					 ControlStyles.UserPaint, true);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			_titleRect = new Rectangle(0, 0, this.Width, Padding.Top - 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			using (SolidBrush b = new SolidBrush(TitleBackColor))
			{
				e.Graphics.FillRectangle(b, _titleRect);
			}

			using (Pen pen = new Pen(Color.FromArgb(204, 204, 204)))
			{
				e.Graphics.DrawLines(pen, new[]
                        {
                            new Point(0, _titleRect.Height),
                            new Point(0, Height - 1),
                            new Point(Width - 1, Height - 1),
                            new Point(Width - 1, _titleRect.Height)
                        });
			}

			Rectangle bounds = _titleRect;
			bounds.Inflate(-Padding.Left, 0);
			TextRenderer.DrawText(e.Graphics, Text, this.Font, bounds, TitleForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Left && Movable)
			{
				if (WindowState == FormWindowState.Maximized)
				{
					return;
				}

				MoveControl();
			}

		}

		[SecuritySafeCritical]
		private void MoveControl()
		{
			WinApi.ReleaseCapture();
			WinApi.SendMessage(Handle, (int)WinApi.Messages.WM_NCLBUTTONDOWN, (int)WinApi.HitTest.HTCAPTION, 0);
		}


		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// BaseDialog
			// 
			this.ClientSize = new System.Drawing.Size(381, 331);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BaseDialog";
			this.Padding = new System.Windows.Forms.Padding(10, 50, 10, 10);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BaseDialog";
			this.ResumeLayout(false);

		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
