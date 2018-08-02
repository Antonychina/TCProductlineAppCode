using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;
using System.Windows.Forms;

/// <summary>
///  Data Table Result Operations define
///  Include Query Update Insert and Delete
/// </summary>
namespace GP.MAGICL6800.ORM
{
    public class SampleResultBLL
    {
        private static LogInfoTableAdapter _logInfoAdapter;

        private static Mubea_BTDataSet.LogInfoDataTable _logInfoTable;
        public static Mubea_BTDataSet.LogInfoDataTable LogInfoTable
        {
            get
            {
                if (_logInfoTable == null)
                {
                    _logInfoTable = _logInfoAdapter.GetLogInfo();
                }

                return _logInfoTable;
            }
        }

        static SampleResultBLL()
        {
            _logInfoAdapter = new LogInfoTableAdapter();
        }


        public List<string> GetLogInfo()
        {
            List<string> logResultArr = new List<string>();

            foreach (var val in LogInfoTable)
            {
                logResultArr.Add(val.LogId);
            }

            return logResultArr;
        }

        public static void Insert(string LogID, string Operator, string LogMessage, DateTime OptTime)
        {
            _logInfoAdapter.InsertLog(LogID, Operator, LogMessage, OptTime);
            
        }

       

        public static int Delete(string logID)
        {
            int ret = _logInfoAdapter.DeleteLog(logID);
			return ret;
        }
    }
}
