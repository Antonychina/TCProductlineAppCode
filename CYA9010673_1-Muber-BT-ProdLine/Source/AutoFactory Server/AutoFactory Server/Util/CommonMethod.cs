using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AH.Network;
using AH.AutoServer;
using System.IO;
namespace AH.Util
{
    class CommonMethod
    {
        /// <summary>
        /// while receive a message from client , set the client ip&port to stationlist
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="sender"></param>
        public static void RegisterIPtoStation(string stationId, string sender)
        {
            int id = int.Parse(stationId);
            Server.stationList[id - 1].ahaddress = sender;      
        }

        /// <summary>
        /// if a client is offline, then show the warning in GUI
        /// </summary>
        /// <param name="sender"></param>
        public static void ShowClientOffLine(string sender)
        {
            for (int id = 1; id <= 6; id++)
            {
                if (Server.stationList[id - 1].ahaddress == sender)
                {
                    Server.gufList[id - 1].InitGUF(-1, id, 2);   // when client offline, reset the guflist
                    Server.stationList[id - 1].InitStationList(id); // when client offline, reset the station list
                    Server.stationList[id - 1].aTimer.Stop();
                    string msgTemp = string.Format("station: {0} is offline", id);
                    Server.stationList[id - 1].errorInfo = msgTemp;
                    ClientGUFStatus.UpdateStationsData(id, 1, ClientStatusType.Unknown);
                    ClientGUFStatus.UpdateStationsData(id, 2, ClientStatusType.Unknown);
                    ClientGUFStatus.UpdateStationsData(id, 3, ClientStatusType.Unknown);
                    ClientGUFStatus.UpdateStationsData(id, 4, ClientStatusType.Unknown);
                    LogerHelper2.ToAutoTestLogFile(DateTime.Now.ToString() + ": station " + id + " is offline");
                }
            }
        }

        public static string ChangeOneBitToTwoBit(string s)
        {
            if (s.Length < 2 && s != null)
            {
                s = "0" + s;
            }
            return s;
        }

        public static string ChangeTwoBitToOneBit(string s)
        {
            if (s.Length > 1 && int.Parse(s) < 10)
            { s = s.Substring(1, 1); }
            return s;
        }

        public static string CreateLogFile(string folderName)
        {
            string exePath = System.Environment.CurrentDirectory;
            string newPath = System.IO.Path.Combine(exePath, folderName);
            System.IO.Directory.CreateDirectory(newPath);
            string l_strLogPath = newPath + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
            return l_strLogPath;
        }
    }



    class LogerHelper2
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
                    //start to write  
                    sw.Write(content);

                    //crear the buffer
                    sw.Flush();

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
        public static void ToLog(String inputlog, int bSend)
        {
            if (inputlog != null)
            {
                string sLog = "";


                string sTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                if (bSend ==1)
                {
                    sLog += sTime + " Send: ";
                }
                else if (bSend == 0)
                {
                    sLog += sTime + " Recv: ";
                }
                else if (bSend == 3)
                {
                    sLog += sTime + " Exeption: ";
                }
                else if (bSend == 2)
                {
                    sLog += sTime + ": ";
                }
                else if (bSend == 4)
                {
                    sLog += sTime + ": ";
                    if (inputlog.Length > 0)
                        sLog += inputlog;
                    sLog += "\r\n";
                    string l_strLogPath2 = CommonMethod.CreateLogFile("heartbeatlog");
                    ToFile(l_strLogPath2, sLog);
                    return;
                }
                if (inputlog.Length > 0)
                {
                    sLog += inputlog;
                }

                sLog += "\r\n";
                string l_strLogPath = CommonMethod.CreateLogFile("log");
                ToFile(l_strLogPath, sLog);
            }
        }


        public static void RecordDutTestResultToSystemLog(string stationId, string dutID, ClientStatusType testResult)
        {
            ToAutoTestLogFile(DateTime.Now.ToString() + ": Tester " + stationId + " DUT ID " + dutID + " test result " + testResult.ToString());
        }

        public static void ToAutoTestLogFile(string content)
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
                    //start to write
                    byte[] string2Bytes = new byte[content.Length];
                    string2Bytes = new ASCIIEncoding().GetBytes(content);
                    sw.Write(content);
                    sw.Flush();
                    sw.Dispose();
                }
                fs.Dispose();
            }
        }
    }

}
