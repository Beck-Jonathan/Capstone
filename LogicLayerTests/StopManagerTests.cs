using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessFakes;
using DataObjects;
using LogicLayer.RouteStop;
using DataObjects.RouteObjects;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Stop manager test classes
    /// </summary>
    [TestClass]
    public class StopManagerTests
    {
        IStopManager _stopManager;
        [TestInitialize]
        public void Init()
        {
            _stopManager = new StopManager(new StopAccessorFakes());
        }

        [TestMethod]
        public void TestGetStopsReturnsListOfStops()
        {
            int expected = 4;
            int actual = 0;

            actual = _stopManager.GetStops().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertStopReturnsCorrectValue()
        {
            int expected = 100004;
            int actual = 0;

            actual = _stopManager.AddStop(new Stop()
            {
                StopId = 100004,
                StreetAddress = "901 Add Street",
                ZIPCode = "52240",
                Latitude = 41.6578m,
                Longitude = 91.5346m,
                IsActive = true
            });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInsertStopThrowsExceptionWithDuplicateID()
        {
            int expected = 100000;
            int actual = 0;

            actual = _stopManager.AddStop(new Stop()
            {
                StopId = 100002,
                StreetAddress = "901 Add Street",
                ZIPCode = "52240",
                Latitude = 41.6578m,
                Longitude = 91.5346m,
                IsActive = true
            });

            Assert.AreEqual(expected, actual);
        }
    }
}
