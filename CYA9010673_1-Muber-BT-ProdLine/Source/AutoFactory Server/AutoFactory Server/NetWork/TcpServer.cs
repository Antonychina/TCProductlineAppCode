using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using AH.Util;
using AH.AutoServer;


namespace AH.Network
{

    class TcpServer
    {

        public delegate void MessageReceivedHandler(object sender, Message msg);
        public event MessageReceivedHandler MessageReceived;


        /// <summary>
        /// TCP server moniter
        /// </summary>
        TcpListener tcpsever = null;
        /// <summary>
        /// listen status
        /// </summary>
        bool isListen = false;

        /// <summary>
        /// the current connected client list
        /// </summary>
        public BindingList<TCPIPClient> lstClient = new BindingList<TCPIPClient>();

        public TcpServer()
        {

            SendMsgToServer.clearAllConnections += ClearSelf;
        }

        /// <summary>
        /// stop TCP moniter
        /// </summary>
        /// <returns></returns>
        public void StopTCPServer()
        {


            try
            {
                tcpsever.Stop();
                isListen = false;
            }
            catch (Exception stopListen_E)
            {
                LogerHelper2.ToLog("methond StopTCPServer catch error :" + stopListen_E.Message, 3);
            }
        }

        /// <summary>
        /// open TCP moniter
        /// </summary>
        /// <returns></returns>
        public void StartTCPServer(string ip, int port)
        {
            try
            {
                tcpsever = new TcpListener(IPAddress.Parse(ip), port);
                tcpsever.Start();
                tcpsever.BeginAcceptTcpClient(new AsyncCallback(Acceptor), tcpsever);
                isListen = true;
            }
            catch (Exception e)
            {
                LogerHelper2.ToLog("methond StartTCPServer catch error :" + e.Message, 3);
            }

        }

        /// <summary>
        /// init client connect 
        /// </summary>
        /// <param name="o"></param>
        private void Acceptor(IAsyncResult o)
        {

            TcpListener server = o.AsyncState as TcpListener;
            try
            {
                TCPIPClient newClient = new TCPIPClient();
                newClient.NetWork = server.EndAcceptTcpClient(o);
                if (lstClient.Count > 0) //Server need to update the listClient(not to add new) when the connection was created from the same Client
                {
                    int endIndex = newClient.Name.IndexOf(":");
                    string s = newClient.Name.Substring(0, endIndex - 1);
                    for (int i = 0; i < lstClient.Count; i++)
                    {
                        if (lstClient[i].Name.Contains(s) && lstClient[i].Name != newClient.Name)
                        {
                            LogerHelper2.ToLog("old client : " + lstClient[i].Name + " has been killed!", 2);
                            lstClient[i].Disconnect();
                            lstClient.Remove(lstClient[i]);

                        }
                    }
                }
                lstClient.Add(newClient);
                newClient.isClientOnline = true;
                LogerHelper2.ToLog("client : " + newClient.Name + " is connect to server", 2);
                newClient.NetWork.GetStream().BeginRead(newClient.buffer, 0, newClient.buffer.Length, new AsyncCallback(TCPCallBack), newClient);
                server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);//continue moniter client 
            }
            catch (ObjectDisposedException ex)
            {
                LogerHelper2.ToLog(ex.Message, 3);
            }

        }

        /// <summary>
        /// server send message to station clinet 
        /// </summary>
        /// <param name="msg">message information which need to send</param>
        /// <param name="ip">client ip and port</param>
        /// <returns></returns>
        public bool SendMsg(Message msg, string ip)
        {
            string s = null;
            int clientId = 99;
            bool b = true;
            for (int i = 0; i < lstClient.Count; i++)
            {
                s = lstClient[i].Name;
                if (string.Compare(ip, s) == 0)
                {
                    clientId = i;
                }
            }
            if (clientId == 99)
            {
                LogerHelper2.ToLog("station:" + msg.stationId + " is offline, can't control or send commond to this station", 3);
                return false;
            }
            string msgTemp = string.Format("to: {4}  StationID: {0}\tMessageID: {1}\tType: {2}\t Info:{3}", msg.stationId, msg.MsgId, msg.MsgType.ToString(), msg.Info, lstClient[clientId].Name);
            //if (msg.MsgType != MsgType.Ack)
            //{
            //    LogerHelper2.ToLog(msgTemp, 1);
            //}
            for (int i = 0; i < 3; i++)  // if first time is send fail, then send again
            {
                b = Send(Message.ConvertMsgToByte(msg), lstClient[clientId]);
                if (b == false)
                {
                    LogerHelper2.ToLog("commond send fail", 3);
                    Server.stationList[int.Parse(msg.stationId) - 1].errorInfo = msg.Info + "commond send fail";
                    ClientGUFStatus.UpdateStationsData(int.Parse(msg.stationId), 4, ClientStatusType.Unknown);
                    continue;
                }
                else
                    break;
            }
            return b;
        }

        private bool Send(byte[] data, TCPIPClient client)
        {
            try
            {
                client.NetWork.GetStream().Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog("methond TcpServer.Send catch exception :" + client.Name + ex.Message, 3);
                client.Disconnect();
                return false;
            }
            return true;
        }

        /// <summary>
        /// callback method 
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            TCPIPClient client = (TCPIPClient)ar.AsyncState;
            if (client.NetWork.Connected)
            {
                try
                {
                    NetworkStream ns = client.NetWork.GetStream();
                    byte[] recdata = new byte[ns.EndRead(ar)];
                    string receivedstring = (new ASCIIEncoding().GetString(client.buffer));
                    receivedstring = receivedstring.Substring(0, recdata.Length);
                    if (recdata.Length > 0)
                    {
                        if (receivedstring.Contains("\r\n"))
                        {
                            string[] recdataArray = receivedstring.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < recdataArray.Length; i++)
                            {
                                if (MessageReceived != null)
                                {
                                    MessageReceived.BeginInvoke(client.Name, Message.ConvertStringToMessage(recdataArray[i]), null, null);//async output data
                                }
                            }
                            ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                        }
                        else
                        {
                            Array.Copy(client.buffer, recdata, recdata.Length);
                            if (MessageReceived != null)
                            {
                                MessageReceived.BeginInvoke(client.Name, Message.ConvertByteToMessage(recdata), null, null);//async output data
                            }
                            ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                        }
                    }
                    else
                    {
                        client.Disconnect();
                        LogerHelper2.ToLog("client :" + client.Name + " is disconnect from server", 2);
                        client.isClientOnline = false;
                        CommonMethod.ShowClientOffLine(client.Name);
                        lstClient.Remove(client);
                    }
                }
                catch (Exception ex)
                {
                    client.Disconnect();
                    LogerHelper2.ToLog(ex.Message + "client :" + client.Name + " is disconnect from server", 3);
                    client.isClientOnline = false;
                    CommonMethod.ShowClientOffLine(client.Name);
                    lstClient.Remove(client);
                }
            }
        }

        /// <summary>
        /// clear
        /// </summary>
        public void ClearSelf()
        {
            foreach (TCPIPClient client in lstClient)
            {
                client.Disconnect();
                LogerHelper2.ToLog("client :" + client.Name + "is disconnect from server", 2);
            }
            lstClient.Clear();
            if (tcpsever != null)
            {
                tcpsever.Stop();
            }
        }
        ~TcpServer()
        {
            //ClearSelf();
        }

    }
}
