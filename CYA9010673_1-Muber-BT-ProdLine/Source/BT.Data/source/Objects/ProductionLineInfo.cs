using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;


namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class ProductionLine
    {

        private byte _productionLineNo;
        private string _orderNo;
        private int _orderCount;
        private string _productNo;
       
    
        /// <summary>
        /// The name. E.G.
        /// </summary>       


        public byte ProductionLineNo
        {
            get { return _productionLineNo; }
            set { _productionLineNo = value; }
        }


        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }




        public int orderCount
        {
            get { return _orderCount; }
            set { _orderCount = value; }
        }

        public string ProductNo
        {
            get { return _productNo; }
            set { _productNo = value; }
        }



        public static void AddOrderAndProductionLineInfo(byte _productLineNo , string _orderNo, int _orderCount, string _productNo)
        {

            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0 && _orderCount > 0 && _productNo.Length > 0)
            {
                ProductionLineInfoBLL.InsertProductionLineInfo(_productLineNo, _orderNo , _orderCount,  _productNo);
            }

        }


        public static void RemoveOrderAndProductionLineInfo(byte _productLineNo , string _orderNo)
        {

            if (_productLineNo > 0 && _productLineNo < 10 && _orderNo.Length > 0)
            {
                ProductionLineInfoBLL.DeleteProductionLineInfo(_productLineNo, _orderNo);
            }

        }


        public static void CheckProductionLineExsit( string _orderNo, byte _productLineNo, string _productNo, int _orderCount)
        {
            try
            {
                if (!ProductionLineInfoBLL.ExistThisProductionLineInfo(_productLineNo, _orderNo))
                {
                    //byte _productionLineNo, string _orderNo, int _orderCount, string _productNo
                    ProductionLineInfoBLL.InsertProductionLineInfo(_productLineNo, _orderNo, _orderCount, _productNo);
                    return;
                }

               
            }
            catch
            { 
                
            }
            

        }

      
    }
}
