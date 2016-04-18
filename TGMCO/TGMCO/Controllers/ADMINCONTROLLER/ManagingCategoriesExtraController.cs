using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingCategoriesExtraController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        /// <summary>
        /// 
        /// </summary>
        private CATEGORIES_EXTRA m_objCATEGORY_EXTRA = new CATEGORIES_EXTRA();
        /// <summary>
        /// 
        /// </summary>
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(20);

        //
        // POST: /ManagingCategories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection _frmCollect, HttpPostedFileBase fileUpload)
        {
            try
            {
                string _CategoryExtraName = _frmCollect.Get("CategoryExtraName").ToString();
                if (ModelState.IsValid)
                {
                    if (db.CATEGORIES_EXTRA.Where(n => n.CATEGORY_EXTRA_NAME == _CategoryExtraName).Count() == 0)
                    {
                        //Binding
                        m_objCATEGORY_EXTRA.CATEGORY_EXTRA_NAME = _frmCollect.Get("CategoryExtraName").ToString();
                        m_objCATEGORY_EXTRA.DESCRIPTION = _frmCollect.Get("Description").ToString();
                        m_objCATEGORY_EXTRA.CATEGORY_ID = Int32.Parse((_frmCollect.Get("ListCategory").ToString()));

                        if (_frmCollect.GetValue("IsActive") != null)
                        {
                            m_objCATEGORY_EXTRA.IS_ACTIVE = true;
                        }
                        else
                        {
                            m_objCATEGORY_EXTRA.IS_ACTIVE = false;
                        }
                        if (!string.IsNullOrEmpty(m_objCATEGORY_EXTRA.CATEGORY_EXTRA_NAME))
                        {
                            db.CATEGORIES_EXTRA.Add(m_objCATEGORY_EXTRA);
                            db.SaveChanges();
                            TempData["VB_SuccessCreate"] = "Thêm thành công danh mục con: " + m_objCATEGORY_EXTRA.CATEGORY_EXTRA_NAME + ".";
                        }
                        else
                        {
                            TempData["VB_ErrorCreate"] = "Chưa nhập tên danh mục con.";
                        }

                    }
                    else
                    {
                        TempData["VB_ErrorCreate"] = "Tên danh mục con đã tồn tại.";
                    }
                }
                else
                {
                    TempData["VB_ErrorCreate"] = "Thêm không thành công, kiểm tra lại dữ liệu.";
                }

                return RedirectToAction("ManagingCategoriesExtra", "Admin");
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
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var _CategoryExtra = db.CATEGORIES_EXTRA.Find(id);
                db.CATEGORIES_EXTRA.Remove(_CategoryExtra);
                db.SaveChanges();
                return RedirectToAction("ManagingCategoriesExtra", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            try
            {
                var _CategoryExtra = db.CATEGORIES_EXTRA.Find(id);
                _CategoryExtra.IS_ACTIVE = !_CategoryExtra.IS_ACTIVE;
                db.SaveChanges();

                return RedirectToAction("ManagingCategoriesExtra", "Admin");

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
