using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using PagedList;
using PagedList.Mvc;

namespace FoodDA.Controllers
{
    public class TimKiemController : Controller
    {
        FoodEntities db = new FoodEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            String tukhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = tukhoa;
            var listkq = db.BaiViets.Where(n => n.TenBaiViet.Contains(tukhoa)).ToList();
            ViewBag.ThongBao = "Tìm thấy " + listkq.Count + " bài viết";
            return View(listkq.OrderBy(n => n.TenBaiViet).ToPagedList(pageNumber, sizepage));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(String tukhoa, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            ViewBag.TuKhoa = tukhoa;
            var listkq = db.BaiViets.Where(n => n.TenBaiViet.Contains(tukhoa)).ToList();
            ViewBag.ThongBao = "Tìm thấy " + listkq.Count + " bài viết";
            return View(listkq.OrderBy(n => n.TenBaiViet).ToPagedList(pageNumber, sizepage));
        }
        [HttpPost]
        public ActionResult KetQuaTimKiemsampham(FormCollection f, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            String tukhoa = f["txtTimKiemsanpham"].ToString();
            ViewBag.TuKhoa = tukhoa;
            var listkq = db.SanPhams.Where(n => n.TenSP.Contains(tukhoa)).ToList().ToPagedList(pageNumber, sizepage);
            ViewBag.ThongBao = "Tìm thấy " + listkq.Count + " Sản phẩm";
            return View(listkq);
        }
        [HttpGet]
        public ActionResult KetQuaTimKiemsampham(String tukhoa, int page = 1, int sizepage = 5)
        {
            int pageNumber = page;
            ViewBag.TuKhoa = tukhoa;
            var listkq = db.SanPhams.Where(n => n.TenSP.Contains(tukhoa)).ToList().ToPagedList(pageNumber, sizepage);
            ViewBag.ThongBao = "Tìm thấy " + listkq.Count + " Sản phẩm";
            return View(listkq);
        }

    }

}