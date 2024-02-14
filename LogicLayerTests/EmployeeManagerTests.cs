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

        /// <summary>
        ///   Test to verify that the GetEmployee method returns null when
        ///   provided with an incorrect employee ID that does not exist.
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="_employeeManager"/>: Returns null.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions:Argument Exception
        /// <br />
        ///  <see cref="ArgumentException">ArgumentException</see>:Thrown when an employee is not found in the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        [TestMethod]
        public void TestGetEmployeeReturnsIncorrectEmployeeThrowsArgumentException()
        {
            // Arrange
            int incorrectEmployeeId = 999; // ID that doesn't exist 

            // Act & Assert Test fails when connected to database  
            Assert.ThrowsException<ArgumentException>(() => _employeeManager.GetEmployee(incorrectEmployeeId));
        }

        /// <summary>
        ///   Test to verify that the GetEmployee method returns an Employee when
        ///   provided with a correct ID that does exist.
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="_employeeManager"/>: Returns employee by id.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        [TestMethod]
        public void TestGetEmployeeReturnsCorrectEmployee()
        {
            // Arrange
            int correctEmployeeId = 100000; // Employee ID exists in the fake data

            // Act 
            var correctEmployee = _employeeManager.GetEmployee(correctEmployeeId);

            // Assert
            Assert.IsNotNull(correctEmployee, "Correct employee should be found");
            Assert.AreEqual(correctEmployeeId, correctEmployee.Employee_ID, "Employee IDs should match");
        }
        // checked by Jared R.
    }
}
// Checked by Nathan Toothaker