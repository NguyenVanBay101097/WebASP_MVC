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
    public class VaiTrosController : Controller
    {
        private BanHang db = new BanHang();

        // GET: Admin/VaiTros
        [checkQuyen(Xem =true)]
        public ActionResult Index()
        {
            return View(db.VaiTros.ToList());
        }

        // GET: Admin/VaiTros/Details/5
        [checkQuyen(Xem = true)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTros vaiTros = db.VaiTros.Find(id);
            if (vaiTros == null)
            {
                return HttpNotFound();
            }
            return View(vaiTros);
        }

        // GET: Admin/VaiTros/Create
        [checkQuyen(Them = true)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VaiTros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [checkQuyen(Them = true)]
        public ActionResult Create([Bind(Include = "TenVT,MoTa")] VaiTros vaiTros)
        {
            if (ModelState.IsValid)
            {
                db.VaiTros.Add(vaiTros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaiTros);
        }

        // GET: Admin/VaiTros/Edit/5
        [checkQuyen(Sua = true)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTros vaiTros = db.VaiTros.Find(id);
            if (vaiTros == null)
            {
                return HttpNotFound();
            }
            return View(vaiTros);
        }

        // POST: Admin/VaiTros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [checkQuyen(Sua = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenVT,MoTa")] VaiTros vaiTros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaiTros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaiTros);
        }

        // GET: Admin/VaiTros/Delete/5
        [checkQuyen(Xoa = true)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTros vaiTros = db.VaiTros.Find(id);
            if (vaiTros == null)
            {
                return HttpNotFound();
            }
            return View(vaiTros);
        }

        // POST: Admin/VaiTros/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [checkQuyen(Xoa = true)]
        public ActionResult DeleteConfirmed(string id)
        {
            VaiTros vaiTros = db.VaiTros.Find(id);
            var a = vaiTros.TenVT;
            
            db.VaiTros.Remove(vaiTros);
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
