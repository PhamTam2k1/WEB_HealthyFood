using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeathyFood.Models.Entities;

namespace HeathyFood.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private FoodHeathyContext db = new FoodHeathyContext();
          public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dsUser()
        {
            try
            {
                var dsUser = (from da in db.Users
                            select new
                            {
                                UserID = da.UserID,
                                FirstName = da.FirstName,
                                LastName = da.LastName,
                                Sdt = da.Sdt,
                                Email = da.Email,
                                NgaySinh = da.NgaySinh,
                                DiaChi =da.DiaChi,
                                Password = da.Password,
                                Anh= da.Anh,
                                isAdmin = da.isAdmin
                            }).ToList();

                //return View("Index", dsUser);
                return Json(new { dsUser = dsUser }, JsonRequestBehavior.AllowGet);
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
        public ActionResult chiTietUser(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.Users.SingleOrDefault(x => x.UserID == id);
            return Json(new { code = 200, da = da }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult editUser(int userid, string firstname, string lastname, string diachi, string sdt, string email, DateTime ngaysinh, int isAdmin)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var da = db.Users.SingleOrDefault(x => x.UserID == userid);

            da.FirstName = firstname;
            da.DiaChi = diachi;
            da.Sdt = sdt;
            da.isAdmin = isAdmin;
            da.NgaySinh = ngaysinh;
            
            da.LastName = lastname;
            da.Email = email;
            
            db.SaveChanges();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }

        [HttpPost]
        public ActionResult xoaUser(int id)
        {
           
                User da = db.Users.Find(id);
                db.Users.Remove(da);
                db.SaveChanges();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);


        }
    }
}
