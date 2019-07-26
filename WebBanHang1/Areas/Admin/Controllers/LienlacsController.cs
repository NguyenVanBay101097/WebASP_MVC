using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang1.Models;

namespace WebBanHang1.Areas.Admin.Controllers
{
    public class LienlacsController : Controller
    {
        private BanHang db = new BanHang();

        // GET: Admin/Lienlacs
        public ActionResult Index()
        {
            var ds = db.Lienlac.Where(x => x.Xem == false).ToList();
            foreach (var item in ds)
            {
                item.Xem = true;
                db.SaveChanges();
            }
            return View(db.Lienlac.ToList());
        }

        // GET: Admin/Lienlacs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lienlac lienlac = db.Lienlac.Find(id);
            if (lienlac == null)
            {
                return HttpNotFound();
            }
            return View(lienlac);
        }

        // GET: Admin/Lienlacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Lienlacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,adress,contents,phone,date,status")] Lienlac lienlac)
        {
            if (ModelState.IsValid)
            {
                db.Lienlac.Add(lienlac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lienlac);
        }

        // GET: Admin/Lienlacs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lienlac lienlac = db.Lienlac.Find(id);
            if (lienlac == null)
            {
                return HttpNotFound();
            }
            return View(lienlac);
        }

        // POST: Admin/Lienlacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,adress,contents,phone,date,status")] Lienlac lienlac)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(lienlac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lienlac);
        }

        // GET: Admin/Lienlacs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lienlac lienlac = db.Lienlac.Find(id);
            if (lienlac == null)
            {
                return HttpNotFound();
            }
            return View(lienlac);
        }

        // POST: Admin/Lienlacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Lienlac lienlac = db.Lienlac.Find(id);
            db.Lienlac.Remove(lienlac);
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
