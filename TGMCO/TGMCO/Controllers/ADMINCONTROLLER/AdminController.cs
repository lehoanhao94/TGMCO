using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using System.Security.Cryptography;

namespace TGMCO.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        /// <summary>
        /// 
        /// </summary>
        TGMCOEntitiesDB m_DATA_TGMCO = new TGMCOEntitiesDB();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _User = m_DATA_TGMCO.USERS.Where(user => user.USER_NAME.Equals(UserName)).FirstOrDefault();
                    if (_User != null)
                    {
                        if(Password.Equals(StringCipher.Decrypt(_User.PASSWORD, _User.USER_NAME)))
                        {
                            Session["SS_USER"] = _User;
                            Session["SS_USER_PROFILE"] = m_DATA_TGMCO.USER_PROFILES.Where(user => user.USER_ID.Equals(_User.USER_ID));
                            return RedirectToAction("Index", "Admin");
                            
                        }
                        else
                        {
                            ViewBag.VB_ErrorLogin = "Lỗi: Mật khẩu không đúng.";
                        }
                    }
                    else
                    {
                        ViewBag.VB_ErrorLogin = "Lỗi: Người dùng không tồn tại.";
                    }
                }               
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Login", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult ManagingSuppliers()
        {
            try
            {
                if(Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    List<SUPPLIER> _lstSUPPLIER = m_DATA_TGMCO.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList();
                    return View(_lstSUPPLIER);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
