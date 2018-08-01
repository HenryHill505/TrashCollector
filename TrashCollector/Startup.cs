using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TrashCollector.Models;
using System.Linq;
using System;

[assembly: OwinStartupAttribute(typeof(TrashCollector.Startup))]
namespace TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
            PickupManager.UpdatePickups();
        }

        public void CreateRolesandUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }

        public void SchedulePickups()
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
