using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;


/// <summary>
///  Data Table Result Operations define
///  Include Query Insert and Delete
/// </summary>
namespace GP.MAGICL6800.ORM
{
    /// <summary>
    /// define class to match the Sample query result
    /// </summary>

    public class ProductionLineMaterialBLL
    {
        private static ProdLineMaterialInfoTableAdapter _prodLineMaterialTableAdapter;

        private static  Mubea_BTDataSet.ProdLineMaterialInfoDataTable _prodLineMaterialTable;


        public static Mubea_BTDataSet.ProdLineMaterialInfoDataTable ProdLineMaterialTable
        {
            get
            {
                {
                    _prodLineMaterialTable = _prodLineMaterialTableAdapter.GetData();
                }

                return _prodLineMaterialTable;
            }
        }


        static ProductionLineMaterialBLL()
        {
            _prodLineMaterialTableAdapter = new ProdLineMaterialInfoTableAdapter();
        }


        /// <summary>
        /// BLL functions
        /// </summary>

        ///
        public static bool InsertProdLineMaterial(string materialNo, string materialName, int usedCount, byte productionLineNo, int receivedCount, string orderNo)
        {
            _prodLineMaterialTableAdapter.InsertProdLineMaterial(materialNo, materialName, usedCount, productionLineNo, receivedCount, orderNo);  

            return true;
        }


        public static bool UpdateProdLineMaterialReceivedCount(string _materialNo, byte _productionLineNo, string _orderNo, int _receivedCount)
        {
            _prodLineMaterialTableAdapter.UpdateReceivedCount(_receivedCount, _materialNo, _productionLineNo, _orderNo );  
            return true;
        }


        public static bool UpdateProdLineMaterialUsedCount(string _materialNo, byte _productionLineNo, string _orderNo, int _usedCount, int _usedCounter)
        {
            _prodLineMaterialTableAdapter.UpdateUsedCount(_usedCount, _usedCounter, _materialNo, _productionLineNo, _orderNo);  
            return true;
        }


        public static bool DeleteProdLineMaterial(string _materialNo, byte _productionLineNo, string _orderNo)
        {
            _prodLineMaterialTableAdapter.DeleteProdLineMaterial(_materialNo, _productionLineNo, _orderNo);

            return true;
        }


        public static int GetProdLineMaterialUsedCount(string _materialNo, byte _productionLineNo, string _orderNo)
        {
            int _usedCount = 0;
            if (null == _prodLineMaterialTableAdapter.QueryUsedCount(_materialNo, _productionLineNo, _orderNo))
                _usedCount = 0;
            else
                _usedCount = (int)_prodLineMaterialTableAdapter.QueryUsedCount(_materialNo, _productionLineNo, _orderNo);

            return _usedCount;
        }

        public static int GetProdLineMaterialUsedCounter(string _materialNo, byte _productionLineNo, string _orderNo)
        {
            int _usedCount = 0;
            if (null == _prodLineMaterialTableAdapter.QueryUsedCounter(_materialNo, _productionLineNo, _orderNo))
                _usedCount = 0;
            else
                _usedCount = (int)_prodLineMaterialTableAdapter.QueryUsedCounter(_materialNo, _productionLineNo, _orderNo);

            return _usedCount;
        }


        public static int GetTotalUsedCounter( byte _productionLineNo, string _orderNo)
        {
            int _totalusedCount = 0;
            if (null == _prodLineMaterialTableAdapter.QuerySumUsedCounter(_productionLineNo, _orderNo))
                _totalusedCount = 0;
            else
                _totalusedCount = (int)_prodLineMaterialTableAdapter.QuerySumUsedCounter(_productionLineNo, _orderNo);

            return _totalusedCount;
        }


        public static int GetProdLineMaterialReceivedCount(string _materialNo, byte _productionLineNo, string _orderNo)
        {
            int _receivedCount = 0;
            if (null ==_prodLineMaterialTableAdapter.QueryReceivedCount(_materialNo, _productionLineNo, _orderNo))
                _receivedCount = 0;
            else
                _receivedCount = (int)_prodLineMaterialTableAdapter.QueryReceivedCount(_materialNo, _productionLineNo, _orderNo);

            return _receivedCount;
        }


        public static int GetProdLineMaterialRecord(string _materialNo, byte _productionLineNo, string _orderNo)
        {
            int _receivedCount = 0;
            if (null == _prodLineMaterialTableAdapter.QueryRecord(_materialNo, _productionLineNo, _orderNo))
                _receivedCount = 0;
            else
                _receivedCount = (int)_prodLineMaterialTableAdapter.QueryRecord(_materialNo, _productionLineNo, _orderNo);

            return _receivedCount;
        }

    }

}
