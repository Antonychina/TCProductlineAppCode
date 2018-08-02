using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading.Tasks;
using AH.Network;
using AH.AutoServer;
using AH.Util;

namespace AH.AutoServer
{
    public class Station
    {
        public int stationId;
        public int stationNo;
        public string ahaddress;
        public int clientId;
        public int currentDUTtestedCount;
        public int DUTTestConsecutiveFailedTimes;
        public string onTestingSerialNumber = "";
        public string errorInfo = "";
        public ClientStatusType clientStatusType;
        public bool isAvaliable;
        public bool isForcedOffLine = false;
        public Timer aTimer = new Timer(91000);
        public int hertbeatCount = 0;
        public Timer timeoutTimer = new Timer(3600 * 1000);


        public Station(int StationId)
        {
            InitStationList(StationId);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedShow);
            aTimer.AutoReset = false;

            timeoutTimer.Elapsed += timeoutTimer_Elapsed;
            timeoutTimer.AutoReset = false;
        }

        void timeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LogerHelper2.ToLog("Station " + stationId.ToString() + " has Started over " + timeoutTimer.Interval / 60000 + " Mins", 3);
            Server.stationList[stationId - 1].errorInfo = "Station " + stationId.ToString() + " has started over " + timeoutTimer.Interval / 60000 + " Mins";
            ClientGUFStatus.UpdateStationsData(stationId, 4, ClientStatusType.Error);
        }
        ~Station()
        {

        }

        /// <summary>
        /// when server haven't receive the client in time, then do this action
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTimedShow(object source, ElapsedEventArgs e)
        {
            hertbeatCount = 0;
            LogerHelper2.ToLog("Server doesn't receive the Client Heartbeat in time from station" + stationId.ToString(), 3);
            Server.stationList[stationId - 1].errorInfo = "Server doesn't receive the Client Heartbeat in time";
            ClientGUFStatus.UpdateStationsData(stationId, 1, ClientStatusType.Error);
            ClientGUFStatus.UpdateStationsData(stationId, 4, ClientStatusType.Error);
        }

        /// <summary>
        /// init station list
        /// </summary>
        /// <param name="StationId">station id is like 1,2,3,4,5,6</param>
        public void InitStationList(int StationId)
        {
            currentDUTtestedCount = 0;
            DUTTestConsecutiveFailedTimes = 0;
            clientStatusType = ClientStatusType.Unknown;
            switch (StationId)
            {
                case 1:
                    stationId = 1;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                case 2:
                    stationId = 2;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                case 3:
                    stationId = 3;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                case 4:
                    stationId = 4;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                case 5:
                    stationId = 5;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                case 6:
                    stationId = 6;
                    stationNo = -1;
                    ahaddress = "0.0.0.0";
                    break;
                default:
                    break;

            }
        }
    }
}
