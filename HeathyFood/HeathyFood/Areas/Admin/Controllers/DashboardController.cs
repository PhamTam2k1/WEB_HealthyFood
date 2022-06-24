using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeathyFood.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private FoodHeathyContext db = new FoodHeathyContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}