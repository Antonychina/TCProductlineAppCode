using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mubea.GUI.CustomControl
{
	public class SelectChangeEventArgs : EventArgs
	{
		public int SelectIndex { get; set; }
		public bool IsSelected { get; set; }
	}

	public delegate void SelectChangedHandle(object sender, SelectChangeEventArgs e);

	public class GraphicButton
	{
		public bool _bEnable = true;
		public bool Enabled
		{
			get { return _bEnable; }
			set { _bEnable = value; } 
		}

		public RectangleF BtnRect { get; set; }

		public GraphicsPath BtnPath { get; set; }

		public Region BtnRegion { get; set; }

		public bool MouseHover { get; set; }

		public bool Pressed { get; set; }

		public bool Selected { get; set; }

		public string Text { get; set; }

		public Image BackgroundImage { get; set; }

		public Image SelectedBKImage { get; set; }
	}
}
