using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Interaction Logic for Routes
    /// </summary>
    public interface IRouteManager
    {
        RouteVM getRouteById(int routeId);
        IEnumerable<RouteVM> getRoutes();
        int AddRoute(RouteVM route);
        int EditRoute(RouteVM oldRoute, RouteVM newRoute);
        int DeactivateRoute(int routeId);
        int ActivateRoute(int routeId);
        Task<BingMapsResponse> getRouteLine(RouteVM route);
    }
}
