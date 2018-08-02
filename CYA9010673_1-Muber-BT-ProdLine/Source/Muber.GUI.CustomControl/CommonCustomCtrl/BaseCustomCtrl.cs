using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mubea.GUI.CustomControl
{
	public class BaseCustomCtrl : ScrollableControl
	{
		private CustomEventHandler _eventHD;

		protected int MouseHoverIndex
		{
			get { return _eventHD.MouseHoverIndex; }
		}

		public int SelectedIndex
		{
			get { return _eventHD.SelectedIndex; }
			protected set { _eventHD.SelectedIndex = value; }
		}

		protected int PressedIndex
		{
			get { return _eventHD.PressedIndex; }
		}

		public bool MultiSelect
		{
			get { return _eventHD.MultiSelect; }
			set { _eventHD.MultiSelect = value; }
		}

		protected bool EnableCancelSelectSelf
		{
			get { return _eventHD._bEnableCancelSelectSelf; }
			set { _eventHD._bEnableCancelSelectSelf = value; }
		}

		public event SelectChangedHandle BtnClick;
		public event SelectChangedHandle BtnSelect;

		protected List<GraphicButton> _gpBtnArr
		{
			get { return _eventHD._gpBtnArr; }
			set { _eventHD._gpBtnArr = value; }
		}

		public BaseCustomCtrl()
		{
			_eventHD = new CustomEventHandler(this);

			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			_eventHD.OnMouseMove(e.Location);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			_eventHD.OnMouseLeave();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			_eventHD.OnMouseDown(BtnSelect);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			_eventHD.OnMouseUp(BtnClick);
		}

	}
}
