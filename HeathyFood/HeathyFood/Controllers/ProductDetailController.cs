using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace HeathyFood.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            FoodHeathyContext db = new FoodHeathyContext();

            var loai = db.SanPhams.Where(sp => sp.MaSP==id).Include(x => x.LoaiSanPham).SingleOrDefault();
           
            return View("Index", loai);
        }
        [HttpGet]
        public ActionResult LoadAnh(int id)
        {

            FoodHeathyContext db = new FoodHeathyContext();
            db.Configuration.ProxyCreationEnabled = false;
            List<AnhSanPham> dsanh = (db.AnhSanPhams.Where(x => x.MaSP == id)).ToList();
            int count_ = dsanh.Count();
            

            return Json(new { code = 200, dsanh = dsanh,count_=count_ }, JsonRequestBehavior.AllowGet);

           
        }

    }
}