using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrashCollector.Models;

namespace TrashCollector
{
    public static class PickupManager
    {
        public static void UpdatePickups()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.ToList();
            var pickups = db.Pickups.ToList();
            
            foreach (ApplicationUser user in users)
            {
                DateTime pickupDay = DateTime.Today;
                while (pickupDay.DayOfWeek.ToString() != user.PickupDay && (pickupDay.DayOfWeek.ToString() != "Saturday" && pickupDay.DayOfWeek.ToString() != "Sunday"))
                {
                    pickupDay = pickupDay.AddDays(1);
                }
                if (pickupDay.DayOfWeek.ToString() == user.PickupDay && pickups.Where(p => p.UserId == user.Id).Where(p => p.Date == pickupDay.Date).Count() == 0)
                {
                    Pickup pickup = new Pickup() { UserId = user.Id, User = user, Date = pickupDay.Date, Cost = 1, Status = "Incomplete" };
                    db.Pickups.Add(pickup);
                }
            }
            db.SaveChanges();
        }
    }
}