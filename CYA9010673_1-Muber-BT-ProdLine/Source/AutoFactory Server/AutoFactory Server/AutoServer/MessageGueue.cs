using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Network;
using AH.Util;

namespace AH.AutoServer
{
    class IDMessageQueue
    {
        public int messageNum;

        public IDMessageQueue(string name)
        {
            messageNum = 0;

        }
    }
}
