using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NightRiderMVC.Controllers
{
    public class RideScheduleController : Controller
    {
        private IRideManager _rideManager = new RideManager();

        /// <summary>
        /// AUTHOR: Jacob Wendt
        /// <br />
        /// DATE: 2024-04-23
        /// <br />
        ///  Loads list of rides for client and displays the available services
        /// </summary>
        public ActionResult Index(int clientID)
        {
            try
            {
                var rides = _rideManager.GetRidesByClientID(clientID);
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
            try
            {
                bool result = _rideManager.AddRide(ride);

                return RedirectToAction("Index", new { clientID = 100000 });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(ride);
            }
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
            try
            {
                var ride = _rideManager.GetRideByID(rideID);

                return View(ride);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", new { clientID = 100000 });
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
            try
            {
                _rideManager.EditRide(ride);

                return RedirectToAction("Index", new { clientID = 100000 });
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(ride);
            }
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
            try
            {
                _rideManager.DeactivateRide(rideID);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return RedirectToAction("Index", new { clientID = 100000 });
        }
    }
}