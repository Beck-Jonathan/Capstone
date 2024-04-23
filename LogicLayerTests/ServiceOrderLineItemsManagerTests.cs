using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceOrderLineItemsManagerTests
    {


        ServiceOrderLineItemsManager _serviceLineManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _serviceLineManager = new ServiceOrderLineItemsManager(new ServiceOrderLineItemsFakes());
        }

        [TestMethod]
        public void TestGetAllServiceOrderLineItemsReturnsCorrectCount()
        {
            int expected = 4;
            int actual = 0;

            actual = _serviceLineManager.GetServiceOrderLineItems().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddServiceOrderLineItemReturnsCorrectResult()
        {
            ServiceOrderLineItems_VM line = new ServiceOrderLineItems_VM()
            {
                Service_Order_ID = 1,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 1,
                Quantity = 1
            };
            int expected = 1;
            int actual = 0;

            actual = _serviceLineManager.AddServiceOrderLineItem(line);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddServiceOrderLineItemFailsWithDuplicateLine()
        {
            ServiceOrderLineItems_VM line = new ServiceOrderLineItems_VM
            {
                Service_Order_ID = 100000,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 1,
                Quantity = 1
            };
            int actual = 0;

            actual = _serviceLineManager.AddServiceOrderLineItem(line);
        }


    }
}
