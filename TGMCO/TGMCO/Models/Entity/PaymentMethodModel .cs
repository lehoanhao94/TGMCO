using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class PaymentMethodModel
    {
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public string GetName(int id)
        {
            try
            {
                return db.PAYMENT_METHOD.Find(id).DESCRIPTION;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}