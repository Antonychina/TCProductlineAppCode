using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Timers;
using System.Net.Sockets;
using System.ComponentModel;
using AH.Util;
using AH.AutoServer;

namespace AH.PLCModel
{

    class MainPLCServer
    {

        /// <summary>
        /// data received delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public delegate void MessageReceivedHandler(object sender, MainPLCMessage msg);

        /// <summary>
        /// date receiver event
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// tcp lister port
        /// </summary>
        TcpListener plcSever = null;
        public Timer plcHeartBeatTimer = new Timer(91000);
        public Timer plcCommandFeedBackTimer = new Timer(300000); //five mins
        public Timer plcResendCommandTimer = new Timer(5000);
        public string lastAction;
        public bool isPLConLine = false;
        public int hertbeatCount = 0;
        /// <summary>
        /// listen state
        /// </summary>
        bool isListen = false;

        public MainPLCServer()
        {

            plcHeartBeatTimer.Elapsed += new ElapsedEventHandler(OnTimedShow);
            plcHeartBeatTimer.AutoReset = false;
            plcCommandFeedBackTimer.Elapsed += new ElapsedEventHandler(plcCommandOnTimedShow);
            plcCommandFeedBackTimer.AutoReset = false;
            plcResendCommandTimer.Elapsed += plcResendTimer_Elapsed;
            SendMsgToServer.clearAllConnections += ClearSelf;
        }



        /// <summary>
        /// while the server receive the heartbeat from plc, reset the timer
        /// </summary>
        public void CheckHeartbeat()
        {
            if (hertbeatCount == 0)
            {
                plcHeartBeatTimer.Enabled = true; //timer can use
                hertbeatCount++;

            }
            else
            {
                plcHeartBeatTimer.Stop();
                plcHeartBeatTimer.Start();
            }
        }

        /// <summary>
        /// while receive the PLC feedback for move command, then stop the timer
        /// </summary>
        public void stopPlcCommandTimer()
        {
            plcCommandFeedBackTimer.Stop();
        }

        /// <summary>
        /// while the server didn't receive the heartbeat from plc, then show the warning
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnTimedShow(object source, ElapsedEventArgs e)
        {
            hertbeatCount = 0;
            LogerHelper2.ToLog("Server does not receive the PLC Heartbeat in time", 3);
            RobotPLCStatus.UpdateRobotPLCStatus("Disconnected");
        }

        /// <summary>
        /// while the server didn't receive the  feedback for move command from plc, then show the warning
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void plcCommandOnTimedShow(object source, ElapsedEventArgs e)
        {
            LogerHelper2.ToLog("Server does not receive the PLC command feedback in time", 3);
            RobotPLCStatus.UpdateRobotPLCStatus("Timeout");
        }

        /// <summary>
        /// while the server didn't receive the ACK for move command from plc, then resend CMD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void plcResendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LogerHelper2.ToLog("Server does not receive the PLC command ACK in time", 3);
            if (lastAction != string.Empty)
            {
                SendMessage(lastAction);
            }
        }

        /// <summary>
        /// reserved part ,the client bind 
        /// </summary>
        public BindingList<PLCClient> lstClient = new BindingList<PLCClient>();

        //server Tpye
        //
        enum serverType
        {
            IPC = 0,
            serverPC = 1,
        };


        /// <summary>
        /// start server
        /// </summary>
        /// <param name="serverType"></param>
        public void StartServerforPLC(string ip, int port)
        {

            //test ip and port for IPC
            plcSever = new TcpListener(IPAddress.Parse(ip), port);
            plcSever.Start();
            plcSever.BeginAcceptTcpClient(new AsyncCallback(Acceptor), plcSever);
            isListen = true;

        }

        /// <summary>
        /// stop listen
        /// </summary>
        /// <returns></returns>
        public void StopServerforPLC()
        {
            try
            {
                plcSever.Stop();
                isListen = false;
            }
            catch (Exception stopListen_E)
            {
                LogerHelper2.ToLog("testmethod StopServerforPLC is catch exception" + stopListen_E.Message, 3);
            }
        }

