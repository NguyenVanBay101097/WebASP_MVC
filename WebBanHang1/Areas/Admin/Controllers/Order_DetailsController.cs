using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Areas.Admin.Common;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class Order_DetailsController : Controller
    {
        private BanHang db = new BanHang();

        // GET: Admin/Order_Details
        [checkQuyen(Xem =true)]
        public ActionResult Index(string idOrder)
        {
          
          
            var order_Details = db.Order_Details.Where(x => x.OrderID == idOrder).Include(o => o.Orders).Include(o => o.Products);
            var order = db.Orders.Single(x => x.OrderID == idOrder);
            order.Xem = true;
            db.SaveChanges();
            
            return View(order_Details.ToList());
        }

        // GET: Admin/Order_Details/Edit/5
        [checkQuyen(Sua = true)]
        public ActionResult Edit(string id,string idPr)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id,idPr);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // POST: Admin/Order_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [checkQuyen(Sua = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,ProductName,PricePresent,Quantity,TotalMoney")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                if (!Regex.IsMatch(order_Details.PricePresent.ToString(), @"^\d+$")|| !Regex.IsMatch(order_Details.TotalMoney.ToString(), @"^\d+$"))
                {
                    return View(order_Details);
                }
                db.Entry(order_Details).State = EntityState.Modified;
                var order = db.Orders.SingleOrDefault(x=>x.OrderID==order_Details.OrderID);
                var lst = db.Order_Details.Where(x=>x.OrderID==order.OrderID).ToList();
                double tongtien = 0;
                foreach (Order_Details item in lst)
                {
                    tongtien += (double)item.TotalMoney;
                }
                order.TotalMoney = tongtien;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","/Order_Details",new { idOrder=order_Details.OrderID });
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Admin/Order_Details/Delete/5
        [checkQuyen(Xoa = true)]
        public ActionResult Delete(string id, string idPr)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id,idPr);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // POST: Admin/Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [checkQuyen(Xoa = true)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string idPr)
        {
            Order_Details order_Details = db.Order_Details.Find(id,idPr);
            db.Order_Details.Remove(order_Details);
            db.SaveChanges();
            return RedirectToAction("Index",new {idOrder=id });
        }
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
