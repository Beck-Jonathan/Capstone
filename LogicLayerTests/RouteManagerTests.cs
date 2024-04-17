using System;
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
    /// </remarks>
    [TestClass]
    public class RouteManagerTests
    {
        IRouteManager _routeManager;
        [TestInitialize]
        public void Init() 
        { 
            _routeManager = new RouteManager(new RouteAccessorFakes());
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
