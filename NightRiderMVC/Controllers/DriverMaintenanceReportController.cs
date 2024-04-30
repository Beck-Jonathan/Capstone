using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace NightRiderMVC.Controllers
{
    public class DriverMaintenanceReportController : Controller
    {
        Driver_Maintenance_ReportManager _mgr;
        // GET: DriverMaintenanceReport
        //Jonathan Beck 2024-04-2024
        // List View of reports. Shows all to managers, shows ones for a particular driver if user is in the driver role
        public ActionResult Index()
        {
            List<DriverMaintenanceReportVM> driverMaintenanceReports = new List<DriverMaintenanceReportVM>();
            _mgr = new Driver_Maintenance_ReportManager();
            try
            {
                if (User.IsInRole("Driver"))
                {
                    int id = getEmployeeID();
                    driverMaintenanceReports = _mgr.GetAllDriverMaintenacneReportsByEmployeeId(id);
                }
                else
                {
                    driverMaintenanceReports = _mgr.getActiveDriverMaintenacenReports();
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            return View(driverMaintenanceReports);
        }

        // GET: DriverMaintenanceReport/Details/5
        //Jonathan Beck 2024-04-2024
        //Get details of a report
        public ActionResult Details(int id)
        {
            DriverMaintenanceReportVM report = new DriverMaintenanceReportVM();
            _mgr = new Driver_Maintenance_ReportManager();
            try
            {
                report = _mgr.getAllDriverMaintenanceReportsById(id);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            return View(report);
        }

        // GET: DriverMaintenanceReport/Create
        //Jonathan Beck 2024-04-2024

        public ActionResult Create()
        {



            dropdowns();

            return View();
        }

        // POST: DriverMaintenanceReport/Create
        //Jonathan Beck 2024-04-2024
        [HttpPost]
        public ActionResult Create(DriverMaintenanceReport report)
        {

            dropdowns();
            report.DriverID = 100000;
            ViewBag.ErrorMessage = "";
            //report.DriverID = getEmployeeID();

            if (ViewBag.ErrorMessage != "")
            {
                return View("Error");
            }

            report.Is_Active = true;

            _mgr = new Driver_Maintenance_ReportManager();
            if (ModelState.IsValid)
            {
                try
                {
                    _mgr.addDriverMaintenanceReport(report);

                    return RedirectToAction("Index","Home");
                }
                catch
                {
                    return View(report);
                }
            }
            else
            {
                return View(report);
            }
        }

        // GET: DriverMaintenanceReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DriverMaintenanceReport/Edit/5
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

        // GET: DriverMaintenanceReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DriverMaintenanceReport/Delete/5
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

        //Fills the drop downs for serverity and Vehicles
        //Jonathan Beck 2024-04-2024
        private void dropdowns()
        {
            try
            {

                VehicleManager vehicleManager = new VehicleManager();
                List<Vehicle> vehicles = vehicleManager.getVehicleTuplesForDropDown();
                ViewBag.Vehicles = vehicles;

            }
            catch (Exception ex)
            {

                ViewBag.Vehicles = new List<Vehicle>();
            }
            List<String> severity = new List<String>() { "Low", "Medium", "High" };
            ViewBag.SeverityList = severity;



        }
        //Translate Identity employee id to an employee id for our desktop system
        //Jonathan Beck 2024-04-2024
        private int getEmployeeID()
        {

            string username = User.Identity.GetUserName();
            int result = 0;
            try
            {
                LogicLayer.EmployeeManager employeeManager = new EmployeeManager();
                Employee employee = employeeManager.GetEmployeeByEmail(username);
                result = employee.Employee_ID;

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}