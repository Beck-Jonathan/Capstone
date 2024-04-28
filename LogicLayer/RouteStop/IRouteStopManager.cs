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
    /// Interaction Logic for Route Stop
    /// </summary>
    public interface IRouteStopManager
    {
        /// <summary>
        ///     Gets a list of all stops (And RouteStop data) for a given route
        /// </summary>
        /// <param name="routeId">
        ///    The ID of the route whose stops we want
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{T}">IEnumerable</see>: The list of stops, in order
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> route: The ID of the route whose stops we want
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when an error is caught from the data accessor
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        IEnumerable<RouteStopVM> GetRouteStopByRouteId(int routeId);
        int AddRouteStop(RouteStopVM routeStopVM);
        int EditRouteStop(RouteStopVM existingRouteStop, RouteStopVM newRouteStop);
        int DeleteRouteStop(RouteStopVM routeStop);
        /// <summary>
        /// used to change just the ordinal part of a routestop - to update the list.
        /// </summary>
        /// <param name="routeStop"></param>
        /// <returns></returns>
        bool UpdateOrdinal(RouteStopVM routeStop);
    }
}
