using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using DataAccessFakes;
using LogicLayer;
using LogicLayer.RouteStop;
using DataObjects.RouteObjects;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-19
    /// <br />
    ///     Class for testing RouteStop Functionality
    /// </summary>
    [TestClass]
    public class RouteStopManagerTests
    {
        public IRouteStopManager _routeStopManager;
        [TestInitialize] public void Init()
        {
            _routeStopManager = new RouteStopManager(new RouteStopAccessorFake());
        }
        [TestMethod]
        public void TestGetRouteStopsByIdReturnsData()
        {
            int expectedCount = 2;
            int actualCount = 0;
            actualCount = _routeStopManager.GetRouteStopByRouteId(100001).Count();
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestAddStopToRoute_SuccessfullyAdds()
        {
            int expectedNum = 4;
            int actualNum = 0;

            RouteStopVM routeStopVM = new RouteStopVM()
            {
                RouteId = 100003,
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
            };

            actualNum = _routeStopManager.AddRouteStop(routeStopVM);

            Assert.AreEqual(expectedNum, actualNum);
        }

        [TestMethod]
        public void TestUpdatingOrdinal_SuccessfullyUpdates()
        {
            bool expectedOrdinal = true;
            bool actualOrdinal = false;
            RouteStopVM fakeUpdate = new RouteStopVM()
            {
                RouteStopId = 1,
                RouteId = 100001,
                StopId = 0,
                StopNumber = 3,
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
            };
            actualOrdinal = _routeStopManager.UpdateOrdinal(fakeUpdate);

            Assert.AreEqual(expectedOrdinal, actualOrdinal);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdatingOrdinal_ExceptsWhenNoRecordExists()
        {
            bool expectedOrdinal = true;
            bool actualOrdinal = false;
            RouteStopVM fakeUpdate = new RouteStopVM()
            {
                RouteStopId = 8000,
                RouteId = 100001,
                StopId = 0,
                StopNumber = 3,
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
            };
            actualOrdinal = _routeStopManager.UpdateOrdinal(fakeUpdate);

            Assert.AreEqual(expectedOrdinal, actualOrdinal);
        }

        [TestMethod]
        public void TestDeactivatingRouteStop_Succeeds()
        {
            int expectedResult = 1;
            int actualResult = 0;

            RouteStopVM toBeDeleted = new RouteStopVM()
            {
                RouteStopId = 1,
                RouteId = 100001,
                StopId = 0,
                StopNumber = 3,
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
            };

            actualResult = _routeStopManager.DeleteRouteStop(toBeDeleted);

            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestDeactivatingRouteStop_SuccessfullyNoUpdates()
        {
            int expectedResult = 0;
            int actualResult = 1;

            RouteStopVM toBeDeleted = new RouteStopVM()
            {
                RouteStopId = 80,
                RouteId = 100001,
                StopId = 0,
                StopNumber = 3,
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
            };

            actualResult = _routeStopManager.DeleteRouteStop(toBeDeleted);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
