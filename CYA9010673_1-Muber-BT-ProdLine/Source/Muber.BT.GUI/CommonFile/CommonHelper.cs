using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using Mubea.AutoTest;
using Mubea.AutoTest.GUI;
using Mubea.AutoTest.GUI.Localization;
using Mubea.GUI.CustomControl;
using AH.Network;

namespace CommonHelper
{
	/// <summary>  
	/// 对Config文件中的Settings段进行读写配置操作  
	/// 注意：调试时，写操作将写在vhost.exe.config文件中  
	/// </summary>  
	class ConfigHelper
	{
		private string _configName;
		private string _fullPath;

		public string FileName
		{
			get 
			{
				if (_configName == null)
				{
					_configName = string.Empty;
				}
				return _configName;
			}

			set 
			{ 
				_configName = value;
				_fullPath = Application.StartupPath + "\\" + _configName;
			}
		}

        #region XML Common Operations
        /// <summary>  
		/// 写入值  
		/// </summary>  
		/// <param name="key"></param>  
		/// <param name="value"></param>  
		public void SetValue(string key, string value)
		{
			try
			{
				//增加的内容写在Settings段下 </>  
				File.SetAttributes(_fullPath, FileAttributes.Archive);
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

				XmlNode root = xmlDoc.SelectSingleNode("Settings");

				string[] nodes = key.Split('/');

				for (int i = 0; i < nodes.Length; i++)
				{
					XmlNode node = root.SelectSingleNode(nodes[i]);

					if (node == null)
					{
						node = xmlDoc.CreateNode(XmlNodeType.Element, nodes[i], null);
						if (i == nodes.Length - 1)
						{
							node.InnerText = value;
						}
						root.InsertAfter(node, root.FirstChild);

						root = node;
					}
					else
					{
						if (i == nodes.Length - 1)
						{
							node.InnerText = value;
						}

						root = node;
					}
				}

				xmlDoc.Save(_fullPath); 
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}
		}

		/// <summary>  
		/// 读取指定key的值  
		/// </summary>  
		/// <param name="key"></param>  
		/// <returns></returns>  
		public string GetValue(string NodeName, string key)
		{
			string conn = "";
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

                XmlNode root = xmlDoc.SelectSingleNode(NodeName);

				if (root != null)
				{
					XmlNode node = root.SelectSingleNode(key);
					if (node != null)
					{
						conn = node.InnerText;
					}
				}
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}
			
			return conn;
		}

        public void CreateXmlFile()
        {
			if (File.Exists(_fullPath))
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Settings");
            xmlDoc.AppendChild(root);
        
            try
            {
				xmlDoc.Save(_fullPath);
            }
            catch (Exception e)
            {
				//LogHelper.ToLog("XmlErr.txt", e);
            }
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

		/// <summary>
		/// 判断Settings某个节点是否存在
		/// </summary>
		/// <param name="nodeName"></param>
		/// <returns></returns>
		public bool IsNodeExists(string nodeName)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

				XmlNode root = xmlDoc.SelectSingleNode("Settings");

				if (root != null)
				{
					XmlNode node = root.SelectSingleNode(nodeName);

					return (node != null);
				}
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}

