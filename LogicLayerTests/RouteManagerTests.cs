﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer;
using LogicLayer.RouteStop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Tests for the RouteManager class; does not test database connections.
    /// </summary>
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-03-05
    /// COMMENTS:
    ///     Added Activate and deactivate route tests.
    /// <br /><br />
    /// UPDATER: Michael Springer
    /// UPDATED: 2024-04-19
    /// COMMENTS:
    ///     Added tests for getAllRoutesWithStops
    /// </remarks>
    [TestClass]
    public class RouteManagerTests
    {
        IRouteManager _routeManager;
        // for testing getRoutesWithStops
        IRouteManager _routeManager2;

        [TestInitialize]
        public void Init() 
        { 
            _routeManager = new RouteManager(new RouteAccessorFakes());
            _routeManager2 = new RouteManager(new RouteAccessorFakes(), new RouteStopAccessorFake());
        }
        [TestMethod]
        public void TestGetRoutesGetsListofRoutes()
        {
            int expected = 4;
            int actual = 0;

            actual = _routeManager.getRoutes().Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeactivateRouteByIdReturnsCorrectValue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _routeManager.DeactivateRoute(100002);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeactivateRouteByIdThrowsExceptionWithBadId()
        {
            bool expectedResult = false;
            bool actualResult = false;

            actualResult = _routeManager.DeactivateRoute(10);

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// /// <br /><br />
        ///    UPDATER: Nathan Toothaker
        /// <br />
        ///    UPDATED: 2024-04-12
        /// <br />
        ///     Method was written to test _routeManager.ActivateRoute(int routeId), 
        ///     but was calling _routeManager.DeactivateRoute(int routeId)
        ///     Changed method call.
        /// </remarks>
        [TestMethod]
        public void ActivateRouteByIdReturnsCorrectValue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _routeManager.ActivateRoute(100002);

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// /// <br /><br />
        ///    UPDATER: Nathan Toothaker
        /// <br />
        ///    UPDATED: 2024-04-12
        /// <br />
        ///     Method was written to test _routeManager.ActivateRoute(int routeId), 
        ///     but was calling _routeManager.DeactivateRoute(int routeId)
        ///     Changed method call.
        /// </remarks>

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivateRouteByIdThrowsExceptionWithBadId()
        {
            bool expectedResult = false;
            bool actualResult = false;

            actualResult = _routeManager.ActivateRoute(10);

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED ON: 024-04-19
        /// </summary>
        [TestMethod]
        public void GetRoutesWithStops_GetsListofRoutesWithStops()
        {
            int expected = 4;
            int actual = 0;

            // 4 routes
            IEnumerable<RouteVM> routes = _routeManager2.GetRoutesWithStops();
            actual = routes.Count();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED ON: 024-04-19
        /// </summary>
        [TestMethod]
        public void GetRoutesWithStops_IncludesRouteStopsContainingStops()
        {
            int expected = 2;
            int actual = 0;
            int expectedStopId = 0;
            string expectedStopZip = "52404";

            IEnumerable<RouteVM> routes = _routeManager2.GetRoutesWithStops();
            //route 100001 has 2 stops (from RoutestopAccessorFake, not RouteStopFake)
            actual = routes.ElementAt(0).RouteStops.Count();

            //Assert
            // contains routeStops
            Assert.AreEqual(expected, actual);
            // routeStop 0 contains Valid Stop data
            Assert.AreEqual(routes.ElementAt(0).RouteStops.ElementAt(0).stop.StopId, expectedStopId);
            Assert.AreEqual(routes.ElementAt(0).RouteStops.ElementAt(0).stop.ZIPCode, expectedStopZip);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED ON: 024-04-19
        /// </summary>
        [TestMethod]
        public void GetRouteWithStops_RouteWithoutStopsReturnsEmptyList()
        {
            int expected = 0;
            int actual = 5;

            // route at index 3 has no stops
            actual = _routeManager2.GetRoutesWithStops().ElementAt(3).RouteStops.Count();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED ON: 024-04-19
        /// </summary>
        [TestMethod]
        public void GetRouteById_ReturnsCorrectRoute()
        {
            string expected = "Weekend";
            string actual = "Weekday";

            actual = _routeManager2.getRouteById(100001).RouteName;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED ON: 024-04-19
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void GetRouteById_InvalidIdThrowsException()
        {
            string expected = "FakeRouteName";
            string actual = _routeManager.getRouteById(10).RouteName;
        }

        /// <summary>
        /// Cread By: Nathan Toothaker <br />
        /// Created: 2024-04-12 <br />
        /// Tests to ensure that the Route Manager can successfully add a route to the list.
        /// </summary>
        [TestMethod]
        public void InsertRoute_SuccessfullyInserts()
        {
            int expectedResult = 5;
            int actualResult = 0;
            RouteVM newRoute = new RouteVM()
            {
                RouteId = 8,
                RouteName = "IAmANewRoute",
                StartTime = new DataObjects.HelperObjects.Time(7, 0, 0),
                EndTime = new DataObjects.HelperObjects.Time(11, 0, 0),
                RepeatTime = new TimeSpan(1, 0, 0),
                DaysOfService = new DataObjects.HelperObjects.ActivityWeek("1100111".ToCharArray())
            };

            actualResult = _routeManager.AddRoute(newRoute);

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Cread By: Nathan Toothaker <br />
        /// Created: 2024-04-12 <br />
        /// Tests to ensure that the Route Manager can't add a route that already exists.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InsertRoute_FailsInsert()
        {
            int expectedResult = 5;
            int actualResult = 0;
            RouteVM newRoute = new RouteVM()
            {
                RouteId = 100001,
                RouteName = "IAmANewRoute",
                StartTime = new DataObjects.HelperObjects.Time(7, 0, 0),
                EndTime = new DataObjects.HelperObjects.Time(11, 0, 0),
                RepeatTime = new TimeSpan(1, 0, 0),
                DaysOfService = new DataObjects.HelperObjects.ActivityWeek("1100111".ToCharArray())
            };

            actualResult = _routeManager.AddRoute(newRoute);

        }
        /// <summary>
        /// Cread By: Nathan Toothaker <br />
        /// Created: 2024-04-12 <br />
        /// Tests to ensure that the Route Manager can successfully update a route to the list.
        /// </summary>
        [TestMethod]
        public void EditRoute_Succeeds()
        {
            int expectedResult = 1;
            int actualResult = 0;
            RouteVM newRoute = new RouteVM()
            {
                RouteId = 100001,
                RouteName = "IAmANewRoute",
                StartTime = new DataObjects.HelperObjects.Time(7, 0, 0),
                EndTime = new DataObjects.HelperObjects.Time(11, 0, 0),
                RepeatTime = new TimeSpan(1, 0, 0),
                DaysOfService = new DataObjects.HelperObjects.ActivityWeek("1100111".ToCharArray())
            };
            // copy-pasted from routeAccessorFakes
            RouteVM oldRoute = new RouteVM()
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
            };
            actualResult = _routeManager.EditRoute(oldRoute, newRoute);

            Assert.AreEqual(expectedResult, actualResult);
        }
        /// <summary>
        /// Cread By: Nathan Toothaker <br />
        /// Created: 2024-04-12 <br />
        /// Tests to ensure that the Route Manager verifies that the routes changed have the same ID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditRoute_FailsChangingPrimaryKey()
        {
            int expectedResult = 1;
            int actualResult = 0;
            RouteVM newRoute = new RouteVM()
            {
                RouteId = 100001,
                RouteName = "IAmANewRoute",
                StartTime = new DataObjects.HelperObjects.Time(7, 0, 0),
                EndTime = new DataObjects.HelperObjects.Time(11, 0, 0),
                RepeatTime = new TimeSpan(1, 0, 0),
                DaysOfService = new DataObjects.HelperObjects.ActivityWeek("1100111".ToCharArray())
            };
            // copy-pasted from routeAccessorFakes
            RouteVM oldRoute = new RouteVM()
            {
                RouteId = 100002,
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
            };
            actualResult = _routeManager.EditRoute(oldRoute, newRoute);

        }

    }


}
