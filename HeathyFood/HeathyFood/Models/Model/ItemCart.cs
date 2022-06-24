using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;


namespace HeathyFood.Models.Model
{
    public class ItemCart
    {
        FoodHeathyContext db = new FoodHeathyContext();
        public List<GioHang> AddCart(GioHang gh)
        {
            List<GioHang> list = new List<GioHang>();

            //var lst_gio = db.GioHangs.FirstOrDefault(x => x.UserID == gh.MaUser);
            //var lst_Ct_Gio = db.CTGioHangs.Where(k => k.MaSP == gh.MaSP).Include(x => x.SanPham).ToList();
            //if (lst_gio != null)
            //{
            //    foreach (var item in lst_Ct_Gio)
            //    {
            //        GioHang gio = new GioHang(item.MaSP, item.SanPham.TenSP, item.SanPham.Anh, item.SL, item.SanPham.Gia, item.SanPham.GiaKM, lst_gio.MaGioHang, lst_gio.UserID);
            //        list.Add(gio);

            //    }

            //    System.Web.HttpContext.Current.Session["GioHang"] = list;
            //}


            if (System.Web.HttpContext.Current.Session["GioHang"].Equals(""))
            {

                list.Add(gh);
                Models.Entities.GioHang gio_hang = new Models.Entities.GioHang(gh.MaUser, gh.SL, gh.ThanhTien);
                Models.Entities.CTGioHang CTgiohang = new CTGioHang(gh.MaGH, gh.MaSP, gh.SL, gh.ThanhTien);
                db.GioHangs.Add(gio_hang);
                db.CTGioHangs.Add(CTgiohang);
                db.SaveChanges();
                
                System.Web.HttpContext.Current.Session["GioHang"] = list;
            }
            else
            {
                list = (List<GioHang>)System.Web.HttpContext.Current.Session["GioHang"];
                if (list.Where(s => s.MaSP == gh.MaSP).Count() != 0)
                {
                    int vt = 0;
                    foreach (var item in list)
                    {
                        if (item.MaSP == gh.MaSP)
                        {
                            list[vt].SL += 1;
                            if (list[vt].GiaKM != null)
                                list[vt].ThanhTien = list[vt].SL * list[vt].GiaKM;
                            else list[vt].ThanhTien = list[vt].SL * list[vt].Gia;

                            CTGioHang da = db.CTGioHangs.Where(g => g.MaGioHang==gh.MaGH && g.MaSP==gh.MaSP).FirstOrDefault();
                            db.CTGioHangs.Remove(da);
                            db.SaveChanges();
                            gh.SL = list[vt].SL;
                            gh.ThanhTien = list[vt].ThanhTien;
                           
                            Models.Entities.CTGioHang CTgiohang = new CTGioHang(gh.MaGH, gh.MaSP, gh.SL, gh.ThanhTien);
                            db.CTGioHangs.Add(CTgiohang);

                            //CTGioHang CTgiohang = new CTGioHang();
                            //CTgiohang.SL = list[vt].SL;
                            //CTgiohang.TongTien = list[vt].ThanhTien;
                            //db.Entry(CTgiohang).State = EntityState.Modified;
                            db.SaveChanges();
                           
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["GioHang"] = list;
                }
                else
                {
                    list.Add(gh);
                   
                    Models.Entities.CTGioHang CTgiohang = new CTGioHang(gh.MaGH, gh.MaSP, gh.SL, gh.ThanhTien);
                  
                    db.CTGioHangs.Add(CTgiohang);
                    db.SaveChanges();
                    
                    System.Web.HttpContext.Current.Session["GioHang"] = list;
                }

            }
            return list;
        }
        public void UpdateCart(int id, string[] arr)
        {
            List<Models.Model.GioHang> list = this.getCart(id);
            int vt = 0;
            foreach(GioHang gio in list)
            {
                
                list[vt].SL=int.Parse(arr[vt]);
                if(list[vt].GiaKM==null)
                list[vt].ThanhTien = (list[vt].SL* list[vt].Gia);
                else list[vt].ThanhTien = (list[vt].SL * list[vt].GiaKM);
               
                var CT = new CTGioHang(list[vt].MaGH, list[vt].MaSP, list[vt].SL, list[vt].ThanhTien);
                var ctgio = db.CTGioHangs.Find(list[vt].MaGH, list[vt].MaSP);
                db.CTGioHangs.Remove(ctgio);
                db.SaveChanges();
                db.CTGioHangs.Add(CT);
                db.SaveChanges();
                vt++;

            }
            System.Web.HttpContext.Current.Session["GioHang"] = list;
            
        }
        public void DelCart(int magh,int masp)
        {
            List<GioHang> list = (List<GioHang>)System.Web.HttpContext.Current.Session["GioHang"];
            int vt = 0;
            
            foreach (var item in list)
            {
                if (item.MaSP == masp &&item.MaGH==magh)
                {
                    list.RemoveAt(vt);
                 

                  var da = db.CTGioHangs.Find(magh, masp);
                    db.CTGioHangs.Remove(da);
                  

                  
                    //CTGioHang CTgiohang = new CTGioHang();
                    //CTgiohang.SL = list[vt].SL;
                    //CTgiohang.TongTien = list[vt].ThanhTien;
                    //db.Entry(CTgiohang).State = EntityState.Modified;
                    db.SaveChanges();
                    break;

                }
                vt++;
            }

            if (list.Count() <1) {
               var g = db.GioHangs.Find(magh);
                db.GioHangs.Remove(g);
                db.SaveChanges();
            }
           
            System.Web.HttpContext.Current.Session["GioHang"] = list;
        }
        public List<GioHang> getCart(int id)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            List<Models.Model.GioHang> list = new List<Models.Model.GioHang>();

            var lst_gio = db.GioHangs.FirstOrDefault(x => x.UserID ==id);
            
            if (lst_gio != null)
            {
                var lst_Ct_Gio = db.CTGioHangs.Where(x => x.MaGioHang == lst_gio.MaGioHang).Include(x => x.SanPham).ToList();
                foreach (var item in lst_Ct_Gio)
                {
                    Models.Model.GioHang gio = new Models.Model.GioHang(item.MaSP, item.SanPham.TenSP, item.SanPham.Anh, item.SL, item.SanPham.Gia, item.SanPham.GiaKM, lst_gio.MaGioHang, lst_gio.UserID);
                    list.Add(gio);
                }

                System.Web.HttpContext.Current.Session["GioHang"] = list;
            }
            return HttpContext.Current.Session["GioHang"].Equals("") ? null : (List<GioHang>)System.Web.HttpContext.Current.Session["GioHang"];
        }
    }
}