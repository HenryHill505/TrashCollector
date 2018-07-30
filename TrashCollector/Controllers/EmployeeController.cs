using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using Microsoft.AspNet.Identity;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            int zip = db.Users.Where(u => u.Id == id).Select(u => u.ZipCode).FirstOrDefault();
            List<Pickup> pickups = db.Pickups.Where(p => p.User.ZipCode == zip).ToList();
            return View(pickups);
        }
    }
}