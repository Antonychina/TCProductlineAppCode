using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Mubea.GUI.CustomControl;

namespace Mubea.AutoTest.GUI
{
	public class BaseContainerForm : Form
	{
		protected FreshTabCtrl tabMain;
	
		public BaseContainerForm()
		{
			InitializeComponent();
		}

		private void BaseContainerForm_Load(object sender, EventArgs e)
		{
			tabMain.PreventChangeTabPage += tabMain_PreventChangeTabPage;
		}

		public void SwitchTo(int index)
		{
			tabMain.SelectedIndex = index;
		}

		public void RefreshForm(int index)
		{
			BaseViewForm form = tabMain.TabPages[index].Controls[0] as BaseViewForm;     
			if (form.RefreshForm != null)
			{
				form.RefreshForm.Invoke();
			}
		}

		public void RefreshForm()
		{
            RefreshForm(tabMain.SelectedIndex);
		}

		public void DisableLeave()
		{
			tabMain_PreventChangeTabPage(tabMain.SelectedIndex);
		}

		public void ChangeLanguage()
		{
			foreach (TabPage page in tabMain.TabPages)
			{
				foreach (Control form in page.Controls)
				{
					if (form is BaseViewForm)
					{
						(form as BaseViewForm).ChangeLanguage();
					}
				}
			}
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
            this.tabMain = new Mubea.GUI.CustomControl.FreshTabCtrl();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabMain.IsDisplayBorder = false;
            this.tabMain.ItemSize = new System.Drawing.Size(280, 55);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(417, 384);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 0;
            this.tabMain.TabTextFont = new System.Drawing.Font("YaHei Consolas Hybrid", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // BaseContainerForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(417, 384);
            this.Controls.Add(this.tabMain);
            this.Name = "BaseContainerForm";
            this.Load += new System.EventHandler(this.BaseContainerForm_Load);
            this.ResumeLayout(false);

		}

		private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshForm(tabMain.SelectedIndex);
		}

		private void tabMain_PreventChangeTabPage(int tabPageIndex)
		{
			BaseViewForm form = tabMain.TabPages[tabPageIndex].Controls[0] as BaseViewForm;
			form.DisableLeave();
		}
	}
}
