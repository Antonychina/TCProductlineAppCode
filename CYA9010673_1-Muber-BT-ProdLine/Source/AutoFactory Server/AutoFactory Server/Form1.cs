using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AH.AutoServer;

namespace AH
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {

            Server S = new Server();
            S.InitServer("", "", 0, 0, 0, false);

        }
    }
}
