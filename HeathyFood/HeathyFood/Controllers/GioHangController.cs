using HeathyFood.Models.Entities;
using HeathyFood.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace HeathyFood.Controllers
{
    public class GioHangController : Controller
    {
        FoodHeathyContext db = new FoodHeathyContext();
        ItemCart ItemCart = new ItemCart();
        // GET: GioHang
        public ActionResult Index(int id)
        {
            List<Models.Model.GioHang> list = ItemCart.getCart(id);
            return View(list);
        }
        public ActionResult AddCart(int id,int user_id)
        {
            
            SanPham sp = db.SanPhams.Find(id);
            Models.Entities.GioHang Gio = db.GioHangs.FirstOrDefault(x => x.UserID == user_id);
            if (Gio == null)
            {


                db.GioHangs.Add(new Models.Entities.GioHang(user_id, 1, 0));
                db.SaveChanges();

            }
            Models.Entities.GioHang Gio1 = db.GioHangs.FirstOrDefault(x => x.UserID == user_id);

            Models.Model.GioHang gh = new Models.Model.GioHang(sp.MaSP, sp.TenSP, sp.Anh, 1, sp.Gia, sp.GiaKM, Gio1.MaGioHang, user_id);
            List<Models.Model.GioHang> list = ItemCart.AddCart(gh);
            return RedirectToAction("Index", "Categories");
        }
        public ActionResult View_GioHang(int user_id)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            List<Models.Model.GioHang> list = new List<Models.Model.GioHang>();
           
            var lst_gio = db.GioHangs.FirstOrDefault(x => x.UserID == user_id);
            
            if (lst_gio != null)
            {
                var lst_Ct_Gio = db.CTGioHangs.Where(x => x.MaGioHang == lst_gio.MaGioHang).Include(x => x.SanPham).ToList();
                foreach (var item in lst_Ct_Gio)
                {
                    Models.Model.GioHang gio = new Models.Model.GioHang(item.MaSP, item.SanPham.TenSP, item.SanPham.Anh, item.SL, item.SanPham.Gia, item.SanPham.GiaKM, lst_gio.MaGioHang, lst_gio.UserID);
                    list.Add(gio);
                }

               Session["GioHang"] = list;
            }
            if (Session["GioHang"].Equals(""))
            {
                return RedirectToAction("Index", "GioHang", new { id = user_id });
            }
            List < Models.Model.GioHang > lst= (List<Models.Model.GioHang>)Session["GioHang"];
            return View("View_GioHang", lst);

        }
        public ActionResult CartDel(int magh,int masp,int user_id)
        {
            ItemCart.DelCart(magh, masp);
            return  RedirectToAction("Index","GioHang",new { id= user_id});
        }
        public ActionResult SLHangTrongGio(int id)
        {

            List<Models.Model.GioHang> list = ItemCart.getCart(id);
            return PartialView("SLHangTrongGio",list);
        }
        public ActionResult CartUpdate(int id, FormCollection form)
        {
           
            if (!string.IsNullOrEmpty(form["CAPNHAT"])) { 
                var listqty = form["SOLUONG"]; //1,2,3,4,5,6,7
                var listarr = listqty.Split(',');
                ItemCart.UpdateCart(id,listarr);
               
             }
            return RedirectToAction("Index", "Giohang", new { id = id });
           
        }
    }
}