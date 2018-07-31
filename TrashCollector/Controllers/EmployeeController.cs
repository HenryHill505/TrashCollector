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
            List<Pickup> pickups = db.Pickups.Where(p => p.User.ZipCode == zip).Where(p => p.Status == "Incomplete").ToList();
            return View("Index", pickups);
        }

        public ActionResult CompletePickup(int Id)
        {
            Pickup completedPickup = db.Pickups.Where(p => p.Id == Id).FirstOrDefault();
            completedPickup.Status = "Complete";
            db.SaveChanges();

            return Index();
        }

        public ActionResult WeekdayPickups()
        {
            string id = User.Identity.GetUserId();
            int zip = db.Users.Where(u => u.Id == id).Select(u => u.ZipCode).FirstOrDefault();
            List<Pickup> pickups = db.Pickups.Include("User").Where(p => p.User.ZipCode == zip).Where(p => p.Status == "Incomplete").ToList();
            return View(pickups);
        }

        public ActionResult CustomerProfile(string Id)
        {
            ApplicationUser customer = db.Users.Where(c => c.Id == Id).FirstOrDefault();
            ViewBag.APIString = Keychain.APIString;
            return View(customer);
        }
    }
}