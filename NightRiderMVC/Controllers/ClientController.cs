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

    [Authorize(Roles ="Client")]
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
        // GET: Client
        public ActionResult Index()
        {
            _clientManager = new LogicLayer.ClientManager();
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
            _clientManager = new LogicLayer.ClientManager();
            Client client = GetSessionClient();
            return View(client);

        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-27
        ///  Updates client information
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        // POST: /CLient/Edit
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            string result = "";
            _clientManager = new LogicLayer.ClientManager();

            if (ModelState.IsValid)
            {
                Client_VM newClient = new Client_VM()
                {
                    ClientID = client.ClientID,
                    GivenName = client.GivenName,
                    FamilyName = client.FamilyName,
                    MiddleName = client.MiddleName,
                    DOB = client.DOB,
                    Email = client.Email,
                    PostalCode = client.PostalCode,
                    City = client.City,
                    Region = client.Region,
                    Address = client.Address,
                    TextNumber = client.TextNumber,
                    VoiceNumber = client.VoiceNumber,
                    IsActive = client.IsActive
                };

                try
                {
                    _clientManager.EditClient(newClient);
                    result = "Success! Your changes have been saved.";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    return View(client);
                }
            }
            
            ViewBag.Result = result;
            return View(client);
        }

        // GET: /Client/Delete
        public ActionResult Delete()
        {
            _clientManager = new LogicLayer.ClientManager();
            Client client = GetSessionClient();
            return View(client);

        }

        // POST: /Client/Delete
        [HttpPost]
        public ActionResult Delete(Client client)
        {

            _clientManager = new LogicLayer.ClientManager();
            try
            {
                Client sessionClient = GetSessionClient();
                var applicationuser = _userManager.FindByEmail(sessionClient.Email);
                var deleteLogin = _userManager.RemoveLogin(applicationuser.Id, new UserLoginInfo("Local", applicationuser.Email));
                var deleteUser = _userManager.Delete(applicationuser);
                if(deleteLogin.Succeeded && deleteUser.Succeeded)
                {
                    _clientManager.DeactivateClient(sessionClient.ClientID);
                    // logout
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    //home page
                    return RedirectToAction("Index", "Home");
                }                
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
                return View(client);
            }
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