using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace AH.PLCModel
{


    //message form PLC to PC     
    //tese messages divide to 3 kinds 
    // feedback message: what PC want to get after one command send to PLC.
    // heartbeat message: the heart beat from PLC ,what is timely and with no other additional message.
    // report message: what the message PLC report to PC, such as dut id, GUF connect ok,or other these kind of with additional message.
    public enum PLCMsgType
    {

        Feedback = 'F',
        Heartbead = 'H',
        ReportMsg = 'R',
        Ack = 'A'
    }


    public class MainPLCMessage
    {


        // message head
        //------------------------------------------------------------------------------
        //                                    message head                 
        //------------------------------------------------------------------------------
        //1 Move dut to station
        public const string MoveDUTToStation = "M000";
        public const string MoveDUTToStationACK = "F000";

        //2 move dut from station
        public const string MoveDUTFromStation = "M100";
        public const string MoveDUTFromStationACK = "F010";

        //3 main PLC send dut ID to server
        public const string DUTIdToServer = "R000";
        public const string DUTIdToServerACK = "F020";

        //4 plc heart beat message
        public const string HeartBeatfromPLC = "H000";
        public const string HeartBeatfromPLCACK = "F030";

        //5 query PLC state
        public const string queryPLCState = "Q000";
        public const string queryPLCStateACK = "F040";

        //report GUF connect
        public const string ReportGUFConnect = "R010";

        public enum MainPLCErrType
        {
            Busy = 0,
            Cylinder_Move_Error = 1,
            Cylinder_Rest_Error = 2,
            E_Stop = 3,
            Product_Error = 4,

        }

        public enum GUFNotAccessType
        {

            Cylinder_Move_Error = 2,
            Cylinder_Rest_Error = 3,
            E_Stop = 4,

        }



        //public int MsgId { get; private set; }

        public PLCMsgType MsgType { get; set; }
        public string MsgHead { get; set; }
        //message info
        public string Info { get; set; }

        // GUF busy state
        public bool PLCIsBusy { get; set; }

        // PLC busy,true for busy, false for idle
        //any sensor of PLC have problem the isok flag should be false,other true 
        public bool isok { get; set; }

        //error code
        public int ErrorCode { get; set; }
        //invalid message
        public bool InvalidMessage { get; set; }


        //message head length
        public const int msgHeadLength = 4;
        //message state length
        public const int msgStateLength = 2;
        //error   code length
        public const int errCodeLength = 2;
        public const int dutIDLength = 10;
        public const int _GUFIDLength = 2;
        public int StationId { get; set; }
        public const int _StationIdLength = 2;
        public MainPLCMessage()
        {

        }
        //PLC message convert
        public MainPLCMessage(byte[] data)
        {
            string temp = new ASCIIEncoding().GetString(data);
            temp = temp.Trim();
            string messageState = "";
            string errorCode = "";

            //useful message no less than message head
            if (temp.Length >= msgHeadLength)
            {
                this.MsgType = (PLCMsgType)temp[0];                     //get messagetype
                this.MsgHead = temp.Substring(0, msgHeadLength);        //get message head
                this.InvalidMessage = false;                            //vaild message
                // messageState = temp.Substring(4, errCodeLength);
                switch (this.MsgType)
                {
                    case PLCMsgType.Feedback:
                        this.Info = "";
                        if (this.MsgHead == MoveDUTToStationACK)
                        {
                            this.StationId = Convert.ToInt16(temp.Substring(4, _StationIdLength));
                            messageState = temp.Substring(6, errCodeLength);
                            if (messageState == "00")
                            {
                                this.isok = true;
                                this.Info = " DUT placed to Station " + StationId;
                            }
                            else if (messageState == "01")
                            {
                                this.isok = false;
                                //error code handle add to this.info
                            }
                        }
                        else if (this.MsgHead == MoveDUTFromStationACK)
                        {
                            this.StationId = Convert.ToInt16(temp.Substring(4, _StationIdLength));
                            messageState = temp.Substring(6, errCodeLength);
                            if (messageState == "00")
                            {
                                this.isok = true;
                                this.Info = "DUT move away from Station " + StationId;

                            }
                            else if (messageState == "01")
                            {
                                this.isok = false;
                                //error code handle add to this.info
                            }
                        }
                        else if (this.MsgHead == queryPLCStateACK)
                        {
                            messageState = temp.Substring(4, errCodeLength);
                            if (messageState == "00")
                            {
                                this.isok = true;
                                this.PLCIsBusy = false;

                            }
                            else if (messageState == "01")
                            {
                                this.isok = true;
                                this.PLCIsBusy = true;

                            }
                            else if (messageState == "02")
                            {
                                this.isok = false;
                                //error code handle add to this info
                            }
                        }

                        break;

                    case PLCMsgType.Heartbead:
                        this.Info = "Heartbeat";
                        break;

                    case PLCMsgType.ReportMsg:
                        if (this.MsgHead == DUTIdToServer)
                        {
                            this.isok = true;
                            this.Info = temp.Substring(4, dutIDLength);
                        }
                        break;
                    case PLCMsgType.Ack:
                        this.Info = "Ack";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.InvalidMessage = true;
                this.Info = temp;
            }
        }


        //error code handle method
        public void GUFPLCErrorMessageHandle(int errorcode)
        {
            GUFNotAccessType _errorCode = (GUFNotAccessType)errorcode;
            switch (_errorCode)
            {

                case GUFNotAccessType.Cylinder_Move_Error:
                    this.Info = "errcode 02 GUF cylinder move error";
                    break;
                case GUFNotAccessType.Cylinder_Rest_Error:
                    this.Info = "errcode 03 GUF cylinder reset error";
                    break;
                case GUFNotAccessType.E_Stop:
                    this.Info = "errcode 04 GUF E_Stop error";
                    break;
                default:
                    break;
            }
        }

        static public MainPLCMessage ConvertByteToMessage(byte[] data)
        {
            MainPLCMessage plcmessage = new MainPLCMessage();
            try
            {
                plcmessage = new MainPLCMessage(data);
            }
            catch (Exception e)
            {
                plcmessage.InvalidMessage = true;
                plcmessage.Info = new ASCIIEncoding().GetString(data);
            }
            return plcmessage;
        }


    }


}
