using DataAccessFakes;
using DataObjects.RouteObjects;
using LogicLayer.RouteAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Tests for Route Assignments Manager
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    [TestClass]
    public class RouteAssignmentManagerTests
    {
        RouteAssignmentManager _routeAssignmentManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            _routeAssignmentManager = new RouteAssignmentManager(new RouteAssignmentAccessorFake());
        }
        [TestMethod]
        public void GetAllRouteAssignmentByDriverID_WithValidDriverID_ReturnsCorrectAssignments()
        {
            // Arrange
            int validDriverID = 1;

            // Act
            IEnumerable<Route_Assignment_VM> assignments = _routeAssignmentManager.GetAllRouteAssignmentByDriverID(validDriverID);

            // Assert
            Assert.IsNotNull(assignments);
            Assert.AreEqual(2, assignments.Count());
            Assert.IsTrue(assignments.All(a => a.DriverID == validDriverID));
        }

        [TestMethod]
        public void GetAllRouteAssignmentByDriverID_WithInvalidDriverID_ReturnsEmptyList()
        {
            // Arrange
            int invalidDriverID = 999;

            // Act
            IEnumerable<Route_Assignment_VM> assignments = _routeAssignmentManager.GetAllRouteAssignmentByDriverID(invalidDriverID);

            // Assert
            Assert.IsNotNull(assignments);
            Assert.AreEqual(0, assignments.Count());
        }
    }
}
