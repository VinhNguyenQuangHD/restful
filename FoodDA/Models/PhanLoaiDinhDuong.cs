using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDA.Models;

namespace FoodDA.Models
{
    public class PhanLoaiDinhDuong
    {
        FoodEntities db = new FoodEntities();
        public float CanNang { get; set; }

        public float ChieuCao { get; set; }

        public float BMI
        {
            get
            {
                { return CanNang / (ChieuCao * ChieuCao); }
            }
        }
    }
}