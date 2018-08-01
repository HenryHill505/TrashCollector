using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrashCollector.Models;

namespace TrashCollector
{
    public static class PickupManager
    {
        private static DateTime GetWeekStart(DateTime initialDate)
        {
            DateTime dateObject = initialDate;
            if (dateObject.DayOfWeek.ToString() == "Saturday") { return dateObject.AddDays(2); }
            if (dateObject.DayOfWeek.ToString() == "Sunday") { return dateObject.AddDays(1); }
            while (dateObject.DayOfWeek.ToString() != "Monday")
            {
                dateObject = dateObject.AddDays(-1);
            }
            return dateObject;
        }

        private static DateTime GetWeekEnd(DateTime initialDate)
        {
            DateTime dateObject = initialDate;
            if (dateObject.DayOfWeek.ToString() == "Saturday") { return dateObject.AddDays(6); }
            if (dateObject.DayOfWeek.ToString() == "Sunday") { return dateObject.AddDays(5); }
            while (dateObject.DayOfWeek.ToString() != "Friday")
            {
                dateObject = dateObject.AddDays(1);
            }
            return dateObject;
        }

        public static void UpdatePickups()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.Where(u => u.UserRole == "Customer").ToList();
            var pickups = db.Pickups.ToList();
            DateTime weekStart = GetWeekStart(DateTime.Today);
            DateTime weekEnd = GetWeekEnd(DateTime.Today);

            foreach (ApplicationUser user in users)
            {
                DateTime pickupDay = DateTime.Today;
                while(pickupDay < weekEnd && pickupDay.DayOfWeek.ToString() != user.PickupDay)
                {
                    pickupDay = pickupDay.AddDays(1);
                }
                //while (pickupDay.DayOfWeek.ToString() != user.PickupDay && (pickupDay.DayOfWeek.ToString() != "Saturday" && pickupDay.DayOfWeek.ToString() != "Sunday"))
                //{
                //    pickupDay = pickupDay.AddDays(1);
                //}

                if (pickupDay.DayOfWeek.ToString() == user.PickupDay && pickups.Where(p => p.UserId == user.Id).Where(p => p.Date == pickupDay.Date).Count() == 0)
                {
                    Pickup pickup = new Pickup() { UserId = user.Id, User = user, Date = pickupDay.Date, Cost = 10, Status = "Incomplete", Type = "Weekly" };
                    db.Pickups.Add(pickup);
                }
            }
            db.SaveChanges();
        }

        public static void RemoveExtraWeeklyPickups(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            DateTime weekEnd = GetWeekEnd(DateTime.Today);

            List<Pickup> thisWeeksPickups = db.Pickups.Where(p => p.User == user).Where(p => p.Date >= DateTime.Today && p.Date <= weekEnd).Where(p => p.Type == "Weekly").ToList();
            foreach(Pickup pickup in thisWeeksPickups)
            {
                if (pickup.Date.DayOfWeek.ToString() != user.PickupDay)
                {
                    db.Pickups.Remove(pickup);
                }
            }
            db.SaveChanges();
        }

        public static bool HasUserHadPickupThisWeek(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            DateTime weekStart = GetWeekStart(DateTime.Today);

            List<Pickup> pickupsThisWeek = db.Pickups.Where(p => p.User == user).Where(p => p.Date >= weekStart && p.Date < DateTime.Today).Where(p => p.Type == "Weekly").ToList();

            if (pickupsThisWeek.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}