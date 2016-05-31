using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class ProductController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Detail(int id)
        {
            try
            {
                PRODUCT _PRODUCT = db.PRODUCTS.Find(id);
                return View(_PRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }          
        }
        [HttpPost]
        public ActionResult Search(FormCollection f)
        {
            try
            {
                string key = f.Get("txtKeySearch").ToString().Trim();
                SUPPLIER _SUPPLIER = (SUPPLIER)Session["SUPPLIER_MODEL"];
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => (n.PRODUCT_CODE.Contains(key) || n.PRODUCT_NAME.Contains(key)) && n.SUPPLIER_ID == _SUPPLIER.SUPPLIER_ID).ToList();
                ViewBag.KeySearch = key;
                ViewBag.NumProduct = _lstPRODUCT.Count;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

    }
}
