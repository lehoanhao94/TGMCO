using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class OrderMailModel
    {
        public ORDER ORDER { get; set; }
        public List<ORDER_DETAILS> lstORDER_DETAILS { get; set; }      

        public OrderMailModel(ORDER _ORDER, List<ORDER_DETAILS> _lstORDER_DETAILS)
        {
            ORDER = _ORDER;
            lstORDER_DETAILS = _lstORDER_DETAILS;
        }
    }
}