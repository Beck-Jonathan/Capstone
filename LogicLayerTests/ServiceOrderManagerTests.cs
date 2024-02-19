using DataAccessFakes;
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
    }
}
