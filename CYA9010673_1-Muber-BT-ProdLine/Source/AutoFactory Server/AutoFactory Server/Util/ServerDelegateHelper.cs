using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AH.Network;
using AH.PLCModel;

namespace AH.AutoServer
{
    /// <summary>
    /// Station Client and GUF data Changed
    /// </summary>
    /// <param name=></param>
    public delegate void ChangeStationData(int station, int what, ClientStatusType state);

    public class ClientGUFStatus
    {

        // work with the middle layer code communicate with yuran
        public static event ChangeStationData ChangeStationState;

        public static void UpdateStationsData(int station, int what, ClientStatusType state)
        {
            if (ChangeStationState != null)
            {
                ChangeStationState(station, what, state);
            }
        }
    }

    /// <summary>
    /// robot PLC data Changed
    /// </summary>
    /// <param name=></param>
    public delegate void ChangeRobotPLC(string PLCStatus);

    public class RobotPLCStatus
    {


        public static event ChangeRobotPLC ChangeRobotPLCState;

        public static void UpdateRobotPLCStatus(string PLCStatus)
        {
            if (ChangeRobotPLCState != null)
            {
                ChangeRobotPLCState(PLCStatus);
            }
        }
    }

    /// <summary>
    /// record tester yield data
    /// </summary>
    /// <param name=></param>
    public delegate void RecordTesterData(string stationID, ClientStatusType testResult);

    public class TestStationData
    {

        public static event RecordTesterData recordTesterResult;

        public static void RecordTesterYieldData(string stationID, ClientStatusType testResult)
        {
            if (recordTesterResult != null)
            {
                recordTesterResult(stationID, testResult);
            }
        }
    }

    public delegate void SimulatePLC(object sender, MainPLCMessage msg);
    public delegate void ResetTestStation(int stationNo);
    public delegate void ManuallyMoveRobotPLCEvertHeader(int stationNo, string result);
    public delegate void ManuallyRestestEvertHeader(int stationNo, string dutid);
    public delegate void ResetRobotPLC();
    public delegate void AbortTester(int stationNo);
    public delegate void Clearconnection();
    public delegate void UpdateReTestRule(bool isRetest, int alarmAfterDUTFailTimes);
    public delegate void UpdateTesterTimeout(int timeout);

    public class SendMsgToServer
    {

        public static event SimulatePLC simulatePLCToServer;

        public static void PLCSendMsgToServer(object sender, MainPLCMessage msg)
        {
            if (simulatePLCToServer != null)
            {
                simulatePLCToServer(sender, msg);
            }
        }

        public static event ResetTestStation resetTestClient;

        public static void ResetTestStationStatus(int stationNo)
        {
            if (resetTestClient != null)
            {
                resetTestClient(stationNo);
            }
        }

        public static event ManuallyMoveRobotPLCEvertHeader moveRobotPLCManually;

        public static void MoveRobotPLCManually(int stationNo,string result)
        {
            if (moveRobotPLCManually != null)
            {
                moveRobotPLCManually(stationNo, result);
            }
        }

        public static event ManuallyRestestEvertHeader ManuallyRestest;

        public static void ManuallyDUTRestest(int stationNo, string dutid)
        {
            if (ManuallyRestest != null)
            {
                ManuallyRestest(stationNo, dutid);
            }
        }

        public static event ResetRobotPLC resetRobotPLC;

        public static void ResetRobotPLCStatus()
        {
            if (resetRobotPLC != null)
            {
                resetRobotPLC();
            }
        }

        public static event AbortTester abortAnTester;

        public static void AbortDUTTester(int stationNo)
        {
            if (abortAnTester != null)
            {
                abortAnTester(stationNo);
            }
        }

        public static event Clearconnection clearAllConnections;

        public static void ClearAliveConnection()
        {
            if (clearAllConnections != null)
            {
                clearAllConnections();
            }
        }

        public static event UpdateReTestRule updateReTestRule;

        public static void UpdateDUTReTestRule(bool isSupportReTest, int alarmWhenDUTTestFailTimes)
        {
            if (updateReTestRule != null)
            {
                updateReTestRule(isSupportReTest, alarmWhenDUTTestFailTimes);
            }
        }


        public static event UpdateTesterTimeout updateTesterTimeout;

        public static void UpdateTestersTimeout(int timeout)
        {
            if (updateTesterTimeout != null)
            {
                updateTesterTimeout(timeout);
            }
        }

    }

}
