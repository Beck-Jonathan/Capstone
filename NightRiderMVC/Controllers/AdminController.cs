using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using NightRiderMVC.Models;

namespace NightRiderMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager userManager;
        // GET: Admin
        public ActionResult Index()
        {
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            //get a list of roles the user has and put them into a viewbag as roles
            //along with a list of roles the user doesnt have as noRoles
            var empMgr = new LogicLayer.EmployeeManager();
            var rolMgr = new LogicLayer.RoleManager();
            var tempRoles = rolMgr.GetAllRoles();
            List<String> allRoles = new List<String>();
            foreach(var role in tempRoles)
            {
                allRoles.Add(role.RoleID);
            }

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            //Code to prevent removing last admin
            if (role == "Admin")
            {
                var adminUsers = userManager.Users.ToList()
                    .Where(u => userManager.IsInRole(u.Id, "Admin"))
                    .ToList().Count();
                if (adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }
            }
            userManager.RemoveFromRole(id, role);
            if (user.EmployeeID != null)
            {
                try
                {
                    var empMgr = new LogicLayer.EmployeeManager();
                    empMgr.RemoveEmployeeRoles((int)user.EmployeeID, role);
                }
                catch (Exception)
                {
                    //nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }
        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            if (user.EmployeeID != null)
            {
                try
                {
                    var empMgr = new LogicLayer.EmployeeManager();
                    empMgr.InsertEmployeeRoles((int)user.EmployeeID, role);
                }
                catch (Exception)
                {
                    //nothing to do
                }

            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });

        }

    }
}
