using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.RouteObjects;
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
    /// Handles the interaction logic for Routes
    /// </summary>
    public class RouteManager : IRouteManager
    {
        IRouteAccessor _routeAccessor;
        public RouteManager()
        {
            _routeAccessor = new RouteAccessor();
        }
        public RouteManager(IRouteAccessor routeAccessor)
        {
            _routeAccessor = routeAccessor;
        }

        public int ActivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int AddRoute(RouteVM route)
        {
            throw new NotImplementedException();
        }

        public int DeactivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int EditRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            throw new NotImplementedException();
        }

        public RouteVM getRouteById(int routeId)
        {
            throw new NotImplementedException();
        }

        public List<RouteVM> getRoutes()
        {
            List<RouteVM> results = null;
            try
            {
                results = _routeAccessor.selectRoutes();
            } catch (Exception e)
            {
                throw new ApplicationException("Something went wrong, we're verry sorry!", e);
            }
            return results;
        }
    }
}
