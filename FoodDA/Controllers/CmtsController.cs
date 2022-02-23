using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;

namespace FoodDA.Controllers
{
    public class CmtsController : Controller
    {
        private FoodEntities db = new FoodEntities();

        // GET: Cmts
        public ActionResult Index()
        {
            var cmts = db.Cmts.Include(c => c.BaiViet).Include(c => c.NguoiDung);
            return View(cmts.ToList());
        }

        // GET: Cmts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cmt cmt = db.Cmts.Find(id);
            if (cmt == null)
            {
                return HttpNotFound();
            }
            return View(cmt);
        }

        // GET: Cmts/Create
        public ActionResult Create()
        {
            ViewBag.IdBaiViet = new SelectList(db.BaiViets, "IdBaiViet", "TenBaiViet");
            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND");
            return View();
        }

        // POST: Cmts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCmt,NoiDung,IdBaiViet,IdNguoiDung")] Cmt cmt)
        {
            if (ModelState.IsValid)
            {
                db.Cmts.Add(cmt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBaiViet = new SelectList(db.BaiViets, "IdBaiViet", "TenBaiViet", cmt.IdBaiViet);
            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", cmt.IdNguoiDung);
            return View(cmt);
        }

        // GET: Cmts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cmt cmt = db.Cmts.Find(id);
            if (cmt == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBaiViet = new SelectList(db.BaiViets, "IdBaiViet", "TenBaiViet", cmt.IdBaiViet);
            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", cmt.IdNguoiDung);
            return View(cmt);
        }

        // POST: Cmts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCmt,NoiDung,IdBaiViet,IdNguoiDung")] Cmt cmt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cmt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBaiViet = new SelectList(db.BaiViets, "IdBaiViet", "TenBaiViet", cmt.IdBaiViet);
            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", cmt.IdNguoiDung);
            return View(cmt);
        }

        // GET: Cmts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cmt cmt = db.Cmts.Find(id);
            if (cmt == null)
            {
                return HttpNotFound();
            }
            return View(cmt);
        }

        // POST: Cmts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cmt cmt = db.Cmts.Find(id);
            db.Cmts.Remove(cmt);
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
