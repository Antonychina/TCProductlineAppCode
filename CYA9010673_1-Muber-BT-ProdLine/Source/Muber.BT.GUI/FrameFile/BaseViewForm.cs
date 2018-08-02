using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Mubea.GUI.CustomControl;
using Mubea.AutoTest.GUI.Localization;

namespace Mubea.AutoTest.GUI
{
	public enum Operation
	{
		Add = 0,
		Edit,
		Cancel,
		None
	};

	public enum TaskType
	{
		Normal = 0,
		QC = 1,
		Calibration = 2
	}

	public enum VolumnType
	{
		Normal = 0,
		Increase = 1,
		Decrease = 2
	}

	public class BaseViewForm : Form
	{
		protected Operation _opMode = Operation.None;

        public RefreshFormDelegate RefreshForm ; //public event DeviceHelper.RecvCmdDelegate OnRecvCmd;

		public BaseViewForm()
		{
			InitializeComponent();

// 			SetStyle(ControlStyles.UserPaint, true);
// 			SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
// 			SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲  
		}

		private void BaseViewForm_Load(object sender, EventArgs e)
		{
			/*this.Size = new System.Drawing.Size(847, 631);*/
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// BaseViewForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(381, 331);
			this.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Name = "BaseViewForm";
			this.Load += new System.EventHandler(this.BaseViewForm_Load);
			this.ResumeLayout(false);

		}

		public int GetComboBoxSelectedIndex(byte index)
		{
			if (index == 255)
			{
				return -1;
			}

			return index;
		}

		public void InitializeDataGridView(DataGridView dgv)
		{
			dgv.BackgroundColor = Color.White;
			dgv.RowHeadersVisible = false;
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false;
			dgv.ReadOnly = true;
			dgv.AllowUserToResizeRows = false;
			dgv.MultiSelect = false;
			dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		public void InitializeListView(ListView lsv)
		{
			lsv.BackColor = Color.White;
			lsv.FullRowSelect = true;
			lsv.GridLines = true;
			lsv.View = View.Details;
			lsv.HideSelection = false;
			lsv.MultiSelect = false;
		}

// 		public string GetDisplayPos(int pos)
// 		{
// 			pos = (pos - 1) % TrayControl.SampleNum + 1;
// 
// 			return pos.ToString();
// 		}
// 
// 		public string GetDisplayTrayAndPos(int cupPos)
// 		{
// 			int pos = (cupPos - 1) % TrayControl.SampleNum + 1;
// 			int trayNo = (cupPos - 1) / TrayControl.SampleNum + 1;
// 
// 			return string.Format("{0} / {1}", trayNo, pos);
// 		}

		

		public void ChangeLanguage()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
			foreach (Control ctl in this.Controls)
			{
				resources.ApplyResources(ctl, ctl.Name);

				if (ctl is ListView)
				{
					ListView lsv = ctl as ListView;

					foreach (ColumnHeader header in lsv.Columns)
					{
						resources.ApplyResources(header, header.Name);
					}
				}
				else if (ctl is DataGridView)
				{
					DataGridView dgv = ctl as DataGridView;

					foreach (DataGridViewColumn col in dgv.Columns)
					{
						resources.ApplyResources(col, col.Name);
					}
				}
			}
		}

		/// <summary>
		/// 禁止切换界面
		/// </summary>
		/// <param name="bDisable"></param>
		public void DisableSwitchTabPage(bool bDisable)
		{
			MainForm.DisableSwitchForm = bDisable;

			if (this.Parent != null)
			{
				FreshTabCtrl tabCtrl = this.Parent.Parent as FreshTabCtrl;

				if (tabCtrl != null)
				{
					tabCtrl.DisableChangeTable = bDisable;
				}
			}
		}

		virtual public void DisableLeave()
		{
			SpMessageBox.Show(ResourceLang.SwitchFormWarning);		//"添加或编辑操作未完成，不能切换到其他界面！"
			this.Focus();
		}
	}
}
