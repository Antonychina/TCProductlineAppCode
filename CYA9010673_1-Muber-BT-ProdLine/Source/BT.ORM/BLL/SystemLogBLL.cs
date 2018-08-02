using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;

/// <summary>
///  Data Table Result Operations define
///  Include Query Update Insert and Delete
/// </summary>
namespace GP.MAGICL6800.ORM
{

    public class SystemLogBLL
    {
        private static LogInfoTableAdapter _systemLogAdapter;
        private static Mubea_BTDataSet.LogInfoDataTable _systemLogTable;

        public static Mubea_BTDataSet.LogInfoDataTable SystemLogTable(DateTime _testTimeStart)
        {
         
            _systemLogTable = _systemLogAdapter.GetLogInfoByDate(_testTimeStart);
         
            return _systemLogTable;
        }

        static SystemLogBLL()
        {
            _systemLogAdapter = new LogInfoTableAdapter();
        }

        public static void InsertSystemLog(string _operator, string _description, DateTime _logTime)
        {
            string _logId = (_systemLogAdapter.GetLogCount() + 1).ToString();
            _systemLogAdapter.InsertLog(_logId, _operator, _description, _logTime);
        }
    }
}
