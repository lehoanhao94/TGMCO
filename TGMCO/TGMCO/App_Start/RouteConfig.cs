using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TGMCO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "ShoppingCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "chi-tiet/san-pham-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category",
                url: "{supplier_code}/danh-muc/{supplier_id}-{cate_id}",
                defaults: new { controller = "Category", action = "Category" }
            );

            routes.MapRoute(
                name: "Supplier",
                url: "makita",
                defaults: new { controller = "Home", action = "makita" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}