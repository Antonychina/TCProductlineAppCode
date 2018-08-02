using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class SysLog
    {

        private string _logID;
		private string _operator;
	
		private string _logDescription;
		private DateTime _logTime;


        public string LogID
        {
            get { return _logID; }
            set { _logID = value; }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

  
        public string LogDescription
        {
            get { return _logDescription; }
            set { _logDescription = value; }
        }

        public DateTime LogTime
        {
            get { return _logTime; }
            set { _logTime = value; }
        }

		public SysLog()
		{
			_logTime = DateTime.Now;
		}
    }
}
