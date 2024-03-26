using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;
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

    }
}
