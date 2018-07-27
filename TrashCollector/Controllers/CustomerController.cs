using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExtraPickup()
        {
            return View();
        }

        public ActionResult PaymentOwed()
        {
            string Id = User.Identity.GetUserId();
            var pickups = db.Pickups.Where(p => p.UserId == Id).Where(p => p.Status == "Complete").ToList();
            return View(pickups);
        }

        public ActionResult PickupDay()
        {
            string Id = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickupDay(ApplicationUser postedUser)
        {
            string Id = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
            user.PickupDay = postedUser.PickupDay;
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult Suspension()
        {
            return View();
        }
    }
}