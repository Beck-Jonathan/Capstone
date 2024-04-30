using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayer.ServiceOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jared Roberts
    /// <br />
    /// CREATED: 2024-04-28
    /// UPDATER: Jared Roberts
    /// <br />
    /// UPDATED: 2024-04-28
    /// <br />
    ///     Initial Creation
    /// </remarks>
    [TestClass]
    public class MaintenanceScheduleManagerTests
    {
        MaintenanceScheduleManager _maintenanceScheduleManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _maintenanceScheduleManager = new MaintenanceScheduleManager(new MaintenanceScheduleAccessorFake());
        }

        [TestMethod]
        public void GetAllCompleteMaintenanceSchedulesTest()
        {
            int excpectedCount = 1;
            int actual = _maintenanceScheduleManager.GetAllCompleteMaintenanceSchedules().Count;

            Assert.AreEqual(excpectedCount, actual);
        }

        [TestMethod]
        public void GetAllIncompleteMaintenanceSchedulesTest()
        {
            int excpectedCount = 2;
            int actual = _maintenanceScheduleManager.GetAllIncompleteMaintenanceSchedules().Count;

            Assert.AreEqual(excpectedCount, actual);
        }

        [TestMethod]
        public void GetAllMaintenanceSchedulesTest()
        {
            int excpectedCount = 3;
            int actual = _maintenanceScheduleManager.GetAllMaintenanceSchedules().Count;

            Assert.AreEqual(excpectedCount, actual);
        }

        [TestMethod]
        public void TestAddMaintenanceScheduleReturnsCorrectID()
        {
            //arrange
            int actual = -1;
            int expected = 4;
            MaintenanceScheduleVM schedule = new MaintenanceScheduleVM()
            {
                ModelID = 1,
                ServiceTypeID = "Tire Change",
                FrequencyInMonths = 6,
                FrequencyInMiles = null,
                TimeLastCompleted = DateTime.Today
            };
            //act
            actual = _maintenanceScheduleManager.AddScheduledMaintenance(schedule);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddMaintenanceScheduleFailsWithIncompleteData()
        {
            //arrange
            int actual = -1;
            MaintenanceScheduleVM schedule = new MaintenanceScheduleVM()
            {
                FrequencyInMiles = null,
                TimeLastCompleted = DateTime.Today
            };
            //act
            actual = _maintenanceScheduleManager.AddScheduledMaintenance(schedule);
            //assert

        }
    }
}
