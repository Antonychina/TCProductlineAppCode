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
	public partial class SampleStore : BaseStoreCtrl
	{
		public SampleStore()
		{
		//	InitializeComponent();

			_slotOffsetX = -5;
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
	}
}
