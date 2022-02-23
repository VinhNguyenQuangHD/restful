using FoodDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDA.Controllers
{
    public class ThongKeController : Controller
    {
        private FoodEntities db = new FoodEntities();
        // GET: 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SanPham()
        {
            return View();
        }

        public ActionResult BaiViet()
        {
            return View();
        }

        public ActionResult GetPostAnalytics()
        {
            return Json(PreparePostData(), JsonRequestBehavior.AllowGet);
        }

        public List<PostStatic> PreparePostData()
        {
            List<PostStatic> listPostStatic = new List<PostStatic>();

            List<BaiViet> listPost = db.BaiViets.ToList();///

            for (int i = 1; i < 10; i++)
            {
                int Soluong = db.BaiViets.Count(x => x.ChuDe.IdChuDe == i);
                PostStatic postStatic = new PostStatic();
                postStatic.Quanity = Soluong;
                if (i == 1)
                {
                    postStatic.NameTypePost = "Món Khô";
                }
                if (i == 2)
                {
                    postStatic.NameTypePost = "Soup";
                }
                if (i == 3)
                {
                    postStatic.NameTypePost = "Món Xào";
                }
                if (i == 4)
                {
                    postStatic.NameTypePost = "Món chiên";
                }
                if (i == 5)
                {
                    postStatic.NameTypePost = "Món nướng";
                }

                listPostStatic.Add(postStatic);
            }



            return listPostStatic;
        }

        public ActionResult GetProductAnalytics()
        {
            return Json(PrepareProductData(), JsonRequestBehavior.AllowGet);
        }

        public List<PostStatic> PrepareProductData()
        {
            List<PostStatic> listPostStatic = new List<PostStatic>();

            List<SanPham> listPost = db.SanPhams.ToList();

            for (int i = 1; i < 10; i++)
            {
                int Soluong = db.SanPhams.Count(x => x.LoaiSP.IdLoaiSP == i);
                PostStatic postStatic = new PostStatic();
                postStatic.Quanity = Soluong;
                if (i == 1)
                {
                    postStatic.NameTypePost = "Dụng cụ bếp";
                }
                if (i == 2)
                {
                    postStatic.NameTypePost = "Thực phẩm ăn liền";
                }

                listPostStatic.Add(postStatic);
            }



            return listPostStatic;
        }

        public ActionResult GetBillAnalytics()
        {
            return Json(PrepareBillData(), JsonRequestBehavior.AllowGet);
        }

        public List<PostStatic> PrepareBillData()
        {
            List<PostStatic> listPostStatic = new List<PostStatic>();

            List<HoaDon> listPost = db.HoaDons
                .OrderByDescending(Party => Party.Ngay)
                .Take(30)
                .ToList();

            List<string> LoopInDay = new List<string>();

            foreach(var Bill in listPost)
            {
                var Today = Bill.Ngay;

                if (LoopInDay.Any(Day => Day.Equals(Today.ToString())) == true)
                    continue;

                LoopInDay.Add(Today.ToString());

                string TotalIncome = db.HoaDons
                    .Where(Party => Party.Ngay.Month == Today.Month)
                    .Where(Party => Party.Ngay.Year == Today.Year)
                    .Where(Party => Party.Ngay.Day == Today.Day)
                    .Sum(Party => Party.TongTien).ToString();

                PostStatic postStatic = new PostStatic();
                postStatic.Quanity = int.Parse(TotalIncome);
                postStatic.NameTypePost = "Tổng Ngày " + Today.ToString("dd/MM/yyyy");

                listPostStatic.Add(postStatic);
            }

            return listPostStatic;
        }

    }
}