        /// <summary>
        /// when Client Try to connect server
        /// </summary>
        /// <param name="o"></param>
        private void Acceptor(IAsyncResult o)
        {
            TcpListener server = o.AsyncState as TcpListener;
            try
            {
                PLCClient newClient = new PLCClient();
                newClient.NetWork = server.EndAcceptTcpClient(o);
                lstClient.Add(newClient);
                LogerHelper2.ToLog("Robot PLC " + newClient.Name + " is connected now.", 0);
                RobotPLCStatus.UpdateRobotPLCStatus("Connected");
                isPLConLine = true;
                newClient.NetWork.GetStream().BeginRead(newClient.buffer, 0, newClient.buffer.Length, new AsyncCallback(TCPCallBack), newClient);
                server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);//continue listening

                //send F999 to active PLC QueryMessageListAndRunFirstMessage("DUTSTATUS");
                byte[] tempdata = Encoding.ASCII.GetBytes("F999");
                MessageReceived.BeginInvoke("", MainPLCMessage.ConvertByteToMessage(tempdata), null, null);//async data output
                //Server. QueryMessageListAndRunFirstMessage("DUTSTATUS");
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("test method PLCServers.Acceptor catch exception" + ex.Message, 3);
            }
        }


        /// <summary>
        /// PLC Client send message to server
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            PLCClient client = (PLCClient)ar.AsyncState;
            try
            {
                if (client.NetWork.Connected)
                {
                    NetworkStream ns = client.NetWork.GetStream();
                    byte[] recdata = new byte[ns.EndRead(ar)];
                    if (recdata.Length > 0)
                    {
                        Array.Copy(client.buffer, recdata, recdata.Length);
                        if (MessageReceived != null)
                        {
                            MessageReceived.BeginInvoke(client.Name, MainPLCMessage.ConvertByteToMessage(recdata), null, null);//async data output
                        }
                        ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                    }
                    else
                    {
                        client.DisConnect();
                        LogerHelper2.ToLog("plc is disconnected now", 2);
                        plcHeartBeatTimer.Stop();
                        RobotPLCStatus.UpdateRobotPLCStatus("Disconnected");
                        isPLConLine = false;
                        lstClient.Remove(client);
                    }
                }
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("testmothod TCPCallBack catch exception " + ex.Message, 3);
                RobotPLCStatus.UpdateRobotPLCStatus("Disconnected");
                plcHeartBeatTimer.Stop();
                client.DisConnect();
                isPLConLine = false;
                lstClient.Remove(client);
            }
        }

        /// <summary>
        /// send message to client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            if (isPLConLine == false)
            {
                LogerHelper2.ToLog("Robot PLC is offline, can't control or send commond to plc", 3);
                return;
            }
            if (message.Contains("M")) // move command set timer
            {
                plcCommandFeedBackTimer.Enabled = true; //timer可用 
                plcResendCommandTimer.Enabled = true;
                lastAction = message;
                // plcCommandFeedBackTimer.Start();
            }
            bool b = true;
            string s = string.Format("to PLC: {0}", message);

            LogerHelper2.ToLog(s, 1);

            byte[] byteMessage = Encoding.ASCII.GetBytes(message);

            // lstClient[lstClient.Count - 1].NetWork.GetStream().Write(byteMessage, 0, byteMessage.Length);
            //for (int i = 0; i < 3; i++)  // if first time is send fail, then send again
            //{
            b = Send(byteMessage, lstClient[lstClient.Count - 1]);
            //    if (b == false)
            //    {
            //        continue;
            //    }
            //    else
            //        break;
            //}

        }

        private bool Send(byte[] data, PLCClient client)
        {
            try
            {
                client.NetWork.GetStream().Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("methond PLCServer.Send catch exception :" + client.Name + ex.Message, 3);
                client.DisConnect();
                return false;
            }
            return true;
        }
        /// <summary>
        /// PC initial TCP/IP session
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        public void ServerInitial(string IP, int port)
        {

            StartServerforPLC(IP, port);

        }


        /// <summary>
        /// move DUT to test station
        /// </summary>
        /// server PC function
        /// <param name="stationID"></param>
        public void MoveDUTtoTester(string stationID)
        {

            string message = null;
            stationID = CommonMethod.ChangeOneBitToTwoBit(stationID);
            message = MainPLCMessage.MoveDUTToStation + stationID;
            SendMessage(message);
            RobotPLCStatus.UpdateRobotPLCStatus("Buf -> T" + stationID);
        }



        /// <summary>
        /// move DUT away from test station
        /// </summary>
        /// server PC function
        /// <param name="stationID"></param>
        public void MoveDUTFromTester(string stationID, string testResult)
        {
            string message = null;
            stationID = CommonMethod.ChangeOneBitToTwoBit(stationID);
            message = MainPLCMessage.MoveDUTFromStation + stationID + testResult;
            SendMessage(message);
            RobotPLCStatus.UpdateRobotPLCStatus("T" + stationID + " -> " + (testResult == "00" ? "OK" : "NG"));
        }

        /// <summary>
        /// Query Main PLC state
        /// </summary>
        /// server PC function
        /// <param name=""></param>
        public void QueryPLCState()
        {
            string message = null;
            message = MainPLCMessage.queryPLCState;
            SendMessage(message);
        }

        /// <summary>
        /// PC feedback for the heartbeat message
        /// </summary>
        /// server PC function
        /// <param name=""></param>
        public void HBFeedback()
        {
            string message = null;
            message = MainPLCMessage.HeartBeatfromPLCACK;
            SendMessage(message);
        }

        /// <summary>
        /// PC feedback for the New serialNumber message
        /// </summary>
        ///  server PC function
        /// <param name=""></param>
        public void NewSNFeedback()
        {
            string message = null;
            message = MainPLCMessage.DUTIdToServerACK;
            SendMessage(message);
        }
        ///function to kill socket session
        /// <summary>
        /// clear
        /// </summary>
        public void ClearSelf()
        {
            foreach (PLCClient client in lstClient)
            {
                plcHeartBeatTimer.Stop();
                StopServerforPLC();
                client.DisConnect();

                LogerHelper2.ToLog("plc is disconnected now\r\n", 0);
            }
            lstClient.Clear();
            if (plcSever != null)
            {
                plcSever.Stop();
            }
        }
        ~MainPLCServer()
        {
            //ClearSelf();
        }

    }

}
