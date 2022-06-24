using HeathyFood.DAO;
using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PagedList;

namespace HeathyFood.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        SanPhamDAO sanPhamDAO = new SanPhamDAO();
        FoodHeathyContext db = new FoodHeathyContext();
        public ActionResult Index(int? page,string SearchString = "")
        {
            
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = page ?? 1;
            if (SearchString != "")
            {
                IPagedList<SanPham> loai1 = db.SanPhams.Where(k => k.Active == 1).OrderBy(k => k.MaSP).Include(x => x.LoaiSanPham).Where(s => s.TenSP.ToUpper().Contains(SearchString.ToUpper())).ToPagedList(pageNumber, pageSize);
                return View("Index", loai1);
            }

            IPagedList<SanPham> loai = db.SanPhams.Where(k => k.Active == 1).OrderBy(k => k.MaSP).Include(x => x.LoaiSanPham).ToPagedList(pageNumber, pageSize);
           
           

            return View("Index", loai);
        }
        public ActionResult LoadCate()
        {
            var loai = db.LoaiSanPhams.ToList();
            return PartialView("LoadCate",loai);
        }

        [HttpGet]
        public ActionResult LoadProductCategory(int id, int? page)
        {
           
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = page ?? 1;
            IPagedList<SanPham> product = db.SanPhams.Where(sp => sp.MaLoai == id).OrderBy(k => k.MaSP).Include(x => x.LoaiSanPham).ToPagedList(pageNumber, pageSize); 
            return View(product);
        }
      
        
    }
}