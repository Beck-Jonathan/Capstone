using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using NightRiderMVC.Models;

namespace NightRiderMVC.Controllers
{
    [Authorize(Roles = "Driver, Dispatcher, FleetAdmin, Maintenanace, Mechanic, Operator, PartsPerson")]

    public class VehicleChecklistController : Controller
    {
        IVehicleManager _vehicleManager = new VehicleManager();
        IEmployeeManager _employeeManager = new EmployeeManager();
        int _currentUserID = 0;

        // GET: VehicleChecklist
        public ActionResult Index()
        {
            return View();
        }

        // GET: VehicleChecklist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleChecklist/Create
        public ActionResult Create()
        {
            // ViewBag.user = System.Web.HttpContext.Current.User.Identity.GetUserId();
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
            ViewBag.userID = _currentUserID;

            VehicleChecklist checklist = new VehicleChecklist();
            checklist.EmployeeID = _currentUserID;

            dropDowns();
            return View(checklist);
        }

        private void dropDowns()
        {
            try
            {
                List<SelectListItem> vehicleList = new List<SelectListItem>();
                foreach (var vehicle in _vehicleManager.VehicleLookupList())
                {
                    vehicleList.Add(new SelectListItem() { Text = vehicle.VehicleNumber, Value = vehicle.VIN });
                }
                ViewBag.VehicleList = vehicleList;

                List<SelectListItem> employeeList = new List<SelectListItem>();
                foreach (var employee in _employeeManager.GetAllEmployees())
                {
                    employeeList.Add(new SelectListItem() { Text = employee.Family_Name + ", " + employee.Given_Name, Value = employee.Employee_ID.ToString() });
                }
                ViewBag.EmployeeList = employeeList;

                List<SelectListItem> fuelList = new List<SelectListItem>();
                fuelList.Add(new SelectListItem() { Text = "Full", Value = "5" });
                fuelList.Add(new SelectListItem() { Text = "3/4", Value = "4" });
                fuelList.Add(new SelectListItem() { Text = "Half", Value = "3" });
                fuelList.Add(new SelectListItem() { Text = "1/4", Value = "2" });
                fuelList.Add(new SelectListItem() { Text = "Low", Value = "1" });
                fuelList.Add(new SelectListItem() { Text = "Empty", Value = "0" });

                ViewBag.FuelLevel = fuelList;
            }
            catch (Exception)
            {

                ViewBag.EmployeeList = new List<SelectListItem>();
                ViewBag.FuelLevel = new List<SelectListItem>();
            }
        }

        // POST: VehicleChecklist/Create
        [HttpPost]
        public ActionResult Create(VehicleChecklist checklist)
        {
            try
            {
                checklist.EmployeeID = (int)Session["currentUserID"];

                checklist.ChecklistDate = DateTime.Now;
                if (!checklist.Cosmetic.isNotEmptyOrNull())
                {
                    checklist.Cosmetic = "";
                }
                if (!checklist.Notes.isNotEmptyOrNull())
                {
                    checklist.Notes = "";
                }


                if (ModelState.IsValid)
                {
                    int result = _vehicleManager.AddVehicleChecklist(checklist);
                    //if (result >= 100000)
                    //{
                    //    ViewBag.checklistId = result;
                    //}

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                dropDowns();
                return View(checklist);
            }
        }

        // GET: VehicleChecklist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehicleChecklist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleChecklist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleChecklist/Delete/5
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
