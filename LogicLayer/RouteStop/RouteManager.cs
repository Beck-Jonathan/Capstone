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
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route.
    /// </remarks>
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

        public bool ActivateRoute(int routeId)
        {
            bool result = false;
            try
            {
                result = (1 == _routeAccessor.UpdateRouteByIDAsActive(routeId));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to activate vehicle", ex);
            }
            return result;
        }

        public int AddRoute(RouteVM route)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateRoute(int routeId)
        {
            bool result = false;
            try
            {
                result = (1 == _routeAccessor.UpdateRouteByIDAsInactive(routeId));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to deactivate vehicle", ex);
            }
            return result;
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
