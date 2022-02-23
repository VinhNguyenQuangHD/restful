namespace FoodDA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cmt")]
    public partial class Cmt
    {
        [Key]
        public int IdCmt { get; set; }

        public int IdBaiViet { get; set; }

        public int IdNguoiDung { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public int? rating { get; set; }

        public DateTime Ngay { get; set; }

        public virtual BaiViet BaiViet { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
