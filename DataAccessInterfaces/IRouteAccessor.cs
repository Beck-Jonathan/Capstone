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
        /// <summary>
        ///     Returns a route located by ID.
        /// </summary>
        /// <param name="routeId">The ID of the route to be located.</param>
        /// <returns>
        ///    <see cref="RouteVM">RouteVM</see>: The route with the given ID
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        RouteVM selectRouteById(int routeId);
        /// <summary>
        ///     Returns the list of routes.
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{RouteVM}">IEnumerable</see>: The list of routes
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        IEnumerable<RouteVM> selectRoutes();
        /// <summary>
        ///     Inserts a route and returns the ID primary key from the databse.
        /// </summary>
        /// <param name="route">The route data to be inserted.</param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the newly inserted route.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        int InsertRoute(RouteVM route);
        /// <summary>
        ///     Updates a route record in the database.
        /// </summary>
        /// <param name="oldRoute">The route data prior to the update.</param>
        /// <param name="newRoute">The route data after the update.</param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows updated. 0 means the update failed for some reason.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
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
