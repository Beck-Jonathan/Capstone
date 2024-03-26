/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// Test Class for Parts_Inventory
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using System.Runtime.InteropServices;
using System.IO.Ports;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// Jonathan Beck
    /// Created: 2024/01/31
    /// 
    /// Initailzie the test with a set of a Parts_Inventory Manager liked to the Parts_Inventory Fakes
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    [TestClass]
    public class Parts_Inventory_Tests
    {
        IParts_InventoryManager _mgr = null;
        IVehicleModelManager _vehicleModelManager = null;
        
        [TestInitialize]
        public void testSetup()
        {
            _mgr = new Parts_InventoryManager(new Parts_Inventory_Fakes());
            VehicleManager _vmangaer = new VehicleManager(new VehicleAccessorFakes());
            List<string> _vehicles = _vmangaer.GetVehicleModels();
             

        }

        [TestMethod]
        public void TestUpdatePartsInventoryReturnsRowsAffected()
        {
            //arrange
            Parts_Inventory oldPart = _mgr.GetParts_InventoryByID(1);
            Parts_Inventory newPart = oldPart;
            newPart.Part_Quantity = 16;
            int expected = 1;
            int actual = 0;
            //act
            actual = _mgr.EditParts_Inventory(oldPart, newPart);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Max Fare
        /// Created: 2024/02/04
        /// 
        /// Test that attempting to update without providing correct data to update to
        /// throws an argumentexception
        /// 
        /// </summary>
        ///<exception cref="ArgumentException"></exception>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdatePartsInventoryFailsWithBadInput()
        {
            //arrange
            Parts_Inventory oldPart = _mgr.GetParts_InventoryByID(1);
            Parts_Inventory newPart = null;
            int actual = 0;
            //act
            actual = _mgr.EditParts_Inventory(oldPart, newPart);
        }

        [TestMethod]
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Test that correct part is retrieved with a particular key
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: 2024-02-06
        /// </remarks>
        public void TestgetParts_InventoryByPrimaryKeyGetsCorrectPart()
        {
            //arrage
            int partid = 1;
            string actual = "";
            string expected = "Sprocket";
            Parts_Inventory part = null;
            //act
            actual = _mgr.GetParts_InventoryByID(partid).Part_Name;

            //assert
            Assert.AreEqual(actual, expected);

        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Test that an exception is thrown if a part can not be found
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: 2024-02-06
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestgetParts_InventoryByPrimaryKeyFailsWithBadData()
        {
            //arrange
            Parts_Inventory part = null;
            int partid = 1000;
            //act
            part = _mgr.GetParts_InventoryByID(partid);
            //assert nothing to do
        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/02
        /// 
        /// Test that grabbing the full list of parts records returns the full list of part records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestGetAllPartsInventoryReturnsAllActiveInventorty()
        {
            //arrange
            int exepcted = 3;
            int actual = 0;
            //act
            actual = _mgr.GetAllParts_Inventory().Count;
            //assert
            Assert.AreEqual(exepcted, actual);


        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-26
        /// 
        /// Test that grabbing the list of active parts returns the correct number of parts
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestGetActivePartsInventoryReturnsAllActiveInventorty()
        {
            //arrange
            int exepcted = 2;
            int actual = 0;
            //act
            actual = _mgr.GetActiveParts_Inventory().Count;
            //assert
            Assert.AreEqual(exepcted, actual);


        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-23
        /// Changes the Is_Active field of the test part number,
        /// returning 1 if the field is changed, and 0 otherwise
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        [TestMethod]
        public void TestRemovePartsInventoryReturnsCorrectData()
        {
            //arrange
            int partid = 1;
            int expected = 1;
            int actual = 0;
            //act
            actual = _mgr.RemoveParts_Inventory(_mgr.GetParts_InventoryByID(partid));
            //assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Max Fare
        /// Created: 2023-02-23
        /// This test fails when attempting to change a part that does not exist.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemovePartsInventoryThrowsIllegalArgumentException()
        {
            //arrange
            Parts_Inventory part = new Parts_Inventory
            {
                Parts_Inventory_ID = 100
            };
            //act
            _mgr.RemoveParts_Inventory(part);

        }

        /// <summary>
        ///     Test that retrieving parts compatible with vehicle model id returns the expected number of results
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-22
        /// </remarks>
        [TestMethod]
        public void GetPartsCompatibleWithVehicleModelID_ReturnsParts()
        {
            // Arrange
            int expectedNumResults = 2;

            // Act
            var results = _mgr.GetPartsCompatibleWithVehicleModelID(1);

            // Assert
            Assert.AreEqual(expectedNumResults, results.Count());
        }

        /// <summary>
        ///     Test that proves the Manager's delete function can delete a compatibile part
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>
        [TestMethod]
        public void DeletePartCompatibilityDeletesRecord()
        {
            // Arrange
            int expectedNumResults = 1;

            // Act
            int results = _mgr.PurgeModelPartCompatibility(1,1);

            // Assert
            Assert.AreEqual(expectedNumResults, results);
        }

        /// <summary>
        ///     Test that proves the Manager's delete function will do nothing if no matching parts/Models are found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>

        [TestMethod]
        public void DeletePartCompatibilityFailsWithBadData()
        {
            // Arrange
            int expectedNumResults = 2;

            // Act
            _mgr.PurgeModelPartCompatibility(5, 100);
            var results = _mgr.GetPartsCompatibleWithVehicleModelID(1);

            // Assert
            Assert.AreEqual(expectedNumResults, results.Count());
        }

        // Reviewed By: John Beck
    }
}
