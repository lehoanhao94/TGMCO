using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

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

    }
}
