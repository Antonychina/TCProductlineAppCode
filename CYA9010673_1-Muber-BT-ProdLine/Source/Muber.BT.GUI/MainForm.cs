using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mubea.GUI.CustomControl;
using System.Runtime.InteropServices;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO.Ports;
using System.IO.Pipes;
using CommonHelper;
using Mubea.AutoTest.GUI;
using Microsoft.Win32;
using Mubea.AutoTest.GUI.Localization;
using AH.AutoServer;

namespace Mubea.AutoTest.GUI
{
    public enum WorkForm
    {
        HomePage = 0,
        EmergencySupply,
        SystemLog,
        UserAuthority,
        ProdSetting
    }

    public partial class MainForm : Form
    {
        static private Form[] _formArr;

        //static private int TimeRemain;
        //static private byte machineStatus_UnknownTimes = 0;
        //static private long ReferenceTimeSec;

        /// <summary>
        /// 等待窗口初始化结束事件
        /// </summary>
        private AutoResetEvent _autoEvent = new AutoResetEvent(false);

        private ConfigHelper _config = new ConfigHelper();
        public static System.Timers.Timer timer_ReadSensors;
        public static bool TimerFinished = true;
        public static byte[] SensorsValues = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private bool formClosed = false;
        public static int ModubusReadCounter = 0;
        public static byte[] ReadbufferFirstmodbus = new byte[5];
        public static byte[] ReadbufferSecondmodbus = new byte[5];
        public static List<MaterialAndLabel> CurrentOrderBOMList = new List<MaterialAndLabel>();   // 当前生产的产品的BOM list
        public static List<MaterialAndLabel> MaterialAndLabelList = new List<MaterialAndLabel>();  // 从配置文件里读到的物料号和对应寄存器地址的对应关系
        public static List<SerialPortAndADAM> SerialPortNoAndADAMList = new List<SerialPortAndADAM>();

        public static List<string> PlanedSerialPortNoList = new List<string>();
        public static List<string> SupportedSerialPortNoList = new List<string>();
        public static List<SerialPort> SerialPortsList = new List<SerialPort>();

        public static List<string> PlanedModbusPortNoList = new List<string>();
        public static List<string> SupportedModbusPortNoList = new List<string>();
        public static List<SerialPort> ModbusPortsList = new List<SerialPort>();


        static public bool DisableSwitchForm { get; set; }	//是否允许切换界面

        public MainForm()
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;	//启动时隐藏主界面

            InitializeComponent();
            timer_ReadSensors = new System.Timers.Timer(2000); //实例化Timer类，设置间隔时间为2000 ms
            timer_ReadSensors.Elapsed += ReadSensorsTimer_Tick;//到达时间的时候执行事件；

            this.IsMdiContainer = true;
            this.splitBack.IsSplitterFixed = true;

            this.BackColor = Color.FromArgb(85, 110, 128);

            this.splitBack.Panel1.BackColor = Color.FromArgb(156, 175, 189);
            this.titleBar.BackColor = Color.FromArgb(156, 175, 189);

            _config.FileName = "AutoTestConfig.xml";

            //SetupShakeHandTimer();   //debug for GUI

            SideBarHighLight.SideBarUnreadMessageCount += UpdateAlarmMessageCount;
            LogicalHandler.DateFormat += UpdateDateTimeFormat;
            RobotPLCStatus.ChangeRobotPLCState += updateRobotPLCState;

            //ptbConnect.Image = Properties.Resources.unconnect;
            //			DeviceHelper.Instance.InitLogDelegate(ToLog);

            // 			var Task2 = System.Threading.Tasks.Task.Factory.StartNew(() =>
            // 			{
            // 				for (int i = 0; i < 10000; i++)
            // 				{
            // 					LogHelper.ToFile("aaa.txt", string.Format("aaaa{0}\r\n", i));
            // 				}
            // 			});
            // 
            // 			var Task3 = System.Threading.Tasks.Task.Factory.StartNew(() =>
            // 			{
            // 				for (int i = 0; i < 10000; i++)
            // 				{
            // 					LogHelper.ToFile("aaa.txt", string.Format("bbbb{0}\r\n", i));
            // 				}
            // 			}); 
        }

        static MainForm()
        {
            DisableSwitchForm = false;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            try
            {
                //SystemSleepManagement.PreventSleep(true);	//阻止系统待机

                InitSystem();

                InitTitleBar();
                InitSideBar();

                _autoEvent.Set();

                InitAppConfig();

                this.label_LoginName.Text = Login.UserName;
                this.label_ProductionLine.Text = Login.CurrentProductionLine;
               
            }
            catch (Exception ex)
            {
                //LogHelper.ToLog("UnkownErr.txt", ex);
            }
        }

        private void InitTitleBar()
        {
            List<TitleBtnItem> itemArr = new List<TitleBtnItem>();

            TitleBtnItem item = new TitleBtnItem();

            item.Text = "帮助";
            item.BackgroundImage = Properties.Resources.help;
            itemArr.Add(item);

            item.Text = "退出";
            item.BackgroundImage = Properties.Resources.power;
            itemArr.Add(item);

            titleBar.InitCtrl(itemArr);
            titleBar.BtnClick += TitleBarClick;
        }

