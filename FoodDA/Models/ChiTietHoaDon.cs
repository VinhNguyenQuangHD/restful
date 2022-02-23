namespace FoodDA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key, Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHoaDon { get; set; }

        [Key, Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSanPham { get; set; }

        public double ThanhTien { get; set; }

        public int SoLuong { get; set; }

       

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
 
    }
}
