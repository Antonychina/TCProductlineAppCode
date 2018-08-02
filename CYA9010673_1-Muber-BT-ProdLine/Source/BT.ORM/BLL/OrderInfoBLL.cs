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

    public class OrderInfoBLL
    {
        private static OrderInfoTableAdapter _orderInfoTableAdapter;

        private static  Mubea_BTDataSet.OrderInfoDataTable _orderInfoTable;


        public static Mubea_BTDataSet.OrderInfoDataTable OrderInfoTable
        {
            get
            {
                //if (_SenderInfoTable == null)
                {
                    _orderInfoTable = _orderInfoTableAdapter.GetData();
                }

                return _orderInfoTable;
            }
        }


        static OrderInfoBLL()
        {
            _orderInfoTableAdapter = new OrderInfoTableAdapter();
        }

        public static void RefreshUserInfoTable()
        {
            _orderInfoTable = _orderInfoTableAdapter.GetData();
        }

        /// <summary>
        /// BLL functions
        /// </summary>

        ///
        public static bool OrderInfoInsert(string _orderNo, string _productNo, int _orderCount)
        {
            try
            {
                _orderInfoTableAdapter.InsertOrder(_orderNo, _productNo, "", DateTime.Now, _orderCount);
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public static bool OrderInfoUpdate(string _orderNo, string _productNo, int _orderCount)
        {
            try
            {
                _orderInfoTableAdapter.UpdateOrderInfo(_orderCount, _orderNo, _productNo);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static bool OrderInfoDelete(string _orderNo, string _productNo)
        {
            try
            {
                _orderInfoTableAdapter.DeleteOrder(_orderNo, _productNo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool OrderInfoUpdate(string _orderNo, string _productNo, byte _orderStatus)
        {
            try
            {
                _orderInfoTableAdapter.UpdateOrderStatus(_orderStatus, _orderNo, _productNo);
            }
            catch
            {
                return false;
            }
            return true;
        }
        

        public static bool OrderAlreadyExsit(string _orderNo)
        {
            foreach (var val in _orderInfoTableAdapter.QueryByOrderNo(_orderNo))
            {
                if (val.OrderNo != null && val.OrderNo != "")
                    return true;
            }
               
            return false;
        }

        public static int GetOrderCountByOrderNo(string _orderNo)
        {
            foreach (var val in _orderInfoTableAdapter.QueryByOrderNo(_orderNo))
            {
                if (val.OrderNo != null && val.OrderNo != "")
                    return val.OrderCount;
            }

            return 0;
        }
    }


}
