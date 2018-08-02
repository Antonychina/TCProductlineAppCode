using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Mubea.GUI.CustomControl
{
	public enum TestState
	{
        Idle = 0,		//无任务
        Wait,
		Running,		//正在运行, = busy
		Fail,		//测试fail
        Pass,		//测试pass
        Abort,    //中断
        Unknown,   //未知状态
        Error,
	}

    public enum StationStatus
    {
        Available = 0,    //正常
        Unavailable,    //不能使用
    }

    public enum GUFStatus
    {
        Available = 0, //正常
        Unavailable,  //不能使用
    }

	public class CupInfo
	{
		public string SampleID;
		public string Barcode;
	}

	internal class CupButton : GraphicButton
	{
		public System.Drawing.Font Font { get; set; }

		private Color _bkColor = Color.White;

        private TestState _state;
        public TestState State
		{
			get { return _state; }
			set 
			{
				_state = value;
				switch (_state)
				{
                    case TestState.Idle:
						_bkColor = Color.White;                 //idle
						break;
                    case TestState.Abort:
						_bkColor = Color.OrangeRed;	//OrangeRed
						break;
                    case TestState.Fail:
						_bkColor = Color.FromArgb(255,0,0);	//red
						break;
                    case TestState.Pass:
						_bkColor = Color.FromArgb(0,255,0);	//green
						break;
                    case TestState.Running:
                        _bkColor = Color.Yellow;	//yellow
                        break;
                    case TestState.Error:
                        _bkColor = Color.DarkRed;	//DarkRed
                        break;
                    case TestState.Wait:
                        _bkColor = Color.LightYellow;	//wait
                        break;
                    case TestState.Unknown:
                        _bkColor = Color.Gray;	//Gray
                        break;
					default:
						break;
				}
			}
		}

		private CupInfo _info;
		public CupInfo CupInfo
		{
			get { return _info; }
		}

        public bool IsSlotOn { get; set; }  //表示样本槽是否插上

		public CupButton()
		{
            IsSlotOn = false;
			Enabled = false;
            State = TestState.Idle;
			_info = new CupInfo();

			Font = new System.Drawing.Font("YaHei Consolas Hybrid", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		}

		public void DrawCup(Graphics g)
		{
			using (SolidBrush bsh = new SolidBrush(_bkColor))
			{
				if (IsSlotOn && !this.Enabled)
				{
					bsh.Color = Color.FromArgb(223, 221, 219);
				}

				g.FillPath(bsh, this.BtnPath);
			}

			Color borderColor = Color.Gray;
			float width = 1;
			if (this.Selected)
			{
				borderColor = Color.Blue;
				width = 2;

				using (Pen pen = new Pen(borderColor, width))
				{
					g.DrawPath(pen, this.BtnPath);
				}
			}
			else
			{
				using (Pen pen = new Pen(borderColor, width))
				{
					g.DrawPath(pen, this.BtnPath);
				}
			}
		}

		public void Clear()
		{
			_info.SampleID = "";
			_info.Barcode = "";
		}
	}
}
