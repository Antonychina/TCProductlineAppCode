using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mubea.GUI.CustomControl;
using System.Threading.Tasks;
using Mubea.AutoTest.GUI;
using AH.AutoServer;
using AH.Network;
using CommonHelper;

namespace Mubea.AutoTest.GUI
{
    public partial class NewOrder : BaseViewForm
    {
        private ToolTip _logintip = new ToolTip();
        private ProductBOMs _newOrderProductBOMs = new ProductBOMs();
        //private ProductionLine _productionLine = new ProductionLine();

        public NewOrder()
        {
            InitializeComponent();

            InitializeListView(listView_NewOrder);
         
            //ClientStatus.ChangeStatus += setStaionState;
            //ClientGUFStatus.ChangeStationState += updateStaionState;

        }

        private void HomeOverView_Load(object sender, EventArgs e)
        {
         
            this.txtNewOrderNo.Focus();
            
            this.Activate();
        }

        private void Load_Lsv_NewOrderBOM()
        {

            listView_NewOrder.Items.Clear();

            List<MaterialInfo> materialsList = new List<MaterialInfo>();

            if(this.textProdNo.Text.Trim().Length>0)
                materialsList = _newOrderProductBOMs.GetProductBOMList(this.textProdNo.Text.Trim());

            int serialNumber = 1;
            foreach (var Bominfo in materialsList)
            {
                ListViewItem lsvBOMSubItem = new ListViewItem();

                lsvBOMSubItem.Text = serialNumber++.ToString();
                lsvBOMSubItem.SubItems.Add(Bominfo.MaterialNo.Trim());
                lsvBOMSubItem.SubItems.Add(Bominfo.MaterialName.Trim());
                lsvBOMSubItem.SubItems.Add((Bominfo.MaterialPcs * int.Parse(this.textNewOrderCount.Text.Trim())).ToString());

                listView_NewOrder.Items.Add(lsvBOMSubItem);
            }

            this.txtNewOrderNo.Focus();
        }





        private void btnNewOrderOK_Click(object sender, EventArgs e)
        {
            //query BOM list according to Product No.
            if ((this.txtNewOrderNo.Text.Trim().Length > 0) && (this.textProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.textNewOrderCount.Text.Trim().ToString())))
            {
                //check the order and product in db? if yes, just query the BOM list, else insert the order to db first, then query the BOM list
                CheckOrder();
               
                Load_Lsv_NewOrderBOM();

                SystemLogs.WriteSystemLog(Login.UserName, "Scanning new order " + this.txtNewOrderNo.Text.Trim() + " at " + Login.CurrentProductionLine, DateTime.Now);

                if (this.comboBox_Prepare.SelectedIndex == 0)
                {
                    sendMaterialReq(); //发送备料请求到库房App
                    ShowLoginTip("新订单备料请求已发送到库房.");
                }
            }
            else
            {
                SpMessageBox.Show("请检查订单号，产品号，和订单数量的输入", "订单查询");
            }

        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult diaresult = SpMessageBox.Show("确定要清空当前订单信息吗？", "清空信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (diaresult == DialogResult.OK)
            {
                if (UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.SuperAdmin || UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.Administrator)
                {
                    this.listView_NewOrder.Items.Clear();
                    this.txtNewOrderNo.Clear();
                    this.textNewOrderCount.Clear();
                    this.textProdNo.Clear();
                    this.comboBox_Prepare.SelectedIndex = -1;
                }
                else
                {
                    SpMessageBox.Show("当前用户没有权限执行清空操作，请联系 Administrator.", "清空信息");
                }

            }
        }

        private void sendMaterialReq()
        {
            try
            {
                //调用tcp/ip 协议，发送备料请求到库房App
                MainForm.CheckConnectionWithServer();
                Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(),
                                          this.txtNewOrderNo.Text.Trim() + "|" + this.textProdNo.Text.Trim() + "|" + this.textNewOrderCount.Text.Trim(),
                                          AH.Network.MsgType.NewDut);
            }
            catch (Exception e)
            {
                SpMessageBox.Show("告警：" + e.Message, "通信-库房");
            }

            //ProductionLine.AddOrderAndProductionLineInfo(Login.CurrentProductionLineNumber, 
            //                                              this.txtNewOrderNo.Text.Trim(), 
            //                                              int.Parse(this.textNewOrderCount.Text.Trim()), 
            //                                              this.textProdNo.Text.Trim());
            SystemLogs.WriteSystemLog(Login.UserName, "Sending materials request for new order " + this.txtNewOrderNo.Text.Trim() + " at " + Login.CurrentProductionLine, DateTime.Now);
        }




        private void CheckOrder()
        {
            List<MaterialInfo> materialsList = new List<MaterialInfo>();
            if ((this.textProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.textNewOrderCount.Text.Trim().ToString())))
            {
                OrderInfo.CheckOrderExsit(this.txtNewOrderNo.Text.Trim(), this.textProdNo.Text.Trim(), int.Parse(this.textNewOrderCount.Text.Trim().ToString()));
            }
            else
            {
                SpMessageBox.Show("请检查是否输入了产品号和订单数量？", "订单查询");
            }

        }


        private void ShowLoginTip(string tipstring)
        {
            Point pt = new Point(this.btnNewOrderOK.Location.X, this.btnNewOrderOK.Location.Y + this.btnNewOrderOK.Height);
            pt.Offset(20, 20);
            _logintip.UseFading = true;
            _logintip.RemoveAll();
            _logintip.Show(tipstring, this, pt, 2000);
        }

    }
}
