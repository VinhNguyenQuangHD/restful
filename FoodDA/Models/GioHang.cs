using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDA.Models;

namespace FoodDA.Models
{
    public class GioHang
    {
        FoodEntities db = new FoodEntities();
        public int idSanPham { get; set; }

        public string TenSP { get; set; }

        public string HinhAnh1 { get; set; }

        public double Gia { get; set; }

        public int Soluong { get; set; }

        public double ThanhTien
        {
            get
            {
                { return Soluong * Gia; }
            }
        }
        public GioHang(int IdSanPham)
        {
            idSanPham = IdSanPham;
            SanPham sanPham = db.SanPhams.Single(n => n.IdSanPham == idSanPham);
            TenSP = sanPham.TenSP;
            HinhAnh1 = sanPham.HinhAnh1;
            Gia = double.Parse(sanPham.Gia.ToString());
            Soluong = 1;
        }


    }
}