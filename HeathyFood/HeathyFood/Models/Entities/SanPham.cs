namespace HeathyFood.Models.Entities
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
            AnhSanPhams = new HashSet<AnhSanPham>();
            CTDonHangs = new HashSet<CTDonHang>();
            CTGioHangs = new HashSet<CTGioHang>();
            DanhGias = new HashSet<DanhGia>();
            YeuThiches = new HashSet<YeuThich>();
        }

        [Key]
        public int MaSP { get; set; }

        [StringLength(300)]
        [Required(ErrorMessage = "Mời nhập tên sản phẩm.")]

        public string TenSP { get; set; }

        public int MaLoai { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá")]
        public double? Gia { get; set; }

        public double? KhuyenMai { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn ngày.")]
        [Column(TypeName = "date")]
        public DateTime? NgaySX { get; set; }


        public int? HSD { get; set; }

        public int? KG { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mô tả")]
        public string Mota { get; set; }

        public int? SoLuong { get; set; }

        public int? Active { get; set; }

        public double? GiaKM { get; set; }

        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTGioHang> CTGioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YeuThich> YeuThiches { get; set; }
    }
}
