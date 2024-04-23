using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;
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

        [TestMethod]
        public void ActivateRouteByIdReturnsCorrectValue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _routeManager.DeactivateRoute(100002);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivateRouteByIdThrowsExceptionWithBadId()
        {
            bool expectedResult = false;
            bool actualResult = false;

            actualResult = _routeManager.DeactivateRoute(10);

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

        


    }
}
