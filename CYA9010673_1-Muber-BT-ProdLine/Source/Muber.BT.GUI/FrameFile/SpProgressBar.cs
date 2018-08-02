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
    public partial class SpProgressBar : BaseDialog
    {
        public SpProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set progress value
        /// </summary>
        /// <param name="value"></param>
        public void SetProgressValue(int value)
        {
            this.progressBar1.Value = value;
            // Close here  
            if (value > this.progressBar1.Maximum - 2) this.Close();
        }  
    }
}
