using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class MaterialInfo
    {
        private string _materialNo;
        private string _materialName;
        private int    _materialPcs;
        private byte _materialSeq;
        private int _usageTimePerBox;
        private int _countPerBox;
        private byte _position;


        /// <summary>
        /// The name
        /// </summary>
        public string MaterialNo
        {
            get { return _materialNo; }
            set { _materialNo = value; }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }

        public int MaterialPcs
        {
            get { return _materialPcs; }
            set { _materialPcs = value; }
        }

        public byte MaterialSeq
        {
            get { return _materialSeq; }
            set { _materialSeq = value; }
        }

        public int UsageTimePerBox
        {
            get { return _usageTimePerBox; }
            set { _usageTimePerBox = value; }
        }

        public int CountPerBox
        {
            get { return _countPerBox; }
            set { _countPerBox = value; }
        }

        public byte Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }


    public class MaterialInfos
    {
        public static List<MaterialInfo> GetMaterialInfosList()
        {
            List<MaterialInfo> productMaterialsList = new List<MaterialInfo>();

            foreach (var val in MaterialInfoBLL.GetMaterialInfos())
            {

                MaterialInfo materialTemp = new MaterialInfo();

                materialTemp.MaterialNo = val.MaterialNo;
                materialTemp.MaterialName = val.MaterialName;
                materialTemp.UsageTimePerBox = val.UsageTimePerBox;
                materialTemp.CountPerBox = val.CountPerBox;
                materialTemp.Position = val.Position;

                productMaterialsList.Add(materialTemp);
            }

            return productMaterialsList;

        }
    }
}
