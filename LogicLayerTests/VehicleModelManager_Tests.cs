using DataAccessFakes;
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
    /// CREATED: 2024-03-03
    /// <br />
    ///     Class for testing VehicleModelManager functionality
    /// </summary>
    [TestClass]
    public class VehicleModelManager_Tests
    {
        private VehicleModelManager _vehicleModelManager;
        private List<VehicleModel> _testVehicleModels;

        /// <summary>
        ///     Initialize the required test objects
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-03
        /// </remarks>
        [TestInitialize]
        public void TestInitialize()
        {
            _testVehicleModels = new List<VehicleModel>()
            {
                new VehicleModel
                {
                    VehicleModelID = 100003,
                    Name = "Student Deliverer 10000",
                    VehicleTypeID = "Schoolbus",
                    Make = "Smith Motors",
                    Year = 2009,
                    IsActive = true,
                },
                new VehicleModel
                {
                    VehicleModelID = 100004,
                    Name = "Lighting",
                    VehicleTypeID = "Taxi Cab",
                    Make = "Malway Engineering",
                    Year = 1987,
                    IsActive = true
                },
                new VehicleModel
                {
                    VehicleModelID = 100005,
                    Name = "Maxim 12",
                    VehicleTypeID = "Passenger Bus",
                    Make = "Maxim Tech",
                    Year = 2024,
                    IsActive = true
                },
                new VehicleModel
                {
                    VehicleModelID = 100007,
                    Name = "Inactive Vehicle",
                    VehicleTypeID = "Inactive Type",
                    Make = "",
                    Year = 2000,
                    IsActive = true
                }
            };

            var vehicleModelAccessor = new VehicleModelAccessorFake(_testVehicleModels);

            _vehicleModelManager = new VehicleModelManager(vehicleModelAccessor);
        }

        /// <summary>
        ///     Test that GetVehicleModels returns the expected vehicle models
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-03
        /// </remarks>
        [TestMethod]
        public void GetVehicleModels_ReturnsCorrectVehicleModels()
        {
            // Arrange
            var expectedVehicleModels = new List<VehicleModel>()
            {
                new VehicleModel
                {
                    VehicleModelID = 100003,
                    Name = "Student Deliverer 10000",
                    VehicleTypeID = "Schoolbus",
                    Make = "Smith Motors",
                    Year = 2009,
                    IsActive = true,
                },
                new VehicleModel
                {
                    VehicleModelID = 100004,
                    Name = "Lighting",
                    VehicleTypeID = "Taxi Cab",
                    Make = "Malway Engineering",
                    Year = 1987,
                    IsActive = true
                },
                new VehicleModel
                {
                    VehicleModelID = 100005,
                    Name = "Maxim 12",
                    VehicleTypeID = "Passenger Bus",
                    Make = "Maxim Tech",
                    Year = 2024,
                    IsActive = true
                }
            };

            // Act
            var retrievedVehicleModels = _vehicleModelManager.GetVehicleModels().ToList();

            // Assert
            Assert.IsTrue(
                expectedVehicleModels.All(expectedVehicleModel =>
                    retrievedVehicleModels
                        .Where(retrievedVehicleModel => VehicleModelsAreEqual(expectedVehicleModel, retrievedVehicleModel))
                        .Count() == 1));
        }

        /// <summary>
        ///     Test that InsertVehicleModel inserts the vehicle correctly
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-19
        /// </remarks>
        [TestMethod]
        public void InsertVehicleModel_InsertsVehicleCorrectly()
        {
            // Arrange
            VehicleModelVM vehicle = new VehicleModelVM
            {
                Name = "TestName",
                Make = "TestMake",
                Year = 1999
            };

            // Act
            _vehicleModelManager.AddVehicleModel(vehicle);

            // Assert
            Assert.IsTrue(VehicleModelsAreEqual(vehicle, _testVehicleModels.Last()));
        }

        /// <summary>
        ///     Determines whether two vehicle models are equal
        /// </summary>
        /// <param name="x">
        ///    The first vehicle model
        /// </param>
        /// <param name="y">
        ///    The second vehicle model
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether the vehicle models are equal
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="VehicleModel">VehicleModel</see> x: The first vehicle model
        /// <br />
        ///    <see cref="VehicleModel">VehicleModel</see> y: The second vehicle model
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-03
        /// </remarks>
        private bool VehicleModelsAreEqual(VehicleModel x, VehicleModel y)
        {
            return
                x.VehicleModelID == y.VehicleModelID &&
                x.VehicleTypeID == y.VehicleTypeID &&
                x.Name == y.Name &&
                x.Make == y.Make &&
                x.Year == y.Year &&
                x.IsActive == y.IsActive;
        }

        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        public void TestGetVehicleModelByVINPasses()
        {
            string vin = "aaaabbbbcccceeee0";
            int expectedModelID = 100003;
            int actualModelID = 0;

            VehicleModel vehicleModel = _vehicleModelManager.GetVehicleModelByVIN(vin);
            actualModelID = vehicleModel.VehicleModelID;

            Assert.AreEqual(expectedModelID, actualModelID);
        }
        //Created By: James Williams
        //Date: 2024-04-26
        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void TestGetVehicleModelByVINFailesThrowsSystemException()
        {
            //supply a vin that doesn't exist in the fake data file
            string vin = "fakefakefakefake1";
            VehicleModel vm = _vehicleModelManager.GetVehicleModelByVIN(vin);
        }
    }
}
