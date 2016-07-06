using System;
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
                        var fileName = Path.GetFileName(file) + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/Images/PRODUCTS/" + _fileNameRandom + fileName));
                        fileContent.SaveAs(path);
                        IMAGES_UPLOAD _IMAGE_UPLOAD = new IMAGES_UPLOAD();
                        _IMAGE_UPLOAD.NEWS_ID = 0;
                        _IMAGE_UPLOAD.NAME = _fileNameRandom + fileName;
                        _IMAGE_UPLOAD.URL = path;
                        db.IMAGES_UPLOAD.Add(_IMAGE_UPLOAD);
                        db.SaveChanges();

                        return path;
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
    }
}
