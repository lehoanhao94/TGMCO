using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class OrderController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Order()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    TempData["Fail"] = "Bạn chưa đăng nhập, vui lòng đăng nhập trước khi mua hàng.";
                    
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                }

                if(Session["ShoppingCart"] == null)
                {
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                }
                int id = 0;
                ORDER _ORDER = new ORDER();
                if(Session["Order_Id"] != null)
                {
                    id = (int)Session["Order_Id"];
                    _ORDER = db.ORDERS.Find(id);                   
                }
               
                ViewBag.ListShipper = new SelectList(db.SHIPPERS.OrderByDescending(n => n.SHIPPER_ID).ToList(), "Shipper_ID", "SHIPPER_NAME");
                return View(_ORDER);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }         
        }

        [HttpPost]
        public ActionResult Create(FormCollection f)
        {
            try
            {
                string _Name = f.Get("Name").ToString();
                string _Address = f.Get("Address").ToString();
                string _Note = f.Get("Note").ToString();
                string _Email = f.Get("Email").ToString();
                string _Mobile = f.Get("Mobile").ToString();
                int _ShipVia = int.Parse(f.Get("ListShipper").ToString());

                if(Session["Order_Id"] == null)
                {
                    ORDER _ORDER = new ORDER();
                    ORDER_DETAILS _ORDER_DETAILS = null;

                    USER _USER = (USER)Session["SS_USER"];
                    _ORDER.USER_ID = _USER.USER_ID;
                    _ORDER.ORDER_DATE = DateTime.Now;
                    _ORDER.SHIP_NAME = _Name;
                    _ORDER.SHIP_ADDRESS = _Address;
                    _ORDER.SHIP_PHONE = _Mobile;
                    _ORDER.SHIP_EMAIL = _Email;
                    _ORDER.NOTE = _Note;
                    _ORDER.ORDER_STATUS_ID = 3;
                    _ORDER.SHIP_VIA_ID = _ShipVia;
                    _ORDER.FREIGHT = (decimal)db.SHIPPERS.Single(n => n.SHIPPER_ID == _ORDER.SHIP_VIA_ID).FREIGHT;
                    _ORDER.SUBTOTAL = decimal.Parse(Session["TOTAL_PRICE"].ToString());
                    db.ORDERS.Add(_ORDER);
                    db.SaveChanges();
                    List<ProductCart> _lstProductCart = (List<ProductCart>)Session["ShoppingCart"];
                    foreach (var product in _lstProductCart)
                    {
                        _ORDER_DETAILS = new ORDER_DETAILS();
                        _ORDER_DETAILS.ORDER_ID = _ORDER.ORDER_ID;
                        _ORDER_DETAILS.PRODUCT_ID = product.PRODUCT_ID;
                        _ORDER_DETAILS.UNIT_PRICE = product.UNIT_PRICE;
                        _ORDER_DETAILS.UNIT = product.UNIT;
                        _ORDER_DETAILS.QUANTITY = product.QUANTITY;
                        _ORDER_DETAILS.EXTENDED_PRICE = product.TOTAL_PRICE;
                        db.ORDER_DETAILS.Add(_ORDER_DETAILS);
                    }
                    db.SaveChanges();
                    Session["Order_Id"] = _ORDER.ORDER_ID;
                }
                else
                {
                    int _Order_Id = (int)Session["Order_Id"];
                    ORDER _ORDER = db.ORDERS.Find(_Order_Id);

                    USER _USER = (USER)Session["SS_USER"];
                    _ORDER.USER_ID = _USER.USER_ID;
                    _ORDER.ORDER_DATE = DateTime.Now;
                    _ORDER.SHIP_NAME = _Name;
                    _ORDER.SHIP_ADDRESS = _Address;
                    _ORDER.SHIP_PHONE = _Mobile;
                    _ORDER.SHIP_EMAIL = _Email;
                    _ORDER.NOTE = _Note;
                    _ORDER.ORDER_STATUS_ID = 3;
                    _ORDER.SHIP_VIA_ID = _ShipVia;
                    _ORDER.FREIGHT = (decimal)db.SHIPPERS.Single(n => n.SHIPPER_ID == _ORDER.SHIP_VIA_ID).FREIGHT;
                    _ORDER.SUBTOTAL = decimal.Parse(Session["TOTAL_PRICE"].ToString());
                    db.SaveChanges();                   
                    Session["Order_Id"] = _ORDER.ORDER_ID;
                }

                return RedirectToAction("Payment", "Payment");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
