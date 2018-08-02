using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Network
{
    /// <summary>
    /// message type
    /// </summary>
    public enum MsgType
    {       
        Status = 'S',   //缺料
        Emerg = 'E',   //紧急缺料
        NewDut = 'N',    //备料请求
        Register = 'R',   // 注册到 库房App的消息请求
        Deduct = 'D',   //扣料
        Heartbeat = 'H',   //心跳
        Pause = 'P',   //生产暂停
        Begin = 'B' //恢复生产 use Begin instead of Recover, 避免和Register 重复
    }
  
    /// <summary>
    /// TM test status
    /// </summary>
    public enum ClientStatusType
    {
        Idle = 'I',
        Run = 'R',
        Abort = 'A',
        Pass = 'P',
        Fail = 'F',
        Unknown = 'U',
        Error = 'E',
        Wait = 'W'
    }

    /// <summary>
    /// GUF status
    /// </summary>
    public enum GufStatusType
    {
        Open = 'O',
        Close = 'C',
        HaveDut = 'H',
        NODut = 'N'
    }
}
