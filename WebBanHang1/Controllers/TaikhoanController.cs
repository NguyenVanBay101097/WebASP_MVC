using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang1.Controllers
{
    public class TaikhoanController : Controller
    {
        // GET: Taikhoan
        WebBanHang1.Models.BH db = new Models.BH();
        public ActionResult Index()
        {
            if (Session["customer"]==null)
            {
                TempData["chuadangnhap"] = "<script>swal('bạn chưa đăng nhập!');</script>";
                TempData["url"] = "~/Taikhoan";
                return RedirectToAction("Dangnhap","Trangchu");
            }
            var model = Session["customer"] as WebBanHang1.Models.Customers;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Capnhat(WebBanHang1.Models.Customers c,HttpPostedFileBase myImage)
        {
            if (myImage!=null)
            {
                if (myImage.ContentType.Contains("image"))
                {


                    myImage.SaveAs(Server.MapPath("~/Images/") + myImage.FileName);
                    c.Picture = myImage.FileName;
                }
            }
            Session["customer"] = db.CapnhatTK(c);
            return RedirectToAction("Index","Taikhoan");
            //vẫn đang lưu tempdata dăng nhập
        }
        public ActionResult Xoa()
        {
            WebBanHang1.Models.Customers c = Session["customer"] as WebBanHang1.Models.Customers;
            db.XoaTK(c);
            Session["customer"] = null;
            return RedirectToAction("Index", "Trangchu");
            //vẫn đang lưu tempdata dăng nhập
        }

    }
}