        private void InitSideBar()
        {
            //sidebar.BackColor = Color.FromArgb(37, 40, 45);

            List<TitleBtnItem> itemArr = new List<TitleBtnItem>();

            TitleBtnItem item = new TitleBtnItem();
            item.Text = "生产订单";
            item.BackgroundImage = Properties.Resources.order;
            item.SelectedBKImage = Properties.Resources.order;
            itemArr.Add(item);

            item.Text = "紧急补料";
            item.BackgroundImage = Properties.Resources.emerg;
            item.SelectedBKImage = Properties.Resources.emerg;
            itemArr.Add(item);

            item.Text = "操作日志";
            item.BackgroundImage = Properties.Resources.log_new;
            item.SelectedBKImage = Properties.Resources.log_new;
            itemArr.Add(item);

             if (Login.CurrentProductionLineNumber == 0)
            {
                item.Text = "用户管理";
                item.BackgroundImage = Properties.Resources.user_new;
                item.SelectedBKImage = Properties.Resources.user_new;
                itemArr.Add(item);

                item.Text = "生产设置";
                item.BackgroundImage = Properties.Resources.prod_setting;
                item.SelectedBKImage = Properties.Resources.prod_setting;
                itemArr.Add(item);
            }

            sidebar.EnableDisplayTime = true;
            sidebar.InitCtrl(itemArr);
            sidebar.BtnClick += SibarClick;
            sidebar.SetSidebarSelected(0);

            _formArr = new Form[itemArr.Count];

            SetCarrierForm((int)WorkForm.HomePage, typeof(ResultContainerForm));
            SetCarrierForm((int)WorkForm.EmergencySupply, typeof(SupplyContainerForm));
            SetCarrierForm((int)WorkForm.SystemLog, typeof(SystemLog));

            if (Login.CurrentProductionLineNumber == 0)
            {
                SetCarrierForm((int)WorkForm.UserAuthority, typeof(UserAuthority));
                SetCarrierForm((int)WorkForm.ProdSetting, typeof(SettingContainerForm));
            }

            for (int i = 0; i < _formArr.Length; i++)
            {
                if (_formArr[i] != null)
                {
                    _formArr[i].Hide();
                }
            }
            _formArr[0].Show();
        }

        private void InitAppConfig()
        {
            ReadAppConfig(); //read configurations
            
            CheckComPorts();  //check the serial ports for label screens

            CheckModbusComPorts(); //check the serial ports for sensors

        }


        private void ReadAppConfig()
        {

            #region Read_PreviousShelfStatus
            //ShelfStatus
            string shelfStatusArraystring = _config.GetValue(@"Settings", @"ShelfStatus");
            string[] shelfStatusArray = _config.GetValue(@"Settings", @"ShelfStatus").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < shelfStatusArray.Length; i++)
            {
                SensorsValues[i] = byte.Parse(shelfStatusArray[i]);

            }
            #endregion

