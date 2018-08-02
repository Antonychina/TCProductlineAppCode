using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using AH.Util;
namespace AH.Network
{
    class TCPIPClient
    {

        /// <summary>
        /// the client name
        /// </summary>
        private string _Name = "undefine";
        public bool isClientOnline = false;
        ~TCPIPClient()
        {
            this.Disconnect();
        }
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
        /// data buffer
        /// </summary>
        public byte[] buffer = new byte[1024];

  

        /// <summary>
        /// disconnect client 
        /// </summary>
        public void Disconnect()
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
                LogerHelper2.ToLog( "methond Disconnect catch exception" +ex.Message, 3);
            }
        }
    }
}

