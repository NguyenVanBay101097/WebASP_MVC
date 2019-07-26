using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class TrangchuController : Controller
    {
        WebBanHang1.Models.BH db = new Models.BH();
        // GET: Admin/Trangchu
        //[RequireHttps]
        public ActionResult Index()
        {
            if (Session["employee"] == null)
            {
                return RedirectToAction("Login", "Trangchu");
            }
            ViewBag.XemOrder = db.DSHD().Where(x => x.Xem == false).Count();
            ViewBag.XemLienlac = db.DSLienlac().Where(x => x.Xem == false).Count();

            return View();
        }
        private class tam
        {
            public int name { get; set; }
            public double count { get; set; }
        }
        public ActionResult GetData()
        {
            var query = new WebBanHang1.Models.BanHang().Database.SqlQuery<tam>("select top(10)  month(OrderDate) [name],SUM(TotalMoney) [count] " +
"from Orders where DATEDIFF(MONTH, OrderDate, GETDATE()) <= 11 " +
"group by YEAR(OrderDate), MONTH(OrderDate) order by month(OrderDate) ").ToList();

            var b = query.Count;
            //  Order_Details.Include("Products").
            //GroupBy(p => p.Products.ProductName)
            //.Select(g => new { name = g.Key, count = g.FirstOrDefault().Quantity });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            
            if (Session["employee"] != null)
            {
                return RedirectToAction("Index", "Trangchu");
            }
          
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenDN, string MatKhau)
        {
            using (var db = new WebBanHang1.Models.BanHang())
            {
                var emp = db.Employees.SingleOrDefault(x => x.TenDN == TenDN);
                if (emp != null)
                {
                    emp = db.Employees.SingleOrDefault(x => x.TenDN == TenDN && x.MatKhau == MatKhau);
                    if (emp != null)
                    {
                        Session["employee"] = emp;
                        return RedirectToAction("Index", "/Trangchu");
                    }
                    else
                    {
                        TempData["err_matkhau"] = "<script>swal('sai mật khẩu!');</script>";
                    }
                }
                else
                {
                    TempData["err_tendangnhap"] = "<script>swal('sai tên đăng nhập!');</script>";
                }
            }

            return View();
        }
        public void Dangxuat()
        {
            Session["employee"] = null;

        }
        public ActionResult Thongtintaikhoan()
        {
            if (Session["employee"] == null)
            {
                return RedirectToAction("Index", "Trangchu");
            }
            var em = Session["employee"] as WebBanHang1.Models.Employees;
            return View(em);
        }
        [HttpPost]
        public ActionResult Thongtintaikhoan(WebBanHang1.Models.Employees e, HttpPostedFileBase myImage)
        {
            var a = e.gioitinh;
            var b = e.Picture;
            var c = e.TenVT;
            var d = e.EmployeeID;
            var dd = e.TenDN;
            var dfd = e.BirthDate;
            var dfds = e.Address;
            if (myImage != null)
            {
                if (myImage.ContentType.Contains("image"))
                {


                    myImage.SaveAs(Server.MapPath("~/Images/") + myImage.FileName);
                    e.Picture = myImage.FileName;
                }
            }
            Session["employee"] = db.CapnhatNhanvien(e);
            return View(Session["employee"]);
            //vẫn đang lưu tempdata dăng nhập
        }
    }
}