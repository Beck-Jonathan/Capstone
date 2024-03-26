using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IRouteStopAccessor
    {
        IEnumerable<RouteStopVM> selectRouteStopByRouteId(int routeId);
        int InsertRouteStop(RouteStopVM routeStopVM);
        int UpdateRouteStop(RouteStopVM oldRouteStopVM, RouteStopVM newRouteStopVM);
        int DeactivateRouteStop(RouteStopVM routeStopVM);
        int ActivateRouteStop(RouteStopVM routeStopVM);
    }
}
