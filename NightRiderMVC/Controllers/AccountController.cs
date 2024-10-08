﻿using System;
using System.CodeDom;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NightRiderMVC.Models;

namespace NightRiderMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via Username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            const int MIN_VALID_CLIENT_ID = 100000;
            // effectivley const but DateTime is a struct and can't be declared as const
            DateTime DEFAULT_DOB = new DateTime(1970, 1, 1);

            //JACOBS NOTES FOR PERSONAL COMPREHENSION TO PREVENT INSANSITY

            //If an id exists, that means a login exists for it. 
            //If a login exists need to make sure the username and password match
            //thus call authenticate employee if employee authenticates good create
            //if employee doesnt authenticate dont let it happen...
            // only allow admin to edit Employees


            if (ModelState.IsValid)
            {
                //check to see if this user is in the existing database
                LogicLayer.Utilities.IPasswordHasher passwordHasher = new LogicLayer.Utilities.PasswordHasher();
                LogicLayer.EmployeeManager employeeMgr = new LogicLayer.EmployeeManager();
                LogicLayer.ClientManager clientMgr = new LogicLayer.ClientManager();
                LogicLayer.LoginManager loginMgr = new LogicLayer.LoginManager(passwordHasher);
                try
                {
                    //checks for employee email in the employee table, if it is null goes
                    //else if statement for client below
                    if (employeeMgr.GetEmployeeByEmail(model.Email).Employee_ID >= 100000)
                    {
                        //this requires the user to use the same password as the one in the internal database
                        //                                          TEMPORARY VALUE CHANGE IT
                        var oldUser = loginMgr.AuthenticateEmployee(loginMgr.GetEmployeeUserNameByEmail(model.Email), model.Password);
                        var user = new ApplicationUser
                        {
                            //populate these fields with existing data from olduser
                            GivenName = oldUser.Given_Name,
                            FamilyName = oldUser.Family_Name,
                            EmployeeID = oldUser.Employee_ID,

                            //populate these fields normally
                            UserName = model.Email,
                            Email = model.Email
                        };
                        //create the user with the identity system UserManager Normally
                        var result = await UserManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            //use the oldUser.Roles list to add internally assigned roles to the user
                            foreach (var role in oldUser.Roles)
                            {
                                UserManager.AddToRole(user.Id, role.RoleID);
                            }
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToAction("Index", "Home");
                        }
                        AddErrors(result);
                    }
                    else if (clientMgr.FindClient(model.Email))
                    {
                        // ALL CLIENTS WILL REGISTER THROUGH THE MVC SITE
                        //var placeholderUsername = loginMgr.GetClientUserNameByEmail(model.Email);
                        //var oldUser = loginMgr.AuthenticateClient(model.Email, model.Password);
                        //var user = new ApplicationUser
                        //{
                        //    //populate these fields with existing data from olduser
                        //    GivenName = oldUser.GivenName,
                        //    FamilyName = oldUser.FamilyName,
                        //    ClientID = oldUser.ClientID,

                        //    //populate these fields normally
                        //    UserName = model.Email,
                        //    Email = model.Email
                        //};
                        ////create the user with the identity system UserManager Normally
                        //var result = await UserManager.CreateAsync(user, model.Password);
                        //if (result.Succeeded)
                        //{
                        //    //use the oldUser.Roles list to add internally assigned roles to the user
                        //    //Commented out because Clients dont really have implemented roles.
                        //    //foreach (var role in oldUser.Roles)
                        //    //{
                        //    //    UserManager.AddToRole(user.Id, role.ClientRoleID);
                        //    //}
                        //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //    return RedirectToAction("Index", "Home");
                        //}
                        //AddErrors(result);
                        ViewBag.RegisterErrorMessage = "An account with that email already exists. Please login.";
                        return View(model);

                    }
                    else // if not existing user create a user with the client role
                    {
                        // new client object for the Client Table
                        var client = new DataObjects.Client_VM()
                        {
                            GivenName = model.GivenName,
                            FamilyName = model.FamilyName,
                            Email = model.Email,
                            DOB = DEFAULT_DOB
                        };
                        //set an invalid clientId
                        int newClientId = 0;
                        // add that client to the table
                        clientMgr.AddClient(client);
                        // get id of the client we just added
                        newClientId = clientMgr.GetClientByEmail(model.Email).ClientID;
                        // if that worked, make an ASPUser for the client
                        if(newClientId >= MIN_VALID_CLIENT_ID)
                        {
                            var user = new ApplicationUser
                            {
                                
                                ClientID = newClientId,
                                GivenName = model.GivenName,
                                FamilyName = model.FamilyName,
                                UserName = model.Email,
                                Email = model.Email
                                
                            };
                            var result = await UserManager.CreateAsync(user, model.Password);
                            if (result.Succeeded)
                            {
                                // assign the client role to the user
                                UserManager.AddToRole(user.Id, "Client");

                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                return RedirectToAction("Index", "Home");

                            }
                            AddErrors(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //creating old user failed probably because authenticate user failed
                    return View(model);
                }
                //modelstate was not valid
            }
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        //GET: /Account/RegisterEmployeeUser
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterEmployeeUser()
        {
            return View();
        }

        //POST: /Acount/RegisterEmployeeUser
        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmployeeUser(RegisterEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //check to see if this user is in the existing database
                LogicLayer.EmployeeManager empMgr = new LogicLayer.EmployeeManager();
                LogicLayer.LoginManager loginMgr = new LogicLayer.LoginManager();
                try
                {
                    if (empMgr.GetEmployeeByEmail(model.Email).Employee_ID >= 100000)
                    {
                        // if this user already exists we need to use the regular register method
                        return RedirectToAction("Register", "Account");
                    }
                    else // if not existing user create a user without roles
                    {
                        var employee = new DataObjects.Employee_VM()
                        {
                            //these fields needed by sp_insert_user
                            Email = model.Email,
                            Given_Name = model.GivenName,
                            Family_Name = model.FamilyName,
                            Phone_Number = model.PhoneNumber,
                            DOB = new DateTime(1973, 4, 5),
                            Address = "",
                            City = "",
                            State = "",
                            Country = "",
                            Zip = "",
                            Position = "",
                            

                        };
                        if (empMgr.AddEmployee(employee) >= 100000) //add the dataobjects.user to employee
                        {
                            
                            var employeeID = empMgr.RetrieveEmployeeIDFromEmail(model.Email);
                            var login = loginMgr.AddEmployeeLogin(model.Email, employeeID);
                            var user = new ApplicationUser // if it worked create an identity user
                            {
                                EmployeeID = employeeID,
                                GivenName = model.GivenName,
                                FamilyName = model.FamilyName,
                                UserName = model.Email,
                                Email = model.Email,
                            };
                            var result = await UserManager.CreateAsync(user, "newuser");
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                            AddErrors(result);

                        }

                    }
                }
                catch (Exception ex)
                {
                    //creating old user failed probably because authenticate user failed
                    return View(model);
                }
                //modelstate was not valid
            }
            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}