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
    public class Category_ChildsController : Controller
    {
        
        private BanHang db = new BanHang();

        // GET: Admin/Category_Childs
        public ActionResult Index(string idcha)
        {
            if (idcha == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category_Childs = db.Category_Childs.Where(x=>x.CategoryFatherID==idcha).ToList();
            ViewBag.tencha = db.Category_Fathers.Find(idcha).CategoryName;
            return View(category_Childs.ToList());
        }

        // GET: Admin/Category_Childs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Childs category_Childs = db.Category_Childs.Find(id);
            if (category_Childs == null)
            {
                return HttpNotFound();
            }
            return View(category_Childs);
        }

        // GET: Admin/Category_Childs/Create
        public ActionResult Create(string idcha)
        {
            ViewBag.CategoryFatherID = new SelectList(db.Category_Fathers, "CategoryFatherID", "CategoryName",idcha);
            return View();
        }

        // POST: Admin/Category_Childs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryChildID,CategoryName,CategoryFatherID")] Category_Childs category_Childs)
        {
            if (ModelState.IsValid)
            {
                db.Category_Childs.Add(category_Childs);
                db.SaveChanges();
                return RedirectToAction("Index", "Category_Childs", new { idcha = category_Childs.CategoryFatherID });

            }

            ViewBag.CategoryFatherID = new SelectList(db.Category_Fathers, "CategoryFatherID", "CategoryName", category_Childs.CategoryFatherID);
            return View(category_Childs);
        }

        // GET: Admin/Category_Childs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Childs category_Childs = db.Category_Childs.Find(id);
            if (category_Childs == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryFatherID = new SelectList(db.Category_Fathers, "CategoryFatherID", "CategoryName", category_Childs.CategoryFatherID);
            return View(category_Childs);
        }

        // POST: Admin/Category_Childs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryChildID,CategoryName,CategoryFatherID")] Category_Childs category_Childs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_Childs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Category_Childs",new { idcha=category_Childs.CategoryFatherID });
            }
            ViewBag.CategoryFatherID = new SelectList(db.Category_Fathers, "CategoryFatherID", "CategoryName", category_Childs.CategoryFatherID);
            return View(category_Childs);
        }

        // GET: Admin/Category_Childs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Childs category_Childs = db.Category_Childs.Find(id);
            if (category_Childs == null)
            {
                return HttpNotFound();
            }
            return View(category_Childs);
        }

        // POST: Admin/Category_Childs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category_Childs category_Childs = db.Category_Childs.Find(id);
            var d = category_Childs.CategoryFatherID;
            db.Category_Childs.Remove(category_Childs);
            db.SaveChanges();
            return RedirectToAction("Index", "Category_Childs", new { idcha = d });

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
