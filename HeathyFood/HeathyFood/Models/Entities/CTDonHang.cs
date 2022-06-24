namespace HeathyFood.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTDonHang")]
    public partial class CTDonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CTDonHang()
        {
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public double? GiaBan { get; set; }

        public int? SoLuong { get; set; }

        public double? SoTien { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual SanPham SanPham { get; set; }
      
        public CTDonHang(int mahd, int masp,double? gia, int? sl, double? tien)
        {
            MaHD = mahd;
            GiaBan = gia;
            MaSP = masp;
            SoLuong = sl;
            SoTien = tien;
        }
    }
}
