using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NightRiderMVC.Controllers
{
    public class RideRequestController : Controller
    {
        // GET: RideRequest
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Request a Ride";
            return View();
        }
    }
}