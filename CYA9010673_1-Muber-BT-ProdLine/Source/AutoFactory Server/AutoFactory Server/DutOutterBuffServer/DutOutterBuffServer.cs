using AH.AutoServer;
using AH.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AutoFactory_Server.DutOutterBuffServer
{
    class DutOutterBuffServer
    {
        /// <summary>
        /// data received delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public delegate void StateChangedHandler(object sender, bool newState);

        /// <summary>
        /// date receiver event
        public event StateChangedHandler OutBuffChanged;

        /// <summary>
        /// tcp lister port
        /// </summary>
        TcpListener BufferServer = null;
        //public Timer outBuffer HeartBeatTimer = new Timer(91000);
        //public Timer outBuffer CommandFeedBackTimer = new Timer(300000); //five mins
        //public Timer outBuffer ResendCommandTimer = new Timer(5000);
        //public string lastAction;
        //public bool isoutBuffer onLine = false;
        //public int hertbeatCount = 0;
        ///// <summary>
        ///// listen state
        ///// </summary>
        ////bool isListen = false;
        bool _outBuffIsFree = false;
        byte[] _sendData ={0x02,0x00};

        public bool HasDutFinishTest
        {
            set
            {
                if (value)
                    _sendData[0] = 0x01;
                else
                    _sendData[0] = 0x02;
            }
        }

        public bool HasAlarm
        {
            set
            {
                if (value)
                    _sendData[1] = 0x01;
                else
                    _sendData[1] = 0x00;
            }
        }
        public bool OutBuffIsFree
        {
            get { return _outBuffIsFree; }
            set { _outBuffIsFree = value; }
        }
        public DutOutterBuffServer()
        {
            SendMsgToServer.clearAllConnections += ClearSelf;
        }
        /// <summary>
        /// reserved part ,the client bind 
        /// </summary>
        public BindingList<OutBufferClient> lstClient = new BindingList<OutBufferClient>();

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
        public void StartServerforoutBuffer(string ip, int port)
        {

            //test ip and port for IPC
            BufferServer = new TcpListener(IPAddress.Parse(ip), port);
            BufferServer.Start();
            BufferServer.BeginAcceptTcpClient(new AsyncCallback(Acceptor), BufferServer);
            //isListen = true;

        }

        /// <summary>
        /// stop listen
        /// </summary>
        /// <returns></returns>
        public void StopServerforoutBuffer()
        {
            try
            {
                BufferServer.Stop();
                //isListen = false;
            }
            catch (Exception stopListen_E)
            {
                LogerHelper2.ToLog("testmethod StopServerforoutBuffer  is catch exception" + stopListen_E.Message, 3);
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
                OutBufferClient newClient = new OutBufferClient();
                newClient.NetWork = server.EndAcceptTcpClient(o);
                lstClient.Add(newClient);
                LogerHelper2.ToLog("Robot outBuffer  " + newClient.Name + " is connected now.", 0);
                //RobotoutBuffer Status.UpdateRobotoutBuffer Status("Connected");
                //isoutBuffer onLine = true;
                newClient.NetWork.GetStream().BeginRead(newClient.buffer, 0, newClient.buffer.Length, new AsyncCallback(TCPCallBack), newClient);
                server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);//continue listening

                //send F999 to active outBuffer  QueryMessageListAndRunFirstMessage("DUTSTATUS");
                // byte[] tempdata = Encoding.ASCII.GetBytes("F999");
                //MessageReceived.BeginInvoke("", MainoutBuffer Message.ConvertByteToMessage(tempdata), null, null);//async data output
                //Server. QueryMessageListAndRunFirstMessage("DUTSTATUS");
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("test method outBuffer Servers.Acceptor catch exception" + ex.Message, 3);
            }
        }


        /// <summary>
        /// outBuffer  Client send message to server
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            OutBufferClient client = (OutBufferClient)ar.AsyncState;
            try
            {
                if (client.NetWork.Connected)
                {
                    NetworkStream ns = client.NetWork.GetStream();
                    byte[] recdata = new byte[ns.EndRead(ar)];
                    if (recdata.Length > 0)
                    {
                        Array.Copy(client.buffer, recdata, recdata.Length);
                        if (recdata.Length == 1)
                        {
                            
                            if (recdata[0] == 0x01)
                            {
                               _outBuffIsFree = true;
                            }
                            else
                            {
                                _outBuffIsFree = false;
                            }
                            if (_outBuffIsFree != OutBuffIsFree)
                            {
                                OutBuffIsFree = _outBuffIsFree;
                                if (OutBuffChanged != null)
                                {
                                    OutBuffChanged.BeginInvoke(client.Name, (_outBuffIsFree), null, null);//async data output
                                }
                            }
                        }                     
                        client.NetWork.GetStream().Write(_sendData, 0, _sendData.Length);
                        ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                    }
                    else
                    {
                        client.DisConnect();
                        LogerHelper2.ToLog("outBuffer  is disconnected now", 2);
                        //outBuffer HeartBeatTimer.Stop();
                        //RobotoutBuffer Status.UpdateRobotoutBuffer Status("Disconnected");
                        //isoutBuffer onLine = false;
                        lstClient.Remove(client);
                    }
                }
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("testmothod TCPCallBack catch exception " + ex.Message, 3);
                //RobotoutBuffer Status.UpdateRobotoutBuffer Status("Disconnected");
                //outBuffer HeartBeatTimer.Stop();
                client.DisConnect();
                //isoutBuffer onLine = false;
                lstClient.Remove(client);
            }
        }



        private bool Send(byte[] data, OutBufferClient client)
        {
            try
            {
                client.NetWork.GetStream().Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("methond outBuffer Server.Send catch exception :" + client.Name + ex.Message, 3);
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

            StartServerforoutBuffer(IP, port);

        }

        ///function to kill socket session
        /// <summary>
        /// clear
        /// </summary>
        public void ClearSelf()
        {
            foreach (OutBufferClient client in lstClient)
            {

                //StopServerforoutBuffer();
                client.DisConnect();

                LogerHelper2.ToLog("outbuffer is disconnected now\r\n", 0);
            }
            lstClient.Clear();
            if (BufferServer != null)
            {
                BufferServer.Stop();
            }
        }


    }
}
