using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceOrderLineItemsTests
    {
        ServiceOrderLineItemsManager _manager = null;
        [TestInitialize]
        public void TestSetup()
        {
            _manager = new ServiceOrderLineItemsManager(new ServiceOrderLineItemsFakes());
        }

        [TestMethod]

    }
}