            #region Read_AllComSerialPorts
            string configTxt_PlanedSerialPorts = LogerHelper.ReadConfigFile("BT_SerialPorts.cfg");// read materials <-> labes config
            string[] serialPortsArray = configTxt_PlanedSerialPorts.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < serialPortsArray.Length; i++)
            {
                //string configtmp = "";
                if (serialPortsArray[i].Length == 5)
                {
                    //configtmp = 
                    PlanedSerialPortNoList.Add(serialPortsArray[i].Trim());
                }

            }
            #endregion
            

            #region Read_MaterialAndLabel
            string configTxt_MaterialAndLabel = LogerHelper.ReadConfigFile("BT_MaterialsAndLabels.cfg");// read materials <-> labes config
            string[] sysLogArray = configTxt_MaterialAndLabel.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sysLogArray.Length;  i++)
            {
                MaterialAndLabel configtmp = new MaterialAndLabel();
                if(sysLogArray[i].Length>=3)
                {
                    configtmp.MaterialNo = sysLogArray[i].Trim().Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    configtmp.LabelsNo = int.Parse(sysLogArray[i].Trim().Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    MaterialAndLabelList.Add(configtmp);
                }
                
            }
            #endregion



            #region Read_SerialPortAndADAM

            string configTxt_SerialPortAndADAM = LogerHelper.ReadConfigFile("BT_ADAMAndComSerial.cfg");// read materials <-> labes config
            string[] _sysLogArray = configTxt_SerialPortAndADAM.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < _sysLogArray.Length; i++)
            {
                SerialPortAndADAM _configtmp = new SerialPortAndADAM();
                if (_sysLogArray[i].Length >= 3)
                {
                    _configtmp.SerialPort = _sysLogArray[i].Trim().Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    _configtmp.AdamForMaterialFrame = (_sysLogArray[i].Trim().Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    SerialPortNoAndADAMList.Add(_configtmp);
                    PlanedModbusPortNoList.Add(_configtmp.SerialPort);
                }
            }
            #endregion
            //(byte currentStatus, byte previousStatus, string comPortName, int halfPart 
            //findWhichBoxesChanged(0xA6, 0x24, "COM5", 0);
        }
            



        private void InitSystem()
        {
            Thread sendThread = new Thread(ThreadInitProc);
            sendThread.IsBackground = true;
            sendThread.Start();
        }

        private void ThreadInitProc()
        {
            this.Invoke(new Action(() =>
            {
                this.Location = new Point(0, 0);
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;	//显示主界面
            }));

        }

        /// <summary>
        /// 设备连接初始化（5寸电子屏）
        /// </summary>
        private void InitConectDeviceProc(AutoResetEvent portEvent)
        {
            try
            {
                // Bring out information if sample and reagent inserted

                string strOut = string.Format("Time:{0} begin CreateCA\r\n", DateTime.Now);
                

                strOut = string.Format("Time:{0} end CreateCA\r\n", DateTime.Now);
                

                portEvent.WaitOne();

            //    if (!DeviceHelper.Instance.IsCommPortOpen())
            //    {
            //        LogHelper.ToFile("OpenFailed.txt", string.Format("{0}\r\n", DateTime.Now));

            //        _autoEvent.WaitOne();
            //        return;
            //    }

            //    if (!MainConfigHelper.IsDebug)
            //    {
            //        bool bConnected = DeviceHelper.Instance.Connect();

            //        if (!bConnected)
            //        {
            //            DevParamsSrc paramsSrc = ParamsHelper.LoadDevParams(ParamsHelper.DevParams);    //设备参数
            //            if (paramsSrc == DevParamsSrc.NoneFile)
            //            {
            //                //报运行参数读取失败;
            //            }

            //            //this.Invoke(new Action(() => this.lab_ConnectionStatus.Text = "未连接"));

            //            MachineState.SetMachineDisconnected();

            //            LogHelper.ToFile("ConnectFail.txt", string.Format("{0}\r\n", DateTime.Now));

            //            _autoEvent.WaitOne();
            //            return;
            //        }

            //        //this.BeginInvoke(new Action(() => this.lab_ConnectionStatus.Text = "已连接"));

            //        MachineState.SetMachineConnected();

            //        GlobalData.MachineConnectState = true;

            //        if (DeviceHelper.Instance.SyncDevParams(ref ParamsHelper.DevParams))   //接收到中位机上传的参数
            //        {
            //            if (ParamsHelper.ReLoadDevParams(ParamsHelper.DevParams))   //存在未下发的参数文件
            //            {
            //                if (DeviceHelper.Instance.DownLoadParams(ParamsHelper.DevParams))
            //                {
            //                    ParamsHelper.SaveDevParams(ParamsHelper.DevParams, true);
            //                }
            //            }
            //            else
            //            {
            //                ParamsHelper.SaveDevParams(ParamsHelper.DevParams, true);   //将上传的参数保存到xml文件中区
            //            }
            //        }
            //        else    //没有接收到中位机上传的参数
            //        {
            //            DevParamsSrc paramsSrc = ParamsHelper.LoadDevParams(ParamsHelper.DevParams);

            //            if (DeviceHelper.Instance.DownLoadParams(ParamsHelper.DevParams))
            //            {
            //                if (paramsSrc == DevParamsSrc.UnDownloadDevParamsFile)
            //                {
            //                    ParamsHelper.SaveDevParams(ParamsHelper.DevParams, true);
            //                }
            //            }
            //        }

            //        SetupShakeHandTimer();

            //        this.Invoke((EventHandler)(delegate { label_MachineStatus.Visible = true; }));

            //        if (Login.NeedClearCup)
            //        {
            //            if (!DeviceHelper.Instance.ClearCup())
            //            {
            //                _isDevClearCupSuccess = false;

            //                _autoEvent.WaitOne();
            //                return;
            //            }
            //        }

            //        if (!DeviceHelper.Instance.Initalize())
            //        {
            //            _isDevInitalizeSuccess = false;

            //            _autoEvent.WaitOne();
            //            return;
            //        }

            //        //	暂时屏蔽

            //        if (DeviceHelper.Instance.QueryStackerStatus())
            //        {
            //            //						if (!DeviceHelper.Instance.PrimiingFlow())
            //            //						{
            //            //							_isDevPrimiingFlowSuccess = false;
            //            //						}
            //        }
            //        else
            //        {
            //            _isDevQueryStackerStatus = false;
            //        }

            //        _autoEvent.WaitOne();
            //    }
            }
            catch (Exception ex)
            {
            //    LogHelper.ToLog("UnkownErr.txt", ex);
            }
        }


        private void SetupShakeHandTimer()
        {
            timer_ReadSensors.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            timer_ReadSensors.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void SetCarrierForm(int index, Type type)
        {

            Form form = (Form)Activator.CreateInstance(type);
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Parent = splitBack.Panel2;
            form.Location = new Point(sidebar.Location.X + sidebar.Right, sidebar.Top);
            form.Size = new System.Drawing.Size(splitBack.Panel2.Width - sidebar.Width, splitBack.Panel2.Height);

            form.Show();

            _formArr[index] = form;

        }

        private void SibarClick(object sender, SelectChangeEventArgs e)
        {
            if (!DisableSwitchForm)
            {
                int index = e.SelectIndex;

                if (_formArr[index] != null)
                {
                    _formArr[index].Show();

                    // here add the fresh actions to fresh the form
                    RefreshForm(index);
                }

                for (int i = 0; i < _formArr.Length; i++)
                {
                    if (_formArr[i] != null)
                    {
                        if (index != i)
                        {
                            _formArr[i].Hide();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _formArr.Length; i++)
                {
                    if (_formArr[i].Visible)
                    {
                        if (i != e.SelectIndex)
                        {
                            sidebar.SetSidebarSelected(i);

                            if (_formArr[i] is BaseContainerForm)
                            {
                                (_formArr[i] as BaseContainerForm).DisableLeave();
                            }
                            else
                            {
                                BaseViewForm form = (_formArr[i] as BaseViewForm);

                                form.DisableLeave();
                            }
                        }

                        return;
                    }
                }
            }
        }

        private void TitleBarClick(object sender, SelectChangeEventArgs e)
        {
            if (!DisableSwitchForm)
            {

                if (e.SelectIndex.Equals(1))
                {
                    DialogResult diaresult = SpMessageBox.Show("确定退出？", "BT产线测试系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (diaresult == DialogResult.OK)
                    {
                        _config.SetValue(@"ShelfStatus", SensorsValues[0] + "," + SensorsValues[1] + "," + SensorsValues[2] + "," + SensorsValues[3]);
                   
                        //close all active serial ports
                        SerialPort sp = new SerialPort();//实例化串口通讯类
                        string[] serialPorts = SerialPort.GetPortNames();

                        foreach (string port in serialPorts)
                        {
                            sp.PortName = port;//串口号
                            // open 串口
                            try
                            {
                                if (sp.IsOpen)
                                {
                                    sp.Close();
                                    
                                }
                            }
                            catch (Exception ex)
                            {
                                SpMessageBox.Show("告警：" + ex.Message, "串口通信");
                            }
                        }

                        //string passWord = "";
                        //SpMessageBox.ShowInputDialog("Exit Auto Test System", out passWord, true);

                        if (true)
                        {
                            //string loginTrace = string.Format("{0}: User {1} Logout", DateTime.Now, Login.UserName);
                            //LogerHelper.ToAutoTestLogFile(loginTrace);
                            SystemLogs.WriteSystemLog(Login.UserName, "Logout from " + Login.CurrentProductionLine, DateTime.Now);

                            SendMsgToServer.ClearAliveConnection();
                            formClosed = true;
                            this.Close(); //close the gui
                        }
                        //else
                        //{
                        //    SpMessageBox.Show("Please input the correct password! or contact the Administrator.", "Reset Tester");

                        //    return;
                        //}

                    }
                }
                else
                {
                    string path = GET_AFAM_Path();
                    if (path != "")
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = path + "\\Muber_BT_Test_Helper.pdf";
                        process.Start();
                    }
                    else
                    {
                        SpMessageBox.Show("Please contact Administrator");
                    }
                }
            }
            else
            {
                SpMessageBox.Show(ResourceLang.AddOrEditActionWarning);	//"当前界面正处于添加或编辑状态，不能进行其他操作！"
            }
        }
        private string GET_AFAM_Path()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
            foreach (string subkeyname in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(subkeyname);
                object disname = subkey.GetValue("DisplayName");
                if (disname != null)
                {
                    if (disname.ToString().Contains("TeamViewer"))
                    {
                        string ss = subkey.GetValue("InstallLocation").ToString();
                        return ss;
                    }
                }

            }
            return "";
        }

        static public void SwitchTo(WorkForm formIndex, int subIndex)
        {
            int index = (int)formIndex;
            if (_formArr[index] != null)
            {
                (_formArr[index] as BaseContainerForm).SwitchTo(subIndex);

                _formArr[index].Show();
            }

            for (int i = 0; i < _formArr.Length; i++)
            {
                if (_formArr[i] != null)
                {
                    if (index != i)
                    {
                        _formArr[i].Hide();
                    }
                }
            }
        }

        static public void RefreshForm(WorkForm formIndex, int subIndex)
        {
            int index = (int)formIndex;
            if (_formArr[index] != null)
            {
                (_formArr[index] as BaseContainerForm).RefreshForm(subIndex);
            }
        }

        static public void RefreshForm(int index)
        {
            if (_formArr[index] != null)
            {
                if (_formArr[index] is BaseContainerForm)
                {
                    (_formArr[index] as BaseContainerForm).RefreshForm();
                }
                else
                {
                    BaseViewForm form = (_formArr[index] as BaseViewForm);
                    if (form.RefreshForm != null)
                    {
                        form.RefreshForm.Invoke();
                    }
                }
            }
        }

        static public void ChangeLanguage(string cultureName)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);

                foreach (var form in _formArr)
                {
                    if (form is BaseContainerForm)
                    {
                        (form as BaseContainerForm).ChangeLanguage();
                    }
                    else
                    {
                        ChangeFormLanguage(form);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static private void ChangeFormLanguage(Form form)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(form.GetType());
            foreach (Control ctl in form.Controls)
            {
                resources.ApplyResources(ctl, ctl.Name);
            }
        }



        private void ReadSensorsTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            //timer_ReadSensors.Interval = 600000;  // for debug
            if (!TimerFinished)   //异常处理：避免网络/数据库等原因导致在一个interval内没有执行完毕
                return;
            else
                TimerFinished = false;

            if (ModbusPortsList.Count > 0)
            {
                for (int i = 0; i < ModbusPortsList.Count; i++)
                {
                    SerialPort sp = ModbusPortsList[i];

                    if (!sp.IsOpen)
                    {
                        sp.Open();//打开串口
                    }

                    if (sp.IsOpen)
                    {
                        try
                        {
                            byte[] modbuscmd_1 = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x10, 0x79, 0xC6};
                            byte[] readbuffermodbus = new byte[10];

                            //sp.DiscardInBuffer(); // clear this com buffer

                            sp.Write(modbuscmd_1, 0, modbuscmd_1.Length);
                            System.Threading.Thread.Sleep(100);
                            
                            sp.Read(readbuffermodbus, 0, sp.BytesToRead);

                            ModubusReadCounter++;

                            if (ModubusReadCounter % 3 == 1)  //第一次读取，直接直接将读到的值赋给ReadbufferFirstmodbus
                            {
                                ReadbufferFirstmodbus[3] = readbuffermodbus[3];
                                ReadbufferFirstmodbus[4] = readbuffermodbus[4];
                            }
                            else if(ModubusReadCounter % 3 == 2)  // check 第一次读到的值和第二次的是否一样？
                            {
                                ReadbufferFirstmodbus[3] = (byte)(ReadbufferFirstmodbus[3] & readbuffermodbus[3]);
                                ReadbufferFirstmodbus[4] = (byte)(ReadbufferFirstmodbus[4] & readbuffermodbus[4]);
                            }
                            else if ((ModubusReadCounter) % 3 == 0)  //第三次， 同第二次
                            {
                                ReadbufferFirstmodbus[3] = (byte)(ReadbufferFirstmodbus[3] & readbuffermodbus[3]);
                                ReadbufferFirstmodbus[4] = (byte)(ReadbufferFirstmodbus[4] & readbuffermodbus[4]);


                                if (readbuffermodbus[0] == 1 && readbuffermodbus[1] == 2 && readbuffermodbus[2] == 2)  //check the received message validation
                                {
                                    byte[] readResult = new byte[] { readbuffermodbus[0], readbuffermodbus[1], readbuffermodbus[2], readbuffermodbus[3], readbuffermodbus[4] };
                                    byte[] CRCResult = RegularHelper.GetCRC16(readResult);
                                    if ((CRCResult[0] == readbuffermodbus[5]) && (CRCResult[1] == readbuffermodbus[6])) // check CRC code
                                    {
                                        readbuffermodbus[3] = ReadbufferFirstmodbus[3];  // 将组合后的值，赋给readbuffermodbus[3 and 4]
                                        readbuffermodbus[4] = ReadbufferFirstmodbus[4];

                                        LogerHelper.ToLog("1st modbus-1.1: current:" + readbuffermodbus[3].ToString() + ", previous:" + SensorsValues[0].ToString(), false);

                                        findWhichBoxesChanged(readbuffermodbus[3], SensorsValues[0], sp.PortName, 0);
                                        SensorsValues[0] = readbuffermodbus[3];  //把当前sensor 状态赋值到 SensorsValues 

                                        LogerHelper.ToLog("1st modbus-1.2: current:" + readbuffermodbus[4].ToString() + ", previous:" + SensorsValues[1].ToString(), false);
                                        findWhichBoxesChanged(readbuffermodbus[4], SensorsValues[1], sp.PortName, 1);
                                        SensorsValues[1] = readbuffermodbus[4];  //把当前sensor 状态赋值到 SensorsValues 
                                    }
                                    else
                                    {
                                        SpMessageBox.Show("告警：1st modbus CRC验证码错误, 串口通信有误！", "Modbus串口通信");
                                    }
                                }
                                else
                                {
                                    SpMessageBox.Show("告警：1st modbus data头信息错误, 串口通信有误！", "Modbus串口通信");
                                }
                            }

                           


                            byte[] modbuscmd_2 = { 0x02, 0x02, 0x00, 0x00, 0x00, 0x10, 0x79, 0xF5 };   // 2nd modbus
                            readbuffermodbus = new byte[10];

                            //sp.DiscardInBuffer(); // clear this com buffer

                            sp.Write(modbuscmd_2, 0, modbuscmd_2.Length);
                            System.Threading.Thread.Sleep(100);

                            sp.Read(readbuffermodbus, 0, sp.BytesToRead);

                            if (ModubusReadCounter % 3 == 1)  //第一次读取，直接直接将读到的值赋给ReadbufferSecondmodbus
                            {
                                ReadbufferSecondmodbus[3] = readbuffermodbus[3];
                                ReadbufferSecondmodbus[4] = readbuffermodbus[4];
                            }
                            else if (ModubusReadCounter % 3 == 2)  // check 第一次读到的值和第二次的是否一样？
                            {
                                ReadbufferSecondmodbus[3] = (byte)(ReadbufferSecondmodbus[3] & readbuffermodbus[3]);
                                ReadbufferSecondmodbus[4] = (byte)(ReadbufferSecondmodbus[4] & readbuffermodbus[4]);
                            }
                            else if ((ModubusReadCounter) % 3 == 0)  //第三次， 同第二次
                            {
                                ModubusReadCounter = 0;

                                ReadbufferSecondmodbus[3] = (byte)(ReadbufferSecondmodbus[3] & readbuffermodbus[3]);
                                ReadbufferSecondmodbus[4] = (byte)(ReadbufferSecondmodbus[4] & readbuffermodbus[4]);

                                if (readbuffermodbus[0] == 2 && readbuffermodbus[1] == 2 && readbuffermodbus[2] == 2)  //check the received message validation
                                {
                                    byte[] readResult = new byte[] { readbuffermodbus[0], readbuffermodbus[1], readbuffermodbus[2], readbuffermodbus[3], readbuffermodbus[4] };
                                    byte[] CRCResult = RegularHelper.GetCRC16(readResult);
                                    if ((CRCResult[0] == readbuffermodbus[5]) && (CRCResult[1] == readbuffermodbus[6])) // check CRC code
                                    {
                                        readbuffermodbus[3] = ReadbufferSecondmodbus[3];  // 将组合后的值，赋给readbuffermodbus[3 and 4]
                                        readbuffermodbus[4] = ReadbufferSecondmodbus[4];

                                        LogerHelper.ToLog("2nd modbus-2.1: current:" + readbuffermodbus[3].ToString() + ", previous:" + SensorsValues[2].ToString(), false);
                                        findWhichBoxesChanged(readbuffermodbus[3], SensorsValues[2], sp.PortName, 2);  // 后 8个 货架格子
                                        SensorsValues[2] = readbuffermodbus[3];  //把当前sensor 状态赋值到 SensorsValues 

                                        LogerHelper.ToLog("2nd modbus-2.2: current:" + readbuffermodbus[4].ToString() + ", previous:" + SensorsValues[3].ToString(), false);
                                        findWhichBoxesChanged(readbuffermodbus[4], SensorsValues[3], sp.PortName, 3);
                                        SensorsValues[3] = readbuffermodbus[4];  //把当前sensor 状态赋值到 SensorsValues 
                                    }
                                    else
                                    {
                                        SpMessageBox.Show("告警：2nd modbus CRC验证码错误, 串口通信有误！", "Modbus串口通信");
                                    }
                                }

                               // ModubusReadCounter = 0;   //move to the begining of this scope, to avoid the database opration exception
                            }

                            //ModubusReadCounter++;

                        }
                        catch (Exception ex)
                        {
                            SpMessageBox.Show("告警：" + ex.Message, "Modbus串口通信");
                            sp.Close();
                        }
                    }
                    break;
                }
            }

            TimerFinished = true;
        }



        /// <summary>
        /// update the alarm message count at sidebar"系统日志"
        /// </summary>
        private void UpdateAlarmMessageCount(byte AlarmMessageCount)
        {
            BeginInvoke((EventHandler)(delegate
            {
                sidebar.SetHighLightNumber(6, AlarmMessageCount);
            }));
        }


        private void UpdateDateTimeFormat(string format)
        {
            BeginInvoke((EventHandler)(delegate
            {
                sidebar.TimeFormat = format + " HH:mm";
            }));
        }


        private void updateRobotPLCState(string robotPLCState)
        {
            string timestamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": ";
            BeginInvoke((EventHandler)(delegate
            {
                if (robotPLCState.ToLower().Contains("dutid"))
                {
                    SpMessageBox.ShowModelessDialog(timestamp + "More than one DUTID, please Check all IDs and restart server!", "Mubea Auto Test");
                }
                else if (robotPLCState.ToLower().Contains("timeout"))
                {
                    SpMessageBox.ShowModelessDialog(timestamp + "Server does not receive the PLC Move command feedback in time", "Mubea Auto Test");

                    LogerHelper.ToAutoTestLogFile(timestamp + "Server does not receive the PLC Move command feedback in time");
                }
                else if (robotPLCState.ToLower().Contains("busy"))
                {
                    SpMessageBox.ShowModelessDialog(timestamp + "Robot is busy, please try again when idle.", "Mubea Auto Test");
                }
                else
                {
                    //this.label_RobotPLC_Status.Text = robotPLCState;
                    if (robotPLCState.ToLower().Contains("dis"))
                    {
                        SpMessageBox.ShowModelessDialog(timestamp + "Server does not receive the PLC Heartbeat in time", "Ericsson Auto Test");
                        LogerHelper.ToAutoTestLogFile(timestamp + "Server does not receive the PLC Heartbeat in time");
                    }
                }
            }));

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formClosed)
                SendMsgToServer.ClearAliveConnection();
        }

        private void pcbUser_Click(object sender, EventArgs e)
        {
            string newUser = "";
            string newUserPwd = "";
            SpMessageBox.ShowInputDialog("用户切换", out newUser, false, "用户名：");
            SpMessageBox.ShowInputDialog("输入密码", out newUserPwd, true, "密码：");
            if (newUser.Trim().Length > 0)
            {
                if (Login.CheckUserandPswd(newUser.Trim(), newUserPwd.Trim()))
                {
                    Login.UserName = newUser;
                    label_LoginName.Text = newUser;
                    //string loginTrace = string.Format("{0}: User {1} Login", DateTime.Now, newUser);
                    //LogerHelper.ToAutoTestLogFile(loginTrace);
                    SystemLogs.WriteSystemLog(Login.UserName, "switch login to " + Login.CurrentProductionLine, DateTime.Now);
                }
                else
                    SpMessageBox.Show("用户名或密码不正确，请重新输入.", "用户切换");
            }
            else
            {
                SpMessageBox.Show("请输入正确的用户名.", "用户切换");
            }

        }



        public static void CheckComPorts()
        {
            //get active 串口号 from PC
            string[] serialPorts = SerialPort.GetPortNames();
            SupportedSerialPortNoList.Clear();
            SerialPortsList.Clear();

            foreach (string port in serialPorts)
            {
                if (PlanedSerialPortNoList.Count > 0 && PlanedSerialPortNoList.Contains(port))
                {
                    SupportedSerialPortNoList.Add(port);

                    SerialPort sp = new SerialPort();//实例化串口通讯类
                    sp.BaudRate = 115200;//波特率
                    sp.DataBits = 8;//数据位
                    sp.StopBits = (StopBits)1;//停止位
                    sp.ReadTimeout = 2000;//读取数据的超时时间，引发ReadExisting异常
                    sp.PortName = port;//串口号

                    SerialPortsList.Add(sp);

                    // open 串口
                    try
                    {
                        if (sp.IsOpen)
                        {
                            sp.Close();
                            sp.Open();//打开串口
                            sp.Close();
                        }
                        else
                        {
                            sp.Open();//打开串口
                            sp.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        SpMessageBox.Show(sp.PortName + "告警：" + ex.Message, "电子标签屏串口通信");
                    }
                }
            }

            SupportedSerialPortNoList.Sort();
            //SerialPortsList.Sort();
            SerialPortsList = SerialPortsList.OrderBy(o => o.PortName).ToList();//升序

            // here add checking the 串口个数是否满足条件
            if (SerialPortsList.Count < 1)
            {
                SpMessageBox.Show("没有检测到电子标签屏串口, 请检查连接.", "电子标签屏串口通信");
            }
        }


        public static void CheckModbusComPorts()
        {
            //get active 串口号 from PC
            string[] serialPorts = SerialPort.GetPortNames();
            SupportedModbusPortNoList.Clear();
            ModbusPortsList.Clear();

            foreach (string port in serialPorts)
            {
                if (PlanedModbusPortNoList.Count > 0 && PlanedModbusPortNoList.Contains(port))
                {
                    SupportedModbusPortNoList.Add(port);

                    SerialPort sp = new SerialPort();//实例化串口通讯类
                    sp.BaudRate = 9600;//波特率 modbus
                    sp.DataBits = 8;//数据位
                    sp.StopBits = StopBits.One;//停止位
                    sp.ReadTimeout = 2000;//读取数据的超时时间，引发ReadExisting异常
                    sp.WriteTimeout = 2000;
                    sp.Parity = Parity.None;//校验位 None
                    sp.PortName = port;//串口号
                    //sp.DataReceived += new SerialDataReceivedEventHandler(ComReceive);//串口接收中断
                    sp.Handshake = Handshake.None;
                    ModbusPortsList.Add(sp);

                    // open 串口
                    try
                    {
                        if (sp.IsOpen)
                        {
                            sp.Close();
                            //sp.Open();//打开串口
                            //sp.Close();
                        }
                        else
                        {
                            sp.Open();//打开串口

                            //byte[] modbuscmd = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x10, 0x79, 0xC6 };
                            //byte[] readbuffermodbus = new byte[50];

                            //sp.Write(modbuscmd, 0, modbuscmd.Length);
                            //Array.Clear(readbuffermodbus, 0, 50);
                            //sp.Read(readbuffermodbus, 0, 20);


                            sp.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        SpMessageBox.Show("告警：" + ex.Message, "Modbus串口通信");
                    }
                }
            }

            SupportedModbusPortNoList.Sort();
     
            ModbusPortsList = ModbusPortsList.OrderBy(o => o.PortName).ToList();//升序
        }


        public static void ComReceive(object sender, SerialDataReceivedEventArgs e)//接收数据 中断只标志有数据需要读取，读取操作在中断外进行
        {
            SerialPort spTmp = new SerialPort();

            if (sender == null)
                return;
            else
                 spTmp = (SerialPort)sender;

            Thread.Sleep(10);//发送和接收均为文本时，接收中为加入判断是否为文字的算法，发送你（C4E3），接收可能识别为C4,E3，可用在这里加延时解决
            //if (recStaus)//如果已经开启接收
            {
                byte[] recBuffer;//接收缓冲区
                if (spTmp.PortName.Contains("COM5") || spTmp.PortName.Contains("COM6"))
                {
                    try
                    {
                        recBuffer = new byte[8];//接收数据缓存大小
                        spTmp.Read(recBuffer, 0, recBuffer.Length);//读取数据
                        spTmp.DiscardInBuffer();//清接收缓存
                    }
                    catch
                    {
                        MessageBox.Show("接收数据error，原因未知！");
                    }
                }

            }

            spTmp.DiscardOutBuffer();//清发送缓存
            spTmp.DiscardInBuffer();//清接收缓存
   
        }

        /// <summary>
        /// findWhichBoxChanged
        /// </summary>
        /// <param name="previousStatus"></param>
        /// <param name="currentStatus"></param>
        /// <param name="comPortName"></param>
        /// <param name="halfPart"></param>
        private int findWhichBoxChanged( byte currentStatus, byte previousStatus, string comPortName, int halfPart /*, out int changedNo*/)
        {
            //return the position on the frame, then find the material No. on this position
            int changedBox = 0xFF;
            //changedNo = 0;
            int andResult = previousStatus | currentStatus;
            if (andResult > 0)
            {
                switch (andResult)
                {
                    case 1:  //0000 0001
                        changedBox = 0 + 8 * halfPart;
                        break;
                    case 2:  //0000 0010
                        changedBox = 1 + 8 * halfPart;
                        break;
                    case 4:  //0000 0100
                        changedBox = 2 + 8 * halfPart;
                        break;
                    case 8:  //0000 1000
                        changedBox = 3 + 8 * halfPart;
                        break;
                    case 16:  //0001 0000
                        changedBox = 4 + 8 * halfPart;
                        break;
                    case 32:  //0010 0000
                        changedBox = 5 + 8 * halfPart;
                        break;
                    case 64:  //0100 0000
                        changedBox = 6 + 8 * halfPart;
                        break;
                    case 128:  //1000 0000
                        changedBox = 7 + 8 * halfPart;
                        break;

                    default:
                        changedBox = 0xFF;
                        break;
                }
            }

            return (changedBox / 2) + 1;
        }

        //findWhichBoxesChanged(readbuffermodbus[3], SensorsValues[i * 2], sp.PortName, 2 * i))
        private List<MaterialsChangedInfo> findWhichBoxesChanged(byte currentStatus, byte previousStatus, string comPortName, int halfPart )
        {
            //return the position on the frame, then find the material No. on this position
            List<MaterialsChangedInfo> changedBoxesList = new List<MaterialsChangedInfo>();
            int changedBox = 0xFF;
            int changedNo = 0;
            int orResult = previousStatus ^ currentStatus;
            if (orResult > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    switch (orResult % 4)
                    {
                        case 1:  //消耗掉1个
                            changedBox = (i + 1) + 4 * halfPart;
                            changedNo = 1;
                            break;
                        case 2:  //消耗掉1个
                            changedBox = (i + 1) + 4 * halfPart;
                            changedNo = 1;
                            break;
                        case 3:  //消耗掉2个
                            if ((previousStatus % 4 == 1 || previousStatus % 4 == 2) && (currentStatus % 4 == 1 || currentStatus % 4 == 2))
                            {
                                // 01 <-> 10, 这种情况算是没有消耗or新来料
                                changedBox = 0xFF;
                                changedNo = 0;
                            }
                            else
                            {
                                // 00 <-> 11,
                                changedBox = (i + 1) + 4 * halfPart;
                                changedNo = 2;
                            }
                            
                            break;
                        default:  // 无消耗
                            changedBox = 0xFF;
                            changedNo = 0;
                            break;
                    }

                    if (changedBox > 0 && changedBox < 20 && changedNo > 0)
                    {
                        if ((currentStatus % 4) < (previousStatus % 4))  //有物料箱被消耗掉
                        {
                            
                            LogerHelper.ToLog("有物料箱被消耗掉" + findMatrialNoBySeq((byte)changedBox) + "," + changedNo, false);
                            try
                            {
                                //找到哪个物料被消耗掉，更新数据库（used +1），然后发送补料请求到库房app
                                ProductionLineMaterial.UpdateProdLineMaterialUsdCount(findMatrialNoBySeq((byte)changedBox), Login.CurrentProductionLineNumber, CurrentOrder.CurrentProductOrder, changedNo);
                            }
                            catch (Exception e)
                            {
                                SpMessageBox.Show("告警："+e.Message, "更新数据库消耗");
                            }

                            // sned message to Store APP
                            try
                            {
                                MainForm.CheckConnectionWithServer();
                                Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(), findMatrialNoBySeq((byte)changedBox) + "|" + changedNo, AH.Network.MsgType.Status);
                            }
                            catch (Exception e)
                            {
                                SpMessageBox.Show("告警：" + e.Message, "通信-库房");
                            }
                        }
                        else if ((currentStatus % 4) > (previousStatus % 4))  //有新物料箱被送来
                        {
                            LogerHelper.ToLog("有新物料箱被送来" +findMatrialNoBySeq((byte)changedBox) + "," + changedNo, false);
                            try
                            {
                                //找到哪个物料被送来，更新数据库（received +1）
                                ProductionLineMaterial.UpdateProdLineMaterialRcvCount(findMatrialNoBySeq((byte)changedBox), Login.CurrentProductionLineNumber, CurrentOrder.CurrentProductOrder, changedNo);
                            }
                            catch (Exception e)
                            {
                                SpMessageBox.Show("告警：" + e.Message, "更新数据库来料");
                            }
                        }

                        changedBoxesList.Add(new MaterialsChangedInfo() { MaterialNoSeq = changedBox, MaterialChangedCount = changedNo });
                    }

                    //changedBoxesList.Add(new MaterialsChangedInfo() { MaterialNoSeq = changedBox, MaterialChangedCount = changedNo });

                    orResult       = orResult >> 2;
                    currentStatus  = (byte)(currentStatus >> 2);
                    previousStatus = (byte)(previousStatus >> 2);
                }

            }
            return changedBoxesList;
        }


        private string findMatrialNoBySeq(byte _materialNoSeq)
        {
            foreach (var ordertmp in CurrentOrder.CurrentOrderMaterialsList)
            {
                if (ordertmp.MaterialSeq == _materialNoSeq)
                {
                        return ordertmp.MaterialNo.Trim();
                }
            }

            return "";
        }




        public static void CheckConnectionWithServer()
        {

            if (!Program.S.SendHeartbeat())
            {
                Program.S.InitClient(Program.serverIP4Client, int.Parse(Program.clientPortListen));
            }
        }

        private void pb_Pause_Click(object sender, EventArgs e)
        {
            if (this.lb_Pause.Text.Trim().Contains("暂停"))
            {
                this.lb_Pause.Text = "运行";
                this.pb_Pause.Image = global::Mubea.AutoTest.GUI.Properties.Resources.Start;
                MainForm.CheckConnectionWithServer();
                Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(),
                                          "",
                                          AH.Network.MsgType.Begin);
            }
            else if (this.lb_Pause.Text.Trim().Contains("运行"))
            {
                this.lb_Pause.Text = "暂停";
                this.pb_Pause.Image = global::Mubea.AutoTest.GUI.Properties.Resources.Pause;
                MainForm.CheckConnectionWithServer();
                Program.S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(),
                                          "",
                                          AH.Network.MsgType.Pause);
            }
            else
            {
                SpMessageBox.Show("暂停异常，请联系管理员.","生产暂停");
            }

            this.lb_Pause.Update();
        }

    }
}
