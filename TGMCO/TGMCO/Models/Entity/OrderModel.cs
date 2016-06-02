using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class OrderModel
    {
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public int GetNumOrder(int user_id)
        {
            try
            {
                return db.ORDERS.Where(n => n.USER_ID == user_id).ToList().Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumProduct(int user_id)
        {
            try
            {
                int _result = 0;
                foreach(var order in db.ORDERS.Where(n => n.USER_ID == user_id).ToList())
                {
                    _result += db.ORDER_DETAILS.Where(n => n.ORDER_ID == order.ORDER_ID).ToList().Count;
                }

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ORDER_DETAILS> GetListOrderDetail(int order_id)
        {
            try
            {
                return db.ORDER_DETAILS.Where(n => n.ORDER_ID == order_id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}