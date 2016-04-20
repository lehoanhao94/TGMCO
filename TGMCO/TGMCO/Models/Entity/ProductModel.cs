﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class ProductModel
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public Nullable<int> CATEGORY_EXTRA_ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public int SUPPLIER_ID { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public int IDX { get; set; }
        public bool IS_NEW { get; set; }
        public bool IS_STILL { get; set; }
        public bool IS_ACTIVE { get; set; }
        public int QUANTITY_SOLD { get; set; }
        public string CAPACITY { get; set; }
        public string WEIGHT { get; set; }
        public string DESCRIPTION_1 { get; set; }
        public string DESCRIPTION_2 { get; set; }
        public string DESCRIPTION_3 { get; set; }
        public string DESCRIPTION_4 { get; set; }
        public string DESCRIPTION_5 { get; set; }
        public string DESCRIPTION_6 { get; set; }
        public string DESCRIPTION_7 { get; set; }
        public string DESCRIPTION_8 { get; set; }
        public string DESCRIPTION_9 { get; set; }
        public string FEATURED { get; set; }
        public string ACCESSORIES { get; set; }
        public string MADE_IN { get; set; }
        public string UNIT { get; set; }
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public bool IsExistProductCode(string id_product)
        {
            try
            {
                List<PRODUCT> lstProduct = db.PRODUCTS.Where(n => n.PRODUCT_CODE.Equals(id_product)).ToList();
                foreach (var product in lstProduct)
                {
                    if (product.PRODUCT_CODE.Equals(id_product))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}