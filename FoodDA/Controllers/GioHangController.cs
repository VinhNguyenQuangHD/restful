using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;

namespace FoodDA.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        FoodEntities db = new FoodEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang==null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int idSanPham,string strURL)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == idSanPham);
            if(sanpham==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang gh = lstGioHang.Find(n => n.idSanPham == idSanPham);
            if(gh==null)
            {
                gh = new GioHang(idSanPham);
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.Soluong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhapGioHang(int idSanPham,FormCollection f)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == idSanPham);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang giohangsanpham = lstGioHang.SingleOrDefault(n => n.idSanPham == idSanPham);
            if(giohangsanpham!=null)
            {
                giohangsanpham.Soluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int idSanPham)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == idSanPham);
            if(sanpham==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang giohangsanpham = lstGioHang.SingleOrDefault(n => n.idSanPham == idSanPham);
            if(giohangsanpham != null)
            {
                lstGioHang.RemoveAll(n => n.idSanPham == idSanPham);
            }
            if(lstGioHang.Count==0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult ErorrCart()
        {
            return View();
        }

        public ActionResult GioHang()
        {
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("ErorrCart", "GioHang");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        private int TongSL()
        {
            int TongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                TongSoLuong = lstGioHang.Sum(n => n.Soluong);
            }
            return TongSoLuong;
        }
        private double TongTien()
        {
            double Tongtien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                Tongtien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return Tongtien;
        }
        public ActionResult GioHangPartial()
        {
            if(TongSL()==0)
            {
                return PartialView();
            }
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang()
        {
            
            if(Session["TaiKhoan"]==null)
            {
                return RedirectToAction("ErrorDangNhap", "GioHang");
            }
            HoaDon hd = new HoaDon();
            List<GioHang> lstgiohang = LayGioHang();
            NguoiDung ng = (NguoiDung)Session["TaiKhoan"];
            hd.IdNguoiDung = ng.IdNguoiDung;
            hd.Ngay = DateTime.Now;
            hd.TongTien = 0;

            foreach (var item in lstgiohang)
                hd.TongTien += item.ThanhTien;

            db.HoaDons.Add(hd);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach(var item in lstgiohang)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IdHoaDon = hd.IdHoaDon;
                cthd.IdSanPham = item.idSanPham;
                cthd.SoLuong = item.Soluong;
                cthd.ThanhTien = item.ThanhTien;
                db.ChiTietHoaDons.Add(cthd);
            }
            db.SaveChanges();

            foreach (var item in lstgiohang)
            {
                SanPham CartedProduct = db.SanPhams
                   .Where(SP => SP.IdSanPham == item.idSanPham).FirstOrDefault();

                CartedProduct.Ton = CartedProduct.Ton - item.Soluong;

                if (CartedProduct.Ton <= 0) CartedProduct.Ton = 0;

                db.Entry(CartedProduct).State = EntityState.Modified;

                db.SaveChanges();
            }

            Session.Remove("GioHang");

            return RedirectToAction("SucessDK", "GioHang");
        }
        public ActionResult ErrorDangNhap()
        {
            return View();
        }
        public ActionResult SucessDK()
        {
            return View();
        }

    }
}