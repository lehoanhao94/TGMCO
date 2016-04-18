using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;


namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingCategoriesBySupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        /// <summary>
        /// 
        /// </summary>
        private SupplierModel m_SUPPLIER_MODEL = new SupplierModel();
        /// <summary>
        /// 
        /// </summary>
        private CategoryModel m_CATEGORY_MODEL = new CategoryModel();


        //
        // GET: /ManagingCategoriesBySupplier/Supplier/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Supplier(int id = 0)
        {
            try
            {
                List<CATEGORIES_BY_SUPPLIER> lstCategory_By_Supplier = db.CATEGORIES_BY_SUPPLIER.Where(n => n.SUPPLIER_ID == id).ToList();
                ViewBag.Supplier = db.SUPPLIERS.Find(id);
                ViewBag.ListCategory = db.CATEGORIES.Where(n => n.IS_ACTIVE == true).ToList();
                return View(lstCategory_By_Supplier);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCategory(int id_sup, int id_cate)
        {
            try
            {
                CATEGORIES_BY_SUPPLIER _CATEGORIER_BY_SUPPLIER = new CATEGORIES_BY_SUPPLIER();
                _CATEGORIER_BY_SUPPLIER.SUPPLIER_ID = id_sup;
                _CATEGORIER_BY_SUPPLIER.SUPPLIER_NAME = m_SUPPLIER_MODEL.GetSupplierName(id_sup);
                _CATEGORIER_BY_SUPPLIER.CATEGORY_ID = id_cate;
                _CATEGORIER_BY_SUPPLIER.CATEGORY_NAME = m_CATEGORY_MODEL.GetCategoryName(id_cate);
                db.CATEGORIES_BY_SUPPLIER.Add(_CATEGORIER_BY_SUPPLIER);
                db.SaveChanges();
                return RedirectToAction("ManagingCategoriesBySupplier", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult RemoveCategory(int id)
        {
            try
            {
                CATEGORIES_BY_SUPPLIER _CATEGORIER_BY_SUPPLIER = db.CATEGORIES_BY_SUPPLIER.Find(id);
                db.CATEGORIES_BY_SUPPLIER.Remove(_CATEGORIER_BY_SUPPLIER);
                db.SaveChanges();
                return RedirectToAction("ManagingCategoriesBySupplier", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}