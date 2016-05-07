using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;
using System.Security.Cryptography;
using PagedList;
using PagedList.Mvc;
using TGMCO.Controllers.ADMINCONTROLLER;

namespace TGMCO.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

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
                    var _User = db.USERS.Where(user => user.USER_NAME.Equals(UserName)).FirstOrDefault();
                    if (_User != null)
                    {
                        if(Password.Equals(StringCipher.Decrypt(_User.PASSWORD, _User.USER_NAME)) && (_User.IS_ADMIN == true) && (_User.IS_ACTIVE == true))
                        {
                            Session["SS_USER"] = _User;
                            Session["SS_USER_PROFILE"] = db.USER_PROFILES.Where(user => user.USER_ID.Equals(_User.USER_ID)).FirstOrDefault();
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
                throw ex;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagingSuppliers()
        {
            try
            {
                //aaaaacccccccccccccccc
                if(Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    List<SUPPLIER> _lstSUPPLIER = db.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList();
                    return View(_lstSUPPLIER);
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
        /// <returns></returns>
        public ActionResult ManagingCategories()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    List<CATEGORy> _lstCATEGORY = db.CATEGORIES.OrderByDescending(n => n.CATEGORY_ID).ToList();
                    return View(_lstCATEGORY);
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
        /// <returns></returns>
        public ActionResult ManagingCategoriesExtra()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    ViewBag.ListCategory = new SelectList(db.CATEGORIES.Where(n => n.IS_ACTIVE == true).ToList(), "Category_ID", "Category_Name");
                    List<CATEGORIES_EXTRA> _lstCATEGORY_EXTRA = db.CATEGORIES_EXTRA.OrderByDescending(n => n.CATEGORY_ID).ToList();
                    return View(_lstCATEGORY_EXTRA);
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
        /// <returns></returns>
        public ActionResult ManagingCategoriesBySupplier()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    List<SUPPLIER> _lstSUPPLIER = db.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList();
                    return View(_lstSUPPLIER);
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
        /// <returns></returns>
        public ActionResult ManagingProducts(int? page)
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    List<PRODUCT> _lstPRODUCT = db.PRODUCTS.OrderByDescending(n => n.SUPPLIER_ID).OrderByDescending(n => n.IS_ACTIVE).OrderByDescending(n => n.IDX).ToList();
                    return View(_lstPRODUCT);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult ManagingUsers()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    var lstUserProfiles = from u in db.USERS
                                join upr in db.USER_PROFILES on u.USER_ID equals upr.USER_ID
                                orderby upr.POINTS descending
                                select new UserProfilesModel {USER = u, USER_PROFILES = upr };

                    return View(lstUserProfiles.ToList());
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
        /// <returns></returns>
        public ActionResult ManagingOrders()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult ManagingAgencies()
        {
            try
            {
                if (Session["SS_USER"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
