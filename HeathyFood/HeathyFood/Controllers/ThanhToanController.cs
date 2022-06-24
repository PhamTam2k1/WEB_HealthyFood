using HeathyFood.Models.Entities;
using HeathyFood.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeathyFood.Controllers
{
    public class ThanhToanController : Controller
    {
        ItemCart ItemCart = new ItemCart();

        FoodHeathyContext db = new FoodHeathyContext();
        // GET: ThanhToan
        public ActionResult Index(int id)
        {
            List<Models.Model.GioHang> list = ItemCart.getCart(id);
            return View(list);
        }
        
        public ActionResult ThanhToanDonHang(int id)
        {
            List<Models.Model.GioHang> list = ItemCart.getCart(id);
            double? tien = 0;
            
            foreach(var dh in list)
            {
                tien += dh.ThanhTien;
               
            }
            Models.Entities.DonHang donhang = new Models.Entities.DonHang(id,1,DateTime.Now,list.Count(),tien);
           
            db.DonHangs.Add(donhang);
            db.SaveChanges();
           int madh = donhang.MaHD;
            int id_gh=0;
            foreach (var dh in list)
            {
                CTDonHang ctDon = new CTDonHang(madh, dh.MaSP, dh.Gia, dh.SL, dh.ThanhTien);
               
                Models.Entities.CTGioHang CTgiohang = db.CTGioHangs.Find(dh.MaGH,dh.MaSP);
                id_gh = dh.MaGH;
                db.CTGioHangs.Remove(CTgiohang);
                db.CTDonHangs.Add(ctDon);
                db.SaveChanges();
            }
            Models.Entities.GioHang gio_hang = db.GioHangs.Find(id_gh);
            db.GioHangs.Remove(gio_hang);
            db.SaveChanges();
            Session["GioHang"] = "";
            var don = db.DonHangs.Where(d => d.MaHD == madh).FirstOrDefault();
            return View(don);
        }
    }
}