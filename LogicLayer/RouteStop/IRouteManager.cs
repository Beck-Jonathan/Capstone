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
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route.
    /// <br /><br />
    /// UPDATER: Michael Springer
    /// UPDATED: 2024-04-19
    /// COMMENTS: added method for GetRoutesWithStops
    /// </remarks>
    public interface IRouteManager
    {
        RouteVM getRouteById(int routeId);

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
        IEnumerable<RouteVM> getRoutes();

        IEnumerable<RouteVM> GetRoutesWithStops();

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
        int AddRoute(RouteVM route);
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
        int EditRoute(RouteVM oldRoute, RouteVM newRoute);
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
        Task<BingMapsResponse> getRouteLine(RouteVM route);


        /// <summary>
        /// Method to deactivate a route
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be deactivated
        /// </param>
        /// <returns>
        ///     <see cref="bool">bool</see>: Boolean value if the route was deactivated.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be deactivated
        ///     Exceptions:
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>
        bool DeactivateRoute(int routeId);

        /// <summary>
        /// Method to activate a route
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be activated
        /// </param>
        /// <returns>
        ///     <see cref="bool">bool</see>: Boolean value if the route was activated.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be activated
        ///     Exceptions:
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>
        bool ActivateRoute(int routeId);
    }
}
