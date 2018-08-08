using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthWholesaler.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult New()
        {
            return View();
        }
    }
}