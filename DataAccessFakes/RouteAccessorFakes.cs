using DataAccessInterfaces;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Fake Database Access for Route Unit Tests
    /// </summary>
    public class RouteAccessorFakes : IRouteAccessor
    {
        List<RouteVM> routes;

        public RouteAccessorFakes()
        {
            routes = new List<RouteVM>();
            routes.Add(new RouteVM()
            {
                RouteId = 100001,
                RouteName = "Weekend",
                StartTime = new Time(8, 0, 0),
                EndTime = new Time(16, 0, 0),
                RepeatTime = new TimeSpan(4, 0, 0),
                DaysOfService = new ActivityWeek(new char[] { '0', '0', '0', '0', '0', '1', '1' }),
                IsActive = true
            });
            routes.Add(new RouteVM()
            {
                RouteId = 100002,
                RouteName = "Weekday",
                StartTime = new Time(12, 0, 0),
                EndTime = new Time(16, 0, 0),
                RepeatTime = new TimeSpan(4, 0, 0),
                DaysOfService = new ActivityWeek(new char[] { '1', '1', '1', '1', '1', '0', '0' }),
                IsActive = true
            });
            routes.Add(new RouteVM()
            {
                RouteId = 100003,
                RouteName = "Every Day",
                StartTime = new Time(12, 0, 0),
                EndTime = new Time(20, 0, 0),
                RepeatTime = new TimeSpan(2, 0, 0),
                DaysOfService = new ActivityWeek(new char[] { '1', '1', '1', '1', '1', '1', '1' }),
                IsActive = true
            });
            routes.Add(new RouteVM()
            {
                RouteId = 100004,
                RouteName = "Drunk Bus",
                StartTime = new Time(18, 0, 0),
                EndTime = new Time(23, 59, 59),
                RepeatTime = new TimeSpan(2, 0, 0),
                DaysOfService = new ActivityWeek(new char[] { '0', '0', '0', '1', '1', '1', '0' }),
                IsActive = true
            });

        }

        public int ActivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int DeactivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int InsertRoute(RouteVM route)
        {
            throw new NotImplementedException();
        }

        public RouteVM selectRouteById(int routeId)
        {
            RouteVM result = null;
            foreach(RouteVM route in routes)
            {
                if(route.RouteId == routeId)
                {
                    result = route;
                }
            }
            return result;
        }

        public List<RouteVM> selectRoutes()
        {
            return routes;
        }

        public int UpdateRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            throw new NotImplementedException();
        }
    }
}
