using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;


namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingCategoriesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        /// <summary>
        /// 
        /// </summary>
        private CATEGORy m_objCATEGORY = new CATEGORy();
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
                string _CategoryName = _frmCollect.Get("CategoryrName").ToString();
                string _Type = _frmCollect.Get("Type").ToString();
                if (ModelState.IsValid)
                {
                    if (db.CATEGORIES.Where(n => n.CATEGORY_NAME == _CategoryName).Count() == 0)
                    {
                        //Binding
                        m_objCATEGORY.CATEGORY_NAME = _frmCollect.Get("CategoryrName").ToString();
                        m_objCATEGORY.DESCRIPTION = _frmCollect.Get("Description").ToString();
                        if(_Type.Equals("PK"))
                        {
                            m_objCATEGORY.IS_ACCESSORY = true;
                        }
                        if(_frmCollect.GetValue("IsActive") != null)
                        {
                            m_objCATEGORY.IS_ACTIVE = true;
                        }
                        else
                        {
                            m_objCATEGORY.IS_ACTIVE = false;
                        }
                        if (!string.IsNullOrEmpty(m_objCATEGORY.CATEGORY_NAME))
                        {
                            //Upload File
                            if (fileUpload != null)
                            {
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_objCATEGORY.IMAGE = "~/Images/ADMIN/CATEGORIES/" + _fileNameRandom + fileUpload.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/ADMIN/CATEGORIES/" + _fileNameRandom + fileUpload.FileName));
                                fileUpload.SaveAs(_path);
                            }
                            db.CATEGORIES.Add(m_objCATEGORY);
                            db.SaveChanges();
                            TempData["VB_SuccessCreate"] = "Thêm thành công danh mục: " + m_objCATEGORY.CATEGORY_NAME + ".";
                        }
                        else
                        {
                            TempData["VB_ErrorCreate"] = "Chưa nhập tên danh mục.";
                        }

                    }
                    else
                    {
                        TempData["VB_ErrorCreate"] = "Tên danh mục đã tồn tại.";
                    }
                }
                else
                {
                    TempData["VB_ErrorCreate"] = "Thêm không thành công, kiểm tra lại dữ liệu.";
                }

                if (_Type.Equals("PK")) //Phụ kiện
                {
                    return RedirectToAction("ManagingAccesstories", "Admin");
                }
                else //Sản phẩm
                {
                    return RedirectToAction("ManagingCategories", "Admin");
                }

                
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
                var _Category = db.CATEGORIES.Find(id);
                db.CATEGORIES.Remove(_Category);
                db.SaveChanges();
                return RedirectToAction("ManagingCategories", "Admin");
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
                var _Category = db.CATEGORIES.Find(id);
                _Category.IS_ACTIVE = !_Category.IS_ACTIVE;
                db.SaveChanges();

                return RedirectToAction("ManagingCategories", "Admin");

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
