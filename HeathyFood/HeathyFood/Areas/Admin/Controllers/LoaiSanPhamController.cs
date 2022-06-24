using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeathyFood.DAO;
using HeathyFood.Models.Entities;

namespace HeathyFood.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : BaseController
    {
        private FoodHeathyContext db = new FoodHeathyContext();
        LoaiSanPhamDAO loaiDAO = new LoaiSanPhamDAO();
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsLoaiSanPham()
        {
            var dsTL = (from da in db.LoaiSanPhams
                        select new
                        {
                            MaLoai = da.MaLoai,
                            TenLoai = da.TenLoai

                        }).ToList();
            return Json(new { dsTL = dsTL }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult addLoaiSanPham( string tentl)
        {
            try
            {
                var l = new LoaiSanPham();
              
                l.TenLoai = tentl;
                loaiDAO.Insert(l);
               

                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("Index");



        }

        
        [HttpGet]
        public ActionResult chiTietTL(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.LoaiSanPhams.SingleOrDefault(x => x.MaLoai == id);

            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]

        public ActionResult editLoaiSanPham(int matl, string tentl)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.LoaiSanPhams.SingleOrDefault(x => x.MaLoai == matl);

            da.TenLoai = tentl;

            db.SaveChanges();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
        [HttpPost]
        public ActionResult xoaLoaiSanPham(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var p = db.SanPhams.Where(d => d.MaLoai == id);

            if (p.Count() >= 1) return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            else
            {
                LoaiSanPham da = loaiDAO.getRow(id);
                loaiDAO.Delete(da);
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }


            //  return View("Index");

        }
    }
}
