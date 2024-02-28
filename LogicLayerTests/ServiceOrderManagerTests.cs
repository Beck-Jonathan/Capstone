﻿using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceOrderManagerTests
    {
        ServiceOrderManager _serviceOrderManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _serviceOrderManager = new ServiceOrderManager(new ServiceOrderAccessorFakes());
        }

        [TestMethod]
        public void TestGetAllServiceOrderCountPasses()
        {
            int expected = 2;
            int actual = _serviceOrderManager.GetALlServiceOrders().Count;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestGetAllServiceOrderCountFails()
        {
            int expected = 1;
            int actual = _serviceOrderManager.GetALlServiceOrders().Count;

            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///  Tests that the UpdateServiceOrder method passes when a valid service order is provided.      
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the update operation
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        [TestMethod]
        public void TestUpdateServiceOrderPasses()
        {
            // Arrange
            ServiceOrderAccessorFakes accessor = new ServiceOrderAccessorFakes();
            ServiceOrder serviceOrderToUpdate = new ServiceOrder()
            {
                VIN = "2GNALDEK9C6340800",
                Service_Order_ID = 100000,
                Critical_Issue = true,
                Service_Type_ID = "Engine Tune-up",
                Service_Description = "Perform engine tune-up"
            };

            // Act
            int rowsAffected = accessor.UpdateServiceOrder(serviceOrderToUpdate);

            // Assert
            Assert.AreEqual(1, rowsAffected); // Assuming the update was successful

        }
        /// <summary>
        /// Tests that the UpdateServiceOrder method throws an exception when a null service order is provided.
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the update operation
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentNullException">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        [TestMethod]
        public void TestUpdateServiceOrder_NullServiceOrder()
        {
            // Arrange
            ServiceOrderAccessorFakes accessor = new ServiceOrderAccessorFakes();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => accessor.UpdateServiceOrder(null));
        }
    }
}
