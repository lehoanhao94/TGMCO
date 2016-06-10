using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TGMCO.Controllers.ADMINCONTROLLER
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["SS_USER_ADMIN"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Admin", action = "Login" }));
            } 
            base.OnActionExecuting(filterContext);
        }
    }
}
