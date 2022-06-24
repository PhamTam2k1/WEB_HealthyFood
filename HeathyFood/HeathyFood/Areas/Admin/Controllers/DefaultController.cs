using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HeathyFood.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        FoodHeathyContext db = new FoodHeathyContext();
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            var mail = email;
            var pass = GetMD5(password);
            
            var acc = db.Users.SingleOrDefault(u => u.Email == mail && u.Password == pass && u.isAdmin==1);
           
            if (acc != null)
            {
                // đăng nhập thành công
                Session["admin"] = acc;
                Session["FullName_Admin"] = acc.LastName + " " + acc.FirstName;

                return RedirectToAction("Index", "SanPham");
            }else 
            return View();
        }
        public ActionResult CreateAccount()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "Default");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}