using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteAssignment
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    ///  Manages Interaction Logic for Route Assignments
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public class RouteAssignmentManager : IRouteAssignmentManager
    {
        IRouteAssignmentAccessor _routeAssignmentAccessor;
        IRouteStopAccessor _routeStopAccessor;
        IBingMapsAccessor _bingMapsAccessor;
        public RouteAssignmentManager()
        {
            _routeAssignmentAccessor = new RouteAssignmentAccessor();
            _routeStopAccessor = new RouteStopAccessor();
            _bingMapsAccessor = new BingMapsAccessor();
        }
        public RouteAssignmentManager(IRouteAssignmentAccessor routeAssignmentAccessor)
        {
            _routeAssignmentAccessor = routeAssignmentAccessor;
            _routeStopAccessor = new RouteStopAccessor();
            _bingMapsAccessor = new BingMapsAccessor();
        }
        public IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID)
        {
            List<Route_Assignment_VM> results = _routeAssignmentAccessor.GetAllRouteAssignmentByDriverID(Driver_ID).ToList();
            try
            {
                foreach (Route_Assignment_VM rs in results)
                {
                    if (rs != null && rs.routeVM != null)
                    {
                        rs.routeVM.RouteStops = _routeStopAccessor.selectRouteStopByRouteId(rs.routeVM.RouteId);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error getting list of Routes for Driver");
            }

            return results;
        }
        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-06
        /// <br />
        ///     Asynchronous method used to pull data for navigation from the Bing Maps API.
        /// </summary>
        /// <param name="route">The route whose path we're trying to calculate.</param>
        /// <returns>
        /// <see cref="Task">A Task</see> Object which, when awaited, returns a <see cref="BingMapsResponse">BingMapsResponse</see> Object.
        /// </returns>
        public async Task<BingMapsResponse> getRouteLineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> route)
        {
            BingMapsResponse result;
            try
            {
                result = await _bingMapsAccessor.getMapPolylineForRouteAssignmentVM(route);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Map service unavailable at this time, sorry!");
            }
            return result;
        }
    }
}
