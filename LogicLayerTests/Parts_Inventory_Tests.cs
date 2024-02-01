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
        [TestInitialize]
        public void testSetup() { 
        _mgr = new Parts_InventoryManager(new Parts_Inventory_Fakes());
        
        }
        [TestMethod]
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Test that correct part is retreived with a particular key
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public void TestgetParts_InventoryByPrimaryKeyGetsCorrectPart() {
            //arrage
            int partid = 1;
            string actual = "";
            string expected = "Sprocket";
            Parts_Inventory part = null;
            //act
            actual = _mgr.getParts_InventoryByPrimaryKey(partid).Part_Name;

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
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestgetParts_InventoryByPrimaryKeyFailsWithBadData() {
            //arrange
            Parts_Inventory part = null;
            int partid = 1000;
            //act
            part=_mgr.getParts_InventoryByPrimaryKey(partid);
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
        public void TestSeletAllPartsInventoryReturnsAllInventorty() {
            //arrange
            int exepcted = 3;
            int actual = 0;
            //act
            actual = _mgr.getAllParts_Inventory().Count;
            //assert
            Assert.AreEqual(exepcted, actual);
        
        
        }

    }
}
