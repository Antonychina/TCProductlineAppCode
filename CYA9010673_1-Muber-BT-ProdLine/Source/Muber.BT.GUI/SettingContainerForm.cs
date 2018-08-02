using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mubea.AutoTest.GUI
{
	public partial class SettingContainerForm : BaseContainerForm
	{
        public SettingContainerForm()
		{
			InitializeComponent();
		}

        private void SettingContainerForm_Load(object sender, EventArgs e)
		{
            tabMain.LoadTableControl(typeof(MaterialMaintForm));
            tabMain.LoadTableControl(typeof(ProductMaintForm));

		}
	}
}
