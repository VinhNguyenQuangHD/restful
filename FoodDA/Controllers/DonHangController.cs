using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using PagedList;

namespace FoodDA.Controllers
{
    public class DonHangController : Controller
    {
        private FoodEntities db = new FoodEntities();

        // GET: ChiTietHoaDons
        public ActionResult Index(int page = 1, int pageSize = 7)
        {
            int pageNumber = page;

            return View(db.HoaDons.ToList().OrderBy(n => n.IdHoaDon).ToPagedList(page, pageSize));
        }

        public ActionResult ChiTietHoaDon(int id)
        {
            var sp = db.ChiTietHoaDons.Where(n => n.IdHoaDon == id).ToList();
            //ViewBag.IdBaiViet = sp.IdHoaDon;
            if (sp == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            return View(sp);
        }

        /*        // GET: ChiTietHoaDons/Details/5
                public ActionResult Details(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
                    if (chiTietHoaDon == null)
                    {
                        return HttpNotFound();
                    }
                    return View(chiTietHoaDon);
                }

                // GET: ChiTietHoaDons/Create
                public ActionResult Create()
                {
                    ViewBag.IdHoaDon = new SelectList(db.HoaDons, "IdHoaDon", "IdHoaDon");
                    ViewBag.IdSanPham = new SelectList(db.SanPhams, "IdSanPham", "TenSP");
                    return View();
                }

                // POST: ChiTietHoaDons/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create([Bind(Include = "IdHoaDon,IdSanPham,ThanhTien,SoLuong")] ChiTietHoaDon chiTietHoaDon)
                {
                    if (ModelState.IsValid)
                    {
                        db.ChiTietHoaDons.Add(chiTietHoaDon);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.IdHoaDon = new SelectList(db.HoaDons, "IdHoaDon", "IdHoaDon", chiTietHoaDon.IdHoaDon);
                    ViewBag.IdSanPham = new SelectList(db.SanPhams, "IdSanPham", "TenSP", chiTietHoaDon.IdSanPham);
                    return View(chiTietHoaDon);
                }

                // GET: ChiTietHoaDons/Edit/5
                public ActionResult Edit(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
                    if (chiTietHoaDon == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.IdHoaDon = new SelectList(db.HoaDons, "IdHoaDon", "IdHoaDon", chiTietHoaDon.IdHoaDon);
                    ViewBag.IdSanPham = new SelectList(db.SanPhams, "IdSanPham", "TenSP", chiTietHoaDon.IdSanPham);
                    return View(chiTietHoaDon);
                }

                // POST: ChiTietHoaDons/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit([Bind(Include = "IdHoaDon,IdSanPham,ThanhTien,SoLuong")] ChiTietHoaDon chiTietHoaDon)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(chiTietHoaDon).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.IdHoaDon = new SelectList(db.HoaDons, "IdHoaDon", "IdHoaDon", chiTietHoaDon.IdHoaDon);
                    ViewBag.IdSanPham = new SelectList(db.SanPhams, "IdSanPham", "TenSP", chiTietHoaDon.IdSanPham);
                    return View(chiTietHoaDon);
                }

                // GET: ChiTietHoaDons/Delete/5
                public ActionResult Delete(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
                    if (chiTietHoaDon == null)
                    {
                        return HttpNotFound();
                    }
                    return View(chiTietHoaDon);
                }

                // POST: ChiTietHoaDons/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public ActionResult DeleteConfirmed(int id)
                {
                    ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
                    db.ChiTietHoaDons.Remove(chiTietHoaDon);
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
                }*/
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity;

namespace FoodDA.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        FoodEntities db = new FoodEntities();
        public ActionResult Index(int page = 1, int pageSize = 7)
        {
            int pageNumber = page;

            return View(db.HoaDons.ToList().OrderBy(n => n.IdHoaDon).ToPagedList(page, pageSize));
        }
        public ActionResult ChiTietHoaDon(int id)
        {
            ChiTietHoaDon sp = db.ChiTietHoaDons.SingleOrDefault(n => n.IdHoaDon == id);
            ViewBag.IdBaiViet = sp.IdHoaDon;
            if (sp == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            return View(sp);
        }

    }
}*/