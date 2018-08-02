using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Timers;
using System.Collections;
using System.Threading.Tasks;
using AH.Network;
using AH.Util;
using System.Drawing;
using AH.PLCModel;
using AutoFactory_Server.DutOutterBuffServer;

namespace AH.AutoServer
{
    public class Server
    {
        bool PlCisbusy;
        bool isOutBufferFree;
        string newDutNm;
        public static bool[] isKill;
        public static Station[] stationList = new Station[6];
        public static GUF[] gufList = new GUF[6]; //added by antony, for GUF debug
        TcpServer server = new TcpServer();
        MainPLCServer plcserver = new MainPLCServer();
        DutOutterBuffServer outBufferServer = new DutOutterBuffServer();
        public static List<string> dutIdListInQueue = new List<string>();
        public static List<string> plctasksListInQueue = new List<string>();
        private int stationTimeoutTime = 3600 * 1000;
        //string MoveOffSationID = null;
        //Queue newSerailNumber = new Queue();
        Connection ctn;
        delegate void ExitDelegate();
        event ExitDelegate exit;


        public Server()
        {
            SendMsgToServer.simulatePLCToServer += plcserver_MessageReceived;
            SendMsgToServer.resetTestClient += resetTestClientStatus;
            SendMsgToServer.abortAnTester += abortClient;
            SendMsgToServer.updateReTestRule += updateReTestRuleSetting;
            SendMsgToServer.resetRobotPLC += resetRobotPLCState;
            SendMsgToServer.moveRobotPLCManually += SendMsgToServer_moveRobotPLCManually;
            SendMsgToServer.ManuallyRestest += SendMsgToServer_ManuallyRestest;
            SendMsgToServer.updateTesterTimeout += SendMsgToServer_updateTesterTimeout;

        }




        ~Server()
        {

        }

        #region InitServer
        /// <summary>
        /// init the stationlist and guflist
        /// start tcp/plc server then monitor the client 
        /// </summary>
        /// <param name="ip4Client"></param>
        /// <param name="ip4PLC"></param>
        /// <param name="clientPort"></param>
        /// <param name="PLCPort"></param>
        /// <param name="bInitAll"></param>
        public void InitServer(string ip4Client, string ip4PLC, int clientPort, int PLCPort,int OutBufferPort, bool bInitAll)
        {
            //int port = 8090;
            //int plcport = 2219;
            LogerHelper2.ToLog("================== Server Started ==================", 2);
            if (bInitAll)
            {
                PlCisbusy = false;
                isKill = new bool[6];
                newDutNm = null;

                #region station_init
                for (int i = 0; i < stationList.Length; i++)
                {
                    stationList[i] = new Station(i + 1);
                }
                #endregion station_init

                #region GUF_init
                for (int i = 0; i < gufList.Length; i++)
                {
                    gufList[i] = new GUF(-1, i + 1, 2);
                }
                #endregion GUF_init

                server.MessageReceived += server_MessageReceived;
                plcserver.MessageReceived += plcserver_MessageReceived;
                outBufferServer.OutBuffChanged += outBufferServer_OutBuffChanged;
                //init
                server.StartTCPServer(ip4Client, clientPort);
                plcserver.ServerInitial(ip4PLC, PLCPort);
                outBufferServer.ServerInitial(ip4PLC, OutBufferPort);
                
            }
            else
            {
                server.StartTCPServer(ip4Client, clientPort);
                plcserver.ServerInitial(ip4PLC, PLCPort);
                outBufferServer.ServerInitial(ip4PLC, OutBufferPort);
            }

        }




        public void InitClient(string serverIp, int serverPort)
        {
            //int port = 8090;
            //int plcport = 2219;
            LogerHelper2.ToLog("================== start to connect Server ==================", 2);

            ctn = new Network.Connection();
            
       
            //ctn.ServerConnected += ctn_ServerConnected;
            exit += ctn.DisconnectFromServer;
      
            ctn.ConnectToServer(serverIp, serverPort);

        }


        public void SendMessageToWS(string _prodlineNo, string _messge, MsgType _msgType)
        {
            //
            LogerHelper2.ToLog("================== send message to warehouse ==================", 2);

            
            //if (_msgType == MsgType.Register)
            {
                //此处 _messge 为 订单号/产品号/订单数量， 用 '|' 分割
                ctn.SendMsg(new Network.Message(_prodlineNo, 0, _msgType, _messge.Trim()));//生产前的备料请求
            }
            //else
            //{
                //此处 _messge 为 料号
                //ctn.SendMsg(new Network.Message(_prodlineNo, 0, _msgType, _messge));   //生产中的补料和紧急补料请求
            //}
            
        }


        public bool SendHeartbeat()
        {
            return ctn.SendMsg(new Network.Message("", 0, MsgType.Heartbeat, ""));
        }

        void ctn_ServerConnected(object sender, bool isconnected)
        {
            // clientStatus = tm.QueryTmStatus();

       
        }


        void outBufferServer_OutBuffChanged(object sender, bool newState)
        {
            isOutBufferFree = newState;
            if (newState)
            {
            }
          
        }

        #endregion

