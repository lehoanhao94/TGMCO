using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingAgenciesController : Controller
    {
        #region VARIABLES


        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        private AGENCy m_objAgencies = new AGENCy();
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(20);


        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection _frmCollect, HttpPostedFileBase fileUpload)
        {
            try
            {
                string _AgenciesCode = _frmCollect.Get("AgenciesCode").ToString();
                if (ModelState.IsValid)
                {
                    if (db.AGENCIES.Where(n => n.REGION == _AgenciesCode).Count() == 0)
                    {
                        //Binding
                        m_objAgencies.REGION = _frmCollect.Get("Region").ToString();
                        m_objAgencies.ADDRESS = _frmCollect.Get("Address").ToString();
                        m_objAgencies.PHONE = _frmCollect.Get("Phone").ToString();
                        m_objAgencies.FAX = _frmCollect.Get("Fax").ToString();
                        m_objAgencies.URL = _frmCollect.Get("Url").ToString();
                        if (!string.IsNullOrEmpty(m_objAgencies.REGION))
                        {
                            //Upload File
                            if (fileUpload != null)
                            {
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_objAgencies.IMAGE = "~/Images/ADMIN/AGENCIES/" + _fileNameRandom + fileUpload.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/ADMIN/AGENCIES/" + _fileNameRandom + fileUpload.FileName));
                                fileUpload.SaveAs(_path);
                            }
                            db.AGENCIES.Add(m_objAgencies);
                            db.SaveChanges();
                            TempData["VB_SuccessCreate"] = "Thêm thành công chi nhánh: " + m_objAgencies.REGION + ".";
                        }
                        else
                        {
                            TempData["VB_ErrorCreate"] = "Chưa nhập tên chi nhánh.";
                        }

                    }
                    else
                    {
                        TempData["VB_ErrorCreate"] = "Trùng tên chi nhánh.";
                    }
                }
                else
                {
                    TempData["VB_ErrorCreate"] = "Thêm không thành công, kiểm tra lại dữ liệu.";
                }

                return RedirectToAction("ManagingAgencies", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ManagingAgencies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AGENCy agencies = db.AGENCIES.Find(id);
            if (agencies == null)
            {
                return HttpNotFound();
            }
            return View(agencies);
        }

        //
        // POST: /ManagingAgencies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AGENCy agencies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agencies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agencies);
        }

        //
        // GET: /ManagingAgencies/Delete/5

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var _Agencies = db.AGENCIES.Find(id);
                db.AGENCIES.Remove(_Agencies);
                db.SaveChanges();
                return RedirectToAction("ManagingAgencies", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
