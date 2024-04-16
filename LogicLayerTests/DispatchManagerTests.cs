using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class DispatchManagerTests
    {
        DriverScheduleManager _driverScheduleManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _driverScheduleManager = new DriverScheduleManager(new DispatchAccessorFake());
        }

        [TestMethod]
        public void TestGetAllDriverScheduleCountPasses()
        {
            int expected = 1;
            int actual = _driverScheduleManager.GetDriverScheduleList().Count;

            Assert.AreEqual(expected, actual);
        }
    }
}
