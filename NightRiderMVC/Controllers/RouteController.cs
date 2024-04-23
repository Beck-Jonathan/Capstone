using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
/// <summary>
/// AUTHOR: Michael Springer
/// DATE: 2024-04-17
/// Controller for Routes
/// </summary>
/// <remarks>
/// 
/// <br /><br />
/// 
/// </remarks>
namespace NightRiderMVC.Controllers
{
    public class RouteController : Controller
    {
        private RouteManager _routeManager;
        private string BingMapsKey = ConfigurationManager.AppSettings["BingMapsKey"];
        
        // GET: Route
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Route Information";
            ViewBag.BingMapsKey = BingMapsKey;

            _routeManager = new RouteManager();
            IEnumerable<RouteVM> routes = _routeManager.GetRoutesWithStops();
            ViewBag.RoutesData= JsonConvert.SerializeObject(routes, Formatting.None);
            // used for route tracing


           
            return View(routes);
        }
    }
}