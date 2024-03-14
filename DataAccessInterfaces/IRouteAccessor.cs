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
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route.
    /// </remarks>
    public interface IRouteAccessor
    {
        RouteVM selectRouteById(int routeId);
        List<RouteVM> selectRoutes();
        int InsertRoute(RouteVM route);
        int UpdateRoute(RouteVM oldRoute, RouteVM newRoute);

        /// <summary>
        /// Deactivate a route in the database
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be deactivated
        /// </param>
        /// <returns>
        ///     <see cref="int">int</see>: The row count, 1 updated, else error.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be deactivated
        ///     Exceptions:
        ///     <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the database
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>
        int UpdateRouteByIDAsInactive(int routeId);

        /// <summary>
        /// Activate a route in the database
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be activated
        /// </param>
        /// <returns>
        ///     <see cref="int">int</see>: The row count, 1 updated, else error.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be activated
        ///     Exceptions:
        ///     <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the database
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>
        int UpdateRouteByIDAsActive(int routeId);
    }
}
