/// <summary>
/// AUTHOR: Michael Springer
/// DATE: 2024-04-16
///  Used for updating/deleting client information
/// </summary>
/// <br /><br />
///    UPDATER: 
/// <br />
///    UPDATED: 
/// <br />
///     Update Comments
/// </remarks>
using DataObjects;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NightRiderMVC.Controllers
{

    [Authorize]
    public class ClientController : Controller
    {
        private LogicLayer.ClientManager _clientManager;
        private ApplicationUserManager _userManager;



        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-16
        ///  Detail view as index
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        // GET: Client
        public ActionResult Index()
        {
            _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var userID = User.Identity.GetUserId();
            var userEmail = _userManager.FindById(userID).Email;
            Client client = _clientManager.GetClientByEmail(userEmail);
            return View(client);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-16
        ///  Edit view
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        // GET: /Client/Edit
        public ActionResult Edit()
        {
            Client client = GetSessionClient();
            return View(client);

        }

        // POST: /CLient/Edit
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            return View(client);
        }

        // GET: /Client/Delete
        public ActionResult Delete()
        {
            Client client = GetSessionClient();
            return View(client);

        }

        // POST: /Client/Delete
        [HttpPost]
        public ActionResult Delete(Client client)
        {
            return View(client);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-16
        ///  Helper method for getting the currently logged-in client
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        private Client GetSessionClient()
        {
            _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var userID = User.Identity.GetUserId();
            var userEmail = _userManager.FindById(userID).Email;
            Client client = _clientManager.GetClientByEmail(userEmail);
            return client;
        }
    }
}