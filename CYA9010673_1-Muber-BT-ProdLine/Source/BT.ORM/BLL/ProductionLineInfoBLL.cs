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

    public class ProductionLineInfoBLL
    {
        private static ProductionLineInfoTableAdapter _productionLineInfoTableAdapter;

        private static  Mubea_BTDataSet.ProductionLineInfoDataTable _productionLineInfoTable;


        public static Mubea_BTDataSet.ProductionLineInfoDataTable ProductionLineTable
        {
            get
            {
                //if (_SenderInfoTable == null)
                {
                    _productionLineInfoTable = _productionLineInfoTableAdapter.GetData();
                }

                return _productionLineInfoTable;
            }
        }


        static ProductionLineInfoBLL()
        {
            _productionLineInfoTableAdapter = new ProductionLineInfoTableAdapter();
        }

        public static void RefreshUserInfoTable()
        {
            _productionLineInfoTable = _productionLineInfoTableAdapter.GetData();
        }

        /// <summary>
        /// BLL functions
        /// </summary>

        ///
        public static bool InsertProductionLineInfo(byte _productionLineNo, string _orderNo, int _orderCount, string _productNo)
        {
            try
            {
                _productionLineInfoTableAdapter.InsertProductionLine(_productionLineNo, _orderNo, _orderCount, _productNo);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }


        public static bool UpdateOrderNoForProductionLine(string _orderNo, string _productNo, int _updatedOrderCount)
        {
            try
            {
                //if (null == _productionLineInfoTableAdapter.GetProdLineNoForOrderNo(_orderNo, _productNo))
              
                if (_productionLineInfoTableAdapter.GetProdLineNoForOrderNo(_orderNo, _productNo) > 0)
                    _productionLineInfoTableAdapter.UpdateOrderNoProductionLine( _updatedOrderCount,_orderNo, _productNo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }


        public static bool DeleteProductionLineInfo(byte _productionLineNo, string _orderNo)
        {
            try
            {
                _productionLineInfoTableAdapter.DeleteProductionLine(_productionLineNo, _orderNo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }


        public static bool ExistThisProductionLineInfo(byte _productionLineNo, string _orderNo)
        {
            try
            {
                if (((int)_productionLineInfoTableAdapter.CheckProductionLineExist(_productionLineNo)) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // exist = true. not exist = false
        public static bool CheckThisProductionLineInfo(byte _productionLineNo, string _orderNo)
        {
            try
            {
                if (_productionLineInfoTableAdapter.DoubleCheckProductLineInfo(_productionLineNo, _orderNo) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        

    }


}
