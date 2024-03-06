using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Database Logic for Routes
    /// </summary>
    public interface IRouteAccessor
    {
        RouteVM selectRouteById(int routeId);
        List<RouteVM> selectRoutes();
        int InsertRoute(RouteVM route);
        int UpdateRoute(RouteVM oldRoute, RouteVM newRoute);
        int DeactivateRoute(int routeId);
        int ActivateRoute(int routeId);
    }
}
