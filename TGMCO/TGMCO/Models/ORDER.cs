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
    
    public partial class ORDER
    {
        public int ORDER_ID { get; set; }
        public int USER_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }
        public System.DateTime ORDER_DATE { get; set; }
        public string SHIP_CODE { get; set; }
        public Nullable<System.DateTime> SHIPPED_DATE { get; set; }
        public int SHIP_VIA_ID { get; set; }
        public decimal FREIGHT { get; set; }
        public string SHIP_NAME { get; set; }
        public string SHIP_ADDRESS { get; set; }
        public string SHIP_PHONE { get; set; }
        public decimal SUBTOTAL { get; set; }
        public int ORDER_STATUS_ID { get; set; }
        public string SHIP_EMAIL { get; set; }
        public string NOTE { get; set; }
        public Nullable<int> PAYMENT_METHOD_ID { get; set; }
        public string ORDER_CODE { get; set; }
        public string NOTE_EMPLOYEE { get; set; }
    }
}
