namespace FoodDA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int IdSanPham { get; set; }

        public int IdLoaiSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(50)]
        [Display(Name = "TenSP")]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Tồn không được để trống, lớn hơn 0")]
        [Display(Name = "Ton")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int? Ton { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "MoTa")]
        public string MoTa { get; set; }

        [StringLength(200)]
        public string HinhAnh1 { get; set; }    

        [StringLength(200)]
        public string HinhAnh2 { get; set; }

        [Required(ErrorMessage = "Giá không được để trống, lớn hơn 0")]
        [Display(Name = "Gia")]
        public double Gia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
