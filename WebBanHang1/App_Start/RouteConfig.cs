using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHang1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");//bỏ qua gọi trực tiếp file source, có thể thêm nếu muốn bỏ qua

            routes.IgnoreRoute("{*botdetect}",
     new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Khuyenmai",
                url: "Khuyenmai/{id}",
                defaults: new { controller = "Trangchu", action = "Tintuc", id = UrlParameter.Optional },
    namespaces: new[] { "WebBanHang1.Controllers" }

            );
        //    routes.MapRoute(
        //    name: "Trangchu",
        //    url: "Trangchu/{id}",
        //    defaults: new { controller = "Trangchu", action = "Index", id = UrlParameter.Optional }
        //);
            routes.MapRoute(
              name: "gioithieu",
              url: "GioiThieu/{id}",
              defaults: new { controller = "Trangchu", action = "Gioithieu", id = UrlParameter.Optional },
    namespaces:new[] { "WebBanHang1.Controllers" }

          );
            routes.MapRoute(
              name: "Lienlac",
              url: "Lienlac/{id}",
              defaults: new { controller = "Trangchu", action = "Lienlac", id = UrlParameter.Optional },
    namespaces: new[] { "WebBanHang1.Controllers" }

          );
            routes.MapRoute(
              name: "Loai",
              url: "Loai/{ten}-{id}",
              defaults: new { controller = "Sanpham", action = "Loai", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Chungloai",
              url: "Chungloai/{ten}-{id}",
              defaults: new { controller = "Sanpham", action = "Chungloai", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Sanphamchitiet",
              url: "Sanpham/{ten}-{id}",
              defaults: new { controller = "Sanpham", action = "Chitiet", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "danhsachsanphakm",
             url: "Danhsachsanpham/{id}",
             defaults: new { controller = "Sanpham", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
    "Default",
    "{controller}/{action}/{id}",
    new { controller = "Trangchu", action = "Index", id = UrlParameter.Optional },
    new[] { "WebBanHang1.Controllers" }
);
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Trangchu", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
