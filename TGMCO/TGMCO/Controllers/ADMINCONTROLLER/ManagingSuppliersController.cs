using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingSuppliersController : Controller
    {
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        private SUPPLIER m_objSUPPLIER = new SUPPLIER();
        private StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(20);
        //
        // POST: /ManagingSuppliers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection _frmCollect, HttpPostedFileBase fileUpload)
        {
            try
            {
                string _SupplierCode = _frmCollect.Get("SupplierCode").ToString();
                if (ModelState.IsValid)
                {
                    if(db.SUPPLIERS.Where(n => n.SUPPLIER_CODE == _SupplierCode).Count() == 0)
                    {
                        //Binding
                        m_objSUPPLIER.SUPPLIER_CODE = _frmCollect.Get("SupplierCode").ToString();
                        m_objSUPPLIER.SUPPLIER_NAME = _frmCollect.Get("SupplierName").ToString();
                        m_objSUPPLIER.DESCRIPTION = _frmCollect.Get("Description").ToString();
                        if (!string.IsNullOrEmpty(m_objSUPPLIER.SUPPLIER_CODE))
                        {
                            //Upload File
                            if (fileUpload != null)
                            {
                                string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                                m_objSUPPLIER.IMAGE = "~/Images/ADMIN/SUPPLIERS/" + _fileNameRandom + fileUpload.FileName;
                                string _path = Path.Combine(Server.MapPath("~/Images/ADMIN/SUPPLIERS/" + _fileNameRandom + fileUpload.FileName));
                                fileUpload.SaveAs(_path);
                            }
                            db.SUPPLIERS.Add(m_objSUPPLIER);
                            db.SaveChanges();
                            TempData["VB_SuccessCreate"] = "Thêm thành công nhà cung cấp: " + m_objSUPPLIER.SUPPLIER_NAME + ".";
                        }
                        else
                        {
                            TempData["VB_ErrorCreate"] = "Chưa nhập mã nhà cung cấp.";
                        }
                 
                    }
                    else
                    {
                        TempData["VB_ErrorCreate"] = "Mã nhà cung cấp đã tồn tại.";
                    }
                }
                else
                {
                    TempData["VB_ErrorCreate"] = "Thêm không thành công, kiểm tra lại dữ liệu.";
                }

                return RedirectToAction("ManagingSuppliers", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult Update(Int32 id, HttpPostedFileBase fileUpload, FormCollection f)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    m_objSUPPLIER = db.SUPPLIERS.Find(id);
                   
                    //Upload File
                    if (fileUpload != null)
                    {
                        string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                        m_objSUPPLIER.IMAGE = "~/Images/ADMIN/SUPPLIERS/" + _fileNameRandom + fileUpload.FileName;
                        string _path = Path.Combine(Server.MapPath("~/Images/ADMIN/SUPPLIERS/" + _fileNameRandom + fileUpload.FileName));
                        fileUpload.SaveAs(_path);
                    }
                    m_objSUPPLIER.IDX = int.Parse(f.Get("IDX").ToString());
                    db.SaveChanges();
                }
                else
                {
                    TempData["VB_ErrorCreate"] = "Thêm không thành công, kiểm tra lại dữ liệu.";
                }

                return RedirectToAction("ManagingSuppliers", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        //
        // GET: /ManagingSuppliers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SUPPLIER supplier = db.SUPPLIERS.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /ManagingSuppliers/Edit/5

        
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var _Supplier = db.SUPPLIERS.Find(id);
                db.SUPPLIERS.Remove(_Supplier);
                db.SaveChanges();
                return RedirectToAction("ManagingSuppliers", "Admin");
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