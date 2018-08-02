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
    public class MaterialAndLabel
    {
        private string _materialNo;
        private int _labelsNo;


        public string MaterialNo
        {
            get { return _materialNo; }
            set { _materialNo = value; }
        }

        public int LabelsNo
        {
            get { return _labelsNo; }
            set { _labelsNo = value; }
        }


    }


    public class MaterialsChangedInfo
    {
        private int _materialNoSeq;
        private int _materialChangedCount;


        public int MaterialNoSeq
        {
            get { return _materialNoSeq; }
            set { _materialNoSeq = value; }
        }

        public int MaterialChangedCount
        {
            get { return _materialChangedCount; }
            set { _materialChangedCount = value; }
        }

    }
}
