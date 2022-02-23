using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;

namespace Food.Controllers
{
    public class CongThucTheoChuDeController : Controller
    {
        FoodEntities db = new FoodEntities();
        // GET: CongThucTheoChuDe
        public ActionResult CongThucChudePartial()
        {
            var CongThuc = db.BaiViets.Take(4).ToList();
            return View(CongThuc);
        }
        public ActionResult CongThucChudeChienPartial(int idchudechien=4)
        {
            var CongThucchien = db.BaiViets.Where(n=>n.IdChuDe==idchudechien).Take(4).ToList();
            return View(CongThucchien);
        }
        public ActionResult CongThucChudeNuongPartial(int idchudechien = 5)
        {
            var CongThucNuong = db.BaiViets.Where(n => n.IdChuDe == idchudechien).Take(4).ToList();
            return View(CongThucNuong);
        }
        public ActionResult CongThucChudeSoupPartial(int idchudeSoup = 2)
        {
            var CongThucSoup = db.BaiViets.Where(n => n.IdChuDe == idchudeSoup).Take(4).ToList();
            return View(CongThucSoup);
        }
        public ActionResult CongThucChudeXaoPartial(int idchudeXao = 3)
        {
            var CongThucXao = db.BaiViets.Where(n => n.IdChuDe ==idchudeXao).Take(4).ToList();
            return View(CongThucXao);
        }

    }
}