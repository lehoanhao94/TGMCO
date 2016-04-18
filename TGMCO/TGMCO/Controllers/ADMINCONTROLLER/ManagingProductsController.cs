using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingProductsController : Controller
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
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(20);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            try
            {
                ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.ToList(), "Supplier_ID", "Supplier_Name");
                ViewBag.ListCategory = new SelectList(db.CATEGORIES.ToList(), "Category_ID", "Category_Name");

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        //
        // POST: /ManagingProducts/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase Image, HttpPostedFileBase Image_1,
            HttpPostedFileBase Image_2, HttpPostedFileBase Image_3, HttpPostedFileBase Image_4, HttpPostedFileBase Image_5,
            HttpPostedFileBase fileUpload_1, HttpPostedFileBase fileUpload_2, HttpPostedFileBase fileUpload_3)
        {
            try
            {
                m_PRODUCT.PRODUCT_CODE = collection.Get("PRODUCT_CODE").ToString();
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

                db.PRODUCTS.Add(m_PRODUCT);
                db.SaveChanges();
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

                db.SaveChanges();


                return RedirectToAction("ManagingProducts", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        //
        // GET: /ManagingProducts/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ManagingProducts/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ManagingProducts/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ManagingProducts/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
