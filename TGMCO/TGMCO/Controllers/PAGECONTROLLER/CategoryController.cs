using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class CategoryController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ViewResult Category(int supplier_id, string supplier_code, int cate_id)
        {
            try
            {
                SupplierModel _SUPPLIER = new SupplierModel();
                Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(supplier_id);
                Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(supplier_id);

                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => n.SUPPLIER_ID == supplier_id && n.CATEGORY_ID == cate_id).ToList();
                ViewBag.CategoryName = db.CATEGORIES.Single(n => n.CATEGORY_ID == cate_id).CATEGORY_NAME;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                return null;
            }
        }

        public ViewResult Category_Bosch(int id) // BOSCH 23
        {
            try
            {
                SupplierModel _SUPPLIER = new SupplierModel();
                Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(23);
                Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(23);

                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => n.SUPPLIER_ID == 23 && n.CATEGORY_ID == id).ToList();
                ViewBag.CategoryName = db.CATEGORIES.Single(n => n.CATEGORY_ID == id).CATEGORY_NAME;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                return null;
            }
        }

        public ViewResult Accessory_Bosch(int cate_id) // BOSCH 23
        {
            try
            {
                SupplierModel _SUPPLIER = new SupplierModel();
                Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(23);
                Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(23);

                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => n.SUPPLIER_ID == 23 && n.CATEGORY_ID == cate_id).ToList();
                ViewBag.CategoryName = db.CATEGORIES.Single(n => n.CATEGORY_ID == cate_id).CATEGORY_NAME;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                return null;
            }
        }



    }
}
