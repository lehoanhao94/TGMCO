using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class UserController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        [HttpPost]
        public PartialViewResult Login(string UserName, string Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _User = db.USERS.Where(user => user.USER_NAME.Equals(UserName)).FirstOrDefault();
                    if (_User != null)
                    {
                        if (Password.Equals(StringCipher.Decrypt(_User.PASSWORD, _User.USER_NAME)) && (_User.IS_ACTIVE == true))
                        {
                            Session["SS_USER"] = _User;
                            Session["SS_USER_PROFILE"] = db.USER_PROFILES.Where(user => user.USER_ID.Equals(_User.USER_ID)).FirstOrDefault();
                            return PartialView();                       
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public int Register(string UserName, string Email, string Password)
        {
            try
            {
                USER _USER = db.USERS.Where(n => n.USER_NAME == UserName).SingleOrDefault();
                USER_PROFILES _USER_PROFILES = db.USER_PROFILES.Where(n => n.EMAIL == Email).SingleOrDefault();

                if (_USER != null)
                    return 1;
                else if(_USER_PROFILES != null)
                {
                    return 2;
                }
                _USER = new USER();
                _USER_PROFILES = new USER_PROFILES();
                _USER.USER_NAME = UserName;
                _USER.PASSWORD = StringCipher.Encrypt(Password, UserName);
                _USER.IS_ACTIVE = true;
                _USER.IS_ADMIN = _USER.IS_EMPLOYEE = false;
                _USER.CREATED_DATE = DateTime.Now;               
                db.USERS.Add(_USER);
                db.SaveChanges();

                _USER_PROFILES.FULL_NAME = UserName;
                _USER_PROFILES.EMAIL = Email;
                _USER_PROFILES.USER_ID = _USER.USER_ID;
                _USER_PROFILES.POINTS = 0;
                _USER_PROFILES.ADDRESS = "";
                _USER_PROFILES.MOBILE = "";
                _USER_PROFILES.AVATAR = "~/Content/Page_Layout/img/default-avatar.jpg";

                db.USER_PROFILES.Add(_USER_PROFILES);
                db.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult Profile(int id)
        {
            try
            {
                var UserProfiles = from u in db.USERS
                                   join upr in db.USER_PROFILES on u.USER_ID equals upr.USER_ID
                                   where u.USER_ID == id
                                   select new UserProfilesModel { USER = u, USER_PROFILES = upr };
                ViewBag.ListOrder = db.ORDERS.Where(n => n.USER_ID == id).OrderByDescending(n => n.ORDER_STATUS_ID).ToList();
                ViewBag.ListShipper = new SelectList(db.SHIPPERS.OrderByDescending(n => n.SHIPPER_ID).ToList(), "Shipper_ID", "SHIPPER_NAME");
                return View(UserProfiles.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

    }
}
