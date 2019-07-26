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
    public class Category_FathersController : Controller
    {
        private BanHang db = new BanHang();

        // GET: Admin/Category_Fathers
        public ActionResult Index()
        {
            return View(db.Category_Fathers.ToList());
        }

        // GET: Admin/Category_Fathers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Fathers category_Fathers = db.Category_Fathers.Find(id);
            if (category_Fathers == null)
            {
                return HttpNotFound();
            }
            return View(category_Fathers);
        }

        // GET: Admin/Category_Fathers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category_Fathers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryFatherID,CategoryName,icon")] Category_Fathers category_Fathers)
        {
            if (ModelState.IsValid)
            {
                db.Category_Fathers.Add(category_Fathers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category_Fathers);
        }

        // GET: Admin/Category_Fathers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Fathers category_Fathers = db.Category_Fathers.Find(id);
            if (category_Fathers == null)
            {
                return HttpNotFound();
            }
            return View(category_Fathers);
        }

        // POST: Admin/Category_Fathers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryFatherID,CategoryName,icon")] Category_Fathers category_Fathers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_Fathers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category_Fathers);
        }

        // GET: Admin/Category_Fathers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Fathers category_Fathers = db.Category_Fathers.Find(id);
            if (category_Fathers == null)
            {
                return HttpNotFound();
            }
            return View(category_Fathers);
        }

        // POST: Admin/Category_Fathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category_Fathers category_Fathers = db.Category_Fathers.Find(id);
            db.Category_Fathers.Remove(category_Fathers);
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
