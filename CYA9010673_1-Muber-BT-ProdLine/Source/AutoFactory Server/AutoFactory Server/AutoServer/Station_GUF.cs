using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Network;
using System.Timers;
using AH.Util;
namespace AH.AutoServer
{
    public class GUF
    {
        public int stationId;  //same as the client id
        public int stationNo;
        public bool isGufFeedBack;
        public int gufStatus;  // 0-OK, 1-NOK, 2-Unknown
        public Timer aTimer = new Timer(20000);

        public GUF(int stationId, int stationNo, int gufStatus)
        {
            InitGUF(stationId, stationNo, gufStatus);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedShow);
            aTimer.AutoReset = false;
        }

        public GUF()
        {

        }
        ~GUF()
        {

        }

        /// <summary>
        /// when server haven't receive the guf feedback in time, then do this action
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTimedShow(object source, ElapsedEventArgs e)
        {
            isGufFeedBack = false;
            LogerHelper2.ToLog("Server doesn't receive the guf feedback in time", 3);
            Server.stationList[stationId - 1].errorInfo = "Server doesn't receive the guf feedback in time";
            ClientGUFStatus.UpdateStationsData(stationId, 4, ClientStatusType.Error);
        }

        /// <summary>
        /// int guf list
        /// </summary>
        /// <param name="stationId">station id is like 1,2,3,4,5,6</param>
        /// <param name="stationNo">station no</param>
        /// <param name="gufStatus"></param>
        public void InitGUF(int stationId, int stationNo, int gufStatus)
        {
            isGufFeedBack = true;
            switch (stationNo)
            {
                case 1:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                case 2:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                case 3:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                case 4:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                case 5:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                case 6:
                    this.stationId = -1;
                    this.stationNo = stationNo;
                    this.gufStatus = gufStatus;
                    break;
                default:
                    break;
            }
        }
    }
}
