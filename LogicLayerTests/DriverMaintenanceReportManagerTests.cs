using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    /// 
    ///     Test Class for Maintenance Reports Manager
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: xx-xx-xx
    /// <br />
    ///     
    /// </remarks>
    [TestClass]
    public class DriverMaintenanceReportManagerTests
    {
        //test setup, create the manager
        //Jonathan Beck 04-17-2024
        Driver_Maintenance_ReportManager _mgr = null;
        [TestInitialize]
        public void testSetup()
        {
            _mgr = new Driver_Maintenance_ReportManager(new DriverMaintenanceReportFakes());

        }
        //test if this can add a report
        //Jonathan Beck 04-17-2024
        [TestMethod]
        public void TestAddReportAddsAReport()
        {
            //arrage
            bool expected = true;
            bool actual = false;
            DriverMaintenanceReport driverMaintenanceReport = new DriverMaintenanceReport();

            //act

            actual = _mgr.addDriverMaintenanceReport(driverMaintenanceReport);

            //assert

            Assert.AreEqual(expected, actual);
        }
        //test if this can select one report
        //Jonathan Beck 04-17-2024
        [TestMethod]
        public void TestGetDriverMaintanceReportRetreivesCorrectReport()
        {
            //arrange
            string actual = "";
            string expected = "Hit a Deer.";
            int reportid = 1;
            //act
            actual = _mgr.getAllDriverMaintenanceReportsById(reportid).Description;

            //assert
            Assert.AreEqual(expected, actual);
        }

        //test if get single report fails with bad data
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetDriverMaintanceReportFailsWithBadData()
        {
            //arrange
            int reportid = 10;
            //act
            DriverMaintenanceReport report = _mgr.getAllDriverMaintenanceReportsById(reportid);

            //assert nothing to do
        }

        //test if this can select all reports
        //Jonathan Beck 04-17-2024

        [TestMethod]
        public void TestSelectAllReportsGetsAllReports()
        {
            //arrange
            int actual = 0;
            int expected = 3;
            //act
            actual = _mgr.getActiveDriverMaintenacenReports().Count();
            //assert
            Assert.AreEqual(expected, actual);

        }


        //test if this can select reports by driver
        //Jonathan Beck 04-17-2024

        [TestMethod]
        public void TestSelectReportByDriverGetsCorrectNumberOfReports()
        {
            //arrange
            int actual = 0;
            int expected = 2;
            //act
            actual = _mgr.GetAllDriverMaintenacneReportsByEmployeeId(1).Count();
            //assert
            Assert.AreEqual(expected, actual);
        }

        //test if select reports by driver fails with a bad driver id
        //Jonathan Beck 04-17-2024

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetReportByEmployeeIDThrowsErrorIfEmployeeHasNoReports()
        {
            //arrange
            int actual = 0;

            //act
            actual = _mgr.GetAllDriverMaintenacneReportsByEmployeeId(7).Count();
            //assert - nothing to do

        }

    }
}
