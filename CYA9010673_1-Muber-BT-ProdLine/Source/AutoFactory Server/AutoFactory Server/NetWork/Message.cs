using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Network
{
    // The Message Prorocol:
    // <stationId,msgId,msgType,info>\r\n
    /// <summary>
    /// The Message between Server and Clinet
    /// </summary>
    public class Message
    {
        public string stationId { get; private set; }
        public string totalmessage;
        public string DutID { get; private set; } //add buy repeat requirement
        public int MsgId { get; private set; }
        public MsgType MsgType { get; private set; }
        public string Info { get; private set; }
        public string ErrorInfo { get; private set; }
        public bool InvalidMessage;
        public Message() { InvalidMessage = false; }
        public Message(byte[] package)
        {
            string temp = new ASCIIEncoding().GetString(package);
            totalmessage = temp;
            temp = temp.Trim();
            if (temp.StartsWith("<") && temp.EndsWith(">"))
            {
                temp = temp.Substring(1, temp.Length - 2);
                string[] strmsg = temp.Split(',');
                this.stationId = strmsg[0];
                this.MsgId = Convert.ToInt32(strmsg[1]);
                this.MsgType = (MsgType)strmsg[2][0];
                this.Info = strmsg[3];
                if(strmsg.Length>4)
                    this.DutID = strmsg[4];
                string[] strmsg2 = this.Info.Split(':');
                if(strmsg2[0]=="E")
                {
                    this.Info="E";
                    this.ErrorInfo = strmsg2[1];
                }
            }
        }

        public Message(string package)
        {
            string temp = package;
            totalmessage = package;
            temp = temp.Trim();
            if (temp.StartsWith("<") && temp.EndsWith(">"))
            {
                temp = temp.Substring(1, temp.Length - 2);
                string[] strmsg = temp.Split(',');
                this.stationId = strmsg[0];
                this.MsgId = Convert.ToInt32(strmsg[1]);
                this.MsgType = (MsgType)strmsg[2][0];
                this.Info = strmsg[3];
                if (strmsg.Length > 4)
                    this.DutID = strmsg[4];
                string[] strmsg2 = this.Info.Split(':');
                if (strmsg2[0] == "E")
                {
                    this.Info = "E";
                    this.ErrorInfo = strmsg2[1];
                }
            }
        }

        public Message(string StationId, int MsgId, MsgType MsgType, string Info)
        {
            this.stationId = StationId;
            this.MsgType = MsgType;
            this.MsgId = MsgId;
            this.Info = Info;
        }

       
        static public byte[] ConvertMsgToByte(Message msg)
        {
            string msgstr = string.Format("<{0},{1},{2},{3}>\r\n", msg.stationId, msg.MsgId, (char)msg.MsgType, msg.Info);

            return new ASCIIEncoding().GetBytes(msgstr);
        }


        static public Message ConvertByteToMessage(byte[] package)
        {
            Message msg = new Message();
            try
            {
                msg = new Message(package);
            }
            catch
            {
                msg.InvalidMessage = true;
                msg.Info = new ASCIIEncoding().GetString(package);
            }
            return msg;
        }


        static public Message ConvertStringToMessage(string package)
        {
            Message msg = new Message();
            try
            {
                msg = new Message(package);
            }
            catch
            {
                msg.InvalidMessage = true;
                msg.Info = package;
            }
            return msg;
        }

      
    }
}
