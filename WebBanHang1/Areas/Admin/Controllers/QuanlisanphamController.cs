using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Areas.Admin.Common;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class QuanlisanphamController : Controller
    {
        WebBanHang1.Models.BH db = new Models.BH();
        // GET: Admin/Quanlisanpham
        [checkQuyen(Xem =true)]
        public ActionResult Index(int page=1)
        {
           
            TempData["controller"] = "Quanlisanpham";
            TempData["action"] = "Index";
            TempData["back"] = Request.UrlReferrer;
            int pageSize = 8;
            var model = db.DanhsachSanphamMoi().ToPagedList(page,pageSize);
            return View(model);
        }
        public ActionResult Redirect()
        {
            if (TempData["back"] != null)
            {
                return Redirect(TempData["back"].ToString());
            }
           
            return View("Index");
        }
        public JsonResult KhongGiamgia(String id)
        {
            var sp = db.Sanpham(id);
            sp.PricePresent = sp.UnitPrice;
            db.CapnhatSP(sp);
            return Json(sp.PricePresent,JsonRequestBehavior.AllowGet);
        }
        [checkQuyen(Sua = true)]
        public ActionResult Sua(string id)
        {
                TempData["back"] = Request.UrlReferrer;
            if (id == null)
            {
                return RedirectToAction("Index", "Quanlisanpham");
            }
            var dssp = db.Sanpham(id);
            if (dssp == null)
            {
                id = null;
                return RedirectToAction("Index", "Quanlisanpham", new { id });

            }
            ViewBag.DM = db.DanhsachDanhmuc_cha();
            ViewBag.DMC = db.DMCon(id);
            return View("~/Areas/Admin/Views/Quanlisanpham/Sua.cshtml",dssp);
        }
        [HttpPost]
        [checkQuyen(Sua = true)]
        [ValidateInput(false)]
        public ActionResult Sua(Products p, HttpPostedFileBase Picture, HttpPostedFileBase Picture1, HttpPostedFileBase Picture2, HttpPostedFileBase Picture3, HttpPostedFileBase Picture4)
        {
          
                TempData.Keep("back");
            
            if (ModelState.IsValid)
            {
                if (KiemtraImage(Picture, p) == 1)
                {
                    Picture.SaveAs(Server.MapPath("~/Images/") + Picture.FileName);

                    p.Picture = Picture.FileName;
                }
                if (KiemtraImage(Picture1, p) == 1)
                {
                    Picture1.SaveAs(Server.MapPath("~/Images/") + Picture1.FileName);

                    p.Picture1 = Picture1.FileName;
                }
                if (KiemtraImage(Picture2, p) == 1)
                {
                    Picture2.SaveAs(Server.MapPath("~/Images/") + Picture2.FileName);

                    p.Picture2 = Picture2.FileName;
                }
                if (KiemtraImage(Picture3, p) == 1)
                {
                    Picture3.SaveAs(Server.MapPath("~/Images/") + Picture3.FileName);

                    p.Picture3 = Picture3.FileName;
                }
                if (KiemtraImage(Picture4, p) == 1)
                {
                    Picture4.SaveAs(Server.MapPath("~/Images/") + Picture4.FileName);

                    p.Picture4 = Picture4.FileName;
                }
                db.CapnhatSP(p);
            }
            return Redirect(TempData["back"].ToString());
        }
       private int KiemtraImage(HttpPostedFileBase Picture, Products p) {
            if (Picture != null)
            {
                if (Picture.ContentType.Contains("image"))
                {
                    return 1;
                }
            }
            return 0;
        }
        [checkQuyen(Xem = true)]
        public ActionResult Chitiet(string id)
        {
            TempData["back"] = Request.UrlReferrer;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Sanpham(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Admin/Views/Quanlisanpham/Chitiet.cshtml", products);

        }
        [checkQuyen(Xoa =true)]
        public ActionResult Xoa(string id)
        {
           
                TempData["back"] = Request.UrlReferrer;
           
            db.XoaSP(id);
            return Redirect(TempData["back"].ToString());
        }
        [checkQuyen(Them  = true)]
        public  ActionResult Them()
        {
            TempData["back"] = Request.UrlReferrer;

            ViewBag.CategoryChildID = new SelectList(db.DanhsachDanhmuc_cha(), "CategoryFatherID", "CategoryName");
            return View();
        }
        [checkQuyen(Them  = true)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(Products p, HttpPostedFileBase Picture, HttpPostedFileBase Picture1, HttpPostedFileBase Picture2, HttpPostedFileBase Picture3, HttpPostedFileBase Picture4)
        {

           
            if (ModelState.IsValid)
            {
                if (KiemtraImage(Picture, p) == 1)
                {
                    Picture.SaveAs(Server.MapPath("~/Images/") + Picture.FileName);

                    p.Picture = Picture.FileName;
                }
                if (KiemtraImage(Picture1, p) == 1)
                {
                    Picture1.SaveAs(Server.MapPath("~/Images/") + Picture1.FileName);

                    p.Picture1 = Picture1.FileName;
                }
                if (KiemtraImage(Picture2, p) == 1)
                {
                    Picture2.SaveAs(Server.MapPath("~/Images/") + Picture2.FileName);

                    p.Picture2 = Picture2.FileName;
                }
                if (KiemtraImage(Picture3, p) == 1)
                {
                    Picture3.SaveAs(Server.MapPath("~/Images/") + Picture3.FileName);

                    p.Picture3 = Picture3.FileName;
                }
                if (KiemtraImage(Picture4, p) == 1)
                {
                    Picture4.SaveAs(Server.MapPath("~/Images/") + Picture4.FileName);

                    p.Picture4 = Picture4.FileName;
                }
                if (db.ThemSP(p) == 1)
                {
                    return RedirectToAction("Index", "/Quanlisanpham");
                }
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index", "/Quanlisanpham");
        }
        [HttpPost]
        public JsonResult AutocompleteKeysearch(string Prefix)
        {
            //Note : you can bind same list from database  
            List<WebBanHang1.Models.Products> ObjList = new List<WebBanHang1.Models.Products>();
            ObjList = db.DanhsachSanphamMoi();
            //Searching records from list using LINQ query  
            var CityName = (from N in ObjList
                            where N.ProductID.ToLower().StartsWith(Prefix.ToLower()) ||
                            N.ProductName.ToLower().StartsWith(Prefix.ToLower())
                            select new { N.ProductID,N.ProductName });
           
            
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Timkiem(string keysearch,int page=1)
        {
            int pagesize = 8;
            TempData["controller"] = "Quanlisanpham";
            TempData["action"] = "Timkiem";
            ViewBag.keysearch = keysearch;
            var ds = db.Timsanpham(keysearch).ToPagedList(page, pagesize);

            return View("Index",ds);

        }
        public JsonResult DMC(string id)
        {
            List<Category_Childs> objcity = new List<Category_Childs>();
            objcity = db.DanhsachDanhmuc_Con(id);
            SelectList obgcity = new SelectList(objcity, "CategoryChildID", "CategoryName", 0);
            return Json(obgcity, JsonRequestBehavior.AllowGet);
        }
    }
}