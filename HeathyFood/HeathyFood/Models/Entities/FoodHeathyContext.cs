using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HeathyFood.Models.Entities
{
    public partial class FoodHeathyContext : DbContext
    {
        public FoodHeathyContext()
            : base("name=HeathyFoodContext")
        {
        }

        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CTDonHang> CTDonHangs { get; set; }
        public virtual DbSet<CTGioHang> CTGioHangs { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<PhuongThucTT> PhuongThucTTs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<YeuThich> YeuThiches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<DanhGia>()
                .Property(e => e.NoiDung)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Sdt)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.GioHangs)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();
        }
    }
}
