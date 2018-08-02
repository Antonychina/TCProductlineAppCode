using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;

namespace GP.MAGICL6800.ORM
{
    public class ProductBOMBLL
    {
        private static ProductInfoTableAdapter _adapter;

        private static Mubea_BTDataSet.ProductInfoDataTable _table;
        private static Mubea_BTDataSet.ProductInfoDataTable _specificTable;

        public static Mubea_BTDataSet.ProductInfoDataTable Table
        {
            get
            {
                if (_table == null)
                {
                    _table = _adapter.GetData();
                }

                return _table;
            }
        }

        static ProductBOMBLL()
        {
            _adapter = new ProductInfoTableAdapter();
        }

        public static void RefreshTable()
        {
            _table = _adapter.GetData();
        }

        public static  Mubea_BTDataSet.ProductInfoDataTable GetProductBOMList(string _productNo)
        {
            try
            {
                _specificTable = _adapter.GetProductBOM(_productNo);
            }
            catch (Exception e)
            {
                return null;
            }

            return _specificTable;
        }


        public static string GetProductName(string _productNo)
        {
            string productName = "";
            try
            {
                productName = _adapter.GetProductName(_productNo);
            }
            catch
            {
                return productName;
            }

            return productName;
        }


        public static bool InsertProductBOMInfo(string _productNo, string _productName,
            string _material1No = null, byte _material1Count = 0,
            string _material2No = null, byte _material2Count = 0,
            string _material3No = null, byte _material3Count = 0,
            string _material4No = null, byte _material4Count = 0,
            string _material5No = null, byte _material5Count = 0,
            string _material6No = null, byte _material6Count = 0,
            string _material7No = null, byte _material7Count = 0,
            string _material8No = null, byte _material8Count = 0,
            string _material9No = null, byte _material9Count = 0,
            string _material10No = null, byte _material10Count = 0,
            string _material11No = null, byte _material11Count = 0,
            string _material12No = null, byte _material12Count = 0,
            string _material13No = null, byte _material13Count = 0,
            string _material14No = null, byte _material14Count = 0,
            string _material15No = null, byte _material15Count = 0,
            string _material16No = null, byte _material16Count = 0,
            string _material17No = null, byte _material17Count = 0,
            string _material18No = null, byte _material18Count = 0,
            string _material19No = null, byte _material19Count = 0,
            string _material20No = null, byte _material20Count = 0)
        {
            try
            {
                _adapter.InsertProductBOM(_productNo, _productName,
            _material1No, _material1Count,
            _material2No, _material2Count,
            _material3No, _material3Count,
            _material4No, _material4Count,
            _material5No, _material5Count,
            _material6No, _material6Count,
            _material7No, _material7Count,
            _material8No, _material8Count,
            _material9No, _material9Count,
            _material10No, _material10Count,
            _material11No, _material11Count,
            _material12No, _material12Count,
            _material13No, _material13Count,
            _material14No, _material14Count,
            _material15No, _material15Count,
            _material16No, _material16Count,
            _material17No, _material17Count,
            _material18No, _material18Count,
            _material19No, _material19Count,
            _material20No, _material20Count);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static bool UpdateProductBOMInfo(string _productNo, string _productName, 
            string _material1No = null, byte _material1Count = 0, 
            string _material2No = null, byte _material2Count = 0,
            string _material3No = null, byte _material3Count = 0,
            string _material4No = null, byte _material4Count = 0,
            string _material5No = null, byte _material5Count = 0,
            string _material6No = null, byte _material6Count = 0, 
            string _material7No = null, byte _material7Count = 0,
            string _material8No = null, byte _material8Count = 0,
            string _material9No = null, byte _material9Count = 0,
            string _material10No = null, byte _material10Count = 0,
            string _material11No = null, byte _material11Count = 0, 
            string _material12No = null, byte _material12Count = 0,
            string _material13No = null, byte _material13Count = 0,
            string _material14No = null, byte _material14Count = 0,
            string _material15No = null, byte _material15Count = 0,
            string _material16No = null, byte _material16Count = 0, 
            string _material17No = null, byte _material17Count = 0,
            string _material18No = null, byte _material18Count = 0,
            string _material19No = null, byte _material19Count = 0,
            string _material20No = null, byte _material20Count = 0)
        {
            try
            {
                _adapter.UpdateProductBOM( _productName, 
            _material1No ,  _material1Count, 
            _material2No ,  _material2Count,
            _material3No ,  _material3Count,
            _material4No ,  _material4Count,
            _material5No ,  _material5Count,
            _material6No ,  _material6Count, 
            _material7No ,  _material7Count,
            _material8No ,  _material8Count,
            _material9No ,  _material9Count,
            _material10No , _material10Count,
            _material11No , _material11Count, 
            _material12No , _material12Count,
            _material13No , _material13Count,
            _material14No , _material14Count,
            _material15No , _material15Count,
            _material16No , _material16Count, 
            _material17No , _material17Count,
            _material18No , _material18Count,
            _material19No , _material19Count,
            _material20No , _material20Count,
            _productNo);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
