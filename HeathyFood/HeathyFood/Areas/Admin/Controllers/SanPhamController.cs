using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeathyFood.Areas.Admin.Controllers;
using HeathyFood.DAO;
using HeathyFood.Models.Entities;

namespace HeathyFood.Areas.Admin
{
    public class SanPhamController : BaseController
    {
        // private FoodHeathyContext db = new FoodHeathyContext();
        SanPhamDAO spDao = new SanPhamDAO();
        public ActionResult Index()
        {
            
            FoodHeathyContext db = new FoodHeathyContext();
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
            
            return View();
        }
        [HttpGet]
        public ActionResult dsSanPham(int trang)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            try
            {
                var dsSP = (from da in db.SanPhams.Where(c => c.Active>0)
                            select new
                            {
                                MaSP = da.MaSP,
                                TenSP = da.TenSP,
                                MaLoai = da.MaLoai,
                                 Gia = da.Gia,
                                KhuyenMai = da.KhuyenMai,
                                NgaySX = da.NgaySX,
                                HSD =da.HSD,
                                KG = da.KG,
                                Mota= da.Mota,
                                SoLuong = da.SoLuong,
                                Active = da.Active
                            }).ToList();

                int pageSize = 5;
                int soTrang = dsSP.Count() % pageSize == 0 ? dsSP.Count() / pageSize : (dsSP.Count() / pageSize )+1;
                var kqht = dsSP.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { dsSP = kqht, soTrang = soTrang }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy dữ liệu thất bại" + ex.Message
                }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult dsSanPhamCbXoa()
        {
            FoodHeathyContext db = new FoodHeathyContext();
            try
            {
                var dsSP = (from da in db.SanPhams.Where(c => c.Active == 0)
                            select new
                            {
                                MaSP = da.MaSP,
                                TenSP = da.TenSP,
                                MaLoai = da.MaLoai,
                                Gia = da.Gia,
                                KhuyenMai = da.KhuyenMai,
                                NgaySX = da.NgaySX,
                                HSD = da.HSD,
                                KG = da.KG,
                                Mota = da.Mota,
                                SoLuong = da.SoLuong,
                                Active = da.Active
                            }).ToList();


                return Json(new { dsSP = dsSP }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy dữ liệu thất bại" + ex.Message
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Create()
        {
            FoodHeathyContext db = new FoodHeathyContext();
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham sp)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
            if (ModelState.IsValid)
            {

                spDao.Insert(sp);
                return RedirectToAction("Index");
            }
            else
               
                return View(sp);
  
        }
 
        public ActionResult Edit(int id)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
            if (id ==null || ViewBag.MaLoai == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = spDao.getRow(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            if (ModelState.IsValid)
            {
                spDao.Update(sp);
                return RedirectToAction("Index");
            }


            return View(sp);
        }
        [HttpGet]
        public ActionResult chiTietSP(int id)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
       
        [HttpPost]
        public ActionResult xoaSanPham(int id)
        {
            FoodHeathyContext db = new FoodHeathyContext();
            db.Configuration.ProxyCreationEnabled = false;
            SanPham da = spDao.getRow(id);
            spDao.Delete(da);
               
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult Status(int id)
        {
            if (id == null )
            {
                return RedirectToAction("Index","SanPham");
            }
            SanPham sanPham = spDao.getRow(id);
            if (sanPham == null)
            {
                return RedirectToAction("Index", "SanPham");

            }
            sanPham.Active = (sanPham.Active == 1) ? 2 : 1;
            spDao.Update(sanPham);
            return RedirectToAction("Index", "SanPham");
        }
        public ActionResult Trash()
        {
            return View(spDao.getList("Trash"));
        }
        public ActionResult DelTrash(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            SanPham sanPham = spDao.getRow(id);
            if (sanPham == null)
            {
                return RedirectToAction("Index", "SanPham");

            }
            sanPham.Active = 0;
            spDao.Update(sanPham);
            return RedirectToAction("Index", "SanPham");
        }
        public ActionResult ReTrash(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Trash", "SanPham");
            }
            SanPham sanPham = spDao.getRow(id);
            if (sanPham == null)
            {
                return RedirectToAction("Trash", "SanPham");

            }
            sanPham.Active = 1;
            spDao.Update(sanPham);
            return RedirectToAction("Trash", "SanPham");
        }

    }
}
