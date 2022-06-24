using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeathyFood.DAO;
using HeathyFood.Models.Entities;

namespace HeathyFood.Areas.Admin.Controllers
{
    public class AnhSanPhamController : BaseController
    {
        private FoodHeathyContext db = new FoodHeathyContext();
        AnhDAO anhDAO = new AnhDAO();
        public ActionResult Index()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");

            return View();
        }
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            //return PartialView("modal_Th");
            return View();
        }
        [HttpPost]
        public ActionResult Create(AnhSanPham anh, HttpPostedFileBase uploadhinh)
        {

            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");

            if (ModelState.IsValid)
            {
              //  anh = new AnhSanPham();
                int id_last = db.AnhSanPhams.ToList().Last().MaAnh;
                anh.MaAnh = id_last + 1;
                db.AnhSanPhams.Add(anh);

                 db.SaveChanges();
                //db.SaveChanges();
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {

                    int id = anh.MaAnh;
                    string _FileName = "";
                    _FileName = uploadhinh.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Publics/images/SanPham"), _FileName);
                    uploadhinh.SaveAs(_path);
                    AnhSanPham unAnh = db.AnhSanPhams.FirstOrDefault(x => x.MaAnh == id);
                    unAnh.URL = _path;
                    unAnh.TenAnh = _FileName;
                    

                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            else
                //return Json(new { code =500 }, JsonRequestBehavior.AllowGet);
                return View(anh);
            // return RedirectToAction("Loi");

        }

        public ActionResult Edit(int id)
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            if (id == null || ViewBag.MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhSanPham anh = db.AnhSanPhams.Find(id);
            if (anh == null)
            {
                return HttpNotFound();
            }
            return View(anh);
        }
        [HttpPost]
        public ActionResult Edit(AnhSanPham anh, HttpPostedFileBase uploadhinh)
        {
            AnhSanPham unanh = db.AnhSanPhams.FirstOrDefault(x => x.MaAnh == anh.MaAnh);
            unanh.TenAnh = anh.TenAnh;
            unanh.MaSP = anh.MaSP;
            unanh.URL= anh.URL;
            unanh.isMain = anh.isMain;


            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {

                int id = anh.MaAnh;
                string _FileName = "";

                _FileName = uploadhinh.FileName;
                string _path = Path.Combine(Server.MapPath("~/Publics/images/SanPham"), _FileName);
                uploadhinh.SaveAs(_path);
                unanh.URL = _path;
                unanh.TenAnh = _FileName;


            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult dsAnh(int trang)
        {
            try
            {
                int pageSize = 5;

                var dsanh = (from da in db.AnhSanPhams join sp in db.SanPhams on da.MaSP equals sp.MaSP
                            select new
                            {
                                MaAnh = da.MaAnh,
                                TenAnh = da.TenAnh,
                                MaSP = da.MaSP,
                                URL = da.URL,
                                isMain = da.isMain,
                                TenSP= sp.TenSP
                            }).ToList();

                int soTrang = dsanh.Count() % pageSize == 0 ? dsanh.Count() / pageSize : (dsanh.Count() / pageSize) + 1;
                var kqht = dsanh.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, soTrang=soTrang, dsanh = kqht }, JsonRequestBehavior.AllowGet);
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
        public ActionResult chiTietanh(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.AnhSanPhams.SingleOrDefault(x => x.MaAnh == id);
            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public ActionResult xoaAnhSanPham(int id)
        {

            AnhSanPham da = db.AnhSanPhams.Find(id);
            db.AnhSanPhams.Remove(da);
            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);


        }
    }
}
