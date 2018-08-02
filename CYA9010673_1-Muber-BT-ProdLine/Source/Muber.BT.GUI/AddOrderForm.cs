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
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest.GUI
{
    public partial class AddOrder : BaseViewForm
    {
     
        public AddOrder()
        {
            InitializeComponent();

            //ClientStatus.ChangeStatus += setStaionState;
            //ClientGUFStatus.ChangeStationState += updateStaionState;

        }

        private void HomeOverView_Load(object sender, EventArgs e)
        {
         
            this.txtAddOrderNo.Focus();
            
            this.Activate();
        }

    

        private void btn_AddOrder_Click(object sender, EventArgs e)
        {
          
            if ((this.txtAddOrderNo.Text.Trim().Length > 0) && (this.txtAddProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.txtAddOrderCount.Text.Trim())))
            {

                 DialogResult diaresult = SpMessageBox.Show("确定要补充该订单吗？", "订单补充", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                 if (diaresult == DialogResult.OK)
                 {

                     if (OrderInfoBLL.OrderAlreadyExsit(this.txtAddOrderNo.Text.Trim()))
                     {
                         if(OrderInfo.UpdateOrderCount(this.txtAddOrderNo.Text.Trim(), this.txtAddProdNo.Text.Trim(), int.Parse(this.txtAddOrderCount.Text.Trim())))
                             SpMessageBox.Show("该订单数量补充完成", "订单补充"); 
                     }
                     else
                     {
                         SpMessageBox.Show("该订单号不存在，请检查该订单号是否生效", "订单补充");
                     }


                     SystemLogs.WriteSystemLog(Login.UserName, "add order count:" + this.txtAddOrderCount.Text.Trim() + " for order " + this.txtAddOrderNo.Text.Trim() + " at " + Login.CurrentProductionLine, DateTime.Now);
                 }
            }
            else
            {
                SpMessageBox.Show("请检查订单号，产品号，和订单数量的输入", "订单补充");
            }

        }
    }
}
