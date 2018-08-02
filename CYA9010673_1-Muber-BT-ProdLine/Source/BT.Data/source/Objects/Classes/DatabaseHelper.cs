using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace DebugHelper
{
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

                ToFile("log" + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".db.txt", sLog);
                //Console.WriteLine(sLog);
            }
        }

    }
}
