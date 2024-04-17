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
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route.
    /// </remarks>
    /// <remarks>
    /// UPDATER: Nathan Toothaker
    /// UPDATED: 2024-03-26
    /// COMMENTS:
    ///    Added connection to Bing Maps API
    /// </remarks>
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
        /// <summary>
        ///     Activates the route with indicated Id
        /// </summary>
        /// <param name="routeId">
        ///    The ID of the route to be activated
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the route was successfully activated
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> routeId: the ID of the route to be activated
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when an error is caught from the data accessor.
        /// <br /><br />
        ///    CONTRIBUTOR: Chris Bezinger
        /// </remarks>

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
        /// <summary>
        ///     Adds a new route to the database
        /// </summary>
        /// <param name="route">
        ///    The route to be stored.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the stored route
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="RouteVM">RouteVM</see> route: The route to be stored.
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when an error is caught from the data accessor
        ///    , in this case almost always when the route already exists.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public int AddRoute(RouteVM route)
        {
            int result = -1;
            try
            {
                result = _routeAccessor.InsertRoute(route);
            } catch (Exception ex)
            {
                throw new ApplicationException("Route already Exists!", ex);
            }

            return result;
        }
        /// <summary>
        ///     Deactivates the route with indicated Id
        /// </summary>
        /// <param name="routeId">
        ///    The ID of the route to be deactivated
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the route was successfully deactivated
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> routeId: the ID of the route to be deactivated
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when an error is caught from the data accessor.
        /// <br /><br />
        ///    CONTRIBUTOR: Chris Bezinger
        /// </remarks>
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
        /// <summary>
        ///     Updates a route to the database
        /// </summary>
        /// <param name="oldRoute">
        ///    The original route data that should be in storage.
        /// </param>
        /// <param name="oldRoute">
        ///    The new route data to be stored.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the stored route
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="RouteVM">RouteVM</see> oldRoute: The original route data that should be in storage.
        /// <br />
        ///    <see cref="RouteVM">RouteVM</see> newRoute: The new route data to be stored.
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when an error is caught from the data accessor.
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when an improper update is attempted (e.g., trying to write 1 route over another).
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public int EditRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            if (oldRoute.RouteId == newRoute.RouteId)
            {
                try
                {
                    return _routeAccessor.UpdateRoute(oldRoute, newRoute);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to update Route.", ex);
                }
            } else
            {
                throw new ArgumentException("Cannot change a Route's ID.");
            }
        }

        public RouteVM getRouteById(int routeId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Returns the list of routes.
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{RouteVM}">IEnumerable</see>: The list of routes
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when an error is caught from the data accessor.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
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
