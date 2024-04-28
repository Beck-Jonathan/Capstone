using DataAccessFakes;
using DataObjects.Assignment;
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

        [TestMethod]
        public void AddRouteAssignmentPasses()
        {

            bool actual = false;
            actual = _routeAssignmentManager.AddRouteAssignment(100000, "abcdefghijklmnop", 100000, new DateTime(2022, 02, 02),
                new DateTime(2022, 02, 03));
            Assert.IsTrue(actual);
        }

        //Fails test already exists
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void AddRouteAssignmentFailsArgumentException()
        {
            _routeAssignmentManager.AddRouteAssignment(3, "ABCDEFGH123456789", 3, new DateTime(2002, 01, 01), new DateTime(2002, 01, 02));
        }

        [TestMethod]
        public void AddVehicleAndDriverUnavailability()
        {
            bool result = false;
            result = _routeAssignmentManager.AddVehicleAndDriverUnavailabilites("1234567891222222222", 2, new DateTime(2024, 03, 04), new DateTime(2024, 03, 05), "Test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAvailableDriversReturnsCorrectCount()
        {
            int expected = 1;
            int actual = 0;

            //This should return only 1 because the dates should eliminate one driver
            // And the passenger count should eliminate the other driver
            List<Driver> drivers = _routeAssignmentManager.GetAvailableDriversByDateAndPassengerCount(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02), 7);

            actual = drivers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void GetAvailableDriversThrowsException()
        {
            //Doesn't return any drivers because of passenger count
            _routeAssignmentManager.GetAvailableDriversByDateAndPassengerCount(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02), 55);
        }

        [TestMethod]
        public void GetAvailableVehiclesReturnsCorrectCount()
        {
            int expected = 2;
            int actual = 0;

            List<VehicleAssignment> vehicles = _routeAssignmentManager.GetAvailableVehiclesByDateAndPassengerCount(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02), 7);
            actual = vehicles.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void GetAvailableVehiclesThrowsException()
        {
            _routeAssignmentManager.GetAvailableVehiclesByDateAndPassengerCount(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02), 50);
        }

        [TestMethod]
        public void GetRouteAssignmentsByRouteIDPassesCountReturned()
        {
            int expected = 1;
            int actual = 0;

            List<Route_Assignment> assignments = _routeAssignmentManager.GetRouteAssignmentsByRouteIDAndDate(2, new DateTime(2001, 01, 01), new DateTime(2001, 01, 03));
            actual = assignments.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void GetRouteAssignmentsByRouteIDThrowsException()
        {
            _routeAssignmentManager.GetRouteAssignmentsByRouteIDAndDate(2, new DateTime(2003, 01, 01), new DateTime(2003, 01, 03));
        }

        [TestMethod]
        public void GetRouteAssignmentDriverPasses()
        {
            Driver expectedDriver = new Driver()
            {
                Employee_ID = 1,
                Given_Name = "Bob",
                Family_Name = "Trapp",
                Driver_License_Class_ID = "C",
                Max_Passenger_Count = 5
            };
            Driver actualDriver = null;
            int routeAssignmentID = 1;
            actualDriver = _routeAssignmentManager.GetRouteAssignmentDriverByAssignmentID(routeAssignmentID);

            Assert.AreEqual(expectedDriver.Employee_ID, actualDriver.Employee_ID);
            Assert.AreEqual(expectedDriver.Given_Name, actualDriver.Given_Name);
            Assert.AreEqual(expectedDriver.Family_Name, actualDriver.Family_Name);
            Assert.AreEqual(expectedDriver.Driver_License_Class_ID, actualDriver.Driver_License_Class_ID);
            Assert.AreEqual(expectedDriver.Max_Passenger_Count, actualDriver.Max_Passenger_Count);

        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void TestGetRouteAssignmentDriversFailsBadRouteIDGivenThrowsException()
        {
            int badRouteID = 20;
            _routeAssignmentManager.GetRouteAssignmentDriverByAssignmentID(badRouteID);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        public void GetAvailableDriversByDatePasses()
        {
            int expected = 2;
            int actual = 0;

            List<Driver> drivers = _routeAssignmentManager.GetAvailableDriversByDate(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02));

            actual = drivers.Count();

            Assert.AreEqual(expected, actual);
        }

        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void GetAvailableDriversByDateFailsBadDatesThrowsException()
        {
            DateTime badStart = DateTime.Now.AddMonths(1);
            DateTime badEnd = DateTime.Now;

            _routeAssignmentManager.GetAvailableDriversByDate(badStart, badEnd);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        public void GetAvailableVehiclesByDatePasses()
        {
            int expected = 3;
            int actual = 0;

            List<VehicleAssignment> vehicles = _routeAssignmentManager.GetAvailableVehiclesByDate(new DateTime(2002, 01, 01), new DateTime(2002, 01, 02));
            actual = vehicles.Count();

            Assert.AreEqual(expected, actual);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void GetAvailableVehiclesByDateFailsBadDatesThrowsException()
        {
            DateTime badStart = DateTime.Now.AddMonths(1);
            DateTime badEnd = DateTime.Now;

            _routeAssignmentManager.GetAvailableVehiclesByDate(badStart, badEnd);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        public void UpdateRouteAssignmentDriverPasses()
        {
            Assert.IsTrue(_routeAssignmentManager.UpdateRouteAssignmentDriver(1, 7));
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void UpdateRouteAssignmentDriverFailsThrowsSystemException()
        {
            int badRouteID = 20;
            _routeAssignmentManager.UpdateRouteAssignmentDriver(badRouteID, 4);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void UpdateRouteAssignmentVehicleFailsThrowsSystemException()
        {
            int badRouteID = 20;
            _routeAssignmentManager.UpdateRouteAssignmentVehicle(badRouteID, "THISISONLYATEST06");
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        public void UpdateRouteAssignmentVehiclePasses()
        {
            Assert.IsTrue(_routeAssignmentManager.UpdateRouteAssignmentVehicle(1, "THISISONLYATEST06"));
        }
    }
}
