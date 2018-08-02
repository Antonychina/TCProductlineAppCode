using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO.Pipes;
using Mubea.GUI.CustomControl;
using System.Threading.Tasks;
using Mubea.AutoTest.GUI;
using AH.AutoServer;
using AH.Network;
using GP.MAGICL6800.ORM;
using CommonHelper;

namespace Mubea.AutoTest.GUI
{
    public partial class CurrentOrder : BaseViewForm
    {
        public static int timerCounter = 0;
        public static string CurrentProductOrder = "";
        public static List<MaterialInfo> CurrentOrderMaterialsList = new List<MaterialInfo>();
        private ToolTip _logintip = new ToolTip();

        private ProductBOMs _productBOMs = new ProductBOMs();

        public CurrentOrder()
        {
            InitializeComponent();
            
            InitializeListView(listView_CurrentOrder);
            
            //ClientStatus.ChangeStatus += setStaionState;
            //ClientGUFStatus.ChangeStationState += updateStaionState;
            
        }

        private void HomeOverView_Load(object sender, EventArgs e)
        {

            //Load_Lsv_BOMList();
            this.txtOrderNo.Focus();

            this.Activate();

        }

        private void Load_Lsv_BOMList()
        {
            listView_CurrentOrder.Items.Clear();
            MainForm.CurrentOrderBOMList.Clear();

            List<MaterialInfo> materialsList = new List<MaterialInfo>();

            if(this.textProdNo.Text.Trim().Length>0)
            {
                materialsList = _productBOMs.GetProductBOMList(this.textProdNo.Text.Trim());
                materialsList = materialsList.OrderBy(o => o.MaterialSeq).ToList();//升序
                CurrentOrderMaterialsList = materialsList;

                if(CurrentOrderMaterialsList.Count > 1)
                {
                    for(int i = 1; i < CurrentOrderMaterialsList.Count; i++)
                    {
                        if (CurrentOrderMaterialsList[i].MaterialNo == CurrentOrderMaterialsList[i - 1].MaterialNo)
                        {
                            
                            for (int j = i; j < CurrentOrderMaterialsList.Count; j++)
                            {
                                CurrentOrderMaterialsList[j].MaterialSeq = (byte)(CurrentOrderMaterialsList[j].MaterialSeq + 1);
                            }
                        }
                    }
                }
            }

            int serialNumber = 1;
      
            foreach (var Bominfo in materialsList)
            {
                ListViewItem lsvBOMSubItem = new ListViewItem();

                lsvBOMSubItem.Text = serialNumber++.ToString();
                lsvBOMSubItem.SubItems.Add(Bominfo.MaterialNo.Trim());
                lsvBOMSubItem.SubItems.Add(Bominfo.MaterialName.Trim());
                lsvBOMSubItem.SubItems.Add(Bominfo.MaterialSeq.ToString());

                //string aaa = MainForm.MaterialAndLabelList[0].MaterialNo;
                listView_CurrentOrder.Items.Add(lsvBOMSubItem);
                MainForm.CurrentOrderBOMList.Add(new MaterialAndLabel() { MaterialNo = Bominfo.MaterialNo.Trim(), LabelsNo = serialNumber - 1 });
            }
        }

        private void CheckOrder()
        {
            List<MaterialInfo> materialsList = new List<MaterialInfo>();
            if ((this.textProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.textOrderCount.Text.Trim().ToString())))
            {
                OrderInfo.CheckOrderExsit(this.txtOrderNo.Text.Trim(), this.textProdNo.Text.Trim(), int.Parse(this.textOrderCount.Text.Trim().ToString()));
            }
            else
            {
                SpMessageBox.Show("请检查是否输入了产品号和订单数量？","订单查询");
            }

        }



