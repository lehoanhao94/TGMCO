﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingNewsController : Controller
    {
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(15);

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection f)
        {
            try
            {
                string Content1 = f.Get("Content").ToString();                            
                string Title = f.Get("Title").ToString();
                string Description = f.Get("Description").ToString();
                int Supplier_id = int.Parse(f.Get("ListSupplier").ToString());
                NEWS _NEWS = new NEWS();
                _NEWS.TITLE = Title;
                _NEWS.CONTENT_1 = Content1;
                _NEWS.CONTENT_2 = Description;
                _NEWS.CREATED_DATE = DateTime.Now;
                _NEWS.SUPPLIER_ID = Supplier_id;
                _NEWS.IS_PROMOTION = false;
                db.NEWS.Add(_NEWS);
                db.SaveChanges();

                List<IMAGES_UPLOAD> _lstIMAGE_UPLOAD = db.IMAGES_UPLOAD.Where(n => n.NEWS_ID == 0).ToList();
                foreach(var image in _lstIMAGE_UPLOAD)
                {
                    image.NEWS_ID = _NEWS.NEWS_ID;
                }
                db.SaveChanges();
                TempData["SuccessCreate"] = "Thêm thành công bài viết số " + _NEWS.NEWS_ID;
                return RedirectToAction("ManagingNews", "Admin");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }            
        }

        public ActionResult Create_Promotion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create_Promotion(FormCollection f)
        {
            try
            {
                string Content1 = f.Get("Content").ToString();
                string Title = f.Get("Title").ToString();
                string Description = f.Get("Description").ToString();
                int Supplier_id = int.Parse(f.Get("ListSupplier").ToString());
                NEWS _NEWS = new NEWS();
                _NEWS.TITLE = Title;
                _NEWS.CONTENT_1 = Content1;
                _NEWS.CONTENT_2 = Description;
                _NEWS.CREATED_DATE = DateTime.Now;
                _NEWS.SUPPLIER_ID = Supplier_id;
                _NEWS.IS_PROMOTION = true;
                db.NEWS.Add(_NEWS);
                db.SaveChanges();

                List<IMAGES_UPLOAD> _lstIMAGE_UPLOAD = db.IMAGES_UPLOAD.Where(n => n.NEWS_ID == 0).ToList();
                foreach (var image in _lstIMAGE_UPLOAD)
                {
                    image.NEWS_ID = _NEWS.NEWS_ID;
                }
                db.SaveChanges();
                TempData["SuccessCreate"] = "Thêm thành công bài viết số " + _NEWS.NEWS_ID;
                return RedirectToAction("ManagingNews", "Admin");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public string UploadImage(string id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = _fileNameRandom + Path.GetFileName(file) + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/Images/NEWS/" + fileName));
                        string URL = "http://www.vietnamtool.vn/Images/NEWS/" + fileName;
                        fileContent.SaveAs(path);
                        IMAGES_UPLOAD _IMAGE_UPLOAD = new IMAGES_UPLOAD();
                        _IMAGE_UPLOAD.NEWS_ID = 0;
                        _IMAGE_UPLOAD.NAME = _fileNameRandom + fileName;
                        _IMAGE_UPLOAD.URL = URL;
                        db.IMAGES_UPLOAD.Add(_IMAGE_UPLOAD);
                        db.SaveChanges();

                        return URL;
                    }                  
                }
                return string.Empty; 
            }
            catch (Exception ex)
            {
                return string.Empty; 
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var _News = db.NEWS.Find(id);
                db.NEWS.Remove(_News);
                db.SaveChanges();
                return RedirectToAction("ManagingNews", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult Update(int id)
        {
            try
            {
                NEWS _NEWS = db.NEWS.Find(id);
                List<IMAGES_UPLOAD> _lstIMAGES = db.IMAGES_UPLOAD.Where(n => n.NEWS_ID == id).OrderByDescending(n => n.IMAGES_ID).ToList();
                ViewBag.ListImages = _lstIMAGES;
                return View(_NEWS);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(int id, FormCollection f)
        {
            try
            {
                NEWS _NEWS = db.NEWS.Find(id);
                string Content1 = f.Get("Content").ToString();
                string Title = f.Get("Title").ToString();
                string Description = f.Get("Description").ToString();
                int Supplier_id = int.Parse(f.Get("ListSupplier").ToString());
                _NEWS.TITLE = Title;
                _NEWS.CONTENT_1 = Content1;
                _NEWS.CONTENT_2 = Description;
                _NEWS.CREATED_DATE = DateTime.Now;
                _NEWS.SUPPLIER_ID = Supplier_id;
                db.SaveChanges();

                List<IMAGES_UPLOAD> _lstIMAGE_UPLOAD = db.IMAGES_UPLOAD.Where(n => n.NEWS_ID == id || n.NEWS_ID == 0).ToList();
                foreach (var image in _lstIMAGE_UPLOAD)
                {
                    image.NEWS_ID = _NEWS.NEWS_ID;
                }
                db.SaveChanges();
                TempData["SuccessCreate"] = "Cập nhật thành công bài viết số " + _NEWS.NEWS_ID;
                if(_NEWS.IS_PROMOTION == true)
                    return RedirectToAction("ManagingNewsPromotion", "Admin");
                return RedirectToAction("ManagingNews", "Admin");                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
