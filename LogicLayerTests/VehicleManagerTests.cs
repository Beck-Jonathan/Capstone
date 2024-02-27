using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessFakes;
using DataObjects;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Tests for working with vehicle data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 
    /// <br />
    /// 
    ///     Initial creation
    ///     Added Vehicle Lookup 
    /// </remarks>
    /// <remarks>
    /// UPDATE: Chris Baenziger
    /// UPDATED: 2024-02-25
    /// Added tests for deactivating vehicle
    /// </remarks>


    [TestClass]
    public class VehicleManagerTests
    {
        private VehicleManager _vehicleManager = null;
        private VehicleManager _vehicleLookupMgr = null;


        [TestInitialize]
        public void TestSetup()
        {
            _vehicleManager = new VehicleManager(new VehicleAccessorFakes());
            _vehicleLookupMgr = new VehicleManager(new VehicleAccessorFakes());

        }

        [TestMethod]
        public void TestAddVehicleReturnsTrue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.AddVehicle(
                new Vehicle()
                {
                    VIN = "testaddgoodvin123",
                    VehicleNumber = "Test-01",
                    VehicleMileage = 1000,
                    ModelLookupID = 100000,
                    VehicleLicensePlate = "Test01",
                    VehicleMake = "Mercedes",
                    VehicleModel = "Sprinter",
                    VehicleDescription = "Van",
                    VehicleYear = 2024,
                    MaxPassengers = 10,
                    Rental = false,
                    DateEntered = DateTime.Now
                }) ;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddVehicleReturnsErrorWithDuplicateData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.AddVehicle(
                new Vehicle()
                {
                    VIN = "testaddvin1234567",
                    VehicleNumber = "Test-01",
                    VehicleMileage = 1000,
                    ModelLookupID = 100000,
                    VehicleLicensePlate = "Test01",
                    VehicleMake = "Mercedes",
                    VehicleModel = "Sprinter",
                    VehicleDescription = "Van",
                    VehicleYear = 2024,
                    MaxPassengers = 10,
                    Rental = false,
                    DateEntered = DateTime.Now
                });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetVehicleTypesReturnsListOfTypes()
        {
            int exectedResult = 2;
            int acutalResolt = 0;

            acutalResolt = _vehicleManager.GetVehicleTypes().Count;

            Assert.AreEqual(exectedResult, acutalResolt);
        }

        [TestMethod]
        public void TestGetVehicleMakesReturnsListOfMakes()
        {
            int exectedResult = 2;
            int acutalResolt = 0;

            acutalResolt = _vehicleManager.GetVehicleMakes().Count;

            Assert.AreEqual(exectedResult, acutalResolt);
        }

        [TestMethod]
        public void TestGetVehicleModelsReturnsListOfModels()
        {
            int exectedResult = 2;
            int acutalResolt = 0;

            acutalResolt = _vehicleManager.GetVehicleModels().Count;

            Assert.AreEqual(exectedResult, acutalResolt);
        }

        [TestMethod]
        public void TestGetVehicleDetailReturnsCorrectVehicle()
        {
            string expectedResult = "testaddvin1234567";
            string actualResult = "";

            actualResult = _vehicleManager.GetVehicleByVehicleNumber("Test-01").VIN;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetVehicleDetailReturnsExceptionWithInvalidVehicleNumber()
        {
            string expectedResult = "";
            string actualResult = "";

            actualResult = _vehicleManager.GetVehicleByVehicleNumber("00").VIN;
        }

        [TestMethod]
        public void TestSelectAllVehiclesForVehicleLookupListCountPasses()
        {
            //arrange
            int expected = 2;
            int actual = 0;


            //act
            actual = _vehicleLookupMgr.VehicleLookupList().Count;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestSelectAllVehiclesForVehicleLookupListCountFails()
        {
            //arrange
            int expected = 50;
            int actual = 0;


            //act
            actual = _vehicleLookupMgr.VehicleLookupList().Count;

            //assert
            Assert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void TestUpdateVehicleUpdatesVehicleWithCorrectData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.EditVehicle(new Vehicle()
            {
                VIN = "testaddvin1234567",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 3
            }, new Vehicle()
            {
                VIN = "testaddvin1234567",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 3
            }); ;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateVehicleReturnsExceptionWithInvalidData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.EditVehicle(new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2025,
                MaxPassengers = 100
            }, new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test10",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 10
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAddModelLookupReturnsTrue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.AddModelLookup(new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100000,
                VehicleLicensePlate = "Test10",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 10
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddModelLookupThrowsErrorWithDuplicateData()
        {
            bool expectedResult = false;
            bool actualResult = true;

            actualResult = _vehicleManager.AddModelLookup(new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test10",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 10
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetModelLookupIDReturnsVehicleWithLookupID()
        {
            int expectedResult = 100001;
            int actualResult = 0;

            actualResult = _vehicleManager.GetModelLookupID(new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test10",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 10
            }).ModelLookupID;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetModelLookupThrowsErrorWithBadData()
        {
            int expectedResult = 100001;
            int actualResult = 0;

            actualResult = _vehicleManager.GetModelLookupID(new Vehicle()
            {
                VIN = "testaddvin9874567",
                VehicleNumber = "Test-00",
                VehicleMileage = 1010,
                ModelLookupID = 100000,
                VehicleLicensePlate = "Test10",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 10
            }).ModelLookupID;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestDeactivateVehicleReturnsRowsAffected()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _vehicleManager.DeactivateVehicle(new Vehicle()
            {
                VIN = "testaddvin1234567",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                ModelLookupID = 100001,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 3,
                VehicleType = "Van",
                VehicleDescription = "Van",
                Rental = false,
                DateEntered = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateVehicleThrowsErrorWithBadData()
        {
            bool expectedResult = false;
            bool actualResult = true;

            actualResult = _vehicleManager.DeactivateVehicle(new Vehicle()
            {
                VIN = "testaddgoodvin123",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                ModelLookupID = 100000,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleDescription = "Van",
                VehicleYear = 2024,
                MaxPassengers = 10,
                Rental = false,
                DateEntered = DateTime.Now
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

    }

}

