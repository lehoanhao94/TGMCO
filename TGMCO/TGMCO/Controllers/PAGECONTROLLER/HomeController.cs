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

        public ViewResult makita()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("MAKITA")).Single();
                Session["SUPPLIER"] = "MAKITA";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
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
