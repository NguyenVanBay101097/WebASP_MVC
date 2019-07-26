using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Models;

namespace WebBanHang1.Controllers
{
    public class SanphamController : Controller
    {
        WebBanHang1.Models.BH db = new Models.BH();

        // GET: danh sách sản phẩm
        public ActionResult Index()
        {
            Gio tranggio = (Gio)Session["gio"];
          
            return View();

        }
        public ActionResult Banchay()
        {

            return View();

        }
        //danh sách sản phẩm theo chủng loại
        public ActionResult ChungLoai(string ten,String id,int page=1)
        {
            int pagesize = 8;
            if (id==null)
            {
                return RedirectToAction("Index","Trangchu");
            }
            var dssp = db.Sanpham_CL(id).ToPagedList(page,pagesize);
            if (dssp.Count==0)
            {
                return RedirectToAction("Index", "Trangchu");

            }
            return View(dssp);

        }
        public ActionResult Loai(string ten,String id,int page=1)
        {
            int pagesize = 8;
            if (id==null)
            {
                return RedirectToAction("Index","Trangchu");
            }
            var dssp = db.Sanpham_L(id).ToPagedList(page,pagesize);
            if (dssp.Count==0)
            {
                return RedirectToAction("Index", "Trangchu");

            }
            return View(dssp);

        }
       //chi tiet san pham
       //[OutputCache(Duration =60,VaryByParam ="none")]
       public ActionResult Chitiet(string ten, string id)
        {
            if (id==null)
            {
                return RedirectToAction("Index","Sanpham");
            }
            var sp = db.Sanpham(id);
            if (sp ==null)
            {
                id = null;
                return RedirectToAction("Index", "Sanpham",new { id});

            }
            ViewBag.dsdm = db.DanhsachDanhmuc_cha().ToList();
            ViewBag.DSSPLienquan = db.DanhsachSanphamMoi().Where(x => x.CategoryChildID == sp.CategoryChildID).Take(4).ToList();

            return View(sp);
        }
        public ActionResult danhsach()
        {
            WebBanHang1.Models.BanHang bh = new Models.BanHang();
            ViewBag.ds = (from p in bh.Products select new { a=p.Category_Childs.Category_Fathers.CategoryName,b=p.ProductName}).ToList();
            return View();
        }
    }
}