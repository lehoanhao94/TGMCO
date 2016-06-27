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
                url: "{supplier}/{product}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Search",
                url: "ket-qua-tim-kiem",
                defaults: new { controller = "Product", action = "Search"}
            );
            routes.MapRoute(
                name: "AdvanceSearch",
                url: "ket-qua-tim-kiem-nang-cao",
                defaults: new { controller = "Product", action = "AdvanceSearch"}
            );
            routes.MapRoute(
                name: "Category",
                url: "{supplier_code}/danh-muc/{supplier_id}-{cate_id}",
                defaults: new { controller = "Category", action = "Category" }
            );
            routes.MapRoute(
                name: "Order",
                url: "thong-tin-dat-hang",
                defaults: new { controller = "Order", action = "Order" }
            );
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Payment", action = "Payment" }
            );
            routes.MapRoute(
                name: "Success",
                url: "xac-nhan-HD-{id}",
                defaults: new { controller = "Order", action = "PrintBill" }
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