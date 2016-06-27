using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingProductsController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        /// <summary>
        /// 
        /// </summary>
        private PRODUCT m_PRODUCT = new PRODUCT();
        /// <summary>
        /// 
        /// </summary>
        private PRODUCT_IMAGES m_PRODUCT_IMAGES = new PRODUCT_IMAGES();
        /// <summary>
        /// 
        /// </summary>
        private PRODUCT_FILES m_PRODUCT_FILES = new PRODUCT_FILES();
        /// <summary>
        /// 
        /// </summary>
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(10);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            try
            {
                if (Session["SS_USER_ADMIN"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.ToList(), "Supplier_ID", "Supplier_Name");
                    ViewBag.ListCategory = new SelectList(db.CATEGORIES.ToList(), "Category_ID", "Category_Name");
                    ViewBag.ListCategoryExtra = new SelectList(db.CATEGORIES_EXTRA.ToList(), "Category_Extra_ID", "Category_Extra_Name");

                    return View();
                }
            }
            catch 
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        //
        // POST: /ManagingProducts/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="Image"></param>
        /// <param name="Image_1"></param>
        /// <param name="Image_2"></param>
        /// <param name="Image_3"></param>
        /// <param name="Image_4"></param>
        /// <param name="Image_5"></param>
        /// <param name="fileUpload_1"></param>
        /// <param name="fileUpload_2"></param>
        /// <param name="fileUpload_3"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase Image, HttpPostedFileBase Image_1, HttpPostedFileBase Image_2,
            HttpPostedFileBase fileUpload_1, HttpPostedFileBase fileUpload_2, HttpPostedFileBase fileUpload_3)
        {
            try
            {
                m_PRODUCT.PRODUCT_CODE = collection.Get("PRODUCT_CODE").ToString();
                if(db.PRODUCTS.Where(n => n.PRODUCT_CODE.Equals(m_PRODUCT.PRODUCT_CODE)).Count() == 0)
                {                   
                    m_PRODUCT.PRODUCT_NAME = collection.Get("PRODUCT_NAME").ToString();
                    if (!string.IsNullOrEmpty(collection.Get("ListSupplier").ToString()))
                    {                      
                        if (!string.IsNullOrEmpty(collection.Get("ListCategory").ToString()))
                        {
                            m_PRODUCT.SUPPLIER_ID = Int32.Parse(collection.Get("ListSupplier").ToString());
                            m_PRODUCT.CATEGORY_ID = Int32.Parse(collection.Get("ListCategory").ToString());
                            if (!string.IsNullOrEmpty(collection.Get("ListCategoryExtra").ToString()))
                                m_PRODUCT.CATEGORY_EXTRA_ID = Int32.Parse(collection.Get("ListCategoryExtra").ToString());
                            m_PRODUCT.UNIT_PRICE = decimal.Parse(collection.Get("UNIT_PRICE").ToString());
                            m_PRODUCT.UNIT = collection.Get("UNIT").ToString();
                            m_PRODUCT.WARRANTY = collection.Get("WARRANTY").ToString();
                            m_PRODUCT.WEIGHT = collection.Get("WEIGHT").ToString();
                            m_PRODUCT.MADE_IN = collection.Get("MADE_IN").ToString();
                            m_PRODUCT.FEATURED = collection.Get("FEATURED").ToString();
                            m_PRODUCT.ACCESSORIES = collection.Get("ACCESSORIES").ToString();
                            m_PRODUCT.DESCRIPTION = collection.Get("DESCRIPTION_1").ToString();
                            m_PRODUCT.IDX = int.Parse(collection.Get("IDX").ToString());
                            if (collection.GetValue("IsActive") != null)
                            {
                                m_PRODUCT.IS_ACTIVE = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_ACTIVE = false;
                            }
                            if (collection.GetValue("IsStill") != null)
                            {
                                m_PRODUCT.IS_STILL = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_STILL = false;
                            }
                            if (collection.GetValue("IsNew") != null)
                            {
                                m_PRODUCT.IS_NEW = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_NEW = false;
                            }
                            db.PRODUCTS.Add(m_PRODUCT);
                            db.SaveChanges();

                            #region UPLOAD IMAGE
                            //Upload File
                            if (Image != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_1 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);
                                db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                            }

                            if (Image_1 != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_2 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);
                                db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                            }

                            if (Image_2 != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_3 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);
                                db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                            }
                            #endregion

                            #region UPLOAD FILES
                            //Upload File
                            if (fileUpload_1 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_1 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_1.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_1.FileName));
                                fileUpload_1.SaveAs(_path);
                                db.PRODUCT_FILES.Add(m_PRODUCT_FILES);
                            }

                            if (fileUpload_2 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_2 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_2.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_2.FileName));
                                fileUpload_2.SaveAs(_path);
                                db.PRODUCT_FILES.Add(m_PRODUCT_FILES);
                            }

                            if (fileUpload_3 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_3 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_3.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_3.FileName));
                                fileUpload_3.SaveAs(_path);
                                db.PRODUCT_FILES.Add(m_PRODUCT_FILES);
                            }
                            #endregion

                            db.SaveChanges();

                            TempData["SuccessCreate"] = "Thêm thành công sản phẩm mã: " + m_PRODUCT.PRODUCT_CODE + ".";
                            return RedirectToAction("ManagingProducts", "Admin");
                        }
                        else
                        {
                            TempData["ErrorCreate"] = "Chưa chọn danh mục sản phẩm.";
                        }
                    }
                    else
                    {
                        TempData["ErrorCreate"] = "Chưa chọn nhà cung cấp.";
                    }                                  
                }
                else
                {
                    TempData["ErrorCreate"] = "Mã sản phẩm đã tồn tại.";
                }
                return RedirectToAction("Create", "ManagingProducts", FormMethod.Get);
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
        public ActionResult ChangeStatusActive(int id)
        {
            try
            {
                var _Product = db.PRODUCTS.Find(id);
                _Product.IS_ACTIVE = !_Product.IS_ACTIVE;
                db.SaveChanges();

                return RedirectToAction("ManagingProducts", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeStatusStill(int id)
        {
            try
            {
                var _Product = db.PRODUCTS.Find(id);
                _Product.IS_STILL = !_Product.IS_STILL;
                db.SaveChanges();

                return RedirectToAction("ManagingProducts", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeStatusNew(int id)
        {
            try
            {
                var _Product = db.PRODUCTS.Find(id);
                _Product.IS_NEW = !_Product.IS_NEW;
                db.SaveChanges();

                return RedirectToAction("ManagingProducts", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
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
                var _Product = db.PRODUCTS.Find(id);
                db.PRODUCTS.Remove(_Product);
                db.SaveChanges();
                return RedirectToAction("ManagingProducts", "Admin");
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
        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.ToList(), "Supplier_ID", "Supplier_Name");
                ViewBag.ListCategory = new SelectList(db.CATEGORIES.ToList(), "Category_ID", "Category_Name");
                ViewBag.ListCategoryExtra = new SelectList(db.CATEGORIES_EXTRA.ToList(), "Category_Extra_ID", "Category_Extra_Name");
                return View(db.PRODUCTS.Find(id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection, HttpPostedFileBase Image, HttpPostedFileBase Image_1, HttpPostedFileBase Image_2,
            HttpPostedFileBase fileUpload_1, HttpPostedFileBase fileUpload_2, HttpPostedFileBase fileUpload_3)
        {
            try
            {
                m_PRODUCT = db.PRODUCTS.Find(id);
                int _isAddImagesNew = 0;
                int _isAddFilesNew = 0;

                m_PRODUCT_IMAGES = db.PRODUCT_IMAGES.Where(n => n.PRODUCT_ID == id).SingleOrDefault();
                if (m_PRODUCT_IMAGES == null)
                    _isAddImagesNew = 1;
                
                m_PRODUCT_FILES = db.PRODUCT_FILES.Where(n => n.PRODUCT_ID == id).SingleOrDefault();
                if (m_PRODUCT_FILES == null)
                    _isAddFilesNew = 1;
                string _Temp = m_PRODUCT.PRODUCT_CODE;
                m_PRODUCT.PRODUCT_CODE = collection.Get("PRODUCT_CODE").ToString();
                if (db.PRODUCTS.Where(n => n.PRODUCT_CODE.Equals(m_PRODUCT.PRODUCT_CODE)).Count() == 0 || _Temp.Equals(m_PRODUCT.PRODUCT_CODE))
                {
                    m_PRODUCT.PRODUCT_NAME = collection.Get("PRODUCT_NAME").ToString();
                    if (!string.IsNullOrEmpty(collection.Get("SUPPLIER_ID").ToString()))
                    {
                        if (!string.IsNullOrEmpty(collection.Get("CATEGORY_ID").ToString()))
                        {
                            m_PRODUCT.SUPPLIER_ID = Int32.Parse(collection.Get("SUPPLIER_ID").ToString());
                            m_PRODUCT.CATEGORY_ID = Int32.Parse(collection.Get("CATEGORY_ID").ToString());
                            if (!string.IsNullOrEmpty(collection.Get("CATEGORY_EXTRA_ID").ToString()))
                                m_PRODUCT.CATEGORY_EXTRA_ID = Int32.Parse(collection.Get("CATEGORY_EXTRA_ID").ToString());
                            m_PRODUCT.UNIT_PRICE = decimal.Parse(collection.Get("UNIT_PRICE").ToString());
                            m_PRODUCT.UNIT = collection.Get("UNIT").ToString();
                            m_PRODUCT.WARRANTY = collection.Get("WARRANTY").ToString();
                            m_PRODUCT.WEIGHT = collection.Get("WEIGHT").ToString();
                            m_PRODUCT.MADE_IN = collection.Get("MADE_IN").ToString();
                            m_PRODUCT.FEATURED = collection.Get("FEATURED").ToString();
                            m_PRODUCT.ACCESSORIES = collection.Get("ACCESSORIES").ToString();
                            m_PRODUCT.DESCRIPTION = collection.Get("DESCRIPTION_1").ToString();
                            m_PRODUCT.IDX = int.Parse(collection.Get("IDX").ToString());
                            if (collection.GetValue("IsActive") != null)
                            {
                                m_PRODUCT.IS_ACTIVE = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_ACTIVE = false;
                            }
                            if (collection.GetValue("IsStill") != null)
                            {
                                m_PRODUCT.IS_STILL = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_STILL = false;
                            }
                            if (collection.GetValue("IsNew") != null)
                            {
                                m_PRODUCT.IS_NEW = true;
                            }
                            else
                            {
                                m_PRODUCT.IS_NEW = false;
                            }
                            db.SaveChanges();

                            #region UPLOAD IMAGE
                            //Upload File
                            if (m_PRODUCT_IMAGES == null)
                                m_PRODUCT_IMAGES = new PRODUCT_IMAGES();
                            if (Image != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_1 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);
                            }

                            if (Image_1 != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_2 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);
                            }

                            if (Image_2 != null)
                            {
                                m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_IMAGES.IMAGE_3 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                                Image.SaveAs(_path);

                            }

                            if (_isAddImagesNew == 1)
                                db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                            #endregion

                            #region UPLOAD FILES
                            //Upload File
                            int flag = 0;
                            if (m_PRODUCT_FILES == null)
                                m_PRODUCT_FILES = new PRODUCT_FILES();
                            if (fileUpload_1 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_1 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_1.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_1.FileName));
                                fileUpload_1.SaveAs(_path);
                                flag++;
                            }

                            if (fileUpload_2 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_2 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_2.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_2.FileName));
                                fileUpload_2.SaveAs(_path);
                                flag++;

                            }

                            if (fileUpload_3 != null)
                            {
                                m_PRODUCT_FILES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_PRODUCT_FILES.FILE_3 = "~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_3.FileName;
                                string _path = Path.Combine(Server.MapPath("~/FilesUpload/PRODUCTS/" + _fileNameRandom + "-" + fileUpload_3.FileName));
                                fileUpload_3.SaveAs(_path);
                                flag++;

                            }

                            if (_isAddFilesNew == 1 && flag > 0)
                                db.PRODUCT_FILES.Add(m_PRODUCT_FILES);
                            #endregion

                            db.SaveChanges();

                            TempData["SuccessCreate"] = "Cập nhật thành công sản phẩm mã: " + m_PRODUCT.PRODUCT_CODE + ".";
                            return RedirectToAction("Update", "ManagingProducts", FormMethod.Get);
                        }
                        else
                        {
                            TempData["ErrorCreate"] = "Chưa chọn danh mục sản phẩm.";
                        }
                    }
                    else
                    {
                        TempData["ErrorCreate"] = "Chưa chọn nhà cung cấp.";
                    }
                }
                else
                {
                    TempData["ErrorCreate"] = "Mã sản phẩm đã tồn tại.";
                }
                return RedirectToAction("Update", "ManagingProducts", FormMethod.Get);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
