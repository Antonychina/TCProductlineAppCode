using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;
using DebugHelper;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class ProductionLineMaterial
    {
        private string _materialNo;
        private string _materialName;
        private int _usedCount;
        private byte _productionLineNo;
        private int _receivedCount;
        private string _orderNo;
    
        /// <summary>
        /// The name. E.G.
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



        public int UsedCount
        {
            get { return _usedCount; }
            set { _usedCount = value; }
        }

        public byte ProductionLineNo
        {
            get { return _productionLineNo; }
            set { _productionLineNo = value; }
        }


        public int ReceivedCount
        {
            get { return _receivedCount; }
            set { _receivedCount = value; }
        }


        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }



        public static void AddProductionLineMaterial(string _materialNo, string _materialName, int _usedCount, byte _productLineNo , int _receivedCount, string _orderNo)
        {
            //string materialNo, string materialName, int usedCount, byte productionLineNo, int receivedCount, string orderNo
            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0 && _orderNo.Length > 0 && _materialNo.Length > 0)
            {
                ProductionLineMaterialBLL.InsertProdLineMaterial(_materialNo, _materialName, _usedCount, _productLineNo, _receivedCount, _orderNo);
            }

        }


        public static void UpdateProdLineMaterialRcvCount(string _materialNo, byte _productLineNo, string _orderNo, int _receivedCount)
        {//string _materialNo, byte _productionLineNo, string _orderNo, int _usedCount

            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0 && _materialNo.Length > 0 && _receivedCount > 0)
            {
                int _recordNo = ProductionLineMaterialBLL.GetProdLineMaterialRecord(_materialNo, _productLineNo, _orderNo);
                if (_recordNo > 0)
                {
                    int _previousReceivedCount = ProductionLineMaterialBLL.GetProdLineMaterialReceivedCount(_materialNo, _productLineNo, _orderNo);
                    if (ProductionLineMaterialBLL.UpdateProdLineMaterialReceivedCount(_materialNo, _productLineNo, _orderNo, _receivedCount + _previousReceivedCount))
                    {
                        LogerHelper.ToLog("新物料箱更新database " +_orderNo + ":" + _materialNo + "," + _receivedCount + "+" + _previousReceivedCount, false);
                        
                    }
                    else
                    {
                        LogerHelper.ToLog("更新databse出错 " + _orderNo + ":" + _materialNo + "," + _receivedCount + "+" + _previousReceivedCount, false);
                    }
                }
                else
                {
                    if (ProductionLineMaterialBLL.InsertProdLineMaterial(_materialNo, MaterialInfoBLL.GetMaterialName(_materialNo), 0, _productLineNo, _receivedCount, _orderNo))
                    {
                        LogerHelper.ToLog("新物料写入database " + _orderNo + ":" + _materialNo + "," + _receivedCount , false);
                    }
                    else
                    {
                        LogerHelper.ToLog("写databse出错 " + _orderNo + ":" + _materialNo + "," + _receivedCount, false);
                    }
                    
                }
            }

        }


        public static void UpdateProdLineMaterialUsdCount(string _materialNo, byte _productLineNo, string _orderNo, int _usedCount)
        {//string _materialNo, byte _productionLineNo, string _orderNo, int _usedCount

            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0 && _materialNo.Length > 0)
            {
                int _previousUsedCount  = ProductionLineMaterialBLL.GetProdLineMaterialUsedCount(_materialNo, _productLineNo, _orderNo);
                int _previousUsedCounter = ProductionLineMaterialBLL.GetProdLineMaterialUsedCounter(_materialNo, _productLineNo, _orderNo);
                if (ProductionLineMaterialBLL.UpdateProdLineMaterialUsedCount(_materialNo, _productLineNo, _orderNo, _previousUsedCount + _usedCount, _previousUsedCounter + _usedCount))
                {
                    LogerHelper.ToLog("物料消耗更新database " + _orderNo + ":" + _materialNo + "," + _previousUsedCount + "-" + _usedCount, false);
                }
                else
                {
                    LogerHelper.ToLog("物料消耗更新database 出错 " + _orderNo + ":" + _materialNo + "," + _previousUsedCount + "-" + _usedCount, false);
                }
            }

        }

        public static void RemoveProductionLineMaterial(string _materialNo, byte _productLineNo , string _orderNo)
        {

            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0 && _materialNo.Length > 0)
            {
                ProductionLineMaterialBLL.DeleteProdLineMaterial(_materialNo, _productLineNo, _orderNo);
            }

        }


        public static int GetMaterialsTotalRemainConsump(byte _productLineNo , string _orderNo)
        {
            
                if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0)
                {
                    return ProductionLineMaterialBLL.GetTotalUsedCounter(_productLineNo, _orderNo);
                }
                else
                {
                    return 0;
                }
            
           
        }
      
    }
}
