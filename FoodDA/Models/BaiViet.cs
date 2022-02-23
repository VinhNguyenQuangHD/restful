namespace FoodDA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViet()
        {
            Cmts = new HashSet<Cmt>();
        }

        [Key]
        public int IdBaiViet { get; set; }

        public int IdChuDe { get; set; }

        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]

        public DateTime Ngay { get; set; }


        [Required(ErrorMessage = "Tên bài viết không được để trống")]
        [Display(Name = "Tên Bài Viết")]
        public string TenBaiViet { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô Ta")]
        public string MoTa { get; set; }

  
        [Required(ErrorMessage = "Tên nguyên liệu không được để trống")]
        public string NguyenLieu { get; set; }

        public string NguyenLieu1 { get; set; }

        public string NguyenLieu2 { get; set; }

        public string NguyenLieu3 { get; set; }

        public string NguyenLieu4 { get; set; }

        public string NguyenLieu5 { get; set; }

        public string NguyenLieu6 { get; set; }

        public string NguyenLieu7 { get; set; }

        public string NguyenLieu8 { get; set; }

        [Required(ErrorMessage = "Hướng dẫn không được để trống")]
        public string HuongDan { get; set; }

        public string HinhAnh1 { get; set; }

        public string HinhAnh2 { get; set; }

        public string HinhAnh3 { get; set; }

        [Required(ErrorMessage = "Link Youtube không được để trống")]
        public string LinkYoutube { get; set; }

        [Required(ErrorMessage = "Lượng kcal không được để trống")]
        public double? kcal { get; set; }

        public virtual ChuDe ChuDe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cmt> Cmts { get; set; }
    }
}
