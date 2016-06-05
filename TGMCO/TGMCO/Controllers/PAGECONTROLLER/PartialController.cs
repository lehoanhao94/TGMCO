using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class PartialController : Controller
    {
        //
        // GET: /Partial/
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public PartialViewResult HotProducts(int id, string title)
        {
            try
            {
                SupplierModel _SUPPLIER = new SupplierModel();
                    Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(id);
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(id);

                ViewBag.Supplier = db.SUPPLIERS.Find(id);
                ViewBag.TITLE = title;
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => n.SUPPLIER_ID == id && n.IS_ACTIVE && n.IS_NEW).OrderByDescending(n => n.IDX).Take(8).ToList();

                if(_lstPRODUCT.Count > 7)
                {
                    return PartialView(_lstPRODUCT);
                }
                else
                {
                    return PartialView();
                }

            }
            catch (Exception ex)
            {
                throw ex; // 404
            }          
        }

        public PartialViewResult CategoryMenu(int id)
        {
            try
            {
                SupplierModel _SUPPLIER = new SupplierModel();
                Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(id);
                Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(id);

                ViewBag.SUPPLIER_ID = id;
                List<CATEGORIES_BY_SUPPLIER> _lstCATEGORY = db.CATEGORIES_BY_SUPPLIER.Where(n => n.SUPPLIER_ID == id).ToList();
                return PartialView(_lstCATEGORY);
            }
            catch (Exception ex)
            {
                throw ex; // 404
            }
        }

        public PartialViewResult VerticalSlider(string _title, string _type, int _supplier_id)
        {
            try
            {
                ViewBag.TITLE = _title;
                List<PRODUCT> _lstPRODUCT = new List<PRODUCT>();

                switch(_type)
                {
                    case "New":
                        _lstPRODUCT = db.PRODUCTS.Where(n => n.IS_NEW == true && n.IS_ACTIVE == true && (n.SUPPLIER_ID == _supplier_id || _supplier_id == 0)).OrderByDescending(n => n.IDX).Take(6).ToList();
                        break;
                    case "Hot":
                        _lstPRODUCT = db.PRODUCTS.Where(n => n.IS_ACTIVE == true && (n.SUPPLIER_ID == _supplier_id || _supplier_id == 0)).OrderByDescending(n => n.IDX).Take(6).ToList();
                        break;
                }

                return PartialView(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
