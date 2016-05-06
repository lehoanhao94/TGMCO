using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class OrderStatusModel
    {
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public string GetName(int id)
        {
            try
            {
                return db.ORDER_STATUS.Find(id).ORDER_STATUS_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}