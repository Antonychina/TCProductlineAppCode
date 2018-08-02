using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AH.Network;
using AH.AutoServer;
using System.IO;
namespace AH.Util
{
    class DUTReTest
    {

        public static bool isSupportReTest = false;
        public static int alarmWhenDUTTestFailTimes = 2;

        /// <summary>
        /// check whether the dut need retest?
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="dutID"></param>
        /// <returns>true, false</returns>
        public static bool isNeedReTest(int stationID, string dutID)
        {
            //still need add code to check the DUT ID, whether this DUT is the first time tested or not
            if (isSupportReTest && dutID == Server.stationList[stationID - 1].onTestingSerialNumber && Server.stationList[stationID - 1].currentDUTtestedCount <= 1)// before is 2, to support change station retest
                return true;
            else
                return false;
        }

        /// <summary>
        /// is the dut Need to Change to other Test Station for ReTest
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="dutID"></param>
        /// <returns>true, false</returns>
        public static bool isNeedChangeTestStationToReTest(int stationID, string dutID, out int nextFreeStationID)
        {
            //still need add code to check the DUT ID, whether this DUT is the first time tested or not
            if (isSupportReTest && dutID == Server.stationList[stationID - 1].onTestingSerialNumber && Server.stationList[stationID - 1].currentDUTtestedCount >= 2)
            {
                if (Server.FetchFreeStation(stationID) >= 1 && Server.FetchFreeStation(stationID) <= 6)
                {
                    Server.stationList[stationID - 1].clientStatusType = ClientStatusType.Wait;
                    nextFreeStationID = Server.FetchFreeStation(stationID);
                    return true;
                }
                else
                {
                    nextFreeStationID = 0;
                    return false;
                }
            }
            else
            {
                nextFreeStationID = 0;
                return false;
            }
        }
    }

}