        #region PLC_Messages
        /// <summary>
        /// the event of server receive message from PLC, different messagetype do different action
        /// </summary>
        /// <param name="sender">plc ip&port</param>
        /// <param name="msg">plc message</param>
        public void plcserver_MessageReceived(object sender, MainPLCMessage msg)
        {
            if (msg.InvalidMessage == true)
            {
                LogerHelper2.ToLog("error format PLC commond" + msg.Info, 3);
                return;
            }
            string msgTemp = null;
            try
            {
                msgTemp = string.Format("From PLC: MessageHead: {0}\t Info:{1}", msg.MsgHead.ToString(), msg.Info.ToString());
            }
            catch (Exception e)
            {
                LogerHelper2.ToLog(e.Message, 2);
            }

            //LogerHelper2.ToLog(msg.MsgType + " " + msg.MsgHead + " " + msg.StationId + " " + msg.Info, 0);

            if (msg.MsgType != PLCMsgType.Heartbead)
            {
                LogerHelper2.ToLog(msgTemp, 0);
            }
            else
            {
                LogerHelper2.ToLog(msgTemp, 4);
            }
            int freeStationID;
            string MessageHead = msg.MsgHead;
            string MessageInfo = msg.Info;
            //LogerHelper2.ToLog(msg.MsgType + " " , 0);
            switch (msg.MsgType)
            {
                case PLCMsgType.ReportMsg:
                    if (MessageHead == MainPLCMessage.DUTIdToServer)
                    {
                        plcserver.NewSNFeedback();  //feedback plc F020

                        //check the dutid, if it same as the current test duts? ignore it if yes
                        if (!check_DUTID_Valid(msg.Info))
                        {
                            LogerHelper2.ToLog("Found new Dut Id <" + msg.Info + ">  is same as current testing Duts' Id, will ignore this new Dut Id ", 2);
                            LogerHelper2.ToAutoTestLogFile(DateTime.Now.ToString() + ": Dut ID: " + msg.Info + " duplicated with the current testing Duts' Id, will ignore it.");
                            return;
                        }

                        freeStationID = FetchFreeStation();
                        int loop = 0;
                        while (loop < stationList.Length)
                        {
                            newDutNm = msg.Info;
                            //newSerailNumber.Enqueue(msg.Info);  // when coming a new sn, add to qu
                            if (freeStationID >= 1 && freeStationID <= 6)
                            {
                                stationList[freeStationID - 1].clientStatusType = ClientStatusType.Wait;
                                //if (server.SendMsg(new Message(freeStationID.ToString(), 1, MsgType.QueryClientStatus, ""), stationList[freeStationID - 1].ahaddress))
                                //{
                                //    break;
                                //}

                                freeStationID = FetchFreeStation(freeStationID);
                            }
                            else
                            {
                                //idMessageQueue.sendMsg("DUTID", msg.Info, System.Messaging.MessagePriority.Normal);  // if there is no idle station, send the id message to messagequeue
                                //LogerHelper2.ToLog("NO freestation, add newdutid <" + msg.Info + ">  to queue. idqueue number is: " + idMessageQueue.messageNum, 2);
                                break;
                            }

                            loop++;
                        }
                        if (loop >= stationList.Length)
                        {
                            //idMessageQueue.sendMsg("DUTID", msg.Info, System.Messaging.MessagePriority.Normal);  // if there is no idle station, send the id message to messagequeue
                            //LogerHelper2.ToLog("NO freestation, add newdutid <" + msg.Info + ">  to queue. idqueue number is: " + idMessageQueue.messageNum, 2);
                        }
                    }
                    break;
                case PLCMsgType.Feedback:
                    plcserver.stopPlcCommandTimer();
                    System.Threading.Thread.Sleep(1000); //sleep wait PLC ready

                    if (MessageHead == MainPLCMessage.MoveDUTToStationACK)
                    {
                        //PlCisbusy = false;
                        RobotPLCStatus.UpdateRobotPLCStatus("Idle");
                        server.SendMsg(new Message(msg.StationId.ToString(), 1, MsgType.NewDut, stationList[msg.StationId - 1].onTestingSerialNumber), stationList[msg.StationId - 1].ahaddress);//dutid
                        UpdateStation(msg.StationId.ToString(), ClientStatusType.Run);
                        //PlcMessageQueue.DeleteMsg(msg.StationId.ToString());
                        PlCisbusy = false;
                    }
                    else if (MessageHead == MainPLCMessage.MoveDUTFromStationACK)
                    {
                        isOutBufferFree = false;

                        // here add if to check whether the tester is set to offline?
                        if ((stationList[msg.StationId - 1].isForcedOffLine == true) || (stationList[msg.StationId - 1].DUTTestConsecutiveFailedTimes >= DUTReTest.alarmWhenDUTTestFailTimes))
                        {
                            stationList[msg.StationId - 1].isAvaliable = false;
                            stationList[msg.StationId - 1].isForcedOffLine = false;
                            stationList[msg.StationId - 1].errorInfo = "This station was forced offline by admin or DUT test consecutive failures";
                            UpdateStation((msg.StationId).ToString(), ClientStatusType.Error);

                            ClientGUFStatus.UpdateStationsData(msg.StationId, 2, ClientStatusType.Unknown);
                            ClientGUFStatus.UpdateStationsData(msg.StationId, 3, ClientStatusType.Unknown);

                        }
                        else
                        {
                            //PlCisbusy = false;
                            RobotPLCStatus.UpdateRobotPLCStatus("Idle");
                            stationList[msg.StationId - 1].isAvaliable = true;
                            stationList[msg.StationId - 1].currentDUTtestedCount = 0;
                            stationList[msg.StationId - 1].onTestingSerialNumber = "";
                            UpdateStation((msg.StationId).ToString(), ClientStatusType.Idle);
                        }

                        PlCisbusy = false;
                    }
                    else if (MessageHead == MainPLCMessage.queryPLCStateACK)
                    {
                        PlCisbusy = msg.PLCIsBusy;
                    }

                    if (PlCisbusy == false)
                        QueryMessageListAndRunFirstMessage("DUTSTATUS");
                    break;
                case PLCMsgType.Heartbead:
                    // RobotPLCStatus.UpdateRobotPLCStatus("Connected");
                    plcserver.CheckHeartbeat();
                    break;
                case PLCMsgType.Ack:
                    plcserver.plcResendCommandTimer.Stop();
                    plcserver.lastAction = string.Empty;
                    break;
                default:
                    break;
            }

        }

