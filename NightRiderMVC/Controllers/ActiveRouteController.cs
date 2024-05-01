using DataObjects.RouteObjects;
using DataObjects.HelperObjects;
using DataObjects;
using LogicLayer.RouteAssignment;
using LogicLayer.RouteStop;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Maps.MapControl.WPF;
using NightRiderMVC.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace NightRiderMVC.Controllers
{
    [Authorize(Roles = "Driver, Dispatcher, FleetAdmin, Operator")]
    public class ActiveRouteController : Controller
    {
        IActiveRouteManager _activeRouteManager = new ActiveRouteManager();
        IRouteAssignmentManager _routeAssignmentManager = new RouteAssignmentManager();
        IRouteManager _routeManager = new RouteManager();
        IVehicleManager _vehicleManager = new VehicleManager();
        int _currentUserID = 0;
        ActiveRoute _activeRoute = null;
        private string BingMapsKey = ConfigurationManager.AppSettings["BingMapsKey"];

        // GET: ActiveRoute
        public ActionResult Index()
        {
            // Get current user to determin routes to display
            try
            {
                ViewBag.user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                if (user.EmployeeID != null)
                {
                    _currentUserID = (int)user.EmployeeID;
                }
                else
                {
                    throw new Exception();
                }
                Session["currentUserID"] = _currentUserID;
            }
            catch (Exception)
            {
                return View();
            }

            // Get list of assigned routes
            IEnumerable<Route_Assignment_VM> routes = _routeAssignmentManager.GetAllRouteAssignmentByDriverID(_currentUserID);
            List<Route_Assignment> routeAssignements = new List<Route_Assignment>();
            foreach (Route_Assignment_VM route in routes)
            {
                routeAssignements = _routeAssignmentManager.GetRouteAssignmentsByRouteIDAndDate(route.Route_ID, DateTime.Today, DateTime.Today);
                //List<Route_Assignment> temp = _routeAssignmentManager.GetRouteAssignmentsByRouteIDAndDate(route.Route_ID, DateTime.Today, DateTime.Today);
                //foreach (Route_Assignment tempRoute in temp)
                //{
                //    routeAssignements.Add(tempRoute);
                //}
            }

            Session["routes"] = routes;

            ViewBag.RoutesVM = routes;
            ViewBag.DriverID = _currentUserID;
            return View(routeAssignements);
        }

        // GET: ActiveRoute/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private void vehicleDropDown()
        {
            try
            {
                List<SelectListItem> vehicleList = new List<SelectListItem>();
                foreach (var vehicle in _vehicleManager.VehicleLookupList())
                {
                    vehicleList.Add(new SelectListItem() { Text = vehicle.VehicleNumber, Value = vehicle.VIN });
                }
                ViewBag.VehicleList = vehicleList;
            }
            catch (Exception)
            {
                ViewBag.VehicleList = new List<SelectListItem>();
                // Populate vehicle list to change if needed
            }

        }

        // GET: ActiveRoute/Create
        public ActionResult Create(int routeID, int assignmentID, string VIN, int driver)
        {
            // RouteVM route = null;
            try
            {
                Session["routeID"] = routeID;
                // display the route to start
                // route = _routeManager.getRouteById(routeID);

                _activeRoute = new ActiveRoute()
                {
                    AssignmentID = assignmentID,
                    DriverID = (int)Session["currentUserID"],
                    VIN = VIN
                };
                Session["assignmentID"] = assignmentID;
                // ViewBag.RouteName = route.RouteName;
                // Session["route"] = route;
            }
            catch (Exception)
            {
                vehicleDropDown();
                return View();
            }

            vehicleDropDown();
            return View(_activeRoute);
        }

        // POST: ActiveRoute/Create
        [HttpPost]
        public ActionResult Create(ActiveRoute route)
        {
            ActiveRoute _activeRoute = null;
            try
            {
                DateTime startTime = DateTime.Now;

                _activeRoute = new ActiveRoute()
                {
                    AssignmentID = (int)Session["assignmentID"],
                    DriverID = (int)Session["currentUserID"],
                    VIN = route.VIN,
                    StartTime = startTime
                };

                var result = _activeRouteManager.AddActiveRoute(_activeRoute);

                if (result)
                {
                    // TODO create retrieve active route from DB and retrieve
                    Session["activeRoute"] = _activeRoute;
                    return RedirectToAction("Edit");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                vehicleDropDown();
                return View();
            }
        }

        // GET: ActiveRoute/Edit/5
        public ActionResult Edit()
        {
            try
            {
                ViewBag.BingMapsKey = BingMapsKey;
                // TODO: Display route information
                ActiveRoute activeRoute = (ActiveRoute)Session["activeRoute"];
                IEnumerable<Route_Assignment_VM> routes = (IEnumerable<Route_Assignment_VM>)Session["routes"];
                //int routeID = routes.Where(r => r.Assignment_ID == activeRoute.AssignmentID).Select(s => s.Route_ID).First();
                int routeID = (int)Session["routeID"];
                // RouteVM routeVM = (RouteVM)Session["route"];
                RouteVM route = _routeManager.GetRoutesWithStops().Where(s => s.RouteId == routeID).First();
                IEnumerable<RouteVM> routsForMap = _routeManager.GetRoutesWithStops();

                ViewBag.RouteName = route.RouteName;
                ViewBag.RouteID = routeID;
                ViewBag.StartTime = route.StartTime;
                ViewBag.EndTime = route.EndTime;
                ViewBag.RepeatTime = route.RepeatTime;

                ViewBag.RoutesData = JsonConvert.SerializeObject(routsForMap, Formatting.None);
                // TODO: End route, redirect to checklist
                return View(route);
            }
            catch
            {
                return View();
            }
        }

        // POST: ActiveRoute/Edit/5
        [HttpPost]
        public ActionResult Edit(string endRoute)
        {

            try
            {
                // TODO: Add update logic here
                if (endRoute == "End Route")
                {
                    try
                    {
                        ActiveRoute activeRoute = (ActiveRoute)Session["activeRoute"];
                        activeRoute.EndTime = DateTime.Now;
                        var result = _activeRouteManager.EndActiveRoute(activeRoute);
                        if (result)
                        {
                            return RedirectToAction("Create", "VehicleChecklist");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ActiveRoute/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActiveRoute/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
