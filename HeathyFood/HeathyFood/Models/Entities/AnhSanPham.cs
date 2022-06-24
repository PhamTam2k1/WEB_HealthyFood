namespace HeathyFood.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSanPham")]
    public partial class AnhSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaAnh { get; set; }

        public int MaSP { get; set; }

        [StringLength(200)]
        public string TenAnh { get; set; }

        public string URL { get; set; }

        public int? isMain { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
