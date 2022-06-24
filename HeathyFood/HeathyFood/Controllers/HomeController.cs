
using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
namespace HeathyFood.Controllers
{
    public class HomeController : Controller
    {
        FoodHeathyContext db = new FoodHeathyContext();
        public ActionResult Index()
        {
           
            return View();
        }


        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login","Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.Users.SingleOrDefault(u => u.Email == email && u.Password == f_password);
               
                if (data != null)
                {
                    //add session
                    Session["user"] = data;
                    Session["LastName"] = data.LastName;
                    Session["FirstName"] = data.FirstName;
                    Session["FullName"] = data.LastName + " " + data.FirstName;
                    Session["Email"] = data.Email;
                    Session["SDT"] = data.Sdt;
                    Session["DiaChi"] = data.DiaChi;
                    Session["NgaySinh"] = data.NgaySinh;
                    Session["UserID"] = data.UserID;
                    
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index","Home");
        }
    }


}
