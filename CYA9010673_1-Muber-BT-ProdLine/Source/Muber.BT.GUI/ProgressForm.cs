using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mubea.AutoTest.GUI
{
	public partial class ProgressForm : BaseDialog
	{
		

		private const int MF_REMOVE = 0x1000;
		private const int SC_CLOSE = 0xF060;

		[DllImport("USER32.DLL")]
		public static extern int GetSystemMenu(int hwnd, int bRevert);
		[DllImport("USER32.DLL")]
		public static extern int RemoveMenu(int hMenu, int nPosition, int wFlags);

        public string DisplayContent { get; set; }
		
		public delegate void WorkAction();
		public WorkAction RunFunction;

		private bool _inited = false;

		private Thread _sendThread;

		public ProgressForm()
		{
			InitializeComponent();
		
            //ProgressBarHelper.KillProgressBarInitFail += killInitProgressBar;

			//ProgressBarHelper.SetProgressBarValueHandler += ProgressBarHelper_SetProgressBarValueHandler;
		}

		private void ProgressForm_Load(object sender, EventArgs e)
		{
			/***************************禁用关闭按钮需要***********************/
			int hMenu = GetSystemMenu(this.Handle.ToInt32(), 0);
			RemoveMenu(hMenu, SC_CLOSE, MF_REMOVE);

			labNote.Text = DisplayContent;

			prgBar.Style = ProgressBarStyle.Marquee;

            if (RunFunction == null)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();

                return;
            }

			_sendThread = new Thread(ThreadProc);
			_sendThread.IsBackground = true;
			_sendThread.Start();          
		}

		public bool Inited
		{
			get { return _inited; }
			set { _inited = value; }
		}

		public void SetProgressBarStyle(ProgressBarStyle prgBarStyle)
		{
			prgBar.Style = prgBarStyle;
		}

		public void SetProgressBarRange(int min, int max)
		{
			prgBar.Minimum = min;
			prgBar.Maximum = max;
		}

		public void SetProgressBarValue(int value)
		{
			prgBar.Value = value;
		}

		private void ThreadProc()
		{
			RunFunction.Invoke();

			if (this.IsHandleCreated)
			{
				this.Invoke(new Action(() =>
				{
					this.DialogResult = DialogResult.OK;
					this.Close();
				}));
			}
		}

		private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_sendThread.IsAlive)
			{
				_sendThread.Abort();
			}
		}

        private void killInitProgressBar()
        {
			if (this.IsHandleCreated)
			{
				this.BeginInvoke((EventHandler)(delegate
				{
					this.DialogResult = DialogResult.Cancel;
					this.Close();
				}));
			}
        }

		private void ProgressBarHelper_SetProgressBarValueHandler(int value)
		{
			if (this.IsHandleCreated)
			{
				this.BeginInvoke((EventHandler)(delegate
				{
					SetProgressBarValue(value);
				}));
			}
		}

	
	}
}
