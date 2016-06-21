using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class NewsController : Controller
    {
        public ActionResult News()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult NewsPromotion()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
