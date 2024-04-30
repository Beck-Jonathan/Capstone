using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NightRiderMVC.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  loads home view and sets session data regarding cookies notice
        ///  allows cookie notice to only be show once during load
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Index()
        {
            if (Session["CookieNoticeShown"] == null)
            {
                Session["CookieNoticeShown"] = true;
                ViewBag.ShowCookieNotice = true;
            }
            else
            {
                ViewBag.ShowCookiesNotice = false;
            }
            return View();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  Created for nav bar, but not fully implemented
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  Created for nav bar, but not fully implemented
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Contact()
        {
            ViewBag.Message = "Kirkwood Community College";

            return View();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  Returns the privacy view-- a static page
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Privacy()
        {
            ViewBag.Message = "NightRider Privacy Policy";
            return View();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  Returns the terms and conditions view-- a static page
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Terms()
        {
            ViewBag.Message = "NightRider Terms of Use";
            return View();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  Returns the cookie policy view-- a static page
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Cookies()
        {
            ViewBag.Message = "NightRider Cookie Policy";
            return View();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-06
        ///  added to populate the navigation system -- not yet fully implemented
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        public ActionResult Help()
        {
            ViewBag.Message = "Request Help";
            return View();
        }
    }
}