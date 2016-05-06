using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;


namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class ManagingUsersController : BaseController
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        USER m_USER = new USER();
        USER_PROFILES m_USER_PROFILES = new USER_PROFILES();
        StringRandom m_STRING_RANDOM_IMAGE = new StringRandom(10);

        //
        // GET: /ManagingUsers/Create
        [HttpGet]
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

        //
        // POST: /ManagingUsers/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase AVATAR)
        {
            try
            {
                m_USER.USER_NAME = collection.Get("USER_NAME").ToString();
                if (db.USERS.Where(n => n.USER_NAME.Equals(m_USER.USER_NAME)).ToList().Count() == 0)
                {
                    m_USER_PROFILES.EMAIL = collection.Get("EMAIL").ToString();
                    if (db.USER_PROFILES.Where(n => n.EMAIL.Equals(m_USER_PROFILES.EMAIL)).ToList().Count() == 0)
                    {
                        m_USER.PASSWORD = StringCipher.Encrypt(collection.Get("PASSWORD").ToString().Trim(), m_USER.USER_NAME.Trim());
                        switch (collection.Get("r3").ToString())
                        {
                            case "user":
                                break;
                            case "employee":
                                m_USER.IS_EMPLOYEE = true;
                                break;
                            case "admin":
                                m_USER.IS_ADMIN = true;
                                break;
                        }
                        m_USER.CREATED_DATE = DateTime.Now;
                        m_USER.IS_ACTIVE = true;
                        db.USERS.Add(m_USER);
                        db.SaveChanges();

                        m_USER_PROFILES.USER_ID = m_USER.USER_ID;
                        m_USER_PROFILES.FULL_NAME = collection.Get("FULL_NAME").ToString();
                        m_USER_PROFILES.ADDRESS = collection.Get("ADDRESS").ToString();
                        m_USER_PROFILES.MOBILE = collection.Get("MOBILE").ToString();
                        m_USER_PROFILES.POINTS = 0;

                        if (AVATAR != null)
                        {
                            string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                            m_USER_PROFILES.AVATAR = "~/Images/AVATAR/" + _fileNameRandom + AVATAR.FileName;
                            string _path = Path.Combine(Server.MapPath("~/Images/AVATAR/" + _fileNameRandom + AVATAR.FileName));
                            AVATAR.SaveAs(_path);
                        }

                        db.USER_PROFILES.Add(m_USER_PROFILES);
                        db.SaveChanges();
                        TempData["SuccessCreate"] = "Tạo tài khoản thành công, Tên đăng nhập: " + m_USER.USER_NAME + ", Mật khẩu: " + collection.Get("PASSWORD").ToString();
                        return RedirectToAction("ManagingUsers", "Admin");
                    }
                    else
                    {
                        TempData["ErrorCreate"] = "Email đã tồn tại, vui lòng nhập email khác.";
                        return RedirectToAction("Create", "ManagingUsers", FormMethod.Get);
                    }
                }
                else
                {
                    TempData["ErrorCreate"] = "Tên đăng nhập đã tồn tại, vui lòng chọn tên đăng nhập khác";
                    return RedirectToAction("Create", "ManagingUsers", FormMethod.Get);
                }
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeStatusActive(int id)
        {
            try
            {
                var _User = db.USERS.Find(id);
                _User.IS_ACTIVE = !_User.IS_ACTIVE;
                db.SaveChanges();

                return RedirectToAction("ManagingUsers", "Admin");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var _User = db.USERS.Find(id);
                db.USERS.Remove(_User);
                db.SaveChanges();
                return RedirectToAction("ManagingUsers", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var UserProfiles = from u in db.USERS
                                   join upr in db.USER_PROFILES on u.USER_ID equals upr.USER_ID
                                   where u.USER_ID == id
                                   select new UserProfilesModel { USER = u, USER_PROFILES = upr };
                ViewBag.ListOrder = db.ORDERS.Where(n => n.USER_ID == id).OrderByDescending(n => n.ORDER_STATUS_ID).ToList();
                return View(UserProfiles.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult Update(int id, FormCollection collection, HttpPostedFileBase AVATAR)
        {
            try
            {
                m_USER = db.USERS.Find(id);

                switch (collection.Get("r3").ToString())
                {
                    case "user":
                        m_USER.IS_ADMIN = false;
                        m_USER.IS_EMPLOYEE = false;
                        break;
                    case "employee":
                        m_USER.IS_EMPLOYEE = true;
                        m_USER.IS_ADMIN = false;
                        break;
                    case "admin":
                        m_USER.IS_ADMIN = true;
                        m_USER.IS_EMPLOYEE = false;
                        break;
                }
                db.SaveChanges();

                m_USER_PROFILES = db.USER_PROFILES.Where(n => n.USER_ID == id).Single();
                m_USER_PROFILES.FULL_NAME = collection.Get("FULL_NAME").ToString();
                m_USER_PROFILES.ADDRESS = collection.Get("ADDRESS").ToString();
                m_USER_PROFILES.MOBILE = collection.Get("MOBILE").ToString();

                if (AVATAR != null)
                {
                    string _fileNameRandom = m_STRING_RANDOM_IMAGE.RandomString();
                    m_USER_PROFILES.AVATAR = "~/Images/AVATAR/" + _fileNameRandom + AVATAR.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Images/AVATAR/" + _fileNameRandom + AVATAR.FileName));
                    AVATAR.SaveAs(_path);
                }
                db.SaveChanges();
                TempData["SuccessCreate"] = "Cập nhật thông tin tài khoản thành công.";
                return RedirectToAction("Update", "ManagingUsers", new {id = id });
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
