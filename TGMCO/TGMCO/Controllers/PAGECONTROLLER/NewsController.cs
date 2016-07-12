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
                List<NEWS> _lstNEWS = db.NEWS.Where(n => n.IS_PROMOTION == false).OrderByDescending(n => n.NEWS_ID).ToList();
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

        public ActionResult News_Promotion(int? page)
        {
            try
            {
                //Số bài trên trang
                int pageSize = 5;
                //Số trang
                int pageNumber = (page ?? 1);
                Session["SUPPLIER"] = "DEFAULT";
                List<NEWS> _lstNEWS = db.NEWS.Where(n => n.IS_PROMOTION == true).OrderByDescending(n => n.NEWS_ID).ToList();
                return View(_lstNEWS.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult News_Bosch(int? page)
        {
            try
            {
                //Số bài trên trang
                int pageSize = 5;
                //Số trang
                int pageNumber = (page ?? 1);
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("BOSCH")).Single();
                Session["SUPPLIER"] = "BOSCH";
                Session["SUPPLIER_MODEL"] = _Supplier;
                List<NEWS> _lstNEWS = db.NEWS.Where(n => n.IS_PROMOTION == false && n.SUPPLIER_ID == 23).OrderByDescending(n => n.NEWS_ID).ToList();
                return View(_lstNEWS.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult News_Bosch_Promotion(int? page)
        {
            try
            {
                //Số bài trên trang
                int pageSize = 5;
                //Số trang
                int pageNumber = (page ?? 1);
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("BOSCH")).Single();
                Session["SUPPLIER"] = "BOSCH";
                Session["SUPPLIER_MODEL"] = _Supplier;
                List<NEWS> _lstNEWS = db.NEWS.Where(n => n.IS_PROMOTION == true && n.SUPPLIER_ID == 23).OrderByDescending(n => n.NEWS_ID).ToList();
                return View(_lstNEWS.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public ActionResult News_Bosch_Detail(int id)
        {
            try
            {
                SUPPLIER _Supplier = db.SUPPLIERS.Where(n => n.SUPPLIER_NAME.Contains("BOSCH")).Single();
                Session["SUPPLIER"] = "BOSCH";
                Session["SUPPLIER_MODEL"] = _Supplier;
                NEWS _NEWS = db.NEWS.Find(id);
                return View(_NEWS);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
