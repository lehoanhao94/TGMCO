//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGMCO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
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
        public string WEIGHT { get; set; }
        public string FEATURED { get; set; }
        public string ACCESSORIES { get; set; }
        public string MADE_IN { get; set; }
        public string UNIT { get; set; }
        public string DESCRIPTION { get; set; }
        public string WARRANTY { get; set; }
    }
}
