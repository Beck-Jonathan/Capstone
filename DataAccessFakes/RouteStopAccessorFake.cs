using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RouteStopAccessorFake : IRouteStopAccessor
    {
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// DATE: 2024-03-05
        /// Fake Data for the Route Stops table.
        /// </summary>
        public List<RouteStopVM> _routeStops;
        public RouteStopAccessorFake()
        {
            _routeStops = new List<RouteStopVM>()
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
                        RouteId = 100002,
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
            };
        }
        public int ActivateRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;
            foreach (RouteStopVM routeStop in _routeStops)
            {
                if (routeStop.RouteId == routeStopVM.RouteId &&
                    routeStop.StopId == routeStopVM.StopId &&
                    routeStop.StopNumber == routeStopVM.StopNumber)
                {
                    routeStop.IsActive = true;
                    result++;
                }
            }
            return result;
        }

        public int DeactivateRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;
            foreach(RouteStopVM routeStop in _routeStops)
            {
                if(routeStop.RouteId == routeStopVM.RouteId &&
                    routeStop.StopId == routeStopVM.StopId &&
                    routeStop.StopNumber == routeStopVM.StopNumber)
                {
                    routeStop.IsActive = false;
                    result++;
                }
            }
            return result;
        }

        public int InsertRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;
            if(_routeStops.Where(
                routeStop => routeStop.RouteId == routeStopVM.RouteId &&
                    routeStop.StopId == routeStopVM.StopId &&
                    routeStop.StopNumber == routeStopVM.StopNumber
                ).Any())
            {
                throw new ArgumentException("RouteStop already exists!");
            }
            _routeStops.Add(routeStopVM);
            result = _routeStops.Count;
            return result;
        }

        public IEnumerable<RouteStopVM> selectRouteStopByRouteId(int routeId)
        {
            return _routeStops.Where(routeStop => routeStop.RouteId == routeId).ToList();
        }

        public int UpdateRouteStop(RouteStopVM oldRouteStopVM, RouteStopVM newRouteStopVM)
        {
            int result = 0;
            if(oldRouteStopVM.RouteId != newRouteStopVM.RouteId || oldRouteStopVM.StopId != newRouteStopVM.StopId) 
            {
                throw new ArgumentException("RouteStop objects don't match!");
            }
            for(int i = 0; i < _routeStops.Count; i++)
            {
                if (_routeStops[i].RouteId == oldRouteStopVM.StopId && _routeStops[i].StopId == oldRouteStopVM.StopId)
                {
                    _routeStops[i] = newRouteStopVM;
                    result = i;
                }
            }


            return result;
        }
    }
}
