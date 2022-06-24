using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeathyFood.Models.Model
{
    public class GioHang
    {
        public int MaGH { get; set; }
        public int? MaUser { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string AnhSP { get; set; }
        public int? SL { get; set; }
        public double? Gia { get; set; }
        public double? ThanhTien { get; set; }

        public double? GiaKM { get; }
   

        public GioHang(int maSP, string tenSP) { }
        public GioHang(int maSP, string tenSP, string anhSP, int? sL, double? price, double? Giakm, int magh, int? ma_user) : this(maSP, tenSP)
        {
            MaSP = maSP;
            TenSP = tenSP;
            AnhSP = anhSP;
            SL = sL;
            Gia = price;
            GiaKM = Giakm;
            if (GiaKM == null || GiaKM == 0)
                ThanhTien = sL * Gia;
            else ThanhTien = sL * GiaKM;
            MaGH = magh;
            MaUser = ma_user;

        }

        //public GioHang(int maSP, string tenSP, string anh, int? sL, double? gia, double? giaKM, int maGioHang, int? userID) : this(maSP, tenSP)
        //{
        //    AnhSP = anh;
        //    SL = sL;
        //    Gia = gia;
        //    GiaKM = giaKM;
        //    MaGH = maGioHang;
        //    MaUser = userID;
        //}
    }
}