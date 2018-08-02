using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class OrderInfo
    {

        private string _orderNo;
        private string _productNo;
        private string _customerName;
        private DateTime _orderGenTime;
        private int _orderCount;
        private byte _orderStatus;
       
    
        /// <summary>
        /// The name. E.G.
        /// </summary>       

        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        public string ProductNo
        {
            get { return _productNo; }
            set { _productNo = value; }
        }

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public DateTime OrderGenTime
        {
            get { return _orderGenTime; }
            set { _orderGenTime = value; }
        }

        public int orderCount
        {
            get { return _orderCount; }
            set { _orderCount = value; }
        }


        public byte OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }
        

        public static void CheckOrderExsit(string _orderNo, string _productNo, int _orderCount)
        {

            if (!OrderInfoBLL.OrderAlreadyExsit(_orderNo))
            {
                OrderInfoBLL.OrderInfoInsert(_orderNo, _productNo, _orderCount);
            }
            else
            {
                OrderInfoBLL.OrderInfoUpdate(_orderNo, _productNo, _orderCount);
            }

        }



        public static bool UpdateOrderCount(string _orderNo, string _productNo, int _updatedOrderCount)
        {
               int originalOrderCount = OrderInfoBLL.GetOrderCountByOrderNo(_orderNo);

               return (OrderInfoBLL.OrderInfoUpdate(_orderNo, _productNo, _updatedOrderCount + originalOrderCount) && 
                   ProductionLineInfoBLL.UpdateOrderNoForProductionLine(_orderNo, _productNo, _updatedOrderCount + originalOrderCount));
        }


        public static void RemoveOrder(string _orderNo, string _productNo)
        {

            if (!OrderInfoBLL.OrderAlreadyExsit(_orderNo))
            {
                OrderInfoBLL.OrderInfoDelete(_orderNo, _productNo);
            }

        }

        public static void UpdateOrderStatus(string _orderNo, string _productNo, byte _orderStatus)
        {

            if (!OrderInfoBLL.OrderAlreadyExsit(_orderNo))
            {
                OrderInfoBLL.OrderInfoUpdate(_orderNo, _productNo,  _orderStatus);
            }

        }

    }
}
