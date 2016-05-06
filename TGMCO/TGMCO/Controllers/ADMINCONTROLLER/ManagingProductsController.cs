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
                if (Session["SS_USER"] == null)
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
        public ActionResult Create(FormCollection collection, HttpPostedFileBase Image, HttpPostedFileBase Image_1,
            HttpPostedFileBase Image_2, HttpPostedFileBase Image_3, HttpPostedFileBase Image_4, HttpPostedFileBase Image_5,
            HttpPostedFileBase fileUpload_1, HttpPostedFileBase fileUpload_2, HttpPostedFileBase fileUpload_3)
        {
            try
            {
                m_PRODUCT.PRODUCT_CODE = collection.Get("PRODUCT_CODE").ToString();
                if(db.PRODUCTS.Where(n => n.PRODUCT_CODE.Equals(m_PRODUCT.PRODUCT_CODE)).Count() == 0)
                {
                    m_PRODUCT.PRODUCT_NAME = collection.Get("PRODUCT_NAME").ToString();
                    m_PRODUCT.SUPPLIER_ID = Int32.Parse(collection.Get("ListSupplier").ToString());
                    m_PRODUCT.CATEGORY_ID = Int32.Parse(collection.Get("ListCategory").ToString());
                    if (!string.IsNullOrEmpty(collection.Get("ListCategoryExtra").ToString()))
                        m_PRODUCT.CATEGORY_EXTRA_ID = Int32.Parse(collection.Get("ListCategoryExtra").ToString());
                    m_PRODUCT.UNIT_PRICE = decimal.Parse(collection.Get("UNIT_PRICE").ToString());
                    m_PRODUCT.UNIT = collection.Get("UNIT").ToString();
                    m_PRODUCT.CAPACITY = collection.Get("CAPACITY").ToString();
                    m_PRODUCT.WEIGHT = collection.Get("WEIGHT").ToString();
                    m_PRODUCT.MADE_IN = collection.Get("MADE_IN").ToString();
                    m_PRODUCT.FEATURED = collection.Get("FEATURED").ToString();
                    m_PRODUCT.ACCESSORIES = collection.Get("ACCESSORIES").ToString();
                    m_PRODUCT.DESCRIPTION_1 = collection.Get("DESCRIPTION_1").ToString();
                    m_PRODUCT.DESCRIPTION_2 = collection.Get("DESCRIPTION_2").ToString();
                    m_PRODUCT.DESCRIPTION_3 = collection.Get("DESCRIPTION_3").ToString();
                    m_PRODUCT.DESCRIPTION_4 = collection.Get("DESCRIPTION_4").ToString();
                    m_PRODUCT.DESCRIPTION_5 = collection.Get("DESCRIPTION_5").ToString();
                    m_PRODUCT.DESCRIPTION_6 = collection.Get("DESCRIPTION_6").ToString();
                    m_PRODUCT.DESCRIPTION_7 = collection.Get("DESCRIPTION_7").ToString();
                    m_PRODUCT.DESCRIPTION_8 = collection.Get("DESCRIPTION_8").ToString();
                    m_PRODUCT.DESCRIPTION_9 = collection.Get("DESCRIPTION_9").ToString();
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

                    if (Image_3 != null)
                    {
                        m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                        string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                        m_PRODUCT_IMAGES.IMAGE_4 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                        string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                        Image.SaveAs(_path);
                        db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                    }

                    if (Image_4 != null)
                    {
                        m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                        string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                        m_PRODUCT_IMAGES.IMAGE_5 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
                        string _path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName));
                        Image.SaveAs(_path);
                        db.PRODUCT_IMAGES.Add(m_PRODUCT_IMAGES);
                    }

                    if (Image_5 != null)
                    {
                        m_PRODUCT_IMAGES.PRODUCT_ID = m_PRODUCT.PRODUCT_ID;
                        string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                        m_PRODUCT_IMAGES.IMAGE_6 = "~/Images/PRODUCTS/" + _fileNameRandom + Image.FileName;
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
                }
                else
                {
                    TempData["ErrorCreate"] = "Mã sản phẩm đã tồn tại.";
                    return RedirectToAction("Create", "ManagingProducts", FormMethod.Get);
                }

                TempData["SuccessCreate"] = "Thêm thành công sản phẩm mã: " + m_PRODUCT.PRODUCT_CODE + ".";
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
                return View(db.PRODUCTS.Find(id));
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
