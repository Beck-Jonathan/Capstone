using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using Microsoft.Maps.MapControl.WPF;
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
    /// Represents the Data Access Interface for the Bing Maps API.
    /// </summary>
    ///    UPDATER: Steven Sanchez
    /// <br />
    ///    UPDATED: 2024-04-06
    /// <br />
    /// Added getMapPolylineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> stops)
    ///  for drivers route assignment
    public interface IBingMapsAccessor
    {
        Task<BingMapsResponse> getMapPolyline(IEnumerable<RouteStopVM> stops);
        Task<BingMapsResponse> getMapPolylineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> stops);
    }
}
