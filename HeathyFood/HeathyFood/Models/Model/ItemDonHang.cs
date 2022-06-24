using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace HeathyFood.Models.Model
{
    public class ItemDonHang
    {
        FoodHeathyContext db = new FoodHeathyContext();
        //public List<DonHang> AddDonHang(DonHang dh)
        //{
        //    List<DonHang> list = new List<DonHang>();

        //    var lst_donhang = db.DonHangs.FirstOrDefault(x => x.UserID == dh.MaUser);
        //    var lst_Ct_donhang = db.CTDonHangs.Where(k => k.MaSP == dh.MaSP).Include(x => x.SanPham).ToList();
        //    if (lst_donhang != null)
        //    {
        //        foreach (var item in lst_Ct_donhang)
        //        {
        //            DonHang donhang = new DonHang(lst_donhang.UserID, item.MaSP, lst_donhang.MaPTTT, item.SanPham.TenSP, item.SanPham.Anh, item.SL, item.SanPham.Gia, lst_donhang.TongTien, lst_donhang.NgayDatHang);
        //            list.Add(donhang);

        //        }
              
        //        System.Web.HttpContext.Current.Session["DonHang"] = list;
        //    }


        //    if (System.Web.HttpContext.Current.Session["DonHang"].Equals(""))
        //    {

        //        list.Add(dh);
        //        Models.Entities.DonHang donhang = new Models.Entities.DonHang(dh.MaUser,dh.MaPTTT,dh.NgayDatHang, dh.SL, dh.TongTien);
        //        Models.Entities.CTDonHang CTDonHang = new CTDonHang(dh.MaDH, dh.MaSP,dh.Gia, dh.SL, dh.TongTien);
        //        db.DonHangs.Add(donhang);
        //        db.CTDonHangs.Add(CTDonHang);
        //        db.SaveChanges();
                
        //        System.Web.HttpContext.Current.Session["DonHang"] = list;
        //    }
        //    else
        //    {
        //        list = (List<DonHang>)System.Web.HttpContext.Current.Session["DonHang"];
               
          
        //            list.Add(dh);

        //            Models.Entities.CTDonHang CTDonHang = new CTDonHang(dh.Madh, dh.MaSP, dh.SL, dh.ThanhTien);

        //            db.CTDonHangs.Add(CTDonHang);
        //            db.SaveChanges();
                   
        //            System.Web.HttpContext.Current.Session["DonHang"] = list;
        //        }

        //    }
        //    return list;
        //}
        //public void UpdateCart()
        //{

        //}
        //public void DelCart(int madh, int masp)
        //{
        //    List<DonHang> list = (List<DonHang>)System.Web.HttpContext.Current.Session["DonHang"];
        //    int vt = 0;
        //    foreach (var item in list)
        //    {
        //        if (item.MaSP == masp && item.Madh == madh)
        //        {
        //            list.RemoveAt(vt);


        //            CTDonHang da = db.CTDonHangs.Find(madh, masp);
        //            db.CTDonHangs.Remove(da);



        //            //CTDonHang CTDonHang = new CTDonHang();
        //            //CTDonHang.SL = list[vt].SL;
        //            //CTDonHang.TongTien = list[vt].ThanhTien;
        //            //db.Entry(CTDonHang).State = EntityState.Modified;
        //            db.SaveChanges();
        //            break;

        //        }
        //        vt++;
        //    }
        //    System.Web.HttpContext.Current.Session["SoLuong"] = list.Count();
        //    System.Web.HttpContext.Current.Session["DonHang"] = list;
        //}
        //public List<DonHang> getCart(int id)
        //{
        //    FoodHeathyContext db = new FoodHeathyContext();
        //    List<Models.Model.DonHang> list = new List<Models.Model.DonHang>();

        //    var lst_donhang = db.DonHangs.FirstOrDefault(x => x.UserID == id);
        //    var lst_Ct_donhang = db.CTDonHangs.Where(x => x.MaDonHang == lst_donhang.MaDonHang).Include(x => x.SanPham).ToList();
        //    if (lst_donhang != null)
        //    {
        //        foreach (var item in lst_Ct_donhang)
        //        {
        //            Models.Model.DonHang donhang = new Models.Model.DonHang(item.MaSP, item.SanPham.TenSP, item.SanPham.Anh, item.SL, item.SanPham.Gia, item.SanPham.GiaKM, lst_donhang.MaDonHang, lst_donhang.UserID);
        //            list.Add(donhang);
        //        }

        //        System.Web.HttpContext.Current.Session["DonHang"] = list;
        //    }
        //    if (System.Web.HttpContext.Current.Session["DonHang"].Equals(""))
        //    {
        //        return null;
        //    }
        //    return (List<DonHang>)System.Web.HttpContext.Current.Session["DonHang"];
        //}
    }
}