        private void UpdateProductionLineInfo()
        {
           
            if ((this.textProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.textOrderCount.Text.Trim().ToString())))
            {//string _orderNo, byte _productLineNo, string _productNo, int _orderCount
               
                ProductionLine.CheckProductionLineExsit(this.txtOrderNo.Text.Trim(), Login.CurrentProductionLineNumber, this.textProdNo.Text.Trim(), int.Parse(this.textOrderCount.Text.Trim().ToString()));

                if (!ProductionLineInfoBLL.CheckThisProductionLineInfo(Login.CurrentProductionLineNumber, this.txtOrderNo.Text.Trim()))  //如果当前产线号存在 productionline表里，需要double check order号是否也一样？ 如果一样则OK, 如果不一样，则需要告警：还没切线呢
                {
                    SpMessageBox.Show("当前产线还未切线，不能输入进行新订单生产，请先执行切线操作", "订单查询");
                }
            }
            else
            {
                SpMessageBox.Show("请检查是否输入了正确的订单号和产品号.","订单查询");
            }

        }


        private void btnDownloadTags_Click(object sender, EventArgs e)
        {
            //MainForm.CheckComPorts();

            if (this.listView_CurrentOrder.Items.Count < 1)
            {
                SpMessageBox.Show("BOM清单为空，请重新查询订单.", "电子标签下发");
                return;
            }

            if (this.listView_CurrentOrder.Items.Count > MainForm.SupportedSerialPortNoList.Count)
            {
                SpMessageBox.Show("串口设备少于BOM清单数，请检查电子标签设备连接.", "电子标签下发");
                return;
            }
         
            
            int i;
            for(i = 0; i < this.listView_CurrentOrder.Items.Count; i++ )
            {
                if (MainForm.SerialPortsList[i] != null)
                {
                    SerialPort sp = MainForm.SerialPortsList[i]; // find the right 实例化串口通讯类 from the list

                    foreach (var sptmp in MainForm.SerialPortsList) 
                    {
                        if (int.Parse(System.Text.RegularExpressions.Regex.Replace(sptmp.PortName, @"[^0-9]+", "").Trim()) == int.Parse(CurrentOrderMaterialsList[i].MaterialSeq.ToString().Trim()) + 9)
                        { //匹配对应的串口， 根据物料的位置号， 获取对应的电子屏COM号, 
                            sp = sptmp;
                            break;
                        }
                    }


                    if (!sp.IsOpen)
                    {
                        try
                        {
                            sp.Open();//打开串口
                        }
                        catch
                        {
                            SpMessageBox.Show("电子标签屏幕所对应的串口" + sp.PortName + "异常, 请检查串口连接.","下发电子标签");
                        }
                    }

                    if (sp.IsOpen)
                    {
                        try
                        {
                            if (this.listView_CurrentOrder.Items[i].SubItems[1].Text.Trim().Length > 0)
                            {   // key codes
                                //packaging switch pictures cmd
                                byte[] cmds = { 0x5A, 0xA5, 0x07, 0x82, 0x00, 0x84, 0x5A, 0x01, 0x00, 0xFF };
                                cmds[9] = findLabelNoByMaterialNo(listView_CurrentOrder.Items[i].SubItems[1].Text.Trim()); // set the picture ID according to the 零件号
                                //cmds[9] = 0x04;  // for debug, will delete later

                                //packaging clear desc
                                byte[] clear = { 0x5A, 0xA5, 0x20, 0x82, 0x10, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                clear[5] = findLabelNoByMaterialNo(listView_CurrentOrder.Items[i].SubItems[1].Text.Trim());
                                //clear[5] = 0x40; // for debug, will delete later

                                //packaging  desc
                                byte[] desps = { 0x5A, 0xA5, 0x20, 0x82, 0x10, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                desps[5] = findLabelNoByMaterialNo(listView_CurrentOrder.Items[i].SubItems[1].Text.Trim());
                                desps[5] = 0x40; // for debug, will delete later

                                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(this.listView_CurrentOrder.Items[i].SubItems[1].Text.Trim());

                                byte[] byteArray1 = System.Text.Encoding.Default.GetBytes(this.listView_CurrentOrder.Items[i].SubItems[1].Text.Trim());

                                for (int j = 0; j < byteArray.Length; j++)
                                {
                                    desps[6 + j] = byteArray[j];
                                }


                                byte[] readbuffer = new byte[50];

                                try
                                {
                                    sp.Write(cmds, 0, cmds.Length);//发送切换picture指令
                                    System.Threading.Thread.Sleep(50);
                                    //sp.Read(readbuffer, 0, 20);  //接收串口返回消息
                                }
                                catch (Exception ex)
                                {
                                    SpMessageBox.Show(sp.PortName + "告警：" + ex.Message, "下发电子标签");
                                }
                                //Array.Clear(readbuffer, 0 , 50);
                                //sp.Write(clear, 0, clear.Length);//clear零件号描述 的指令  
                                //sp.Read(readbuffer, 0, 20);

                                //Array.Clear(readbuffer, 0, 50);
                                //sp.Write(desps, 0, desps.Length);//发送更新零件号描述 指令  
                                //sp.Read(readbuffer, 0, 20);

                                //LogerHelper.ToLog(this.listView_CurrentOrder.Items[i].SubItems[1].ToString() + "|" + "10" + " to " + sp.PortName, true);
                                LogerHelper.ToLog(desps + "|" + "10" + " to " + sp.PortName, true);

                                //if (i == 3)
                                //{
                                //    break;
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            SpMessageBox.Show(sp.PortName + "告警：" + ex.Message, "下发电子标签");
                        }
                        sp.Close();

                    }
                }
            }

            if (i >= this.listView_CurrentOrder.Items.Count)
            {
                ShowLoginTip("电子标签下发成功.");

                // start the timer to detect the material box usages/consumption
                MainForm.timer_ReadSensors.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                MainForm.timer_ReadSensors.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
                MainForm.ModubusReadCounter = 0;
                MainForm.TimerFinished = true;
                //MainForm.SensorsValues = new byte[8]; //切线之前，默认认为货架上的物料被清理干净了
            }
        }

        private void btn_SwitchLine_Click(object sender, EventArgs e)
        {
            
            DialogResult diaresult = SpMessageBox.Show("确定要切线吗？", "切线操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (diaresult == DialogResult.OK)
            {
                if (UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.SuperAdmin || UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.Administrator)
                {
                    try
                    {
                        if (ProductionLineMaterial.GetMaterialsTotalRemainConsump(Login.CurrentProductionLineNumber, this.txtOrderNo.Text.Trim()) > 0) //切线前，先判断是否已经扣料了
                        {
                            SpMessageBox.Show("该订单生产还没有进行扣料，请先进行扣料然后再切线.", "生产切线");

                            try
                            {
                                Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(),
                                              this.txtOrderNo.Text.Trim() + "|" + this.textProdNo.Text.Trim() + "|" + this.textOrderCount.Text.Trim(),
                                              AH.Network.MsgType.Deduct);
                            }
                            catch (Exception excpt)
                            {
                                SpMessageBox.Show("告警：" + excpt.Message, "通信-库房");
                            }
                            return;
                        }
                    }
                    catch (Exception excpt)
                    {
                        SpMessageBox.Show("告警：" + excpt.Message, "生产切线");
                        return;
                    }
                   
                    SystemLogs.WriteSystemLog(Login.UserName, "Switch order from " + this.txtOrderNo.Text.Trim() + "to other order at " + Login.CurrentProductionLine, DateTime.Now);


                    ProductionLine.RemoveOrderAndProductionLineInfo(Login.CurrentProductionLineNumber, this.txtOrderNo.Text.Trim());
                        

                    //切线时，没有必要删除database里这个order.....
                    this.listView_CurrentOrder.Items.Clear();
                    this.txtOrderNo.Clear();
                    this.textOrderCount.Clear();
                    this.textProdNo.Clear();

                    CurrentOrderMaterialsList.Clear();

                    // stop the timer when switch orders/producitonline
                    MainForm.timer_ReadSensors.Enabled = false;
                }
                else
                {
                    SpMessageBox.Show("当前用户没有权限执行切线操作，请联系 Administrator.", "切线操作");
                }
            }
        }


        private void btnOrderOK_Click(object sender, EventArgs e)
        {
            if ((this.txtOrderNo.Text.Trim().Length > 0) && (this.textProdNo.Text.Trim().Length > 0) && (RegularHelper.isNumericString(this.textOrderCount.Text.Trim().ToString())))
            {
                //check the order and product in db? if yes, just query the BOM list, else insert the order to db first, then query the BOM list
                CheckOrder();

                UpdateProductionLineInfo();
                //this.listView_CurrentOrder.Items.Clear();
                Load_Lsv_BOMList();
                CurrentProductOrder = this.txtOrderNo.Text.Trim();
                SystemLogs.WriteSystemLog(Login.UserName, "Scanning order " + this.txtOrderNo.Text.Trim() + " at " + Login.CurrentProductionLine, DateTime.Now);
            }
            else
            {
                SpMessageBox.Show("请检查订单号，产品号，和订单数量的输入.", "订单查询");
            }
            
        }


        #region Private_Methods
        // find 标识位，写到5寸电子屏的地址（这个地址由小于提供）
        private byte findLabelNoByMaterialNo(string _materialNo)
        {
            byte labelNo = 0xFF;

            foreach (var temp in MainForm.MaterialAndLabelList)
            {
                if (temp.MaterialNo == _materialNo)
                {
                    return (byte)temp.LabelsNo;
                }
            }
            return labelNo;
        }


        private void ShowLoginTip(string tipstring)
        {
            Point pt = new Point(this.btnDownloadTags.Location.X, this.btnDownloadTags.Location.Y + this.btnDownloadTags.Height);
            pt.Offset(20, 20);
            _logintip.UseFading = true;
            _logintip.RemoveAll();
            _logintip.Show(tipstring, this, pt, 2000);
        }

        #endregion Private_Methods


    }
}