        #endregion PLC_Messages

        #region Client_Messages
        /// <summary>
        /// the event of server receive message from station client, different messagetype do different action
        /// </summary>
        /// <param name="sender">station client ip&port</param>
        /// <param name="msg">station client message</param>
        public void server_MessageReceived(object sender, Message msg)
        {

            if (msg.InvalidMessage == true)
            {
                LogerHelper2.ToLog("error format commond" + msg.Info, 3);
                return;
            }
            string msgTemp = string.Format("From: {1}   Info:{0}", msg.totalmessage, sender.ToString());

        
            CommonMethod.RegisterIPtoStation(msg.stationId, sender.ToString());
            switch (msg.MsgType)
            {
                case MsgType.Status:
                    AH_Status(msg);
                    break;
                case MsgType.Register:
                    AH_Register(msg, "r");
                    break;
               
                default:
                    break;
            }
        }

        #endregion Client_Messages

        #region AH_TM_Method
        /// <summary>
        /// the message is about TM test states, like pass,fail, abort, error,idle and running
        /// </summary>
        /// <param name="msg"></param>
        public void AH_Status(Message msg)
        {
            if (msg.Info == "P")
            {
                stationList[int.Parse(msg.stationId) - 1].DUTTestConsecutiveFailedTimes = 0;
                UpdateStation(msg.stationId, ClientStatusType.Pass);
                ControlPLCMoveDUTAwayStation(msg);

                //record test result for test yield statistics
                TestStationData.RecordTesterYieldData(msg.stationId, ClientStatusType.Pass);

                //record dut test result to system log
                LogerHelper2.RecordDutTestResultToSystemLog(msg.stationId, msg.DutID.Split('|')[0], ClientStatusType.Pass);
            }
            else if (msg.Info == "F" || msg.Info == "A")
            {
                string[] strmsg = msg.DutID.Split('|');
                string DutId = strmsg[0];
                if (strmsg.Length > 1)  //print the fail infromation in windows
                {
                    stationList[int.Parse(msg.stationId) - 1].errorInfo = strmsg[1];
                    ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 4, ClientStatusType.Fail);
                }
                stationList[int.Parse(msg.stationId) - 1].currentDUTtestedCount++;
                UpdateStation(msg.stationId, msg.Info == "F" ? ClientStatusType.Fail : ClientStatusType.Abort);
                if (DUTReTest.isNeedReTest(int.Parse(msg.stationId), DutId) && isKill[int.Parse(msg.stationId) - 1] == false) // check whether need retest 
                {
                    server.SendMsg(new Message(msg.stationId, 1, MsgType.NewDut, DutId), stationList[int.Parse(msg.stationId) - 1].ahaddress);//dutid     
                }
                else
                {
                    if (isKill[int.Parse(msg.stationId) - 1] == true)
                    {
                        isKill[int.Parse(msg.stationId) - 1] = false;
                    }
                    else
                    {
                        stationList[int.Parse(msg.stationId) - 1].DUTTestConsecutiveFailedTimes++;
                    }
                    ControlPLCMoveDUTAwayStation(msg);

                    //record test result for test yield statistics
                    TestStationData.RecordTesterYieldData(msg.stationId, ClientStatusType.Fail);
                    //record dut test result to system log
                    LogerHelper2.RecordDutTestResultToSystemLog(msg.stationId, DutId, ClientStatusType.Fail);

                    if (stationList[int.Parse(msg.stationId) - 1].DUTTestConsecutiveFailedTimes >= DUTReTest.alarmWhenDUTTestFailTimes)
                    {
                        stationList[int.Parse(msg.stationId) - 1].isAvaliable = false;
                        stationList[int.Parse(msg.stationId) - 1].errorInfo = "DUT test consecutive failures";
                        UpdateStation(msg.stationId, ClientStatusType.Error);
                    }
                }

                //   ControlPLCMoveDUTAwayStation(msg); 

            }
            else if (msg.Info == "I")
            {
                UpdateStation(msg.stationId, ClientStatusType.Idle);
            }
            else if (msg.Info == "R")
            {
                UpdateStation(msg.stationId, ClientStatusType.Run);
            }
            else if (msg.Info == "E")
            {
                //stationList[int.Parse(msg.stationId) - 1].isAvaliable = false;
                stationList[int.Parse(msg.stationId) - 1].errorInfo = msg.ErrorInfo;
                UpdateStation(msg.stationId, ClientStatusType.Error);
            }
            else
            {
                LogerHelper2.ToLog("Client " + msg.stationId + "sends strange status", 4);
            }
        }

