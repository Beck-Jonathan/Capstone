using DataAccessFakes;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using LogicLayer.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Resources;

namespace LogicLayerTests
{
    [TestClass]
    public class SourceManagerTests
    {
        SourceManager sourcemanager;
        [TestInitialize]
        public void TestSetup()
        {
            sourcemanager = new SourceManager(new SourceAccessorFakes());
        }

        [TestMethod]
        public void TestgetSourceByVendorIDandPartsInventoryIdGetsCorrectSource()
        {
            //arrange
            int vendorid = 1;
            int partid = 1;
            decimal expectedPrice = 1;
            decimal actualPrice = 0;
            //act
            actualPrice = sourcemanager.LookupSourceByVendorIDandPartsInventoryID(vendorid, partid).Part_Price;
            //assert
            Assert.AreEqual(expectedPrice, actualPrice);

        }
    }
}
