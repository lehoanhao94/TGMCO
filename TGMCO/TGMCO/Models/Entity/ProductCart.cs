using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;
using TGMCO.Models;


namespace TGMCO.Models.Entity
{
    public class ProductCart
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public int QUANTITY { get; set; }   
        public string IMAGE_1 { get; set; }       
        public string UNIT { get; set; }
        public decimal TOTAL_PRICE { get { return UNIT_PRICE * QUANTITY; } }

        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ProductCart(int id, int quantity)
        {
            try
            {
                PRODUCT _PRODUCT = db.PRODUCTS.Find(id);
                ProductModel _PRODUCT_MODEL = new ProductModel();

                this.PRODUCT_ID = id;
                this.PRODUCT_CODE = _PRODUCT.PRODUCT_CODE;
                this.PRODUCT_NAME = _PRODUCT.PRODUCT_NAME;
                this.UNIT_PRICE = _PRODUCT.UNIT_PRICE;
                this.UNIT = _PRODUCT.UNIT;
                this.IMAGE_1 = _PRODUCT_MODEL.GetImage1(id);
                this.QUANTITY = quantity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
    }
}