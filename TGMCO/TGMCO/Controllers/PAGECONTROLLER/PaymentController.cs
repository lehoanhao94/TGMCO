using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class PaymentController : Controller
    {

        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Payment()
        {
            try
            {
                if (string.IsNullOrEmpty(Session["SUPPLIER"].ToString()))
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }

                List<ProductCart> _lstProductCart = (List<ProductCart>)Session["ShoppingCart"];
                if (Session["ShoppingCart"] == null || _lstProductCart.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (Session["Order_Id"] != null)
                {
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult UpdatePaymentMethod(int id, FormCollection f)
        {
            try
            {
                ORDER _ORDER = new ORDER();
                _ORDER = db.ORDERS.Find(id);
                _ORDER.PAYMENT_METHOD_ID = int.Parse(f.Get("Payment").ToString());
                db.SaveChanges();
                return RedirectToAction("PrintBill", "Order", new { id = _ORDER.ORDER_ID});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
