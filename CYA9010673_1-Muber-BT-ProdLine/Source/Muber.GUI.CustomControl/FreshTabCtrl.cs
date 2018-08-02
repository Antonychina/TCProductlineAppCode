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
	public partial class FreshTabCtrl : TabControl
	{
		#region feild

		private GraphicsPath _tabPath;
		private List<Rectangle> _pageRectArr = new List<Rectangle>();
		private List<GraphicsPath> _tabPathArr = new List<GraphicsPath>();
		public bool DisableChangeTable = false;

		#endregion feild

		#region property

		public Font TabTextFont { get; set; }

		public bool IsDisplayBorder { get; set; }

		public override Color BackColor { get; set; }

		public new Size ItemSize
		{
			get { return base.ItemSize; }
			set
			{
				base.ItemSize = value;

				if (this.TabPages.Count > 0)
				{
					_pageRectArr.Clear();
					_tabPathArr.Clear();

					for (int i = 0; i < TabPages.Count;i++ )
					{
						Rectangle rect = this.GetTabRect(i);
						rect.Height += 2;

						_pageRectArr.Add(rect);

						GraphicsPath path = new GraphicsPath();
						path.AddRectangle(rect);
						_tabPathArr.Add(path);
					}
				}
			}
		}

		#endregion property

		#region custom event

		public delegate void PreventChangeTabPageDelegate(int tabPageIndex);

		public event PreventChangeTabPageDelegate PreventChangeTabPage;	//阻止切换tab时触发此事件

		#endregion custom event

		public FreshTabCtrl()
		{
			InitializeComponent();

			SetStyles();

			TabTextFont = new System.Drawing.Font("YaHei Consolas Hybrid", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

			BackColor = CommonGraphic.BKColor;
		}

		private void SetStyles()
		{
			this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;

			base.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor, true);
			base.UpdateStyles();

			this.SizeMode = TabSizeMode.Fixed;

			this.ItemSize = new Size(280, 55);
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			TabPage page = e.Control as TabPage;
			if (page != null)
			{
				int index = this.TabPages.IndexOf(page);
				Rectangle rect = this.GetTabRect(index);
				rect.Height += 2;

				_pageRectArr.Add(rect);

				GraphicsPath path = new GraphicsPath();
				path.AddRectangle(rect);
				_tabPathArr.Add(path);
			}

			base.OnControlAdded(e);
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			TabPage page = e.Control as TabPage;
			if (page != null)
			{
				int index = e.Control.TabIndex;
				if (index > -1)
				{
					if (index < _pageRectArr.Count)
					{
						_pageRectArr.RemoveAt(index);
					}

					if (index < _tabPathArr.Count)
					{
						_tabPathArr.RemoveAt(index);
					}
				}
			}

			base.OnControlRemoved(e);
		}

		public void LoadTableControl(Type type)
		{
			Form form = (Form)Activator.CreateInstance(type);
			form.BackColor = BackColor;
			form.TopLevel = false;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;
			form.AutoScroll = true;
			form.Show();

			TabPage page = new System.Windows.Forms.TabPage();
			page.Name = form.Name;
			page.AutoScroll = true;
			page.Text = form.Text;
			page.Controls.Add(form);

			this.TabPages.Add(page);
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

			g.Clear(this.BackColor);

			using (SolidBrush brush = new SolidBrush(this.BackColor))
			{
				g.FillRectangle(brush, this.ClientRectangle);
			}

			using (SolidBrush bsh = new SolidBrush(Color.FromArgb(217, 228, 236)))
			{
				if (_tabPath == null)
				{
					Rectangle pageRect = new Rectangle(2, 0, this.DisplayRectangle.Width + 10, this.DisplayRectangle.Top);
					_tabPath = new GraphicsPath();
					_tabPath.AddRectangle(pageRect);
				}
				
				g.FillPath(bsh, _tabPath);
			}

			if (IsDisplayBorder)
			{
				using (Pen pen = new Pen(Color.FromArgb(187, 187, 187)))
				{
					g.DrawRectangle(pen, this.ClientRectangle);
				}
			}

			DrawTab(g);
		}

		private void DrawTab(Graphics graphics)
		{
			if (_tabPathArr.Count == this.TabPages.Count)
			{
				using (SolidBrush bsh = new SolidBrush(BackColor))
				{
					Rectangle txtRect;
					for (int i = 0; i < this.TabPages.Count; i++)
					{
						txtRect = Rectangle.Round(_pageRectArr[i]);

						Color clr;
						if (this.SelectedIndex == i)
						{
							graphics.FillPath(bsh, _tabPathArr[i]);
							clr = Color.DimGray;
						}
						else
						{
							clr = Color.DimGray;

							if (i > 0 
								&& (i < this.SelectedIndex || i > this.SelectedIndex + 1))
							{
								Pen pen = new Pen(BackColor, 2);
								graphics.DrawLine(pen, _pageRectArr[i].Left, _pageRectArr[i].Top,
												_pageRectArr[i].Left, _pageRectArr[i].Bottom);
							}
						}

						TextRenderer.DrawText(graphics,
										TabPages[i].Text,
										TabTextFont,
										txtRect,
										clr,
										TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
					}
				}
			}
		}

		protected override void OnDeselecting(TabControlCancelEventArgs e)
		{
			if (DisableChangeTable)
			{
				e.Cancel = true;

				if (PreventChangeTabPage != null)
				{
					PreventChangeTabPage(e.TabPageIndex);
				}
			}

			base.OnDeselecting(e);
		}

		private GraphicsPath CreateTabPath(RectangleF rect, float offsetX, float offsetY)
		{
			GraphicsPath path = new GraphicsPath();

			path.StartFigure();

			path.AddArc(rect.Left, rect.Top, offsetX, offsetY, 180, 90);						//左上角;
			path.AddArc(rect.Right - offsetX, rect.Top, offsetX, offsetY, 270, 90);				//右上角;

			PointF[] pts = 
			{
				new PointF(rect.Right, rect.Top + offsetY / 2),
				new PointF(rect.Right, rect.Bottom),
				new PointF(rect.Left, rect.Bottom),
				new PointF(rect.Left, rect.Top + offsetY / 2)
			};
			path.AddPolygon(pts);

			path.CloseFigure();

			return path;
		}
	}
}
