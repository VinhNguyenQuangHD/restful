using FoodDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDA.Controllers
{
    public class TKController : Controller
    {
        // GET: TK
        private FoodEntities db = new FoodEntities();
        // GET: 
        public ActionResult Index()
        {
            return View();
        }
        /// Gửi Json
        public ActionResult PostStatic()
        {
            return Json(ResultPost(), JsonRequestBehavior.AllowGet);
        }

        /// Tạo listStatic 

        public List<PostStatic> ResultPost()
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

    }
}