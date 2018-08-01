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

        [HttpPost]
        public ActionResult ExtraPickup(string date)
        {
            string userId = User.Identity.GetUserId();
            DateTime pickupDate = new DateTime();
            pickupDate = DateTime.Parse(date);
            var test = db.Pickups.Where(p => p.UserId == userId).Where(p => p.Date == pickupDate);
            if (db.Pickups.Where(p => p.UserId == userId).Where(p => p.Date == pickupDate).Count() == 0)
            {
                Pickup pickup = new Pickup { UserId = userId, Date = pickupDate, Status = "Incomplete", Cost = 1, Type = "Extra" };
                db.Pickups.Add(pickup);
                db.SaveChanges();
            }
            return View("Index");
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
            PickupManager.RemoveExtraWeeklyPickups(Id);
            PickupManager.UpdatePickups();
            return View("Index");
        }

        public ActionResult Suspension()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Suspension(Suspension suspensionDates)
        {
            string userId = User.Identity.GetUserId();
            Suspension newSuspension = new Suspension() { UserID = userId, StartDate = suspensionDates.StartDate, EndDate = suspensionDates.EndDate };
            db.Suspensions.Add(newSuspension);
            db.SaveChanges();
            return View("Index");
        }
    }
}