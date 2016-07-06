using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using PagedList;
using PagedList.Mvc;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class NewsController : Controller
    {
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult News(int? page)
        {
            try
            {
                //Số bài trên trang
                int pageSize = 5;
                //Số trang
                int pageNumber = (page ?? 1);
                Session["SUPPLIER"] = "DEFAULT";
                List<NEWS> _lstNEWS = db.NEWS.OrderByDescending(n => n.NEWS_ID).ToList();
                return View(_lstNEWS.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult News_Detail(int id)
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                NEWS _NEWS = db.NEWS.Find(id);
                return View(_NEWS);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult NewsPromotion()
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
    }
}