        /// <summary>
        /// the message is about register for station client
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pos"></param>
        public void AH_Register(Message msg, string pos)
        {
            if (pos == "r")
            {
                stationList[int.Parse(msg.stationId) - 1].stationNo = int.Parse(msg.Info);
                stationList[int.Parse(msg.stationId) - 1].isAvaliable = true;
                stationList[int.Parse(msg.stationId) - 1].errorInfo = "";  // after 
                stationList[int.Parse(msg.stationId) - 1].aTimer.Enabled = true; //timer可用
                //UpdateStation(msg.stationId, ClientStatusType.Idle);
                LogerHelper2.ToAutoTestLogFile(DateTime.Now.ToString() + ": Tester " + msg.stationId + " register to server");
            }
            else if (pos == "g")
            {
                gufList[int.Parse(msg.stationId) - 1].stationNo = int.Parse(msg.Info);

                if (gufList[int.Parse(msg.stationId) - 1].stationId == -1)
                {
                    // first time to register GUF
                    gufList[int.Parse(msg.stationId) - 1].gufStatus = 0; //0-OK
                    gufList[int.Parse(msg.stationId) - 1].stationId = int.Parse(msg.stationId);
                    UpdateStation(msg.stationId, ClientStatusType.Idle);
                }
                else
                {
                    //recover the GUF
                    int stid = int.Parse(msg.stationId);
                    //   if (gufList[stid - 1].gufStatus == 1 && (stationList[stid - 1].errorInfo.ToLower().Contains("heartbeat") && stationList[stid - 1].errorInfo.ToLower().Contains("guf"))) // exception
                    {
                        gufList[int.Parse(msg.stationId) - 1].gufStatus = 0; //0-OK
                        stationList[stid - 1].errorInfo = "";
                        ClientGUFStatus.UpdateStationsData(stid, 3, ClientStatusType.Pass);
                        ClientGUFStatus.UpdateStationsData(stid, 4, ClientStatusType.Unknown);
                        if (stationList[stid - 1].clientStatusType == ClientStatusType.Idle && stationList[stid - 1].isAvaliable == true)
                        {
                            QueryMessageListAndRunFirstMessage("DUTID");
                        }

                    }

                }
                LogerHelper2.ToAutoTestLogFile(DateTime.Now.ToString() + ": GUF " + msg.stationId + " register to server");
            }
            else if (pos == "x")
            {
                gufList[int.Parse(msg.stationId) - 1].stationNo = int.Parse(msg.Info);
                gufList[int.Parse(msg.stationId) - 1].gufStatus = 0; //0-OK
                gufList[int.Parse(msg.stationId) - 1].stationId = int.Parse(msg.stationId);
                stationList[int.Parse(msg.stationId) - 1].errorInfo = "";  // after 
                stationList[int.Parse(msg.stationId) - 1].stationNo = int.Parse(msg.Info);
                stationList[int.Parse(msg.stationId) - 1].isAvaliable = true;
                stationList[int.Parse(msg.stationId) - 1].aTimer.Enabled = true; //timer可用
                UpdateStation(msg.stationId, ClientStatusType.Idle);
                LogerHelper2.ToAutoTestLogFile(DateTime.Now.ToString() + ": Tester & GUF " + msg.stationId + " register to server");
            }
            //server.SendMsg(new Message(msg.stationId, msg.MsgId, MsgType.Ack, "ack ok"), stationList[int.Parse(msg.stationId) - 1].ahaddress);
        }

        /// <summary>
        /// the message is about guf status feedback when server send the query to station client
        /// </summary>
        /// <param name="msg"></param>
        public void AH_GufFeedback(Message msg)
        {
            if (msg.Info == "E")
            {
                stationList[int.Parse(msg.stationId) - 1].errorInfo = msg.ErrorInfo;
                //re-load the FetchFreeStation, to avoid the station that just with GUF abnormal will not be used forever 
                int freeStationID = FetchFreeStation(int.Parse(msg.stationId));
                if (freeStationID >= 1 && freeStationID <= 6)
                {
                    stationList[freeStationID - 1].clientStatusType = ClientStatusType.Wait;
                    //server.SendMsg(new Message(freeStationID.ToString(), 1, MsgType.QueryClientStatus, ""), stationList[freeStationID - 1].ahaddress);
                }
                else
                {
                    //idMessageQueue.sendMsg("DUTID", newDutNm, System.Messaging.MessagePriority.Normal);  // if there is no idle station, send the id message to messagequeue
                    //LogerHelper2.ToLog("NO freestation, add newdutid <" + newDutNm + ">  to queue. idqueue number is: " + idMessageQueue.messageNum, 2);
                }
                return;
            }
            string[] strmsg2 = msg.Info.Split('|');
            string ifgufopen = strmsg2[0];
            string ifdut = strmsg2[1];
            if (ifdut == "H" || ifgufopen == "C")
            {
                //stationList[int.Parse(msg.stationId) - 1].isAvaliable = false;
                //stationList[int.Parse(msg.stationId) - 1].clientStatusType = ClientStatusType.Error;
                //IF fixture is not abnormal, then show the warning, but station is still avaliable, or else this station will always unavaliable
                gufList[int.Parse(msg.stationId) - 1].gufStatus = 1;  // 1-NOK
                stationList[int.Parse(msg.stationId) - 1].errorInfo = "Have Dut or the fixture is connect";// msg.ErrorInfo;
                stationList[int.Parse(msg.stationId) - 1].clientStatusType = ClientStatusType.Idle;
                ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 3, ClientStatusType.Error);
                ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 4, ClientStatusType.Error);

