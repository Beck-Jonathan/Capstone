using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteAssignment
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-04-20
    ///     Logic manager interface for working with active route data.
    /// </summary>
    public interface IActiveRouteManager
    {
        /// <summary>
        ///     Add a active route to the database.
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem creating the route.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-04-20
        /// </remarks>
        bool AddActiveRoute(ActiveRoute activeRoute);

        /// <summary>
        ///     End an active route in the database.
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem creating the route.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-04-22
        /// </remarks>
        bool EndActiveRoute(ActiveRoute activeRoute);
    }
}
