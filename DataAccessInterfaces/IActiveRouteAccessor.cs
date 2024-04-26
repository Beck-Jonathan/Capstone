using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-04-20
    ///     Data access interface for accessing active route information from the database.
    /// </summary>
    public interface IActiveRouteAccessor
    {
        /// <summary>
        ///     Add active route to the database.
        /// </summary>
        /// <param name="route">
        ///    The route information to be added as a active route object.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The row count, 1 added, 0 error adding.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="ActiveRoute">ActiveRoute</see> Active Route: The route information to be added.
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-04-20
        /// </remarks>
        int AddActiveRoute(ActiveRoute route);

        /// <summary>
        ///     End an active route in the database.
        /// </summary>
        /// <param name="route">
        ///    The route information for the route to enter an end time.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The row count, 1 added, 0 error adding.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="ActiveRoute">ActiveRoute</see> Active Route: The route information to be updated.
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-04-22
        /// </remarks>
        int EndActiveRoute(ActiveRoute route);
    }
}
