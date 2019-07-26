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
    public class EmployeesController : Controller
    {
        private BanHang db = new BanHang();

        WebBanHang1.Models.BH d = new Models.BH();

        // GET: Admin/Employees
        [checkQuyen(Xem = true)]
        public ActionResult Index()
        {
            var employees = db.Employees;
            return View(employees.ToList());
        }

        [checkQuyen(Them = true)]
        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.TenVT = db.VaiTros;
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [checkQuyen(Them = true)]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Employees e, string ConfirmMatkhau)
        {
            if (db.Employees.FirstOrDefault(x=>x.TenDN==e.TenDN) != null)
            {
                ViewBag.TrungTenDN = "Tên Đăng Nhập đã tồn tại!";
                ViewBag.TenVT = db.VaiTros;
                return View(e);
            }
            if (ConfirmMatkhau == e.MatKhau)
            {
                if (ModelState.IsValid)
                {

                    d.ThemNV(e);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.ConfirmMatkhau = "mật khẩu không trùng khớp";
            }
            ViewBag.TenVT = db.VaiTros;
            return View(e);
        }

        [checkQuyen(Sua = true)]
        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.a = db.VaiTros.ToList();
            return View(employees);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [checkQuyen(Sua = true)]
        public ActionResult Edit(Employees employees)
        {

            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.a = db.VaiTros.ToList();

            return View(employees);
        }

        // GET: Admin/Employees/Delete/5
        [HttpDelete]
        [checkQuyen(Xoa = true)]
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Employees employees = db.Employees.Find(id);
            //if (employees == null)
            //{
            //    return HttpNotFound();
            //}
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(employees);
        }

        // POST: Admin/Employees/Delete/5
        [checkQuyen(Xoa = true)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
