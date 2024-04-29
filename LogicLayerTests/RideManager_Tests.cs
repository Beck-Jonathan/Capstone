using DataAccessFakes;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-04-23
    /// <br />
    ///  Test class for RideManager
    /// </summary>
    [TestClass]
    public class RideManager_Tests
    {
        private RideManager _rideManager;
        private List<Ride_VM> _testRides = new List<Ride_VM>
        {
            new Ride_VM { RideID = 1, IsActive = true },
            new Ride_VM { RideID = 2 },
            new Ride_VM { RideID = 3 },
            new Ride_VM { RideID = 4 },
            new Ride_VM { RideID = 5 },
            new Ride_VM { RideID = 6 }
        };

        [TestInitialize]
        public void TestInitialize()
        {
            _rideManager = new RideManager(new RideAccessorFake(_testRides));
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Ensure that the AddRide method is called and calls the IRideAccessor correctly
        /// </summary>
        [TestMethod]
        public void CreateRide_CallsAccessorCorrectly()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { VIN = "vin" };

            // Act
            bool result = _rideManager.AddRide(ride);

            // Assert
            Assert.AreEqual(result, true);
            Assert.IsTrue(_testRides.Any(r => ride.VIN == r.VIN));
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the AddRide method fails when the IRideAccessor returns an invalid ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateRide_FailsOnInvalidIDReturned()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { VIN = "test_fail" };

            // Act
            _rideManager.AddRide(ride);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the AddRide method fails when the IRideAccessor throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateRide_FailsOnExceptionFromAccessor()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { VIN = "test_except" };

            // Act
            _rideManager.AddRide(ride);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Ensure that the EditRide method calls the IRideAccessor correctly
        /// </summary>
        [TestMethod]
        public void EditRide_CallsAccessorCorrectly()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { RideID = 1, VIN = "vin" };

            // Act
            bool result = _rideManager.EditRide(ride);

            // Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(ride.VIN, _testRides.First().VIN);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the EditRide method fails when the IRideAccessor reports no rows affected;
        ///  Indicates that the ride likely does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditRide_FailsOnNoRowsAffected()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { RideID = -1 };

            // Act
            _rideManager.EditRide(ride);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the EditRide method fails when the IRideAccessor throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditRide_FailsOnExceptionFromAccessor()
        {
            // Arrange
            Ride_VM ride = new Ride_VM { RideID = -2 };

            // Act
            _rideManager.EditRide(ride);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Ensure that the GetRidesByClientID method calls the IRideAccessor correctly
        /// </summary>
        [TestMethod]
        public void GetRidesByClientID_GetsCorrectRides()
        {
            // Arrange
            int[] expectedIds = _testRides.Select(r => r.RideID).ToArray();

            // Act
            var retrievedIds = _rideManager.GetRidesByClientID(1).Select(r => r.RideID).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(expectedIds, retrievedIds);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the GetRidesByClientID method fails when the IRideAccessor throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRidesByClientID_FailsOnExceptionFromAccessor()
        {
            // Act
            _rideManager.GetRidesByClientID(-1);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Ensure that the DeactivateRide method calls the IRideAccessor correctly
        /// </summary>
        [TestMethod]
        public void DeactivateRide_CallsAccessorCorrectly()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _rideManager.DeactivateRide(id);

            // Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(_testRides.First().IsActive, false);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the DeactivateRide method fails when no rows are affected
        ///  Implies that the ride ID does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeactivateRide_FailsOnNoRowsAffected()
        {
            // Act
            _rideManager.DeactivateRide(-1);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the DeactivateRide method fails when the IRideAccessor throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeactivateRide_FailsOnExceptionFromAccessor()
        {
            // Act
            _rideManager.DeactivateRide(-2);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the SelectRideByID method calls the IRideAccessor correctly
        /// </summary>
        [TestMethod]
        public void SelectRideByID_ReturnsCorrectRide()
        {
            // Arrange
            int expectedID = 1;

            // Act
            int retrievedID = _rideManager.GetRideByID(expectedID).RideID;

            // Assert
            Assert.AreEqual(expectedID, retrievedID);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the SelectRideByID method fails when the IRideAccessor returns null
        ///  Implies that the ride ID does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectRideByID_FailsWhenRideIsNull()
        {
            _rideManager.GetRideByID(-1);
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-24
        ///  <br />
        ///  Ensure that the SelectRideByID method fails when the IRideAccessor throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SelectRideByID_FailsOnExceptionFromAccessor()
        {
            _rideManager.GetRideByID(-2);
        }
    }
}
