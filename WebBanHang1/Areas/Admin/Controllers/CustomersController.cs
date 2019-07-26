using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Areas.Admin.Common;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private BanHang db;
        public CustomersController()
        {
           db= new BanHang();
        }
        // GET: Admin/Customers
        [checkQuyen(Xem=true)]
        public ActionResult Index()
        {
          
            return View(db.Customers.ToList());
        }



        // GET: Admin/Customers/Create
        [checkQuyen(Them  = true)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [checkQuyen(Them = true)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Address,CustomerName,TenDN,Email,MatKhau,phonenumber,Picture")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        // GET: Admin/Customers/Edit/5
        [checkQuyen(Sua  = true)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [checkQuyen(Sua  = true)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Address,CustomerName,TenDN,Email,MatKhau,phonenumber,Picture")] Customers customers)
        {
            // khoi doi tuong
            //giong ham main 
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        // GET: Admin/Customers/Delete/5
        [checkQuyen(Xoa = true)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [checkQuyen(Xoa = true)]
        public ActionResult DeleteConfirmed(string id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("Index");
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
