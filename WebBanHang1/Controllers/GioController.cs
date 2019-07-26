using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang1.Models;
namespace WebBanHang1.Controllers
{
    public class GioController : Controller
    {
        // GET: Gio
        BH db = new BH();
       

        public ActionResult Index()
        {

            Gio tranggio = (Gio)Session["gio"];
            if (tranggio.DSSP.Count==0)
            {
                TempData["giotrong"] = "<script>swal('chưa có sản phẩm trong giỏ!');</script>";

                return RedirectToAction("","Sanpham");
            }
           


            return View(tranggio.DSSP);
        }
        public  ActionResult ThanhToan()
        {
            Gio tranggio = Session["gio"] as Gio;
            if (tranggio.DSSP.Count==0)
            {
                return RedirectToAction("", "Trangchu");
            }
            if (Session["customer"]==null)
            {
                TempData["chuadangnhap"] = "<script>swal('bạn chưa đăng nhập!');</script>";
                TempData["url"] = "~/Gio/Thanhtoan";
                return RedirectToAction("Dangnhap","Trangchu");
            }
            ViewBag.customer = Session["customer"];
            if (tranggio.DSSP.Count==0)
            {
                return RedirectToAction("","Sanpham");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ThanhToan(Orders o)
        {
            if (ModelState.IsValid)
            {
                o.CustomerID = (Session["customer"] as Customers).CustomerID;
                o.OrderDate = DateTime.Now;
                var gio = Session["gio"] as Gio;
                o.TotalMoney = gio.TongTien;
               
                db.ThemOrder(o);
                foreach (var item in gio.DSSP)
                {

                    Order_Details or = new Order_Details()
                    {
                        OrderID = o.OrderID,
                        ProductID=item.ProductID,
                        ProductName=item.ProductName,
                        PricePresent=item.PricePresent,
                        Quantity=item.SoLuong,
                        TotalMoney=item.ThanhTien
                    };
                    db.ThemOrderdetails(or);
                }

            }
            TempData["thanhcong"] = "<script>swal('mua hàng thành công, đang chờ xác nhận!');</script>";
            var tranggio = Session["gio"] as Gio;
            tranggio.Xoa();
            return RedirectToAction("","Trangchu");
        }
        public JsonResult DeleteAll()
        {
          
            var tranggio = Session["gio"] as Gio;
            tranggio.Xoa();
            if (tranggio.DSSP.Count == 0)
            {
                TempData["giotrong"] = "<script>swal('chưa có sản phẩm trong giỏ!');</script>";


            }

            return Json(new { status = true });
        }
        [HttpPost]
        public JsonResult Delete(string cartModel)
        {
            
            Gio tranggio = (Gio)Session["gio"];

            tranggio.Xoa(cartModel);
            Session["gio"] = tranggio;
            if (tranggio.DSSP.Count == 0)
            {
                TempData["giotrong"] = "<script>swal('chưa có sản phẩm trong giỏ!');</script>";


            }
            return Json(new { status = true, tongtien = tranggio.TongTien });
        }
        [HttpPost]
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<Products>>(cartModel);
            Gio tranggio = (Gio)Session["gio"];
           
            foreach (var item in tranggio.DSSP)
            {
                
                var jsonItem = jsonCart.LastOrDefault(x=>x.ProductID==item.ProductID);
                if (jsonItem!=null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }
            
            return Json(new { status = true,tongtien=tranggio.TongTien  });
        }

        public ActionResult Them(string id,int sl)
        {
            
            var p = db.Sanpham(id);
            Gio tranggio = (Gio)Session["gio"];

            (tranggio).Them(p,sl);
            var sp = (tranggio.DSSP.FirstOrDefault(x => x.ProductID == id) as Products);
            
            if (sp!=null&&sp.SoLuong > p.SoLuongTrongKho)
            {
                sp.SoLuong = (int)p.SoLuongTrongKho;
            }
            TempData["url"] = Request.UrlReferrer;
            if (TempData["url"] != null)
            {
                TempData["them"] = true;
                return Redirect(TempData["url"].ToString());

            }
            return RedirectToAction("Index","Gio");
        }
        public ActionResult Chitiet()
        {

            Gio tranggio = (Gio)Session["gio"];
          
            return PartialView("~/Views/Partial_view/Gio.cshtml",tranggio.DSSP);
        }
        public ActionResult Kiemtradonhang(int? soluong)
        {
            var tk = (Session["customer"] as Customers);
            if (tk == null)
            {
                TempData["url"] = "~/Gio/Kiemtradonhang";

                TempData["chuadangnhap"] = "<script>swal('bạn chưa đăng nhập!');</script>";
                return RedirectToAction("Dangnhap", "Trangchu");
            }
            var model = db.DSHD(tk.CustomerID);
            if ( soluong == null)
            {


                
                return View(model);
            }
            else
            {
                if (soluong<=0)
                {
                    return RedirectToAction("Kiemtradonhang","Gio");
                }
                else
                {

                    return PartialView("~/Views/Partial_view/Kiemtradonhang.cshtml",model.Take(3).ToList());
                }
            }
        }
        public ActionResult Donhang(string id)
        {
            try
            {
                var model = db.TimHD(id);
                ViewBag.id = id;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Kiemtradonhang", "Gio");

            }

        }
        public ActionResult Xoadonhang(string id)
        {
            try
            {
                db.Xoadonhang(db.TimHD(id));
                TempData["thanhcong"] = "<script>swal('đã hủy đơn hàng!');</script>";

            }
            catch (Exception)
            {

                return RedirectToAction("", "Trangchu");

            }
            return RedirectToAction("","Trangchu");
        }
    }
}