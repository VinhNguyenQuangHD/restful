using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace FoodDA.Controllers
{
    public class BaiVietsController : Controller
    {
        private FoodEntities db = new FoodEntities();

        // GET: BaiViets
        public ActionResult Index(int page = 1, int pageSize = 7)
        {
            int pageNumber = page;

            return View(db.BaiViets.ToList().OrderByDescending(n => n.IdBaiViet).ToPagedList(page, pageSize));
        }

        // GET: BaiViets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // GET: BaiViets/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IdChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IdChuDe", "TenChuDe");
            return View();
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(BaiViet sp, HttpPostedFileBase fileupload1, HttpPostedFileBase fileupload2, HttpPostedFileBase fileupload3)
        {
            ViewBag.IdChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IdChuDe", "TenChuDe");
            if (fileupload1 == null || fileupload2 == null || fileupload3 == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName1 = Path.GetFileName(fileupload1.FileName);
                    var path1 = Path.Combine(Server.MapPath("~/IMG/IMGcongthuc"), fileName1);
                    var fileName2 = Path.GetFileName(fileupload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/IMG/IMGcongthuc"), fileName2);
                    var fileName3 = Path.GetFileName(fileupload2.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/IMG/IMGcongthuc"), fileName3);
                    if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2)|| System.IO.File.Exists(path3))
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    else

                    {
                        fileupload1.SaveAs(path1);
                        fileupload2.SaveAs(path2);
                        fileupload3.SaveAs(path3);
                    }
                    sp.HinhAnh1 = fileName1;
                    sp.HinhAnh2 = fileName2;
                    sp.HinhAnh3 = fileName3;
                    sp.Ngay = DateTime.Now;

                    db.BaiViets.Add(sp);
                    db.SaveChanges();
                  
                }
                return RedirectToAction("Index");

            }
            //Hiển thị chi tiết sp

        }


        // GET: BaiViets/Edit/5
        public ActionResult chitietbaiviet(int id)
        {
            BaiViet sp = db.BaiViets.SingleOrDefault(n => n.IdBaiViet == id);
            ViewBag.IdBaiViet = sp.IdBaiViet;
            if (sp == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            return View(sp);
        }

        // POST: BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        public ActionResult SuaBaiViet(int id)
        {
            BaiViet sp = db.BaiViets.SingleOrDefault(n => n.IdBaiViet == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.IdChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IdChuDe", "TenChuDe");
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaBaiViet(BaiViet sp, HttpPostedFileBase fileupload1, HttpPostedFileBase fileupload2, HttpPostedFileBase fileupload3)
        {
            ViewBag.IdChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "IdChuDe", "TenChuDe");
            if (fileupload1 == null || fileupload2 == null || fileupload3 == null)
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
                    var path2 = Path.Combine(Server.MapPath("~/MG/IMGSamPham"), fileName2);
                    var fileName3 = Path.GetFileName(fileupload3.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/IMG/IMGSamPham"), fileName3);
                    if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2) || System.IO.File.Exists(path3))
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    else
                    {
                        fileupload1.SaveAs(path1);
                        fileupload2.SaveAs(path2);
                        fileupload2.SaveAs(path3);
                    }
                    sp.HinhAnh1 = fileName1;
                    sp.HinhAnh2 = fileName2;
                    sp.HinhAnh3 = fileName3;
                    sp.Ngay = DateTime.Now;

                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
        }
        // GET: BaiViets/Delete/5
        public ActionResult xoabaiviet(int? id)
        {
            BaiViet sp = db.BaiViets.Find(id);
            db.BaiViets.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViets.Find(id);
            db.BaiViets.Remove(baiViet);
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
        [HttpGet]
        public ActionResult XemChiTietBaiViet(int id)
        {
            var product = db.BaiViets.Find(id);
            ViewBag.product = product;
            ViewBag.idproduct = product.IdBaiViet;
            Session["BaiViet"] = product.IdBaiViet;
            Session["idbaiviet"] = product;
            var cmt = new Cmt()
            {
                IdBaiViet = product.IdBaiViet
            };
            ViewBag.congthuc = product.HuongDan.Contains("$");
            return View("XemChiTietBaiViet", cmt);
        }

        [HttpGet]
        public ActionResult showComment(int id = 0, int page = 1, int sizepage = 5)
        {
            ViewBag.idbaivietcm = id;
            int pageNumber = page;
            BaiViet baiviet = db.BaiViets.SingleOrDefault(n => n.IdBaiViet == id);
            var cmts = db.Cmts.Include(c => c.NguoiDung).Where(n => n.IdBaiViet == id).OrderByDescending(n=>n.Ngay).ToList();
            ViewBag.ThongBaoCMT = "Có " + cmts.Count() + " bình luận";
            return View(cmts.ToPagedList(pageNumber, sizepage));
        }

        public ActionResult SendReview(FormCollection f)
        {
            NguoiDung nguoiDung = (NguoiDung)Session["TaiKhoan"];
            BaiViet baiviet = (BaiViet)Session["idbaiviet"];
            Cmt cmt = new Cmt();
            cmt.IdNguoiDung = nguoiDung.IdNguoiDung;
            cmt.IdBaiViet = baiviet.IdBaiViet;
            cmt.Ngay = DateTime.Now;
            cmt.NoiDung = f["NoiDung"].ToString();
            cmt.rating = int.Parse(f["rating"]);
            db.Cmts.Add(cmt);
            db.SaveChanges();
            return RedirectToAction("XemChiTietBaiViet", "BaiViets", new { id = baiviet.IdBaiViet });
        }
        public ViewResult ListBaiViet(int idLoaiSamPham = 1, int page = 1, int pageSize = 5)
        {

            int pageNumber = page;
            var baiviet = db.BaiViets.ToList();
            ViewBag.CountKcal = "Đã tìm thấy " + baiviet.Count + " bài viết cho bạn";
            return View(baiviet.ToPagedList(pageNumber, pageSize));
        }
        public ViewResult ListBaiVietChuDe()
        {
            var listbaivietchude = db.ChuDes.ToList();
            return View(listbaivietchude);
        }
        [HttpPost]
        public ViewResult ListBaiVietKcal(FormCollection f, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            float ChieuCao = float.Parse(f["txtChieuCao"]);
            float CanNang = float.Parse(f["txtCanNang"]);
            float BMI = ((CanNang / (ChieuCao * ChieuCao)) * 10000);
            ViewBag.CanNang = CanNang;
            ViewBag.ChieuCao = ChieuCao;
            ViewBag.BMI = BMI;
            if (BMI >= 40)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ ba";
                var list = db.BaiViets.Where(n => n.kcal < 800).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 35 && BMI < 40)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ hai";
                var list = db.BaiViets.Where(n => n.kcal < 1000).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 30 && BMI <= 35)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ một";
                var list = db.BaiViets.Where(n => n.kcal < 1200).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 25 && BMI <= 30)
            {
                ViewBag.thongbaoKcal = "Thừa cân";
                var list = db.BaiViets.Where(n => n.kcal < 1500).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 18 && BMI <= 25)
            {
                ViewBag.thongbaoKcal = "Bình thường";
                var list = db.BaiViets.Where(n => n.kcal < 1800).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 17 && BMI <= 18)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 1 ";
                var list = db.BaiViets.Where(n => n.kcal < 2000).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI >= 16 && BMI <= 17)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 2 ";
                var list = db.BaiViets.Where(n => n.kcal < 2200).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI < 16)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 3 ";
                var list = db.BaiViets.Where(n => n.kcal < 2500).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            return View();
        }
        [HttpGet]
        public ViewResult ListBaiVietKcal(float BMI, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            ViewBag.BMI = BMI;
            if (BMI >= 40)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ ba";
                var list = db.BaiViets.Where(n => n.kcal < 800).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 35 && BMI < 40)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ hai";
                var list = db.BaiViets.Where(n => n.kcal < 1000).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 30 && BMI <= 35)
            {
                ViewBag.thongbaoKcal = "Béo phì cấp độ một";
                var list = db.BaiViets.Where(n => n.kcal < 1200).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 25 && BMI <= 30)
            {
                ViewBag.thongbaoKcal = "Thừa cân";
                var list = db.BaiViets.Where(n => n.kcal < 1500).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 18 && BMI <= 25)
            {
                ViewBag.thongbaoKcal = "Bình thường";
                var list = db.BaiViets.Where(n => n.kcal < 1800).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI > 17 && BMI <= 18)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 1 ";
                var list = db.BaiViets.Where(n => n.kcal < 2000).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI >= 16 && BMI <= 17)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 2 ";
                var list = db.BaiViets.Where(n => n.kcal < 2200).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            if (BMI < 16)
            {
                ViewBag.thongbaoKcal = "Gầy cấp độ 3 ";
                var list = db.BaiViets.Where(n => n.kcal < 2500).ToList();
                ViewBag.CountKcal = "Đã tìm thấy " + list.Count + " bài viết cho bạn";
                return View(list.ToPagedList(pageNumber, sizepage));
            }
            return View();
        }
        [HttpPost]
        public ActionResult listCalo(FormCollection f, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            float min = float.Parse(f["txtmin"]);
            float max = float.Parse(f["txtmax"]);
            ViewBag.min = min;
            ViewBag.max = max;
            var listcalo = db.BaiViets.Where(n => n.kcal <= max && n.kcal >= min).ToList();
            ViewBag.ThongBao = "Tìm thấy " + listcalo.Count + " bài viết";
            return View(listcalo.ToPagedList(pageNumber, sizepage));
        }
        [HttpGet]
        public ActionResult listCalo(float min, float max, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            ViewBag.min = min;
            ViewBag.max = max;
            var listcalo = db.BaiViets.Where(n => n.kcal <= max && n.kcal >= min).ToList();
            ViewBag.ThongBao = "Tìm thấy " + listcalo.Count + " bài viết";
            return View(listcalo.ToPagedList(pageNumber, sizepage));
        }
        [HttpGet]
        public ViewResult listBaiVietNguyenLieu(string tenNguyenLieu, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            var baiviet = db.BaiViets.Where(n => n.NguyenLieu1 == tenNguyenLieu || n.NguyenLieu2 == tenNguyenLieu
            || n.NguyenLieu3 == tenNguyenLieu
            || n.NguyenLieu4 == tenNguyenLieu
            || n.NguyenLieu5 == tenNguyenLieu
            || n.NguyenLieu6 == tenNguyenLieu
            || n.NguyenLieu7 == tenNguyenLieu
            || n.NguyenLieu8 == tenNguyenLieu).ToList();
            if (baiviet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.nguyenlieu = tenNguyenLieu;
            ViewBag.thongbao = "Tìm thấy " + baiviet.Count + " bài viết có nguyên liệu " + ViewBag.nguyenlieu + " cho bạn";
            return View(baiviet.ToPagedList(pageNumber, sizepage));
        }
    }
}
    