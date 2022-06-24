using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeathyFood.Models.Model
{
    public class DonHang
    {
        private string anh;
        private string sL;

        public int MaDH { get; set; }
        public int? MaUser { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string AnhSP { get; set; }
        public int SoL { get; set; }
        public double? Gia { get; set; }
        public double? TongTien { get; set; }
        public DateTime date { get; set; }
        public int MaPTTT { get; set; }
        public int UserID { get; }
        public DateTime? NgayDatHang { get; }

        public DonHang(int? maUser, int maSP, int maPTTT, string tenSP, string anhSP, int sL, double? gia, double? tongTien, DateTime? date) 
        {
           
            MaUser = maUser;
            MaSP = maSP;
            TenSP = tenSP;
            AnhSP = anhSP;
            SoL = sL;
            Gia = gia;
            TongTien = tongTien;
            date = date;
            MaPTTT = maPTTT;
        }

        public DonHang(int userID, int maSP, int maPTTT, string tenSP, string anh, string sL, double? gia, double? tongTien, DateTime? ngayDatHang)
        {
            UserID = userID;
            MaSP = maSP;
            MaPTTT = maPTTT;
            TenSP = tenSP;
            this.anh = anh;
            this.sL = sL;
            Gia = gia;
            TongTien = tongTien;
            NgayDatHang = ngayDatHang;
        }
    }
}