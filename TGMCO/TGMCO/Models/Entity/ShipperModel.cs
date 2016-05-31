using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class ShipperModel
    {
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public string GetName(int id)
        {
            try
            {
                return db.SHIPPERS.Find(id).SHIPPER_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}