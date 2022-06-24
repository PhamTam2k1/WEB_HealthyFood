namespace HeathyFood.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GioHang()
        {
            CTGioHangs = new HashSet<CTGioHang>();
        }

        [Key]
        public int MaGioHang { get; set; }

        public int? UserID { get; set; }

        public int? TongSL { get; set; }

        public double? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTGioHang> CTGioHangs { get; set; }

        public virtual User User { get; set; }
        public GioHang(int? iduser, int? sl, double? tien)
        {
            UserID = iduser;
            TongSL = sl;
            TongTien = tien;
        }
    }
}
