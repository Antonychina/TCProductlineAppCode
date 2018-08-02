using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Mubea.GUI.CustomControl
{
	class CustomEventHandler
	{
		private int _mouseHoverIndex = -1;
        public int MouseHoverIndex
        {
            get { return _mouseHoverIndex; }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }

        private int _pressedIndex = -1;
        public int PressedIndex
        {
            get { return _pressedIndex; }
        }

		private bool _bMultiSelect = false;			//是否支持多选
		public bool MultiSelect
		{
			get { return _bMultiSelect; }
			set { _bMultiSelect = value; }
		}

        public bool _bEnableCancelSelectSelf = true; //标记在选中状态下，再次单击按钮能否取消选中状态

		public List<GraphicButton> _gpBtnArr;

		private Control _ctrl;

		public CustomEventHandler(Control ctrl)
        {
			_ctrl = ctrl;

             _gpBtnArr = new List<GraphicButton>();
        }

		public void OnMouseMove(Point pt)
		{
			for (int i = 0; i < _gpBtnArr.Count; i++)
			{
				if (_gpBtnArr[i].BtnRegion.IsVisible(pt))
				{
					if (_gpBtnArr[i].MouseHover)
					{
						return;
					}
					else
					{
						if (_mouseHoverIndex > -1)
						{
							_gpBtnArr[_mouseHoverIndex].MouseHover = false;
						}

						if (_gpBtnArr[i].Enabled)
						{
							_gpBtnArr[i].MouseHover = true;

							if (_pressedIndex != -1)
							{
								_gpBtnArr[_pressedIndex].Pressed = _mouseHoverIndex == _pressedIndex;
							}

							_ctrl.Cursor = Cursors.Hand;
						}
						else
						{
							_ctrl.Cursor = Cursors.Default;
						}

						_mouseHoverIndex = i;

						_ctrl.Invalidate();

						return;
					}
				}
			}

			OnMouseLeave();
		}

		public void OnMouseLeave()
		{
			if (_mouseHoverIndex > -1)
			{
				_gpBtnArr[_mouseHoverIndex].MouseHover = false;

				_mouseHoverIndex = -1;

				_ctrl.Invalidate();

				_ctrl.Cursor = Cursors.Default;
			}
		}

		public void OnMouseDown(SelectChangedHandle btnSelect)
		{
			if (_mouseHoverIndex > -1 && _gpBtnArr[_mouseHoverIndex].Enabled)
			{
				bool bSelected = false;

				_gpBtnArr[_mouseHoverIndex].Pressed = true;
				_pressedIndex = _mouseHoverIndex;

				if (_bMultiSelect)
				{
					if (_gpBtnArr[_mouseHoverIndex].Selected)
					{
						if (_bEnableCancelSelectSelf)
						{
							_gpBtnArr[_mouseHoverIndex].Selected = false;

							bSelected = false;
						}
					}
					else
					{
						_gpBtnArr[_mouseHoverIndex].Selected = true;

						bSelected = true;
					}
				}
				else
				{
					if (_selectedIndex == _mouseHoverIndex)
					{
						if (_bEnableCancelSelectSelf)
						{
							_gpBtnArr[_selectedIndex].Selected = false;
							_selectedIndex = -1;

							bSelected = false;
						}
					}
					else
					{
						if (_selectedIndex > -1)
						{
							_gpBtnArr[_selectedIndex].Selected = false;
						}

						_selectedIndex = _mouseHoverIndex;
						_gpBtnArr[_selectedIndex].Selected = true;

						bSelected = true;
					}
				}

				if (btnSelect != null)
				{
					SelectChangeEventArgs evt = new SelectChangeEventArgs();
					evt.SelectIndex = _mouseHoverIndex;
					evt.IsSelected = bSelected;

					btnSelect(this, evt);
				}

				_ctrl.Invalidate();
			} 
		}

		public void OnMouseUp(SelectChangedHandle btnClick)
		{
			if (_pressedIndex != -1)
			{
				if (_gpBtnArr[_pressedIndex].Pressed)
				{
					_gpBtnArr[_pressedIndex].Pressed = false;
				}

				if (btnClick != null)
				{
					SelectChangeEventArgs evt = new SelectChangeEventArgs();
					evt.SelectIndex = _pressedIndex;
					evt.IsSelected = false;

					btnClick(this, evt);
				}

				_pressedIndex = -1;

				_ctrl.Invalidate();
			}
		}
	}
}
