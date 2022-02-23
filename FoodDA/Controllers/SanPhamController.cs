using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity;



namespace Food.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        FoodEntities db = new FoodEntities();
        public ActionResult SanPhamPartial(int Idsanpham = 1)
        {
            var SanPham = db.SanPhams.Where(n => n.IdLoaiSP == Idsanpham).Take(5).ToList();
            return View(SanPham);
        }

        public ActionResult NguyenLieuPartial(int Idsanpham = 2)
        {
            var NguyenLieu = db.SanPhams.Where(n => n.IdLoaiSP == Idsanpham).Take(5).ToList();
            return View(NguyenLieu);
        }
        public ViewResult XemChiTietSanPham(int idSanPham = 0)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == idSanPham);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        public ViewResult SanPhamList(int idLoaiSamPham = 1, int page = 1, int pageSize = 5)
        {
            int pageNumber = page;
            var sanpham = db.SanPhams.Where(n => n.IdLoaiSP == idLoaiSamPham).ToList().ToPagedList(pageNumber, pageSize);
            return View(sanpham);
        }
        public ViewResult DungCuLamBepList(int idLoaiSamPham = 2, int page = 1, int pageSize = 5)
        {
            int pageNumber = page;
            var sanpham = db.SanPhams.Where(n => n.IdLoaiSP == idLoaiSamPham).ToList().ToPagedList(pageNumber, pageSize);
            return View(sanpham);
        }
        public ViewResult ListLoaiSanPham()
        {
            var loaisanpham = db.LoaiSPs;
            return View(loaisanpham);
        }
        public ActionResult SanPhamPartial1(int Idsanpham = 1)
        {
            var SanPham = db.SanPhams.Where(n => n.IdLoaiSP == Idsanpham).Take(5).ToList();
            return View(SanPham);
        }
        //admin
        public ActionResult SanPhamsPartial(int page = 1, int pageSize = 7)
        {
            int pageNumber = page;

            return View(db.SanPhams.ToList().OrderByDescending(n => n.IdSanPham).ToPagedList(page, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoiSanPham()
        {
            ViewBag.IdLoaiSP = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoaiSp), "IdLoaiSP", "TenLoaiSp");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SanPham sp, HttpPostedFileBase fileupload1, HttpPostedFileBase fileupload2)
        {
            ViewBag.IdLoaiSP = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoaiSp), "IdLoaiSP", "TenLoaiSp");
            if (fileupload1 == null || fileupload2== null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName1 = Path.GetFileName(fileupload1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/IMG/IMGSamPham"), fileName1);
                    var fileName2 = Path.GetFileName(fileupload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/IMG/IMGSamPham"), fileName2);
                    if (System.IO.File.Exists(path1)|| System.IO.File.Exists(path2))
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    else

                    {
                        fileupload1.SaveAs(path1);
                        fileupload2.SaveAs(path2);
                    }
                    sp.HinhAnh1 = fileName1;
                    sp.HinhAnh2 = fileName2;

                    db.SanPhams.Add(sp);
                    db.SaveChanges();
                }
                return RedirectToAction("SanPhamsPartial");

            }
            //Hiển thị chi tiết sp
          
        }
        public ActionResult ChiTietSanPham(int id)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IdSanPham == id);
            ViewBag.IdSanPham = sp.IdSanPham;
            if (sp == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            return View(sp);
        }
        //Xóa sp
        //public ActionResult XoaSanPham (int id)
        //{
        //    SanPham sp = db.SanPhams.SingleOrDefault(n => n.IdSanPham == id);
        //    ViewBag.IdSanPham = sp.IdSanPham;
        //    if( sp == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(sp);
        //}
        public ActionResult XoaSanPham(int? id)
        {
            SanPham sp = db.SanPhams.Find(id);
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("SanPhamsPartial");
        }
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IdSanPham == id);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.IdLoaiSP = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoaiSp), "IdLoaiSP", "TenLoaiSp");
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(SanPham sp, HttpPostedFileBase fileupload1, HttpPostedFileBase fileupload2)
        {
            ViewBag.IdLoaiSP = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoaiSp), "IdLoaiSP", "TenLoaiSp");
            if (fileupload1 == null || fileupload2 == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var fileName1 = Path.GetFileName(fileupload1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/IMG/IMGSamPham"), fileName1);
                    var fileName2 = Path.GetFileName(fileupload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/IMG/IMGSamPham"), fileName2);
                    if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2))
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    else
                    {
                        fileupload1.SaveAs(path1);
                        fileupload2.SaveAs(path2);
                    }
                    sp.HinhAnh1 = fileName1;
                    sp.HinhAnh2 = fileName2;
                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("SanPhamsPartial");
            }
        }
    }

}