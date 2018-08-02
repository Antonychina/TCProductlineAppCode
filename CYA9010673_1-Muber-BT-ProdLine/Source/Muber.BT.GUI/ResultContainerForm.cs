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
	public partial class ResultContainerForm : BaseContainerForm
	{
		public ResultContainerForm()
		{
			InitializeComponent();
		}

		private void ResultContainerForm_Load(object sender, EventArgs e)
		{
            tabMain.LoadTableControl(typeof(CurrentOrder));
            tabMain.LoadTableControl(typeof(NewOrder));
		}
	}
}
