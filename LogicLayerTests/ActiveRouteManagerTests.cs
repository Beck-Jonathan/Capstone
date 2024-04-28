using DataAccessFakes;
using DataObjects.RouteObjects;
using LogicLayer.RouteAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-04-23
    ///     Tests for the active route manager.
    /// </summary>

    [TestClass]
    public class ActiveRouteManagerTests
    {
        private ActiveRouteManager _activeRouteManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _activeRouteManager = new ActiveRouteManager(new ActiveRouteAccessorFakes());
        }

        [TestMethod]
        public void TestAddActiveRouteReturnsTrueWithCorrectData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _activeRouteManager.AddActiveRoute(new ActiveRoute()
            {
                AssignmentID = 100003,
                DriverID = 100003,
                VIN = "WAUZZZ4G6BN123456",
                StartTime = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddActiveRouteReturnsExceptionWithBadData()
        {
            bool expectedResult = false;
            bool actualResult = true;

            actualResult = _activeRouteManager.AddActiveRoute(new ActiveRoute()
            {
                AssignmentID = 100000,
                DriverID = 100000,
                VIN = "WAUZZZ4G6BN123456",
                StartTime = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEndActiveRouteReturnsTrueWithCorrectData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _activeRouteManager.EndActiveRoute(new ActiveRoute()
            {
                AssignmentID = 100002,
                DriverID = 100002,
                VIN = "JM1BK32F781234567",
                EndTime = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEndActiveRouteReturnsExceptionWithBadData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _activeRouteManager.EndActiveRoute(new ActiveRoute()
            {
                AssignmentID = 100003,
                DriverID = 100003,
                VIN = "JM1BK32F781234567",
                EndTime = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
