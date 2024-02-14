using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace LogicLayerTests
{

    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    /// Employee Manager Unit tests. Uses EmployeeAccessorFake.cs for data fakes
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    /// initial creation
    /// </remarks>
    [TestClass]
    public class EmployeeManagerTests
    {
        EmployeeManager _employeeManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestGetAllEmployeesCountPasses()
        {
            int expectedCount = 2;
            int actualCount = _employeeManager.GetAllEmployees().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllEmployeesCountFails()
        {
            int wrongCount = 4;
            int actualCount = _employeeManager.GetAllEmployees().Count;

            Assert.AreNotEqual(wrongCount, actualCount);
        }

        [TestMethod]
        public void TestCreateNewEmployeeCountNumberOfEmployeesPasses()
        {
            Employee_VM newEmployee = new Employee_VM()
            {
                Employee_ID = 3,
                Given_Name = "Joseph",
                Family_Name = "Reyes",
                Address = "485 Hilltop Street",
                Address2 = "100 Fake Street",
                City = "Springfield",
                State = "Colorado",
                Country = "USA",
                Zip = "01109",
                Phone_Number = "4317251758",
                Email = "joseph@company.com",
                Position = "Maintenance Manager",
                Is_Active = true,
                Roles = new List<Role>
                {
                    new Role { RoleID = "Admin", IsActive = true }
                }
            };
            _employeeManager.AddEmployee(newEmployee);
            int expectedCount = 3;
            List<Employee_VM> allEmployees = _employeeManager.GetAllEmployees();
            int actualCount = allEmployees.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddEmployeeThrowsApplicationExceptionEmailAlreadyExists()
        {
            Employee_VM employee = new Employee_VM()
            {
                Employee_ID = 2,
                Given_Name = "Fred",
                Family_Name = "Smith",
                Address = "87 State St",
                Address2 = "100 Fountain Lane",
                City = "Chicago",
                State = "Illinois",
                Country = "USA",
                Zip = "86753",
                Phone_Number = "3334541234",
                Email = "fred@company.com",
                Position = "Maintenance II",
                Is_Active = true,
                Roles = new List<Role> { new Role { RoleID = "Maintenance", IsActive = true } }
            };
            _employeeManager.AddEmployee(employee);

        }

        [TestMethod]
        public void TestGetRolesByEmployeeIDRoleCountPasses()
        {
            int id = 2;
            int expectedCount = 1;
            int actualCount = 0;
            IEnumerable<Role> roles = _employeeManager.GetRolesByEmployeeID(id);
            actualCount = roles.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }
        //Reviewed by Steven Sanchez

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetRolesByEmployeeIDThrowsArgumentException()
        {
            int id = 7;
            IEnumerable<Role> roles = _employeeManager.GetRolesByEmployeeID(id);
        }
        //Reviewed by Steven Sanchez
    }
}
// Checked by Nathan Toothaker