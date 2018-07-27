using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
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
            return View();
        }

        public ActionResult PickupDay()
        {
            return View();
        }

        public ActionResult Suspension()
        {
            return View();
        }
    }
}