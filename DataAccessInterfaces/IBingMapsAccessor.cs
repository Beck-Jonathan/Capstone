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
    public interface IBingMapsAccessor
    {
        Task<BingMapsResponse> getMapPolyline(IEnumerable<RouteStopVM> stops);
    }
}
