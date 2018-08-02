using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Data.SqlClient;
using AH.AutoServer;
using CommonHelper;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO.Ports;

namespace Mubea.AutoTest.GUI
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		[DllImport("User32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
		[DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);
        [DllImport("User32.dll")]
        private static extern bool IsWindow(IntPtr windowPtr);
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr windowPtr);

        public static Server S = new Server();
        public static string serverIP4Client = "";
        public static string clientPortListen = "";
		private const int WS_SHOWNORMAL = 1;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
//#if DEBUG
//#else
//            KeyboardHook key = new KeyboardHook();
//            key.KeyMaskStop();
//            key.KeyMaskStart();
//            key.DisableTaskMgr(true);
//            key.HWindows();
//            key.HStar();
//#endif
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Process instance = RunningInstance();

			if (instance == null)
			{
				try
				{
                    ConfigHelper _xmlconfigfile = new ConfigHelper();
                    _xmlconfigfile.FileName = "AutoTestConfig.xml";

                    string dataSrc = _xmlconfigfile.GetValue(@"Settings", @"DataSource");

                    if (dataSrc != "")
                    {
                        GP.MAGICL6800.ORM.ConnectStringHelper.ConnectString = dataSrc;
                    }
					if (!CheckSqlServerService())
					{
						Application.Exit();

						return;
					}


					Login login = new Login();
                    login.ControlBox = false;

                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        //SpMessageBox.Show("Please input the correct username and password!", "Muber BT Test System Login");
                        return;
                    }

                    MainForm autoTestForm = new MainForm();
                    autoTestForm.ControlBox = false;
                    //autoTestForm.Show();

                    //read config file
                    

                    serverIP4Client = _xmlconfigfile.GetValue(@"Settings", @"IP_Port/ServerIpAddress");
                    clientPortListen = _xmlconfigfile.GetValue(@"Settings", @"IP_Port/ClientPortListen");

                    if (!RegularHelper.isNumericString(clientPortListen))
                    {
                        SpMessageBox.Show("Read Config file AutoTestConfig.xml ClientPortListen error, please check the format.");
                        return;
                    }

                    //string PLCPortListen = _xmlconfigfile.GetValue(@"Settings", @"IP_Port/PLCPortListen");
                    //if (!RegularHelper.isNumericString(PLCPortListen))
                    //{
                    //    SpMessageBox.Show("Read Config file AutoTestConfig.xml PLCPortListen error, please check the format.");
                    //    return;
                    //}

                    //string OutBufferListen = _xmlconfigfile.GetValue(@"Settings", @"IP_Port/OutBufferListen");
                    //if (!RegularHelper.isNumericString(OutBufferListen))
                    //{
                    //    SpMessageBox.Show("Read Config file AutoTestConfig.xml OutBufferListen error, please check the format.");
                    //    return;
                    //}

                    //string hostName = Dns.GetHostName();   //获取本机名
                    //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
                    //IPAddress localaddr = localhost.AddressList[0];
                    //string hostIpAddr = localaddr.ToString();


                    {
                        // S.InitServer(serverIP4Client, serverIP4PLC, int.Parse(clientPortListen), int.Parse(PLCPortListen), int.Parse(OutBufferListen), true);
                        S.InitClient(serverIP4Client,  int.Parse(clientPortListen));

                        S.SendMessageToWS(Login.CurrentProductionLineNumber.ToString(), 
                                          "register message", 
                                          AH.Network.MsgType.Register);
                        
                    }


                    Application.Run(autoTestForm);
				}
				catch (Exception ex)
				{
                    SpMessageBox.Show(ex.Message + ", Please contact the Administrator.", "App启动");
					//LogerHelper.ToLog(ex.ToString(), false);
				}
			}
			else
			{
				HandleRunningInstance(instance);
			}
		}

		private static Process RunningInstance()
		{
			Process current = Process.GetCurrentProcess();
			Process[] processes = Process.GetProcessesByName(current.ProcessName);
			foreach (Process process in processes)
			{
				if (process.Id != current.Id)
				{
					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
					{
						return process;
					}
				}
			}
			return null;
		}

		/// <summary> 
		/// 显示已运行的程序。 
		/// </summary> 
		public static void HandleRunningInstance(Process instance)
		{
			ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
			SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
		}

		/// <summary>
		/// 隐藏当前程序
		/// </summary>
		public static void HidRunningInstance()
		{
			Process current = Process.GetCurrentProcess();
			ShowWindowAsync(current.MainWindowHandle, 0);
		}

        //public static bool CheckSqlServerService()
        //{
        //    return true;
        //}

        public static bool CheckSqlServerService()
        {
            int index1 = GP.MAGICL6800.ORM.ConnectStringHelper.ConnectString.IndexOf("=");
            int index2 = GP.MAGICL6800.ORM.ConnectStringHelper.ConnectString.IndexOf(";");
            string dataSrc = GP.MAGICL6800.ORM.ConnectStringHelper.ConnectString.Substring(index1 + 1, index2 - index1 - 1);

            string sqlServalName = "MSSQLSERVER";
            if (dataSrc == "(local)")
            {
                sqlServalName = "MSSQLSERVER";

                ServiceController[] scs = ServiceController.GetServices();
                foreach (var sc in scs)
                {
                    if (sc.ServiceName.IndexOf("MSSQL$") != -1)
                    {
                        sqlServalName = sc.ServiceName;
                        break;
                    }
                }
            }
            else if (dataSrc.ToUpper() == "(LOCAL)\\SQLEXPRESS")
            {
                sqlServalName = "MSSQL$SQLEXPRESS";
            }
            else
            {
                int index = dataSrc.IndexOf("(local)\\");
                if (index != -1)
                {
                    sqlServalName = "SQL Server (" + dataSrc.Substring(index + 8) + ")";
                }
                else
                {
                    sqlServalName = dataSrc;
                }
            }

            try
            {
                ConfigHelper _xmlconfigfile = new ConfigHelper();
                _xmlconfigfile.FileName = "AutoTestConfig.xml";

                string connectionString = string.Format("Server = {0};" + "Database = {1};" + "User ID = {2};" + "Password = {3}",
                    _xmlconfigfile.GetValue(@"Settings", @"DB_Server/Server"),
                    _xmlconfigfile.GetValue(@"Settings", @"DB_Server/Database"),
                    _xmlconfigfile.GetValue(@"Settings", @"DB_Server/UserID"),
                    _xmlconfigfile.GetValue(@"Settings", @"DB_Server/Passwd"));

                SqlConnection Conn = new SqlConnection(connectionString);
                Conn.Open(); //连接打开
                Conn.Close();//连接关闭

                //ServiceController sc = new ServiceController(sqlServalName);

                //if (sc.Status == ServiceControllerStatus.Stopped
                //    || sc.Status == ServiceControllerStatus.StopPending)
                //{
                //    sc.Start();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，请联系管理员.","连接数据库");
                //LogHelper.ToLog("SqlServerErr.txt", ex);

#if DEBUG
#else
				MessageBox.Show(ex.Message);
#endif
                return false;
            }

            return true;
        }
	}
}
