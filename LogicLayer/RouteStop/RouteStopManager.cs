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
