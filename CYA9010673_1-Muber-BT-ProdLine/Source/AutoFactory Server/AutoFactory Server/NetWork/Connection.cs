using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using AH.Util;
using AH.AutoServer;

namespace AH.Network
{

    class Connection
    {
        int TxCount = 0;

        public bool IsConnected { get; private set; }
        
        public delegate void MessageReceivedHandler(object sender, Message msg);
        //public delegate void ServerConnectedEventHandle(object sender, bool connected);
        //public event ServerConnectedEventHandle ServerConnected;
        //  public delegate bool MessageSendHandler(byte[] data);
    
        public event MessageReceivedHandler MessageReceived;

        TCPIPClient client;
        List<Message> recivedMsgList = new List<Message>();
        ~Connection()
        {
            this.DisconnectFromServer();
        }
        public void ConnectToServer(string ip, int port)
        {
            //client.NetWork.

            try
            {
                
                LogerHelper2.ToLog(string.Format("Connecting to Server: {0}:{1}", ip, port), 3);
                client = new TCPIPClient();
                client.NetWork = new TcpClient();
                client.NetWork.Connect(ip.Trim(), port);//connect to server

                client.SetName();
                client.NetWork.GetStream().BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                IsConnected = true;
                
                //if (client.NetWork.Connected)
                //{
                //    ServerConnected(client, true);
                //}
                //   lstClient.Add(client);
                //    BindLstClient();
                TxCount = 0;
             
            }
            catch (Exception ex)
            {
                IsConnected = false;
                client.Disconnect();
                //ServerConnected(client, false);
                MessageBox.Show("不能连接到库房App，请检查库房App是否打开.", "程序启动");
                LogerHelper2.ToLog("connecting Exception:" + ex.Message, 3);

            }
        }

        public void DisconnectFromServer()
        {
            if (client.NetWork.Connected)
            {
          
                LogerHelper2.ToLog("Disconnecting from Server" , 3);
                client.Disconnect();
                //ServerConnected(client, false);
                IsConnected = false;
            }
        }

        public bool SendMsg(Message msg)
        {
            bool success = false;
            //for (int i = 0; i < 3; i++)
            {
                if (success = Send(Message.ConvertMsgToByte(msg)))
                {
                    //if (msg.MsgType != MsgType.Heartbeat)
                    {
                        LogerHelper2.ToLog(string.Format("Send Msg: {0}", msg.Info), 3);
                    }
                
                } 
                else
                {

                    LogerHelper2.ToLog("Client Send message " + msg.Info.Trim() + " failed...", 3);
                   
                }
            }
            return success;
        }
        private bool Send(byte[] data)
        {
            try
            {
                client.NetWork.GetStream().Write(data, 0, data.Length);
                TxCount++;

            }
            catch (Exception ex)
            {
           
                LogerHelper2.ToLog("Sending Exception:" + ex.Message, 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Callback
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            try
            {
                TCPIPClient client = (TCPIPClient)ar.AsyncState;
                if (client.NetWork.Connected)
                {
                    NetworkStream ns = client.NetWork.GetStream();
                    byte[] recdata = new byte[ns.EndRead(ar)];
                    Array.Copy(client.buffer, recdata, recdata.Length);
                    if (recdata.Length > 0)
                    {

                        if (MessageReceived != null)
                        {
                            string[] temp = Encoding.ASCII.GetString(recdata).Split('\n');
                            foreach (string item in temp)
                            {
                                if (item != "")
                                {
                                    MessageReceived.BeginInvoke(client.Name, Message.ConvertStringToMessage(item), null, null);//Async call back
                                }
                            }

                        }
                        ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                    }
                    else
                    {
                        IsConnected = false;
                        client.Disconnect();
                        //ServerConnected(client, false);
                        //    lstClient.Remove(client);
                        //    BindLstClient();
                    }
                }
            }
            catch (Exception e)
            {

                LogerHelper2.ToLog("Exception in Connection TCPCallBack:\r\n" + e.Message, 3);
            }

        }

    }
}
