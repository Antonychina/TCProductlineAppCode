using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommonHelper;

namespace Mubea.AutoTest.GUI
{
	public partial class SystemLog : BaseViewForm
	{
        public static int AlarmMessageCount = 0;

        public SystemLog()
		{
			InitializeComponent();
			InitializeListView(lsvSysLog);
            RefreshForm += LoadSysLogAndShowFromDB;
         
		}

        private void SystemLog_Load(object sender, EventArgs e)
        {
            LoadSysLogAndShowFromDB();
        }


        private void LoadSysLogAndShowFromDB()
        {
            SystemLogs systemLogInfos = new SystemLogs();
            int sequence = 1;
            this.lsvSysLog.Items.Clear();

            foreach (var logtemp in systemLogInfos.GetSystemLogList(dateTime_StartTime.Value))
            {
                ListViewItem lsvStationSubItem = new ListViewItem();

                lsvStationSubItem.Text = sequence++.ToString();
                lsvStationSubItem.SubItems.Add(logtemp.Operator.Trim());
                lsvStationSubItem.SubItems.Add(logtemp.LogDescription.Trim());
                lsvStationSubItem.SubItems.Add(logtemp.LogTime.ToString().Trim());

                this.lsvSysLog.Items.Add(lsvStationSubItem);
            }

        }

        private void LoadSysLogAndShowFromFile()
        {
            this.lsvSysLog.Items.Clear();

            string allSysLog = LogerHelper.ReadAutoTestLogFile();
            string[] sysLogArray = allSysLog.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = sysLogArray.Length - 1; i >= 0; i--)
            {
                ListViewItem lsvStationSubItem = new ListViewItem();

                lsvStationSubItem.Text = (sysLogArray.Length - i).ToString();
                lsvStationSubItem.SubItems.Add(sysLogArray[i].Trim());

                this.lsvSysLog.Items.Add(lsvStationSubItem);
            }

        }


        private void btn_LogSearch_Click(object sender, EventArgs e)
        {
            if (this.textBox_Log_Keyword.Text.Trim().Length > 0)
            {
                this.lsvSysLog.Items.Clear();

                string allSysLog = LogerHelper.ReadAutoTestLogFile();
                string[] sysLogArray = allSysLog.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = sysLogArray.Length - 1; i >= 0; i--)
                {
                    if (sysLogArray[i].ToString().Contains(textBox_Log_Keyword.Text.Trim()))
                    {
                        ListViewItem lsvStationSubItem = new ListViewItem();

                        lsvStationSubItem.Text = (sysLogArray.Length - i).ToString();
                        lsvStationSubItem.SubItems.Add(sysLogArray[i].Trim());

                        this.lsvSysLog.Items.Add(lsvStationSubItem);
                    }
                }
            }
            else
            {
                LoadSysLogAndShowFromDB(); // or LoadSysLogAndShowFromFile
            }
        }


        private void dateTime_StartTime_ValueChangedFromFile(object sender, EventArgs e)
        {
             this.lsvSysLog.Items.Clear();

             string selectedDate = dateTime_StartTime.Value.ToShortDateString();
             string allSysLog = LogerHelper.ReadAutoTestLogFile();
             string[] sysLogArray = allSysLog.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
             for (int i = sysLogArray.Length - 1; i >= 0; i--)
             {
                 if (sysLogArray[i].ToString().Contains(selectedDate))
                 {
                     ListViewItem lsvStationSubItem = new ListViewItem();

                     lsvStationSubItem.Text = (sysLogArray.Length - i).ToString();
                     lsvStationSubItem.SubItems.Add(sysLogArray[i].Trim());

                     this.lsvSysLog.Items.Add(lsvStationSubItem);
                 }
             }
           
        }



        private void dateTime_StartTime_ValueChangedFromDB(object sender, EventArgs e)
        {
            LoadSysLogAndShowFromDB();
        }

    }
}
