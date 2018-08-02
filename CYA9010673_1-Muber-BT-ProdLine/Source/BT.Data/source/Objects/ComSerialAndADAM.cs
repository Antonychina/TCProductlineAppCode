using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample. 
    /// </summary>
    public class SerialPortAndADAM
    {
        private string _serialPort;
        private string _adamForMaterialFrame;


        public string SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        public string AdamForMaterialFrame
        {
            get { return _adamForMaterialFrame; }
            set { _adamForMaterialFrame = value; }
        }


    }
}
