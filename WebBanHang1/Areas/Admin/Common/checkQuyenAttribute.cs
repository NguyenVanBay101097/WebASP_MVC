using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Common
{
    public class checkQuyenAttribute:AuthorizeAttribute
        
    {
        
    
        public bool  Xem { get; set; }
        public bool  Xoa { get; set; }
        public bool  Sua { get; set; }
        public bool  Them { get; set; }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
           var db = new BH();
            
            Employees e = new Employees();
            if (httpContext.Session["employee"]!=null)
            {
               e = httpContext.Session["employee"] as Employees;
            }
            else
            {
                httpContext.Response.Redirect("/admin/Trangchu/Login");
                return false;
            }
            var TenVT = e.TenVT;
            var a = HttpContext.Current.Request.RequestContext.RouteData.Values;
            var TenTW = (string)a["controller"]+"Controller.cs";//chỗ này là sao ấy nhỉ m mô tả tí được ko
            var q = db.TimQuyen(TenVT, TenTW);
            // t debug m coi nghen ok  ? tu dung toi day t tu coi chu m mo chi. m ko cân
            var dsaf = q.Sua;
            if (TenVT=="admin")
            {
                return true;
            }
            if (Xem != null && q.Xem)
            {
                return true;
            }
            else
            {
                if (Them != null && q.Them == Them)
                {
                    return true;
                }
                else
                {
                    if (Sua != null && q.Sua == Sua)
                    {
                        return true;
                    }
                    else
                    {
                        if (Xoa != null && q.Xoa == Xoa)
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }
    }
}