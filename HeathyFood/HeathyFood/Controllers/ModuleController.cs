using HeathyFood.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using HeathyFood.Models.Entities;

namespace HeathyFood.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        FoodHeathyContext db = new FoodHeathyContext();
        public ActionResult LoadCategory()
        {
            LoaiSanPhamDAO loaiDAO = new LoaiSanPhamDAO();
            var lst = loaiDAO.getList();


            return PartialView("LoadCategory", lst);
        }

        public ActionResult ProductDetail(int id)
        {


            var loai = db.SanPhams.Where(sp => sp.MaSP == id).Include(x => x.LoaiSanPham).SingleOrDefault();
            return PartialView("ProductDetail", loai);
        }
        public ActionResult TrendingProduct()
        {
            //var donvi = _db.DONVIs.GroupBy(s => s.MaDV)
            //    .Select(g => new {
            //        g.FirstOrDefault().TenDV,
            //        g.FirstOrDefault().NHANVIENs.Count
            //    });

            var pro = db.SanPhams.GroupBy(p => p.MaSP).Select(g => new
            {
                g.FirstOrDefault().TenSP,
                g.FirstOrDefault().Anh,
                g.FirstOrDefault().Gia,
                g.FirstOrDefault().KhuyenMai,
                g.FirstOrDefault().KG,
                g.FirstOrDefault().GiaKM,
                g.FirstOrDefault().CTDonHangs.Count
            }).Take(5);

            return PartialView("TrendingProduct", pro);


        }
    }
}