using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang1.Areas.Admin.Common;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private BanHang db = new BanHang();

        // GET: Admin/Orders
        [checkQuyen(Xem =true)]
        public ActionResult Index(int page=1)
        {
            TempData["controller"] = "Orders";
            TempData["action"] = "Index";
            int pages = 8;
            var orders = db.Orders.Include(o => o.Customers);
            ViewBag.tongtien = 0;
            foreach (var item in orders)
            {
                if (item.Tralai == false && item.Status==true)
                {
                    ViewBag.tongtien += item.TotalMoney;
                }
            }
            return View(orders.OrderByDescending(x=>x.OrderDate).ToPagedList(page,pages));
        }


        // GET: Admin/Orders/Edit/5
        [checkQuyen(Sua = true)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Address", orders.CustomerID);
            return View(orders);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [checkQuyen(Sua = true)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,OrderDate,RecevierName,Address,Status,TotalMoney,Phone,Email,Tralai")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Address", orders.CustomerID);
            return View(orders);
        }

        // GET: Admin/Orders/Delete/5
        [checkQuyen(Xoa = true)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [checkQuyen(Xoa = true)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Status(String id)
        {
            
            var d = new BH();
            var sp = d.TimHD(id.Trim());
            sp.Status = true;
            sp.Tralai = false;
            d.CapnhatHD(sp);
            return Json(new {status=true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Tralai(String id)
        {

            var d = new BH();
            var sp = d.TimHD(id.Trim());
            sp.Tralai = true;
            d.CapnhatHD(sp);
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public JsonResult AutocompleteKeysearch(string Prefix)
        {
            //Note : you can bind same list from database  
            List<WebBanHang1.Models.Orders> ObjList = new List<WebBanHang1.Models.Orders>();
            ObjList = new BH().DSHD();
            //Searching records from list using LINQ query  
            var CityName = (from N in ObjList
                            where N.OrderID.ToLower().StartsWith(Prefix.ToLower())
                            select new { N.OrderID });


            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Timkiem(string keysearch, int page = 1)
        {
            int pagesize = 8;
            TempData["controller"] = "Orders";
            TempData["action"] = "Timkiem";
            ViewBag.keysearch = keysearch;
            var ds = (new BanHang()).Orders.Where(x=>x.OrderID.ToLower()==keysearch.ToLower()).OrderByDescending(x=>x.OrderDate).ToPagedList(page, pagesize);
            ViewBag.tongtien = 0;
            foreach (var item in ds)
            {
                if (item.Tralai == false && item.Status == true)
                {
                    ViewBag.tongtien += item.TotalMoney;
                }
            }
            return View("Index", ds);

        }
        [HttpPost]
        public JsonResult Timkiem_Theongay(string ListDate)
        {
            var lst = ListDate.Split('|');
            var d1 = DateTime.Parse(lst[0]);
            var d2 = DateTime.Parse(lst[1]);
          
            var data = (new BanHang()).Orders.Where(x => x.OrderDate >= d1 && x.OrderDate <= d2).ToArray();
            return Json(JsonConvert.SerializeObject(data),JsonRequestBehavior.AllowGet);

        }
        public ActionResult ChuyenTrang(string data,int page=1)
        {
            TempData["controller"] = "Orders";
            TempData["action"] = "ChuyenTrang";
            ViewBag.tongtien = 0;
            List<Orders> ds = JsonConvert.DeserializeObject<List<Orders>>(data);
            foreach (var item in ds)
            {
                if (item.Tralai == false && item.Status == true)
                {
                    ViewBag.tongtien += item.TotalMoney;
                }
            }
            return View("Index", ds.ToPagedList(page,8));
        }
    }
}
