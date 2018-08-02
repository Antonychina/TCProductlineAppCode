using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Mubea.GUI.CustomControl
{
	public enum SlotState
	{
		extracted = 0,
		inserted,
		used
	}

	class HolderButton : GraphicButton
	{
		private bool _bHeaderBtn = false;
		public bool HeaderBtn
		{
			get { return _bHeaderBtn; }
			set 
			{
				_bHeaderBtn = value;

				if (_bHeaderBtn && State == SlotState.extracted)
				{
					_backColor = Color.FromArgb(214, 221, 226);
				}
			}
		}

		private SlotState _state = SlotState.extracted;
		public SlotState State 
		{
			get { return _state; }
			set
			{
				switch (value)
				{
					case SlotState.extracted:
						if (HeaderBtn)
						{
							_backColor = Color.FromArgb(214, 221, 226);
							_foreColor = Color.Black;
							_borderColor = Color.FromArgb(192, 205, 214);
						}
						else
						{
							_backColor = Color.FromArgb(214, 221, 226);
							_foreColor = Color.Black;
							_borderColor = Color.FromArgb(192, 205, 214);
						}
						
						break;
					case SlotState.inserted:
						if (HeaderBtn)
						{
							_backColor = Color.FromArgb(96, 222, 131);
							_foreColor = Color.White;
							_borderColor = Color.Gray;
						} 
						else
						{
							_backColor = Color.FromArgb(192, 205, 214);
							_foreColor = Color.Black;
							_borderColor = Color.Gray;
						}
						
						break;
					case SlotState.used:
						if (HeaderBtn)
						{
							_backColor = Color.FromArgb(214, 221, 226);
							_foreColor = Color.White;
							_borderColor = Color.FromArgb(192, 205, 214);
						} 
						else
						{
							_backColor = Color.FromArgb(214, 221, 226);
							_foreColor = Color.Black;
							_borderColor = Color.FromArgb(192, 205, 214);
						}
						
						break;
					default:
						break;
				}

				_state = value;
			}
		}

		private Color _backColor = Color.FromArgb(214, 221, 226);
		public Color BackColor
		{
			get { return _backColor; }
		}

		private Color _foreColor = Color.Black;
		public Color ForeColor
		{
			get { return _foreColor; }
		}

		private Color _borderColor = Color.Gray;
		public Color BorderColor
		{
			get { return _borderColor; }
		}

		private string _slotText = "";
		public string SlotText
		{
			get { return _slotText; }
			set { _slotText = value; }
		}
	}
}
