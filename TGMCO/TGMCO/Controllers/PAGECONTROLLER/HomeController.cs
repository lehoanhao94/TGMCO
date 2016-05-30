using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Index()
        {
            try
            {
                Session["SUPPLIER"] = "STANLEY";
                SUPPLIER _Supplier = db.SUPPLIERS.First();
                Session["SUPPLIER_MODEL"] = _Supplier;
                ViewBag.Suppliers = db.SUPPLIERS.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }          
        }

        public ViewResult Supplier(int id)
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Find(id);
                if (_Supplier.SUPPLIER_NAME.Contains("MAKITA"))
                {
                    Session["SUPPLIER"] = "MAKITA";
                    Session["SUPPLIER_MODEL"] = _Supplier;
                }
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                throw ex; // 404
            }
        }

        [HttpGet]
        public int GetNumItemsCart()
        {
            int i = DateTime.Now.Second;
            return i;
        }
    }
}
