using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class PhanquyensController : Controller
    {
        WebBanHang1.Models.BH db = new BH();
        // GET: Admin/Phanquyens
        public ActionResult Index()
        {
            Khoitao();
            var model = db.ListQuyen();
            return View(model);
        }
        [HttpPost]
        public JsonResult Phanquyen(Quyens quyen)
        {
            var st = db.SuaQuyen(quyen);
           
            return Json(new { status = st });
        }
       
        public void Khoitao()
        {
            string duongdan = @"~/Areas/Admin/Controllers";
            duongdan = Server.MapPath(duongdan);
            DirectoryInfo dr = new DirectoryInfo(duongdan);
            Employees e = new Employees() ;
            if (Session["employee"]!=null)
            {
                e = Session["employee"] as Employees;
            }
            else
            {
                JavaScript(" window.location.href = '/Admin/Trangchu';");
            }
            foreach (var file in dr.GetFiles())
            {
                    var q = new TrangWebs() { TenTW=file.Name};
                    db.ThemTW(q);
            }
            foreach (var item in db.ListVT())
            {
                foreach (var item2 in db.ListTW())
                {
                    var t = new Quyens() { TenVT=item.TenVT,TenTW=item2.TenTW};
                    db.ThemQuyen(t);
                }
            }
        }
    }
}