			return false;
		}

		/// <summary>
		/// 清除nodeName下的子节点
		/// </summary>
		/// <param name="nodeName"></param>
		public void ClearNodes(string nodeName)
		{
			try
			{
				File.SetAttributes(_fullPath, FileAttributes.Archive);

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

				XmlNode root = xmlDoc.SelectSingleNode("Settings");

				if (root != null)
				{
					XmlNode node = root.SelectSingleNode(nodeName);

					XmlNodeList xnlist = node.ChildNodes;

					while (xnlist.Count > 0)
					{
						XmlNode xn = xnlist[0];
						xn.ParentNode.RemoveChild(xn); 
					}

					xmlDoc.Save(_fullPath);
				}
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}
		}

		public void CreateNode(string nodeName)
		{
			try
			{
				//增加的内容写在Settings段下 </>  
				File.SetAttributes(_fullPath, FileAttributes.Archive);

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

				XmlNode root = xmlDoc.SelectSingleNode("Settings");
				XmlNode node = root.SelectSingleNode(nodeName);

				if (null == node)
				{
					node = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, null);
					
					root.InsertAfter(node, root.FirstChild);

					xmlDoc.Save(_fullPath);
				}
				
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}
		}

		/// <summary>
		/// 获得某个节点下所有值
		/// </summary>
		/// <param name="nodeName"></param>
		/// <returns></returns>
		public Dictionary<string, string> GetNodeList(string nodeName)
		{
			Dictionary<string, string> nodeList = new Dictionary<string, string>();

			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(_fullPath);

				XmlNode root = xmlDoc.SelectSingleNode("Settings");

				if (root != null)
				{
					XmlNode node = root.SelectSingleNode(nodeName);

					XmlNodeList xnlist = node.ChildNodes;

					foreach (XmlNode xn in xnlist)
					{
						nodeList.Add(xn.Name, xn.InnerText);
					}
				}
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("XmlErr.txt", ex);
			}

			return nodeList;
		}

        #endregion XML Common Operations

        #region Maintain Plan config
        /// <summary>  
        /// Read the maintenance plan config file and get all the maint plan 
        /// </summary>  
        /// <returns></returns>  
        //public List<MaintPlan> ReadConfigFile(string rootNode)
        //{
        //    List<MaintPlan> maintPlanArr = new List<MaintPlan>();

        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            //mptemp.MaintItemList = new List<MaintItem>();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = (xe.GetAttribute("Name"));    //read task name 
        //            mptemp.MaintFreq = (xe.GetAttribute("Frequency"));  //read task Frequency 
        //            mptemp.MaintStatus = (xe.GetAttribute("MaintStatus"));  //read task execute status 

        //            maintPlanArr.Add(mptemp);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return null;
        //    }

        //    return maintPlanArr;
        //}


        /// <summary>  
        /// Write Next Maintenance Time after the maintain plan was executed 
        /// </summary>  
        /// <returns></returns>  
        //public DateTime WriteNextMaintTime(string rootNode, string maintPlanName)
        //{
        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = xe.GetAttribute("Name");    //read task name 

        //            if (mptemp.MaintTaskName == maintPlanName)
        //            {
        //                DateTime nextmainttime = RegularHelper.GetNextMaintTime(xe.GetAttribute("Frequency"));
        //                if (nextmainttime != DateTime.MaxValue)
        //                    xe.Attributes["NextMaintTime"].Value = nextmainttime.ToString();
        //                else
        //                {
        //                    SpMessageBox.Show("计算下次维护时间失败！", "仪器维护");
        //                    // write debug log? GP6800_SystemErrorLog.txt
        //                    //LogHelper.ToLog("GP6800_SystemErrorLog.txt", "计算下次维护时间失败, 返回有误.");
        //                    return DateTime.MaxValue;
        //                }
        //                xmlDoc.Save(_fullPath);
        //                return nextmainttime;
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return DateTime.MaxValue;
        //    }

        //    return DateTime.MaxValue;
        //}

        /// <summary>
        /// update Maintain task execution status, if all the maintain sub items are executed OK, then the Maintain task execution status should be OK
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="maintPlanName"></param>
        /// <param name="executeResult"></param>
        /// <returns></returns>
        //public bool UpdateMaintStatus(string rootNode, string maintPlanName, string executeResult)
        //{
        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = xe.GetAttribute("Name");    //read task name 

        //            if (mptemp.MaintTaskName == maintPlanName)
        //            {
        //                xe.Attributes["MaintStatus"].Value = executeResult;
                      
        //                xmlDoc.Save(_fullPath);
        //                return true;
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return false;
        //    }

        //    return false;
        //}


        /// <summary>  
        /// Get the maint items according to the maint plan name.  
        /// </summary>  
        /// <returns></returns>  
        //public List<MaintItem> ReadMaintItems(string rootNode, string maintPlanName)
        //{
        //    List<MaintItem> maintItemsArr = new List<MaintItem>();

        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            //mptemp.MaintItemList = new List<MaintItem>();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = (xe.GetAttribute("Name"));    //read task name 

        //            if (mptemp.MaintTaskName == maintPlanName)
        //            {

        //                foreach (XmlNode subnode in xe.ChildNodes)
        //                {
        //                    MaintItem mttemp = new MaintItem();
        //                    XmlElement subxe = (XmlElement)subnode;

        //                    mttemp.ItemName = subxe.GetAttribute("Name"); //read item Name
        //                    mttemp.Description = subxe.GetAttribute("Description"); //read item Description
        //                    mttemp.ItemStatus = subxe.GetAttribute("Status"); //read item execution status
        //                    mttemp.LastMaintTime = subxe.GetAttribute("LastMaintTime"); //read item execution for last time

        //                    maintItemsArr.Add(mttemp); ;
        //                }
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return null;
        //    }

        //    return maintItemsArr;
        //}


        /// <summary>  
        /// Write the maint items status after the maint plan execution.  
        /// </summary>  
        /// <returns>bool:ture or false</returns>  
        //public bool WriteMaintItemsStatus(string rootNode, string maintPlanName, string maintItemName, string executingResult)
        //{
 
        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            //mptemp.MaintItemList = new List<MaintItem>();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = (xe.GetAttribute("Name"));    //read task name 

        //            if (mptemp.MaintTaskName == maintPlanName)
        //            {

        //                foreach (XmlNode subnode in xe.ChildNodes)
        //                {
        //                    MaintItem mttemp = new MaintItem();
        //                    XmlElement subxe = (XmlElement)subnode;

        //                    mttemp.ItemName = subxe.GetAttribute("Name"); //read item Name
        //                    if (mttemp.ItemName == maintItemName)
        //                    {
        //                        subxe.Attributes["Status"].Value = executingResult; //set item execution status
        //                        xmlDoc.Save(_fullPath);
        //                        return true;
        //                    }
        //                    else
        //                        continue;                         
        //                }
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return false;
        //    }

        //    return false;
        //}

        /// <summary>  
        /// Write the maint items execution for last successfully time
        /// </summary>  
        /// <returns>bool:ture or false</returns>  
        //public bool UpdateMaintItemsExecutionTime(string rootNode, string maintPlanName, string maintItemName, string executeTime)
        //{

        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            MaintPlan mptemp = new MaintPlan();
        //            //mptemp.MaintItemList = new List<MaintItem>();
        //            XmlElement xe = (XmlElement)node;

        //            mptemp.MaintTaskName = (xe.GetAttribute("Name"));    //read task name 

        //            if (mptemp.MaintTaskName == maintPlanName)
        //            {

        //                foreach (XmlNode subnode in xe.ChildNodes)
        //                {
        //                    MaintItem mttemp = new MaintItem();
        //                    XmlElement subxe = (XmlElement)subnode;

        //                    mttemp.ItemName = subxe.GetAttribute("Name"); //read item Name
        //                    if (mttemp.ItemName == maintItemName)
        //                    {
        //                        subxe.Attributes["LastMaintTime"].Value = executeTime; //set item execution status
        //                        xmlDoc.Save(_fullPath);
        //                        return true;
        //                    }
        //                    else
        //                        continue;
        //                }
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return false;
        //    }

        //    return false;
        //}
        #endregion Maintain Plan config

        #region system log config
        /// <summary>
        /// Get System Log Info from XML config file
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="syslogcode"></param>
        /// <returns></returns>
        //public SysLog GetSysLogInfo(string rootNode, string syslogcode)
        //{
        //    SysLog sysloginfo = new SysLog();
        //    string logCodeTmp;

        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(_fullPath);

        //        XmlNode root = xmlDoc.SelectSingleNode(rootNode);

        //        XmlNodeList nodes = root.ChildNodes;

        //        foreach (XmlNode node in nodes)
        //        {
        //            XmlElement xe = (XmlElement)node;

        //            logCodeTmp = (xe.GetAttribute("LogCode"));    //read LogCode

        //            if (logCodeTmp == syslogcode)
        //            {
        //                sysloginfo.LogCode = syslogcode;
        //                sysloginfo.LogLevel = SystemLog.LogLevel_Trans2Num(xe.GetAttribute("LogLevel")); //read LogLevel
        //                sysloginfo.LogDescription = xe.GetAttribute("Description"); //read LogLevel
        //                sysloginfo.LogTime = DateTime.Now; //set log time
        //                break;
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ToLog("XmlErr.txt", ex);
        //        return null;
        //    }

        //    return sysloginfo;
        //}
        #endregion system log config

       
    }

    class LogerHelper
	{
        /// <summary>
        /// ToFile
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void ToFile(string fileName, string content)
        {
            using (FileStream fs = new FileStream(fileName,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //开始写入  
                    sw.Write(content);

                    //清空缓冲区  
                    sw.Flush();

                    //关闭流  
                    sw.Dispose();
                }

                fs.Dispose();
            }

        }

        /// <summary>
        /// write info to log
        /// </summary>
        /// <param name="bufArr"></param>
        /// <param name="bSend"></param>
        public static void ToLog(String inputlog, bool bSend)
        {
            if (inputlog != null)
            {
                string sLog = "";


                string sTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (bSend)
                {
                    sLog += sTime + " Send: ";
                }
                else
                {
                    sLog += sTime + " Recv: ";
                }

                if (inputlog.Length > 0)
                {
                    sLog += inputlog;
                }

                sLog += "\r\n";

                ToFile("log" + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt", sLog);
                //Console.WriteLine(sLog);
            }
        }


        public static void ToAutoTestLogFile( string content)
        {
            string fileName = "AutoTestLog.dat";
            content = content + "\r\n";
            using (FileStream fs = new FileStream(fileName,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //开始写入
                    byte[] string2Bytes = new byte[content.Length];
                    string2Bytes = new ASCIIEncoding().GetBytes(content);
                   
                    sw.Write(content);

                    //清空缓冲区  
                    sw.Flush();

                    //关闭流  
                    sw.Dispose();
                }

                fs.Dispose();
            }
        }


        public static string ReadAutoTestLogFile()
        {
            string fileName = "AutoTestLog.dat";
            string stringReadFromFile = string.Empty;
            using (FileStream fs = new FileStream(fileName,
                    FileMode.Open,
                    FileAccess.Read))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    //开始read

                    if (sw.Peek() != -1)
                    {
                        //读取文件中的内容
                         stringReadFromFile = sw.ReadToEnd();
                    }

                    sw.Close();

                    //关闭流  
                    sw.Dispose();
                }

                fs.Dispose();
            }
            return stringReadFromFile;
        }



        public static string ReadConfigFile(string fileName)
        {
            string stringReadFromFile = string.Empty;
            using (FileStream fs = new FileStream(fileName,
                    FileMode.Open,
                    FileAccess.Read))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    //开始read

                    if (sw.Peek() != -1)
                    {
                        //读取文件中的内容
                        stringReadFromFile = sw.ReadToEnd();
                    }

                    sw.Close();

                    //关闭流  
                    sw.Dispose();
                }

                fs.Dispose();
            }
            return stringReadFromFile;
        }
    }

    class TestStateHelper
	{
        public static TestState convertToTestState(ClientStatusType state)
        {
            switch(state)
            {
                case ClientStatusType.Abort:
                    return TestState.Abort;
                case ClientStatusType.Fail:
                    return TestState.Fail;
                case ClientStatusType.Error:
                    return TestState.Error;
                case ClientStatusType.Idle:
                    return TestState.Idle;
                case ClientStatusType.Pass:
                    return TestState.Pass;
                case ClientStatusType.Run:
                    return TestState.Running;
                case ClientStatusType.Wait:
                    return TestState.Wait;
                case ClientStatusType.Unknown:
                    return TestState.Unknown;
                default:
                    return TestState.Unknown;
            }
        }
    }

    public class TestData
    {
        private string _testerName;
        private int _passedCount;
        private int _failedCount;
        private int _totalCount;
        private double _testerYield;


        /// <summary>
        /// tester Name
        /// </summary>
        public string TesterName
        {
            get { return _testerName; }
            set { _testerName = value; }
        }

        /// <summary>
        /// test Passed dut Count
        /// </summary>
        public int PassedCount
        {
            get { return _passedCount; }
            set { _passedCount = value; }
        }

        /// <summary>
        /// test failed dut Count
        /// </summary>
        public int FailedCount
        {
            get { return _failedCount; }
            set { _failedCount = value; }
        }

        /// <summary>
        /// total dut Count
        /// </summary>
        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        /// <summary>
        /// the test yield of the Tester
        /// </summary>
        public double TesterYield
        {
            get { return (_totalCount > 0) ? ((double)_passedCount/_totalCount) : 0.0; }
            set { _testerYield = value; }
        }

    }
}
