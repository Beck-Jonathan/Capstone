using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NightRiderMVC.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace NightRiderMVC.Controllers
{
    [Authorize]
    public class RideScheduleController : Controller
    {
        private IRideManager _rideManager = new RideManager();
        private ApplicationUser _user = null;

        public RideScheduleController()
        {
            var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            _user = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        /// <summary>
        /// AUTHOR: Jacob Wendt
        /// <br />
        /// DATE: 2024-04-23
        /// <br />
        ///  Loads list of rides for client and displays the available services
        /// </summary>
        public ActionResult Index()
        {
            if (_user?.ClientID == null)
            {
                return View("Error");
            }

            try
            {
                var rides = _rideManager.GetRidesByClientID(_user.ClientID.Value);
                return View(rides);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// DATE: 2024-04-23
        /// <br />
        ///  Returns the form for scheduling a new ride
        /// </summary>
        /// <param name="operation">The operation which will serve this ride</param>
        public ActionResult Create(string operation)
        {
            if (_user?.ClientID == null)
            {
                return View("Error");
            }

            ViewBag.Operation = operation;

            return View();
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// DATE: 2024-04-23
        /// <br />
        ///  Handles requests fo scheduling a ride
        /// </summary>
        /// <param name="ride">The ride data to schedule
        [HttpPost]
        public ActionResult Create(Ride_VM ride)
        {
            if (_user?.ClientID == null)
            {
                return View("Error");
            }

            ride.ClientID = _user.ClientID.Value;

            if (ride.ScheduledDate < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(Ride_VM.ScheduledDate), "Date cannot be in the past");
            }
            else if (ride.ScheduledDate == DateTime.Now.Date && ride.ScheduledTime < DateTime.Now.TimeOfDay)
            {
                ModelState.AddModelError(nameof(Ride_VM.ScheduledTime), "Time cannot be in the past");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _rideManager.AddRide(ride);

                    return RedirectToAction("Index", new { clientID = _user.ClientID });
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }

            return View(ride);
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// DATE: 2024-04-24
        /// <br />
        ///  Show form for editing a ride
        /// </summary>
        /// <param name="rideID">The ID of the ride to load for edit
        public ActionResult Edit(int rideID)
        {
            if (_user?.ClientID == null)
            {
                return View("Error");
            }

            try
            {
                var ride = _rideManager.GetRideByID(rideID);

                if (_user.ClientID != ride.ClientID)
                {
                    throw new Exception("Ride not found");
                }

                return View(ride);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", new { clientID = _user.ClientID });
            }
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// DATE: 2024-04-24
        /// <br />
        ///  Handle post requests to edit a ride
        /// </summary>
        /// <param name="ride">The new ride data
        [HttpPost]
        public ActionResult Edit(Ride_VM ride)
        {
            if (_user?.ClientID == null || _user.ClientID != ride.ClientID)
            {
                return View("Error");
            }

            if (ride.ScheduledDate < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(Ride_VM.ScheduledDate), "Date cannot be in the past");
            }
            else if (ride.ScheduledDate == DateTime.Now.Date && ride.ScheduledTime < DateTime.Now.TimeOfDay)
            {
                ModelState.AddModelError(nameof(Ride_VM.ScheduledTime), "Time cannot be in the past");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _rideManager.EditRide(ride);

                    return RedirectToAction("Index", new { clientID = _user.ClientID });
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }

            return View(ride);
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// DATE: 2024-04-24
        /// <br />
        ///  Handle post requests to deactivate a ride
        /// </summary>
        /// <param name="rideID">The ID of the ride to deactivate
        [HttpPost]
        public ActionResult Deactivate(int rideID)
        {
            if (_user?.ClientID == null)
            {
                return View("Error");
            }

            try
            {
                var ride = _rideManager.GetRideByID(rideID);

                if (_user.ClientID != ride.ClientID)
                {
                    throw new Exception("Ride not found");
                }

                _rideManager.DeactivateRide(rideID);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index", new { clientID = _user.ClientID });
        }
    }
}