                //re-load the FetchFreeStation, to avoid the station that just with GUF abnormal will not be used forever 
                int freeStationID = FetchFreeStation(int.Parse(msg.stationId));
                if (freeStationID >= 1 && freeStationID <= 6)
                {
                    stationList[freeStationID - 1].clientStatusType = ClientStatusType.Wait;
                    //server.SendMsg(new Message(freeStationID.ToString(), 1, MsgType.QueryClientStatus, ""), stationList[freeStationID - 1].ahaddress);
                }
                else
                {
                    //idMessageQueue.sendMsg("DUTID", newDutNm, System.Messaging.MessagePriority.Normal);  // if there is no idle station, send the id message to messagequeue
                    //LogerHelper2.ToLog("NO freestation, add newdutid <" + newDutNm + ">  to queue. idqueue number is: " + idMessageQueue.messageNum, 2);
                }
            }
            else if (ifdut == "N" && ifgufopen == "O")
            {
                if (PlCisbusy == false && plcserver.isPLConLine == true)
                {//update the GUI if it was abnormal before
                    stationList[int.Parse(msg.stationId) - 1].errorInfo = "";
                    gufList[int.Parse(msg.stationId) - 1].gufStatus = 0;

                    ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 3, ClientStatusType.Pass);
                    ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 4, ClientStatusType.Unknown);

                    //MoveToStationID = freeStationID;
                    stationList[int.Parse(msg.stationId) - 1].onTestingSerialNumber = newDutNm;  //newSerailNumber.Dequeue().ToString();

                    plcserver.MoveDUTtoTester(msg.stationId);

                    PlCisbusy = true;
                }
                else if (PlCisbusy == true)
                {
                    stationList[int.Parse(msg.stationId) - 1].clientStatusType = ClientStatusType.Idle;
                    //idMessageQueue.sendMsg("DUTID", newDutNm, System.Messaging.MessagePriority.Normal);
                    //LogerHelper2.ToLog("plc is busy,  add <" + newDutNm + "> to idqueue again, id queue number is:" + idMessageQueue.messageNum, 2);
                }
                else
                {
                    //idMessageQueue.sendMsg("DUTID", newDutNm, System.Messaging.MessagePriority.Normal);
                    //LogerHelper2.ToLog("plc is offline,  add <" + newDutNm + "> to idqueue again, id queue number is:" + idMessageQueue.messageNum, 2);
                }

            }
        }
        #endregion AH_TM_Method

        #region Robot_PLC
        /// <summary>
        /// send commond to PLC to move the DUT to pass area or fail area in conveyer belt
        /// </summary>
        /// <param name="msg"></param>
        public void ControlPLCMoveDUTAwayStation(Message msg)
        {
            if (PlCisbusy == false && plcserver.isPLConLine == true)
            {
                if (stationList[int.Parse(msg.stationId) - 1].clientStatusType != ClientStatusType.Run)
                {
				    PlCisbusy = true;
                    if (msg.Info == "P")
                    {

                        plcserver.MoveDUTFromTester(msg.stationId, "00"); //testresult =00 ,pass +
                    }
                    else if (msg.Info == "F" || msg.Info == "A")
                    {
                        plcserver.MoveDUTFromTester(msg.stationId, "01");//testresult =01 ,fail+
                    }
                    else
                        plcserver.MoveDUTFromTester(msg.stationId, "01");//testresult =01 ,fail+

                    //MoveOffSationID = msg.stationId;
                }
            }
            else
            {
                if (check_PLCTask_Valid(msg.stationId + ":" + msg.Info))
                {
                    //PlcMessageQueue.sendMsg("DUTSTATUS", msg.stationId + ":" + msg.Info, System.Messaging.MessagePriority.Normal);
                }
                else
                {
                   // LogerHelper2.ToLog("Duplicated PLC task, will not add to PLC Queue <" + msg.stationId + ":" + msg.Info + "> to queue. plcqueue number is:" + PlcMessageQueue.messageNum, 2);
                }

                if (PlCisbusy == true)
                {
                    //LogerHelper2.ToLog("PLC is busy, add plc active <" + msg.stationId + ":" + msg.Info + "> to queue. plcqueue number is:" + PlcMessageQueue.messageNum, 2);
                }
                else
                {
                    //LogerHelper2.ToLog("PLC is offline, add plc active <" + msg.stationId + ":" + msg.Info + "> to queue. plcqueue number is:" + PlcMessageQueue.messageNum, 2);
                }
            }

        }
        #endregion Robot_PLC

        #region HeartBeat
        /// <summary>
        /// open the timer for moniter the station client heartbeat, if receiver the hearbeat then reset the timer
        /// </summary>
        /// <param name="stationId"></param>
        public void CheckHeartbeat(string stationId)
        {

            if (stationList[int.Parse(stationId) - 1].hertbeatCount == 0)
            {
                stationList[int.Parse(stationId) - 1].aTimer.Enabled = true; //timer可用 
                stationList[int.Parse(stationId) - 1].hertbeatCount++;
                if (stationList[int.Parse(stationId) - 1].errorInfo.ToLower().Contains("heartbeat") && stationList[int.Parse(stationId) - 1].errorInfo.ToLower().Contains("client")) // only update the timeout warning, other warning should kept
                {
                    stationList[int.Parse(stationId) - 1].errorInfo = "";
                    ClientGUFStatus.UpdateStationsData(int.Parse(stationId), 1, ClientStatusType.Pass);
                    ClientGUFStatus.UpdateStationsData(int.Parse(stationId), 4, ClientStatusType.Unknown);
                }
            }
            else
            {
                stationList[int.Parse(stationId) - 1].aTimer.Stop();
                stationList[int.Parse(stationId) - 1].aTimer.Start();
            }
        }
        #endregion HeartBeat

        #region MSG_Queue
        /// <summary>
        /// while the plc is idle, then query the PlcMessageQueue . if the PlcMessageQueue not null, then implement the item in this messagequeue
        /// while the station have idle one, then query the idMessageQueue . if the idMessageQueue not null, then implement the item in this messagequeue
        /// </summary>
        /// <param name="meassagetype"></param>
        public void QueryMessageListAndRunFirstMessage(string meassagetype)
        {
            string message;
            int freeStationID;
            string stationId;
            if (meassagetype == "DUTSTATUS")
            {
                //message = PlcMessageQueue.getMsg(meassagetype);
                //if (message == null)  // if PLC Queue is empty, then continue to handle the DUTID queue
                {
                    //if (idMessageQueue.messageNum > 0)
                    {
                       // message = idMessageQueue.getMsg("DUTID");
                    }
                }
            }
            else
            {
                //message = idMessageQueue.getMsg(meassagetype);
            }

            //if (message != null)
            //{
            //    //if (message.Contains(":"))
            //    {
            //        //LogerHelper2.ToLog("PLC is idle now, plcqueue out <" + message + "> plc queue number is:" + PlcMessageQueue.messageNum, 2);
            //        stationId = message.Substring(0, 1);
            //        if (stationList[int.Parse(stationId) - 1].clientStatusType != ClientStatusType.Run)
            //        {
            //            PlCisbusy = true;
            //            if (message.Contains("P"))
            //            {
            //                plcserver.MoveDUTFromTester(stationId, "00"); //testresult =00 ,pass +
            //            }
            //            else if (message.Contains("F") || message.Contains("E") || message.Contains("A"))
            //            {
            //                plcserver.MoveDUTFromTester(stationId, "01");//testresult =01 ,fail+
            //            }
            //        }

            //    }
            //    //else
            //    {
            //        //LogerHelper2.ToLog("have free station now, idqueue out <" + message + "> id queue number is:" + idMessageQueue.messageNum, 2);
            //        newDutNm = message;
            //        freeStationID = FetchFreeStation();
            //        if (freeStationID >= 1 && freeStationID <= 6 && PlCisbusy == false)
            //        {
            //            stationList[freeStationID - 1].clientStatusType = ClientStatusType.Wait;
            //            //server.SendMsg(new Message(freeStationID.ToString(), 1, MsgType.QueryClientStatus, ""), stationList[freeStationID - 1].ahaddress);
            //        }
            //        else
            //        {
            //           // idMessageQueue.sendMsg("DUTID", message, System.Messaging.MessagePriority.Highest);  // if there is no idle station, send the id message to messagequeue again, but with highest priority, as the message was at the 1st place before it was query out from the MQ
            //            //LogerHelper2.ToLog("No free station,  add <" + message + "> to idqueue again, id queue number is:" + idMessageQueue.messageNum, 2);
            //        }
            //    }
            //}

        }
        #endregion MSG_Queue

        #region Update_Station_Staus
        /// <summary>
        /// after reveiver the TM status , updatesation status according
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="ClientStatusType"></param>
        public void UpdateStation(string StationId, ClientStatusType ClientStatusType)
        {
            if (StationId == null || ClientStatusType == null)
            {
                LogerHelper2.ToLog("station id is null ", 3);
                return;
            }

            int id = int.Parse(StationId);
            int what = 0;    // 1- test station; 2- dut test; 3-GUF ; 4- warning
            switch (ClientStatusType)
            {

                case ClientStatusType.Error:
                    stationList[id - 1].clientStatusType = ClientStatusType.Error;

                    //ClientGUFStatus.UpdateStationsData(id, 2, stationList[id - 1].clientStatusType);
                    ClientGUFStatus.UpdateStationsData(id, 4, stationList[id - 1].clientStatusType);
                    if (stationList[id - 1].errorInfo.ToLower().Contains("guf"))  // just GUF error
                    {
                        gufList[id - 1].gufStatus = 1;
                        what = 3;
                    }
                    else if (stationList[id - 1].errorInfo.ToLower().Contains("crashed") && stationList[id - 1].errorInfo.ToLower().Contains("tm"))   // TM crashed
                    {
                        what = 2;
                    }
                    else if (stationList[id - 1].errorInfo.ToLower().Contains("pop-up") && stationList[id - 1].errorInfo.ToLower().Contains("window"))   // detect pop-up windows during test
                    {
                        what = 4;
                    }
                    else  // test staton error 
                    {
                        stationList[id - 1].isAvaliable = false;
                        ClientGUFStatus.UpdateStationsData(id, 2, stationList[id - 1].clientStatusType);
                        what = 1;
                    }
                    break;
                case ClientStatusType.Pass:
                    stationList[id - 1].clientStatusType = ClientStatusType.Pass;
                    stationList[id - 1].timeoutTimer.Stop();
                    what = 2;
                    break;
                case ClientStatusType.Fail:
                    stationList[id - 1].clientStatusType = ClientStatusType.Fail;
                    stationList[id - 1].timeoutTimer.Stop();
                    what = 2;
                    break;
                case ClientStatusType.Idle:
                    stationList[id - 1].clientStatusType = ClientStatusType.Idle;
                    stationList[id - 1].onTestingSerialNumber = "";
                    ClientGUFStatus.UpdateStationsData(id, 1, ClientStatusType.Pass);  // use Pass to on behalf of avaliable or normal
                    //LogerHelper2.ToLog("stationId is " + id + "," + "what = " + 1 , 0);
                    if (stationList[id - 1].errorInfo.Length == 0)
                    {
                        ClientGUFStatus.UpdateStationsData(id, 4, ClientStatusType.Unknown);  // use Pass to on behalf of avaliable or normal
                        //LogerHelper2.ToLog("stationId is " + id + "," + "what = " + 4, 0);
                    }
                    if (gufList[id - 1].gufStatus == 0)
                    {
                        ClientGUFStatus.UpdateStationsData(id, 3, ClientStatusType.Pass);  // use Pass to on behalf of avaliable or normal
                        //QueryMessageListAndRunFirstMessage("DUTID");
                        //LogerHelper2.ToLog("stationId is " + id + "," + "what = " + 3, 0);
                    }
                    what = 2;  // set the test status to idle, so that could test the rest tasks
                    QueryMessageListAndRunFirstMessage("DUTID");
                    break;
                case ClientStatusType.Unknown:
                    stationList[id - 1].clientStatusType = ClientStatusType.Unknown;
                    what = 2;
                    break;
                case ClientStatusType.Run:
                    stationList[id - 1].clientStatusType = ClientStatusType.Run;
                    if (stationTimeoutTime > 0)
                    {
                        stationList[id - 1].timeoutTimer.Interval = stationTimeoutTime;
                        stationList[id - 1].timeoutTimer.Enabled = true;
                        //stationList[id - 1].timeoutTimer.Start();
                    }
                    what = 2;
                    break;
                case ClientStatusType.Abort:
                    stationList[id - 1].clientStatusType = ClientStatusType.Abort;
                    stationList[id - 1].timeoutTimer.Stop();
                    what = 2;
                    break;
                default:
                    return;
            }

            // add deleget here
            ClientGUFStatus.UpdateStationsData(id, what, stationList[id - 1].clientStatusType);
            //LogerHelper2.ToLog("*********stationId is " + id + "," + "what = " + what, 0);
        }

        /// <summary>
        /// reset the test station to idle for use
        /// </summary>
        /// <param name="stationNo"></param>
        public void resetTestClientStatus(int stationNo)
        {
            UpdateStation(stationNo.ToString(), ClientStatusType.Idle);

            LogerHelper2.ToLog(" Station " + stationNo + " was reset.", 2);
        }


        /// <summary>
        /// the reset funtion open/close setting
        /// </summary>
        /// <param name="isSupportReTest"></param>
        public void updateReTestRuleSetting(bool isSupportReTest, int alarmAfterDUTFailTimes)
        {
            DUTReTest.isSupportReTest = isSupportReTest;
            DUTReTest.alarmWhenDUTTestFailTimes = alarmAfterDUTFailTimes;
            LogerHelper2.ToLog(("Re-test Rule Setting to " + isSupportReTest), 2);
            LogerHelper2.ToLog(("Alarm After DUT Fail Times Setting to " + alarmAfterDUTFailTimes), 2);
        }


        #endregion Update_Station_Staus

        #region Fetch_Free_Station
        /// <summary>
        /// fetch the whether have free station for test
        /// </summary>
        /// <returns></returns>
        public int FetchFreeStation()
        {
            int[] freeStationList = { 0, 0, 0, 0, 0, 0 };
            int freestationCount = 0;
            int i;
            for (i = 1; i <= 6; i++)
            {
                //LogerHelper2.ToLog(stationList[i - 1].clientStatusType.ToString() +  " " +stationList[i - 1].isAvaliable.ToString() + " " + gufList[i - 1].gufStatus.ToString() ,0);

                if (stationList[i - 1].clientStatusType == ClientStatusType.Idle && stationList[i - 1].isAvaliable == true && gufList[i - 1].gufStatus == 0)
                {
                    freeStationList[freestationCount++] = i;
                    //break;
                }
            }
            //if (i > 6)
            //    return 0;

            int subscript = new Random().Next(freestationCount);

            return freeStationList[subscript];
        }

        public static int FetchFreeStation(int stationId)
        {
            int[] freeStationList = { 0, 0, 0, 0, 0, 0 };
            int freestationCount = 0;
            int i;
            for (i = 1; i <= 6; i++)
            {
                if (stationList[i - 1].clientStatusType == ClientStatusType.Idle && stationList[i - 1].isAvaliable == true && gufList[i - 1].gufStatus == 0)
                {
                    if (stationId != i)
                    {
                        freeStationList[freestationCount++] = i;
                        break;
                    }
                }
            }
            //if (i > 6)
            //    return 0;

            int subscript = new Random().Next(freestationCount);

            return freeStationList[subscript];
        }

        public static void delegateforDebug()
        {
            // add deleget here
            ClientGUFStatus.UpdateStationsData(1, 1, ClientStatusType.Unknown);
        }
        #endregion Fetch_Free_Station

        #region Abort_TM
        /// <summary>
        /// abort a client munually
        /// </summary>
        /// <param name="stationNo"></param>
        public void abortClient(int stationNo)
        {
            isKill[stationNo - 1] = true;
            //server.SendMsg(new Message(stationNo.ToString(), 1, MsgType.Kill, ""), stationList[stationNo - 1].ahaddress);
            LogerHelper2.ToLog(("Tester" + stationNo + "Is Aborted"), 2);
        }

        #endregion Abort_TM

        #region Reset Robot PLC
        /// <summary>
        /// reset robot PLC status, change the busy to no busy status, so that auto test could  restart to work
        /// </summary>
        /// <param></param>
        public void resetRobotPLCState()
        {
            PlCisbusy = false;
            if (dutIdListInQueue.Count > 0)
                dutIdListInQueue.RemoveAt(0);
            RobotPLCStatus.UpdateRobotPLCStatus("Idle");
            LogerHelper2.ToLog("Robot Arm has reset.", 2);
        }

        #endregion
        #region Set Tester Timeout
        void SendMsgToServer_updateTesterTimeout(int timeout)
        {
            stationTimeoutTime = timeout * 60 * 1000;
            LogerHelper2.ToLog("Tester Timeout setting to " + timeout, 2);
        }
        #endregion

        #region RobotManualCtrl
        void SendMsgToServer_ManuallyRestest(int stationNo, string dutid)
        {
            //if (PlCisbusy == false)
            {
                gufList[stationNo - 1].gufStatus = 0;
                ClientGUFStatus.UpdateStationsData(stationNo, 3, ClientStatusType.Pass);
                //PlcMessageQueue.DeleteMsg(stationNo.ToString());
                server.SendMsg(new Message(stationNo.ToString(), 1, MsgType.NewDut, dutid), stationList[stationNo - 1].ahaddress);//dutid     
                LogerHelper2.ToLog("Manually Retest DUT " + dutid + " on T" + stationNo, 2);
            }
            //else
            //{
            //    RobotPLCStatus.UpdateRobotPLCStatus("busy");
            //}

        }

        void SendMsgToServer_moveRobotPLCManually(int stationNo, string result)
        {
            if (PlCisbusy == false && plcserver.isPLConLine == true)
            {
                if (result.Contains("NG"))
                {
                    plcserver.MoveDUTFromTester(stationNo.ToString(), "01");//fail
                }
                else
                {
                    plcserver.MoveDUTFromTester(stationNo.ToString(), "00");//pass
                }
                PlCisbusy = true;
                LogerHelper2.ToLog("Manually Move DUT from" + stationNo + " to " + result, 2);
            }
            else
            {
                RobotPLCStatus.UpdateRobotPLCStatus("busy");
            }
        }
        #endregion
        #region Check DUTID valadation
        /// <summary>
        /// check the new dutid is same as the current testing duts id? should not same
        /// </summary>
        /// <param name="dutID"></param>
        /// <returns>bool</returns>
        public static bool check_DUTID_Valid(String dutID)
        {
            if (dutID.Length < 8)
                return false;

            for (int i = 0; i < 6; i++)  // dutid that under testing in the test station
            {
                if ((dutID == stationList[i].onTestingSerialNumber) || (stationList[i].onTestingSerialNumber.Contains(dutID)))
                {
                    return false;
                }
            }

            if (Server.dutIdListInQueue.Count > 0)  //check with the dutid queue
            {
                for (int j = 0; j < Server.dutIdListInQueue.Count; j++)
                {
                    if (Server.dutIdListInQueue[j] == dutID || Server.dutIdListInQueue[j].Contains(dutID))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Check PLC Tasks valadation
        /// <summary>
        /// check the new plc task is same as the tasks remaining in PLC queue
        /// </summary>
        /// <param name="dutID"></param>
        /// <returns>bool</returns>
        public static bool check_PLCTask_Valid(String plcTask)
        {
            if (plcTask.Length < 3)
                return false;

            if (Server.plctasksListInQueue.Count > 0)
            {
                for (int j = 0; j < Server.plctasksListInQueue.Count; j++)
                {
                    if (Server.plctasksListInQueue[j] == plcTask || Server.plctasksListInQueue[j].Contains(plcTask))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
