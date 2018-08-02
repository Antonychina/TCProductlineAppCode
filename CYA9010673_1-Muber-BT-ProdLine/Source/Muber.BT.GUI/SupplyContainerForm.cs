﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mubea.AutoTest.GUI
{
	public partial class SupplyContainerForm : BaseContainerForm
	{
        public SupplyContainerForm()
		{
			InitializeComponent();
		}

        private void SupplyContainerForm_Load(object sender, EventArgs e)
		{
            tabMain.LoadTableControl(typeof(EmergSupply));
            tabMain.LoadTableControl(typeof(AddOrder));
		}
	}
}
