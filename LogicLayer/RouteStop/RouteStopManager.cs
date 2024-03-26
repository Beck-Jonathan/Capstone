using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer.RouteStop
{
    public class RouteStopManager : IRouteStopManager
    {
        private IRouteStopAccessor _routeStopAccessor;
        public RouteStopManager()
        {
            _routeStopAccessor = new RouteStopAccessor();
        }
        public RouteStopManager(IRouteStopAccessor routeStopAccessor)
        {
            _routeStopAccessor = routeStopAccessor;
        }
        public int ActivateRouteStop(int routeStopId)
        {
            throw new NotImplementedException();
        }

        public int AddRouteStop(RouteStopVM routeStopVM)
        {
            throw new NotImplementedException();
        }

        public int DeactivateRouteStop(int routeStopId)
        {
            throw new NotImplementedException();
        }

        public int EditRouteStop(RouteStopVM existingRouteStop, RouteStopVM newRouteStop)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RouteStopVM> GetRouteStopByRouteId(int routeId)
        {
            IEnumerable<RouteStopVM> results = null;
            try
            {
                results = _routeStopAccessor.selectRouteStopByRouteId(routeId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Something went wrong, we're verry sorry!", e);
            }
            return results;
        }
    }
}
