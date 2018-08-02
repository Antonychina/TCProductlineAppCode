using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Mubea.GUI.CustomControl
{
	public abstract class BaseStoreCtrl : BaseCustomCtrl
	{
		protected int _slotNum = 12;
		public int SlotNum
		{
			get { return _slotNum; }
		}

		protected int _cupNum = 0;
		public int CupNum
		{
			get { return _cupNum; }
		}

		/// <summary>
		/// 剩余次数的字体大小
		/// </summary>
		public Font HolderFont { get; set; }

		/// <summary>
		/// 指示区高度因子
		/// </summary>
		public float IndexHeightFactor { get; set; }

		/// <summary>
		/// 指示图标大小因子
		/// </summary>
		public float IndexBtnSizeFactor { get; set; }

		protected int _slotOffsetX = -2;

		private GraphicsPath _titlePath;

		private HolderButton[] _indexBtn;

		private List<GraphicButton> _cupBtnArr;
		private List<GraphicButton> _holderBtnArr;

		public bool IsCupHover { get; set; }

		public enum HoverObject
		{
			CupHover = 0,	//捕获杯的鼠标消息
			SlotHover,		//捕获槽的鼠标消息
			NoneHover,		//不捕获鼠标消息
		}

		public BaseStoreCtrl()
		{
			IndexHeightFactor = 1.0f;
			IndexBtnSizeFactor = 1.0f;

			this.SetStyle(ControlStyles.ResizeRedraw |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.AllPaintingInWmPaint, true);

			this.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

			this.HolderFont = new System.Drawing.Font("YaHei Consolas Hybrid", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		}

		#region public methods
		/// <summary>
		/// 初始化控件
		/// </summary>
		/// <param name="holderNum">盒个数</param>
		/// <param name="cupNum">每个盒的杯个数</param>
		/// <param name="hoverObj">捕获对象类型</param>
		virtual public void Init(int holderNum, int cupNum, HoverObject hoverObj)
		{
			if (holderNum < 1 || cupNum < 0)
			{
				throw new Exception("Invalid groupNum or cupNum");
			}

			if (hoverObj == HoverObject.CupHover)
			{
				_cupBtnArr = _gpBtnArr;
				_holderBtnArr = new List<GraphicButton>();
			}
			else if (hoverObj == HoverObject.SlotHover)
			{
				_cupBtnArr = new List<GraphicButton>();
				_holderBtnArr = _gpBtnArr;
			}
			else
			{
				_cupBtnArr = new List<GraphicButton>();
				_holderBtnArr = new List<GraphicButton>();
			}

			_slotNum = holderNum;
			_cupNum = cupNum;

			//int headerWidth = (this.Width - 2 * 5);				//指示区域总宽度
            int headerHeight = (this.Height - 2 * 5);				//指示区域总宽度
            int headerWidth = (int)(IndexHeightFactor * this.Width / 8);					//指示区域高度
			//int slotWidth = headerWidth / _slotNum;				//每一槽的宽度
            int slotWidth = this.Width - 20 * 2 - headerWidth;				//每一槽的宽度
			//int headerHeight = (int)(IndexHeightFactor * this.Height / 8);					//指示区域高度
            

			int topOffset = 5;

			//int slotTop = 2 * topOffset + headerHeight;
            int slotTop =  topOffset ;
            int slotHeight = (headerHeight - topOffset * holderNum) / holderNum;

			Rectangle indexRect = new Rectangle(10, topOffset, headerWidth, headerHeight);
            indexRect.Width = headerWidth;
			_titlePath = CommonGraphic.CreateRoundRectPath(indexRect, 10, 10);

			_indexBtn = new HolderButton[_slotNum];

			float indexBtnSize = Math.Min(slotHeight, headerWidth) * IndexBtnSizeFactor;
            float indexOffsetX = (headerWidth - indexBtnSize) / 2.0f;
            float indexOffsetY = (slotHeight - indexBtnSize) / 2.0f;

			float flateVal = 0;
            if (slotHeight <= indexBtnSize)
			{
				flateVal = indexBtnSize / 10;  
			}

            float roundSize = slotHeight * 24 / 42f;
			for (int i = 0; i < _slotNum; i++)
			{
				int slotLeft = indexRect.Left * 2 + headerWidth;
                int slotTopOffset = slotTop*(i+1) + i * slotHeight;

                RectangleF rect = new RectangleF(indexRect.Left * 2, slotTopOffset, indexBtnSize, indexBtnSize);
				rect.Offset(indexOffsetX, indexOffsetY);
				rect.Inflate(- flateVal, -flateVal);

				HolderButton indexBtn = new HolderButton();
				indexBtn.BtnRect = rect;
				indexBtn.BtnPath = new GraphicsPath();
				indexBtn.BtnPath.AddEllipse(rect);
				indexBtn.BtnRegion = new Region(indexBtn.BtnRect);
				indexBtn.Text = "T" + (i + 1).ToString();
				indexBtn.HeaderBtn = true;
				_indexBtn[i] = indexBtn;

                Rectangle slotRect = new Rectangle(slotLeft+10, slotTopOffset, slotWidth, slotHeight);

				slotRect.Inflate(_slotOffsetX, 0);
				HolderButton holderBtn = new HolderButton();
				holderBtn.BtnRect = slotRect;
				holderBtn.BtnPath = CommonGraphic.CreateRoundRectPath(slotRect, roundSize, roundSize);
				holderBtn.BtnRegion = new Region(holderBtn.BtnPath);
				_holderBtnArr.Add(holderBtn);

				if (_cupNum > 0)
				{
                    int cupHeight = slotHeight;
                    int offset = (slotHeight - cupHeight) / 2;
                    int widthOffset = 0;
					for (int j = 0; j < _cupNum; j++)
					{
                        int cupTop = widthOffset + slotTop * (i + 1) + i * slotHeight;
                        int cupLeft = slotLeft + 10 + (slotWidth - cupHeight * _cupNum) / (_cupNum + 1) + ((slotWidth - 2 * ((slotWidth - cupHeight * _cupNum) / (_cupNum + 1)) - cupHeight) / (_cupNum - 1)) * j;
                        RectangleF cupRect = new RectangleF(cupLeft, cupTop, slotHeight, cupHeight);
						if (offset > 0)
						{
							cupRect.Inflate(-widthOffset - offset, -widthOffset);
						}
						else
						{
							cupRect.Inflate(-widthOffset, -widthOffset + offset);
						}

						CupButton cupBtn = new CupButton();
						cupBtn.BtnRect = cupRect;
						cupBtn.BtnPath = new GraphicsPath();
						cupBtn.BtnPath.AddEllipse(cupRect);
						cupBtn.BtnRegion = new Region(cupBtn.BtnPath);

						_cupBtnArr.Add(cupBtn);
					}
				}
			}
		}

		public void SetHolderState(int index, SlotState state)
		{
			Debug.Assert(index > -1 && index < _holderBtnArr.Count);

			(_holderBtnArr[index] as HolderButton).State = state;

            if (state == SlotState.extracted)
            {
                (_holderBtnArr[index] as HolderButton).SlotText = "";
                _holderBtnArr[index].Text = "";

            }

			_indexBtn[index].State = state;

            if (state == SlotState.extracted)
            {
                for (int i = index * CupNum; i < (index + 1) * CupNum; i++)
                {
                    CupButton cup = (_cupBtnArr[i] as CupButton);                    
                    cup.IsSlotOn = false;
                    cup.Enabled = false;
                    cup.State = TestState.Idle;
                }
            }
            else if (state == SlotState.inserted)
            {
                for (int i = index * CupNum; i < (index + 1) * CupNum; i++)
                {
                    CupButton cup = (_cupBtnArr[i] as CupButton);
                    cup.IsSlotOn = true;
					cup.Enabled = true;
                }
            }
			this.Invalidate();
		}

        public void SetCupState(int index, TestState state)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			(_cupBtnArr[index] as CupButton).State = state;

			this.Invalidate();
		}

        public TestState GetCupState(int index)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			return (_cupBtnArr[index] as CupButton).State;
		}

		public void SetCupSelect(int index, bool bSelect)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);
			_cupBtnArr[index].Selected = bSelect;

			this.Invalidate();
		}

		public void SetCupSelect(List<int> indexArr, bool bSelect)
		{
			foreach (int index in indexArr)
			{
				_cupBtnArr[index].Selected = bSelect;
			}

			this.Invalidate();
		}

		public bool IsCupSelect(int index)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			return _cupBtnArr[index].Selected;
		}

		public void EnableCup(int index)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			_cupBtnArr[index].Enabled = true;

			this.Invalidate();
		}

		public void DisableCup(int index)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			_cupBtnArr[index].Enabled = false;

			this.Invalidate();
		}

		public List<int> GetCupSelectIndex()
		{
			List<int> selectArr = new List<int>();
			for (int i = 0; i < _cupBtnArr.Count; i++)
			{
				if (_cupBtnArr[i].Selected)
				{
					selectArr.Add(i);
				}
			}

			return selectArr;
		}

		public bool IsCupEnable(int index)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			return _cupBtnArr[index].Enabled;
		}

		public void SetSlotText(int index, string text)
		{
			Debug.Assert(index > -1 && index < _holderBtnArr.Count);

			(_holderBtnArr[index] as HolderButton).SlotText = text;

			this.Invalidate();
		}

		public void SetReagentRemainder(int index, uint times)
		{
			Debug.Assert(index > -1 && index < _holderBtnArr.Count);

			_holderBtnArr[index].Text = times.ToString();

			this.Invalidate();
		}

		public void SetCupInfo(int index, CupInfo info)
		{
			Debug.Assert(index > -1 && index < _cupBtnArr.Count);

			CupButton cup = _cupBtnArr[index] as CupButton;
			cup.CupInfo.SampleID = info.SampleID;
			cup.CupInfo.Barcode = info.Barcode;
		}

        public byte GetCupNO(int index)
        {
            return (byte)(index % SlotNum);
        }

        public byte GetSlotNO(int index)
        {
            return (byte)(index / SlotNum);
        }

		public void ClearSelection()
		{
			_cupBtnArr.ForEach(x => x.Selected = false);
		}

		#endregion
		
		#region protected methods
		private void OnPaint(Graphics g, Rectangle ClipRectangle)
		{
			using (Pen pen = new Pen(Color.Black))
			{
				g.DrawRectangle(pen, ClipRectangle);
			}

// 			if (_titlePath != null)
// 			{
// 				using (SolidBrush bsh = new SolidBrush(Color.LightGray/*Color.FromArgb(204, 204, 204)*/))
// 				{
// 					g.FillPath(bsh, _titlePath);
// 				}
// 			}

			if (_indexBtn != null)
			{
				using (SolidBrush bsh = new SolidBrush(Color.LightGreen))
				{
					using (Pen indexPen = new Pen(Color.Gray))
					{
						for (int i = 0; i < _indexBtn.Length; i++)
						{
							bsh.Color = _indexBtn[i].BackColor;
							g.FillPath(bsh, _indexBtn[i].BtnPath);

							indexPen.Color = _indexBtn[i].BorderColor;
							g.DrawPath(indexPen, _indexBtn[i].BtnPath);

							StringFormat drawFormat = new StringFormat();
							drawFormat.Alignment = StringAlignment.Center;
							drawFormat.LineAlignment = StringAlignment.Center;
							bsh.Color = _indexBtn[i].ForeColor;
							g.DrawString(_indexBtn[i].Text, this.Font, bsh, _indexBtn[i].BtnRect, drawFormat);
						}
					}
				}
			}

			if (_cupBtnArr == null || _holderBtnArr == null)
			{
				return;
			}

			using (Pen pen = new Pen(Color.Gray))
			{
				using (SolidBrush bsh = new SolidBrush(Color.FromArgb(238, 238, 238)))
				{
					foreach (var btn in _holderBtnArr)
					{
						HolderButton holder = btn as HolderButton;

						bsh.Color = holder.BackColor;
						g.FillPath(bsh, btn.BtnPath);

						pen.Color = holder.BorderColor;
						g.DrawPath(pen, btn.BtnPath);

						if (!string.IsNullOrEmpty(holder.SlotText))
						{
							g.TranslateTransform(holder.BtnRect.Left + holder.BtnRect.Width / 2
												, holder.BtnRect.Top + holder.BtnRect.Height / 2
												, MatrixOrder.Prepend);
							g.RotateTransform(-90);

							RectangleF textRect = new RectangleF(-holder.BtnRect.Height / 2,
																-holder.BtnRect.Width / 2,
																holder.BtnRect.Height,
																holder.BtnRect.Width);

							StringFormat drawFormat = new StringFormat();
							drawFormat.Alignment = StringAlignment.Center;
							drawFormat.LineAlignment = StringAlignment.Center;

							bsh.Color = holder.ForeColor;
							g.DrawString(holder.SlotText, this.Font, bsh, textRect, drawFormat);

							g.ResetTransform();
						}

						if (!string.IsNullOrEmpty(holder.Text) && holder.State != SlotState.extracted)
						{
							StringFormat drawFormat = new StringFormat();
							drawFormat.Alignment = StringAlignment.Center;
							drawFormat.LineAlignment = StringAlignment.Far;

							RectangleF holderTextRt = holder.BtnRect;
							holderTextRt.Inflate(0, -10);

							bsh.Color = holder.ForeColor;
							g.DrawString(holder.Text, this.HolderFont, bsh, holderTextRt, drawFormat);
						}
					}
				}
			}

			foreach (var cup in _cupBtnArr)
			{
				(cup as CupButton).DrawCup(g);
			}
			
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;

			g.Clear(this.BackColor);

			OnPaint(g, e.ClipRectangle);
		}
		#endregion
		
	}
}
