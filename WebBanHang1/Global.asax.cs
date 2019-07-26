using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Controllers;
using System.Web.Optimization;
using System.Web.Routing;
using WebBanHang1.Models;

namespace WebBanHang1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        void Session_Start(object sender, EventArgs e)
        {
            Session["gio"] = new Gio();
            
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();
        //    HttpException httpex = ex as HttpException;
        //    RouteData rdata = new RouteData();
        //    rdata.Values.Add("controller", "Trangchu");
        //    if (httpex == null)
        //    {
        //        rdata.Values.Add("action", "Index");
        //    }
        //    else
        //    {
        //        rdata.Values.Add("action", "Index");

        //    }
        //    Server.ClearError();
        //    Response.TrySkipIisCustomErrors = true;
        //    IController errorController = new ErrorController();
        //}
    }
}
