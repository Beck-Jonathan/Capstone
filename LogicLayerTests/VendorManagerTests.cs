using DataAccessFakes;
using LogicLayer;
using LogicLayer.Vendor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// <br />
    /// CREATED: 2024-03-03
    /// <br />
    /// Vendor Manager unit tests. Uses VendorAccessorFakes.cs for data fakes
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    /// initial creation
    /// </remarks>
    public class VendorManagerTests
    {
        private IVendorManager _vendorManager = null;
        /// <summary>
        /// AUTHOR: Chris Baenziger, Everett DeVaux
        /// CREATED: 2024-02-01
        ///     Tests for working with vehicle data.
        /// </summary>


        [TestInitialize]
        public void TestSetup()
        {
            _vendorManager = new VendorManager(new VendorAccessorFakes());


        }
        /// <summary>
        /// AUTHOR: Jonathan Beck
        /// CREATED: 2024-03-03
        ///     Tests that select all works
        /// </summary>
        [TestMethod]
        public void TestGetAllVendorsReturnsAllVendors()
        {
            //arrange
            int actual = 0;
            int expected = 3;
            //act
            actual = _vendorManager.getAllVendors().Count();
            //assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// AUTHOR: Jonathan Beck
        /// CREATED: 2024-03-03
        ///     test that select one works
        /// </summary>
        [TestMethod]
        public void TestGetVendorByIdGetsCorrectVendor()
        {
            //arrange
            String actual = "";
            String expected = "Cedar Rapids";
            //act
            actual = _vendorManager.getVendorByVendorID(1).Vendor_City;
            //assert
            Assert.AreEqual(expected, actual);

        }
        /// <summary>
        /// AUTHOR: Jonathan Beck
        /// CREATED: 2024-03-03
        ///    Test that select one fails with bad id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetVendorByIDFailsWithBadData()
        {
            //arrange
            String actual = "";
            String expected = "Cedar Rapids";
            //act
            actual = _vendorManager.getVendorByVendorID(4).Vendor_City;
            //assert -- nothing to do


        }

        

    }
}
