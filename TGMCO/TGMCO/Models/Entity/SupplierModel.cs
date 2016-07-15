using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class SupplierModel
    {
        public int SUPPLIER_ID { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string IMAGE { get; set; }

        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public string GetSupplierName(int id)
        {
            try
            {
                if (db.SUPPLIERS.Find(id) != null)
                    return db.SUPPLIERS.Find(id).SUPPLIER_NAME;
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}