using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
using System.CodeDom.Compiler;
using DataObjects.HelperObjects;

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
        IBingMapsAccessor _bingMapsAccessor;
        public RouteManager()
        {
            _routeAccessor = new RouteAccessor();
            _bingMapsAccessor = new BingMapsAccessor();
        }
        public RouteManager(IRouteAccessor routeAccessor)
        {
            _routeAccessor = routeAccessor;
            _bingMapsAccessor = new BingMapsAccessor();
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

        public IEnumerable<RouteVM> getRoutes()
        {
            IEnumerable<RouteVM> results = null;
            try
            {
                results = _routeAccessor.selectRoutes();
            } catch (Exception e)
            {
                throw new ApplicationException("Something went wrong, we're verry sorry!", e);
            }
            return results;
        }
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// <br />
        /// CREATED: 2024-03-19
        /// <br />
        ///     Asynchronous method used to pull data for navigation from the Bing Maps API.
        /// </summary>
        /// <param name="route">The route whose path we're trying to calculate.</param>
        /// <returns>
        /// <see cref="Task">A Task</see> Object which, when awaited, returns a <see cref="BingMapsResponse">BingMapsResponse</see> Object.
        /// </returns>
        public async Task<BingMapsResponse> getRouteLine(RouteVM route)
        {
            BingMapsResponse result;
            try
            {
                result = await _bingMapsAccessor.getMapPolyline(route.RouteStops);
            } catch (Exception e)
            {
                throw new ApplicationException("Map service unavailable at this time, sorry!");
            }
            return result;
        }
    }
}
