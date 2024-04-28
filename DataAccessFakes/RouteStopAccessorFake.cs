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
                    RouteStopId = 1,
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
                        RouteStopId = 2,
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
                        RouteStopId = 3,
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
        /// <summary>
        /// Author: Nathan Toothaker
        /// Used to test deleting a RouteStop from the database.
        /// </summary>
        /// <param name="routeStopVM">The routeStop object to be deleted.</param>
        /// <returns></returns>
        public int DeleteRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;
            foreach(RouteStopVM routeStop in _routeStops)
            {
                if(routeStop.RouteStopId == routeStopVM.RouteStopId)
                {
                    routeStop.IsActive = false;
                    result++;
                }
            }
            return result;
        }
        /// <summary>
        /// Author: Nathan Toothaker
        /// Used to test adding a new routestop object.
        /// </summary>
        /// <param name="routeStopVM">The routestop to be added.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when routestop already exists.</exception>
        public int InsertRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;
            if(_routeStops.Where(
                routeStop => routeStop.RouteStopId == routeStopVM.RouteStopId
                ).Any())
            {
                throw new ArgumentException("RouteStop already exists!");
            }
            _routeStops.Add(routeStopVM);
            result = _routeStops.Count;
            return result;
        }
        /// <summary>
        /// used to get all RouteStops for a given route ID
        /// </summary>
        /// <param name="routeId">The ID of the route whose stops we want</param>
        /// <returns>An <see cref="IEnumerable{T}">IEnumerable of RouteStopVMs</see></returns>
        public IEnumerable<RouteStopVM> selectRouteStopByRouteId(int routeId)
        {
            return _routeStops.Where(routeStop => routeStop.RouteId == routeId).ToList();
        }

        /// <summary>
        /// Used to show an example of modifying a routestop entry's ordinal number in teh database.
        /// </summary>
        /// <param name="routeStopVM"></param>
        /// <returns></returns>
        public int UpdateOrdinal(RouteStopVM routeStopVM)
        {
            int result = 0;
            try { 
            RouteStopVM stopToUpdate = _routeStops.Where(p => p.RouteStopId == routeStopVM.RouteStopId).First();

                stopToUpdate.StopNumber = routeStopVM.StopNumber;
                result = 1;
            } catch (Exception ex)
            {
                throw ex;
            }

            return result;
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
