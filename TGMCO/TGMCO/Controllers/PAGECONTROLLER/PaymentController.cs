using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class PaymentController : Controller
    {

        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Payment()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    TempData["Fail"] = "Bạn chưa đăng nhập, vui lòng đăng nhập trước khi mua hàng.";

                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                }

                if (Session["ShoppingCart"] == null)
                {
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
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
