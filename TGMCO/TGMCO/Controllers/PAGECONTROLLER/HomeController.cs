using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        
        public ActionResult Index()
        {
            try
            {
                Session["SUPPLIER"] = "STANLEY";
                SUPPLIER _Supplier = db.SUPPLIERS.First();
                Session["SUPPLIER_MODEL"] = _Supplier;
                ViewBag.Suppliers = db.SUPPLIERS.OrderBy(n => n.IDX).ToList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }          
        }
        public ActionResult About()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult History()
        {   
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult Business()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult OrganizationalChart()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult GuideLogin()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult GuideShoping()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult GuideResetPassword()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult PrivacyPolicy()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult ShippingPolicy()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult OrderProcessing()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult ReturnPolicy()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        public ActionResult ContactUs()
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult ContactUs(FormCollection f)
        {
            try
            {
                var mailMessage = new MailMessage();

                mailMessage.To.Add("info@tgmco.com.vn");
                mailMessage.To.Add("lehoanhao.94@gmail.com");
                mailMessage.Subject = "Thư liên hệ từ: " + f.Get("txtEmail").ToString() + " - " + f.Get("txtTitle").ToString();
                mailMessage.Body = f.Get("txtMessage").ToString();

                var smtpClient = new SmtpClient { EnableSsl = false };
                smtpClient.Send(mailMessage);

                TempData["Success"] = "Bạn đã gửi thông tin liên hệ thành công.";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ViewResult makita()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("MAKITA")).Single();
                Session["SUPPLIER"] = "MAKITA";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }

        public ViewResult bosch()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("BOSCH")).Single();
                Session["SUPPLIER"] = "BOSCH";
                Session["SUPPLIER_MODEL"] = _Supplier;
                TGMCO.Models.Entity.CategoryModel _CATEGORY_MODEL = new Models.Entity.CategoryModel();
                List<NEWS> _lstNEWS = db.NEWS.Where(n => n.SUPPLIER_ID == 23 && n.IS_PROMOTION == true).ToList();
                if (_lstNEWS.Count > 8)
                    _lstNEWS = _lstNEWS.Take(8).ToList();
                List<PRODUCT> _lstMASTER = db.PRODUCTS.Where(n => n.IS_ACTIVE && n.IS_NEW && n.SUPPLIER_ID == _Supplier.SUPPLIER_ID).OrderByDescending(n => n.IDX).ToList();
                List<PRODUCT> _lstPRODUCT = new List<PRODUCT>();
                List<PRODUCT> _lstACCESSORY = new List<PRODUCT>();
                foreach(var product in _lstMASTER)
                {
                    if(!_CATEGORY_MODEL.IsAccessory(product.CATEGORY_ID))
                    {
                        if(_lstPRODUCT.Count < 8)
                        {
                            _lstPRODUCT.Add(product);
                        }
                    }
                    else
                    {
                        if (_lstACCESSORY.Count < 8)
                        {
                            _lstACCESSORY.Add(product);
                        }
                    }

                    if(_lstPRODUCT.Count == 8 &&_lstACCESSORY.Count == 8)
                    {
                        ViewBag.lstNEW_ACCESSORY = _lstACCESSORY;
                        ViewBag.lstNEW_PRODUCT = _lstPRODUCT;
                        ViewBag.lstNEWS = _lstNEWS;
                        return View(_Supplier);
                    }
                }
                return View(_Supplier);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }

        public ViewResult stanleyh()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("STANLEY HAND")).Single();
                Session["SUPPLIER"] = "STANLEY";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }

        public ViewResult stanleyp()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("STANLEY POWER")).Single();
                Session["SUPPLIER"] = "STANLEY";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }

        public ViewResult dewalt()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("DEWALT")).Single();
                Session["SUPPLIER"] = "DEWALT";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }

        public ViewResult blackanddecker()
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("BLACK")).Single();
                Session["SUPPLIER"] = "BLACKANDDECKER";
                Session["SUPPLIER_MODEL"] = _Supplier;
                return View(_Supplier);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                throw ex; // 404
            }
        }
        [HttpGet]
        public int GetNumItemsCart()
        {
            int i = DateTime.Now.Second;
            return i;
        }
    }
}
