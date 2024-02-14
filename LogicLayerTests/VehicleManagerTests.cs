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
                    VehicleLicensePlate = "Test01",
                    VehicleMake = "Mercedes",
                    VehicleModel = "Sprinter",
                    VehicleYear = 2024,
                    VehicleDescription = "Van"
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
                    VehicleLicensePlate = "Test01",
                    VehicleMake = "Mercedes",
                    VehicleModel = "Sprinter",
                    VehicleYear = 2024,
                    VehicleDescription = "Van"
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

    }
}
