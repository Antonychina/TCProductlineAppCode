using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// SystemLogs
    /// </summary>

    public class SystemLogs
    {
        List<SysLog> _systemlogs;

        /// <summary>
        /// Constructure of SystemLogs
        /// </summary>
        public SystemLogs()
        {
            _systemlogs = new List<SysLog>();
        }

        /// <summary>
        /// Get SystemLogs
        /// </summary>
        List<SysLog> SystemLogList
        {
            get { return _systemlogs; }
        }

        /// <summary>
        /// Query System Log by filters
        /// </summary>
        /// <param name="_logCode"></param>
        /// <param name="_logLevel"></param>
        /// <param name="_testTimeStart"></param>
        /// <param name="_testTimeEnd"></param>
        /// <returns></returns>
        public List<SysLog> GetSystemLogList(DateTime _testTimeStart)
        {

            if (SystemLogList.Count > 0)
                SystemLogList.Clear();

            try
            {
                foreach (var valLog in SystemLogBLL.SystemLogTable(_testTimeStart ))
                {
                    SysLog systemlogtemp = new SysLog();

                    systemlogtemp.LogID = valLog.LogId;
                    systemlogtemp.Operator = valLog.Operator;
                    systemlogtemp.LogDescription = valLog.LogMessage;
                    systemlogtemp.LogTime = valLog.OperateTime;

                    SystemLogList.Add(systemlogtemp);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.ToLog("DBErr.txt", ex);
            }

            return SystemLogList;
        }

        public static bool WriteSystemLog(string _operator, string _description, DateTime _testTime)
        {

            try
            {
                SystemLogBLL.InsertSystemLog(_operator, _description, _testTime);
            }
            catch (Exception ex)
            {
                //LogHelper.ToLog("DBErr.txt", ex);

                return false;
            }

            return true;
        }


        public static DataTable GetAllSystemLogs(ListView ListViewName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("操作员");
            dt.Columns.Add("描述");
            dt.Columns.Add("时间");

            int i = 0;
            for (i = 0; i < ListViewName.Items.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = ListViewName.Items[i].Text.ToString();
                dr["操作员"] = ListViewName.Items[i].SubItems[1].Text.ToString();
                dr["描述"] = ListViewName.Items[i].SubItems[3].Text.ToString();
                dr["时间"] = ListViewName.Items[i].SubItems[4].Text.ToString();
                dt.Rows.Add(dr);
            }
            return dt;

        }
    }
}
