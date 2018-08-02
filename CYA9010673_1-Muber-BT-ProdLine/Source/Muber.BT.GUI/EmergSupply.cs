using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using System.Management;
using Microsoft.Reporting.WinForms;
using CommonHelper;
using AH.AutoServer;
using AH.Network;

namespace Mubea.AutoTest.GUI
{
    public partial class EmergSupply : BaseViewForm
    {
        private ToolTip _logintip = new ToolTip();

        public EmergSupply()
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "//MaterialPics//" + "sample.jpg"); 
            RefreshForm += EmergSupply_Refresh;
        }
        private void EmergSupply_Load(object sender, EventArgs e)
        {
            // 根据订单号 把零件号填充到comboMaterialNo 下
            if (CurrentOrder.CurrentOrderMaterialsList.Count > 0)
            {
                this.comboMaterialNo.Items.Clear();

                foreach (var bomList in CurrentOrder.CurrentOrderMaterialsList)
                {
                    this.comboMaterialNo.Items.Add(bomList.MaterialNo);
                }
            }
        }

        private void EmergSupply_Refresh()
        {
         // 根据订单号 把零件号填充到comboMaterialNo 下
            if (CurrentOrder.CurrentOrderMaterialsList.Count > 0)
            {
                this.comboMaterialNo.Items.Clear();

                foreach(var bomList in CurrentOrder.CurrentOrderMaterialsList)
                {
                    this.comboMaterialNo.Items.Add(bomList.MaterialNo.Trim());
                }
            }
        }

        private void btn_EmergSupplyOK_Click(object sender, EventArgs e)
        {
            if (this.comboMaterialNo.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.txtMaterialCount.Text.Trim()))
            {
                try
                {
                    //调用tcp/ip 协议，发送紧急备料请求到库房App
                    //Server.
                    MainForm.CheckConnectionWithServer();
                    Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(),
                                              this.comboMaterialNo.Text.Trim(),
                                              AH.Network.MsgType.Emerg);
                }
                catch (Exception excpt)
                {
                    SpMessageBox.Show("告警：" + excpt.Message, "通信-库房");
                }

                SystemLogs.WriteSystemLog(Login.UserName, "Sending urgent request for material " + this.comboMaterialNo.Text.Trim() + " at " + Login.CurrentProductionLine, DateTime.Now);

                ShowLoginTip("紧急Call料请求已发送.");
            }
            else
            {
                SpMessageBox.Show("请检查订单号，产品号，和订单数量的输入", "订单查询");
            }
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.comboMaterialNo.Text = "";
            this.txtMaterialCount.Clear();
            this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "//MaterialPics//" + "sample.jpg"); 
        }

        private void comboMaterialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string picName = comboMaterialNo.Text.Trim();
            this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "//MaterialPics//" + picName+".jpg"); 
        }


        private void ShowLoginTip(string tipstring)
        {
            Point pt = new Point(this.btn_EmergSupplyOK.Location.X, this.btn_EmergSupplyOK.Location.Y + this.btn_EmergSupplyOK.Height);
            pt.Offset(20, 20);
            _logintip.UseFading = true;
            _logintip.RemoveAll();
            _logintip.Show(tipstring, this, pt, 2000);
        }

        private void comboMaterialNo_Click(object sender, EventArgs e)
        {
            //this.comboMaterialNo.Items.Clear();

            //if (MainForm.CurrentOrderBOMList.Count > 0)
            //{
            //    foreach (var bom in MainForm.CurrentOrderBOMList)
            //    {
            //        this.comboMaterialNo.Items.Add(bom.MaterialNo.Trim());
            //    }
            //}
        }
    }
}
