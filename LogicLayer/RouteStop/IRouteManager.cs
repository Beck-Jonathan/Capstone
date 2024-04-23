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
        IEnumerable<RouteVM> getRoutes();
        IEnumerable<RouteVM> GetRoutesWithStops();
        int AddRoute(RouteVM route);
        int EditRoute(RouteVM oldRoute, RouteVM newRoute);
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
