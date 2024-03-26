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
    public interface IBingMapsAccessor
    {
        Task<BingMapsResponse> getMapPolyline(IEnumerable<RouteStopVM> stops);
    }
}
