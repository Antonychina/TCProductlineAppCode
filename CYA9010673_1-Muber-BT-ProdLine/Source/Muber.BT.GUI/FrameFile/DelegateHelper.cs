using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubea.AutoTest;
using Mubea.GUI.CustomControl;

namespace Mubea.AutoTest.GUI
{
    public delegate void RefreshFormDelegate();

    /// <summary>
    /// 上位机心跳定时器delegate
    /// </summary>
    /// <param name=></param>
    public delegate void setTimerDelegate();

    /// <summary>
    /// 操作MainForm界面倒计时进度条和时间
    /// </summary>
    /// <param name=></param>
    public delegate void hideMainFormProgressBarDelegate();

    /// <summary>
    /// 上位机初始化进度条delegate
    /// </summary>
    /// <param name=></param>
    public delegate void killProgressBarWhenInitFailDelegate();

    /// <summary>
    /// 设置当前进度条进度
    /// </summary>
    /// <param name="value"></param>
    public delegate void SetProgressBarValueDelegate(int value);


    /// <summary>
    /// Update Time Format
    /// </summary>
    /// <param name=></param>
    public delegate void UpdateSideBarUnreadMessageCount(byte unreadMessageCount);


    /// <summary>
    /// Update Time Format
    /// </summary>
    /// <param name=></param>
    public delegate void DateTimeFormat(string format);


    /// <summary>
    /// Station status Changed
    /// </summary>
    /// <param name=></param>
    public delegate void ChangeStationStatus(int station, int what, TestState state);


    /// <summary>
    /// Station Client and GUF data Changed
    /// </summary>
    /// <param name=></param>
    public delegate void ChangeStationData();


    public class SideBarHighLight
    {
        public static event UpdateSideBarUnreadMessageCount SideBarUnreadMessageCount;

        public static void UpdateUnreadMessageCount(byte count)
        {
            if (SideBarUnreadMessageCount != null)
            {
                SideBarUnreadMessageCount(count);
            }
        }
    }


    /// <summary>
    /// Progress bar related event and delegate
    /// </summary>
    public class ProgressBarHelper
    {
        public static event hideMainFormProgressBarDelegate HideMainFormProgressBar;

        public static void HideMainFormProgressBarBySystemInit()
        {
            if (HideMainFormProgressBar != null)
            {
                HideMainFormProgressBar();
            }
        }

        public static event killProgressBarWhenInitFailDelegate KillProgressBarInitFail;

        public static void KillProgressBarWhenInitFail()
        {
            if (KillProgressBarInitFail != null)
            {
                KillProgressBarInitFail();
            }
        }

        public static event SetProgressBarValueDelegate SetProgressBarValueHandler;

        public static void OnSetProgressBarValue(int value)
        {
            if (SetProgressBarValueHandler != null)
            {
                SetProgressBarValueHandler(value);
            }
        }
    }

    /// <summary>
    /// 上位机定时器 event and delegate
    /// </summary>
    public class TimerHelper
    {
        public static event setTimerDelegate SetShakeHandTimer;

        public static void SetShakeHandTimerDuringInit()
        {
			if (SetShakeHandTimer != null)
			{
				SetShakeHandTimer();
			}
        }

    }


    public class LogicalHandler
    {
 
        public static event DateTimeFormat DateFormat;

        public static void UpdateDateTimeFormat(string format)
        {
			if (DateFormat != null)
			{
				DateFormat(format);
			}
		}

    }


    public class ClientStatus
    {
        //
        public static event  ChangeStationStatus ChangeStatus;

        public static void UpdateStationsState(int station, int what, TestState state)
        {
            if (ChangeStatus != null)
            {
                ChangeStatus(station,  what,  state);
            }
        }

        // work with the middle layer code communicate with yuran
        public static event ChangeStationData ChangeStationState;

        public static void UpdateStationsData()
        {
            if (ChangeStationState != null)
            {
                ChangeStationState();
            }
        }
    }

}
