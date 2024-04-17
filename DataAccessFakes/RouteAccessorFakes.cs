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
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route fakes.
    /// </remarks> 
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
                IsActive = true,
                RouteStops = new List<RouteStopVM>()
                {
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 0,
                        StopNumber = 1,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 0,
                            StreetAddress = "6301 Kirkwood Blvd SW, Cedar Rapids, IA",
                            ZIPCode = "52404",
                            Latitude = 41.917250m,
                            Longitude = -91.656470m,
                            IsActive = true
                        }
                    },
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 1,
                        StopNumber = 2,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 1,
                            StreetAddress = "5008, 1220 1st Ave NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 41.989670m,
                            Longitude = -91.649529m,
                            IsActive = true
                        }
                    },
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 2,
                        StopNumber = 3,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 2,
                            StreetAddress = "1330 Elmhurst Dr NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 42.002548m,
                            Longitude = -91.652069m,
                            IsActive = true
                        }
                    }
                }
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

        public int UpdateRouteByIDAsActive(int routeId)
        {
            int rows = 0;
            foreach (var route in routes)
            {
                if (route.RouteId == routeId)
                {
                    route.IsActive = true;
                    rows++;
                }
            }
            if (rows != 1)
            {
                throw new ArgumentException("Route not found");
            }
            return rows;
        }

        public int UpdateRouteByIDAsInactive(int routeId)
        {
            int rows = 0;
            foreach (var route in routes)
            {
                if (route.RouteId == routeId)
                {
                    route.IsActive = false;
                    rows++;
                }
            }
            if (rows != 1)
            {
                throw new ArgumentException("Route not found");
            }
            return rows;
        }

        public int InsertRoute(RouteVM route)
        {
            if(routes.Where(fakeRoute => fakeRoute.RouteId == route.RouteId).Any())
            {
                throw new Exception("route with that id already exists");
            }
            routes.Add(route);
            return routes.Count();
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

        public IEnumerable<RouteVM> selectRoutes()
        {
            return routes;
        }

        public int UpdateRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            int result = 0;
            for (int i = 0;  i < routes.Count; i++)
            {
                if (routes[i].RouteId == oldRoute.RouteId)
                {
                    routes[i] = newRoute;
                    result = 1;
                    break;
                } else if (i == routes.Count - 1)
                {
                    throw new Exception("Old route not in dataset");
                }
            }
            
            return result;
        }
    }
}
