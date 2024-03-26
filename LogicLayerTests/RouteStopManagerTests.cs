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
    }
}
