using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FoodDA.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FoodDA.Controllers
{
    public class NguoiDungsController : Controller
    {
        FoodEntities db = new FoodEntities();
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Details()
        {
            NguoiDung nguoiDung = (NguoiDung)Session["TaiKhoan"];
            var idnguoidung = nguoiDung.IdQuyen;
            if(nguoiDung.IdQuyen==1)
            {
                var listnguoidung = db.NguoiDungs.Where(n => n.IdQuyen == 1).ToList();
                return View(listnguoidung);
            }
            return View();
        }

        public ActionResult Index(int page = 1, int pageSize = 7)
        {
            int pageNumber = page;

            return View(db.NguoiDungs.ToList().OrderBy(n => n.IdNguoiDung).ToPagedList(page, pageSize));
        }

        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        public ActionResult Register(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var check = db.NguoiDungs.FirstOrDefault(n => n.TenND == nguoiDung.TenND || n.UserName == nguoiDung.UserName || n.Sdt == nguoiDung.Sdt);
                //Session["TenNguoiDung"] = db.NguoiDungs.FirstOrDefault(s => s.TenND == nguoiDung.TenND);
                if (check == null)
                {
                    nguoiDung.PassWord = GetMD5(nguoiDung.PassWord);
                    nguoiDung.img = null;
                    nguoiDung.IdQuyen = 2;
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.thongbaoloi = "Email already exists";
                    return RedirectToAction("ErorrDangKyThatBai", "NguoiDungs");
                }
            }
            return RedirectToAction("ErorrDangKyThatBai", "NGuoiDungs");


        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            var Email = form["txtEmail"].ToString();
            var passWordOld = form["txtpassWordOld"].ToString();
            var MD5passWord = GetMD5(passWordOld);
            var check = db.NguoiDungs.Where(n => n.UserName == Email && n.PassWord == MD5passWord).FirstOrDefault();
            if (check != null)
            {
                var userdetail = db.NguoiDungs.FirstOrDefault(c => c.UserName == Email);
                if (userdetail != null)
                {
                    var passWordNew = form["txtPassWordNew"].ToString();
                    userdetail.PassWord = GetMD5(passWordNew);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("ErorrChangedPassword", "NguoiDungs");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, NguoiDung nguoidung)
        {
            String UserName = form["UserName"].ToString();
            String PassWord = form["PassWord"].ToString();
            var MD5PassWord = GetMD5(PassWord);
            nguoidung = db.NguoiDungs.SingleOrDefault(n => n.UserName == UserName && n.PassWord == MD5PassWord);
            Session["TaiKhoan"] = nguoidung;
            if (nguoidung != null)
            {
                Session["username"] = nguoidung.UserName;
                Session["tennguoidung"] = nguoidung.TenND;
                Session["TaiKhoan"] = nguoidung;
                Session["idNguoidung"] = nguoidung.IdNguoiDung;
                Session["idQuyen"] = nguoidung.IdQuyen;
                if (nguoidung.IdQuyen == 1)
                {
                    return RedirectToAction("SanPhamsPartial", "SanPham");
                }
                if (nguoidung.IdQuyen == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.thongbao = "Đăng nhập thất bại!!";
            return RedirectToAction("ErorrDangNhapThatBai", "NguoiDungs");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HistoryPay()
        {
            NguoiDung nguoiDung = (NguoiDung)Session["TaiKhoan"];
            var idnguoidung = nguoiDung.IdNguoiDung;
            var History = db.ChiTietHoaDons.Include(n=>n.HoaDon).Include(n=>n.SanPham).Where(n=>n.HoaDon.IdNguoiDung==nguoiDung.IdNguoiDung).ToList();
            ViewBag.History = History;
            return View(History);
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ErorrChangedPassword()
        {
            return View();
        }
        public ActionResult ErorrDangNhapThatBai()
        {
            return View();
        }
        public ActionResult ErorrDangKyThatBai()
        {
            return View();
        }

    }
}