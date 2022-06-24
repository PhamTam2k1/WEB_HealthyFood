namespace HeathyFood.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            CTDonHangs = new HashSet<CTDonHang>();
        }

        [Key]
        public int MaHD { get; set; }

        public int? UserID { get; set; }

        public int MaPTTT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDatHang { get; set; }

        public int? TongSL { get; set; }

        public double? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHangs { get; set; }

        public virtual PhuongThucTT PhuongThucTT { get; set; }

        public virtual User User { get; set; }

        public DonHang(int? user_id,int pttt,DateTime? ngaydathang, int sl, double? tongtien)
        {
            UserID = user_id;
            MaPTTT = pttt;
            NgayDatHang = DateTime.Now;
            TongSL = sl;
            TongTien = tongtien;
        }
    }
}
