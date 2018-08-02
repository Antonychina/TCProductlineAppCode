using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using AH.Util;


namespace AutoFactory_Server.DutOutterBuffServer
{
    public class OutBufferClient
    {
        /// <summary>
        /// current 
        /// </summary>
        private string _Name = "undefined";
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public void SetName()
        {
            if (_NetWork.Connected)
            {
                IPEndPoint iepR = (IPEndPoint)_NetWork.Client.RemoteEndPoint;
                IPEndPoint iepL = (IPEndPoint)_NetWork.Client.LocalEndPoint;
                _Name = iepL.Port + "->" + iepR.ToString();
            }
        }

        /// <summary>
        /// TCP client
        /// </summary>
        private TcpClient _NetWork = null;
        public TcpClient NetWork
        {
            get
            {
                return _NetWork;
            }
            set
            {
                _NetWork = value;
                SetName();
            }
        }


        /// <summary>
        /// receiver buffer
        /// </summary>
        public byte[] buffer = new byte[1024];

        /// <summary>
        /// disconnect client
        /// </summary>
        public void DisConnect()
        {
            try
            {
                if (_NetWork != null && _NetWork.Connected)
                {
                    NetworkStream ns = _NetWork.GetStream();
                    ns.Close();
                    _NetWork.Close();
                }
            }
            catch (Exception ex)
            {
                LogerHelper2.ToLog(ex.Message, 3);
            }
        }
    }
}
