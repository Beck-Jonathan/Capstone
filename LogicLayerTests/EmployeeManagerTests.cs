using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.Maps.MapControl.WPF.Overlays;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
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
            int actualCount = _employeeManager.GetEmployees().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllEmployeesCountFails()
        {
            int wrongCount = 4;
            int actualCount = _employeeManager.GetEmployees().Count;

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
            List<Employee_VM> allEmployees = _employeeManager.GetEmployees();
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

        [TestMethod]
        public void TestUpdateEmployeeReturnsCorrectRows()
        {
            int expected = 1;
            int actual = 0;
            int employeeID = 100000;
            Employee_VM originalEmployee = _employeeManager.GetEmployee(employeeID);
            Employee_VM updatedEmployee = _employeeManager.GetEmployee(employeeID);
            updatedEmployee.Family_Name = "Jack";

            actual = _employeeManager.EditEmployee(updatedEmployee, originalEmployee);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeThrowsArgumentException()
        {
          
            int actual = 0;
            int employeeID1 = 100000;
            int employeeID2 = 100001;
            Employee_VM originalEmployee = _employeeManager.GetEmployee(employeeID1);
            Employee_VM updatedEmployee = _employeeManager.GetEmployee(employeeID2);
            updatedEmployee.Family_Name = "Jack";

            actual = _employeeManager.EditEmployee(updatedEmployee, originalEmployee);
        }

        [TestMethod]
        public void TestDeactivateEmployeeReturnsCorrectNumberOfRows()
        {
            int employeeID = 100000;
            int rows = 0;
            int expected = 1;
            rows = _employeeManager.DeactivateEmployeeByID(employeeID);
            Assert.AreEqual(expected, rows);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateEmployeeThrowsArgumentException()
        {
            int employeeID = 100008;
            int rows = _employeeManager.DeactivateEmployeeByID(employeeID);
        }

        [TestMethod]
        public void TestReactivateEmployeeReturnsCorrectNumberOfRows()
        {
            int employeeID = 100002;
            int rows = 0;
            int expected = 1;
            rows = _employeeManager.ReactivateEmployeeByID(employeeID);
            Assert.AreEqual(expected, rows);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReactivateEmployeeThrowsArgumentException()
        {
            int employeeID = 100008;
            int rows = _employeeManager.ReactivateEmployeeByID(employeeID);
        }

        [TestMethod]
        public void TestGetEmployeeByEmailReturnsIncorrectEmployeeThrowsArgumentException()
        {
            
            string badEmail = "badExample@company.com"; // email that doesn't exist 

             
            Assert.ThrowsException<ArgumentException>(() => _employeeManager.GetEmployeeByEmail(badEmail));
        }
        [TestMethod]
        public void TestGetEmployeeByEmailReturnsCorrectEmployee()
        {
            
            string goodEmail = "tess@company.com"; // Email should exist

            
            var correctEmployee = _employeeManager.GetEmployeeByEmail(goodEmail);

            
            Assert.IsNotNull(correctEmployee, "Correct employee should be found");
            Assert.AreEqual(goodEmail, correctEmployee.Email, "Employee IDs should match");
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// DATE: 2024-03-24
        /// Tests for Employee Manager GetAllEmployees() method 
        /// gets the count of employees
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>

        [TestMethod]
        public void TestGetAllEmployeesReturnsEmployees()
        {
            // Arrange
            int expectedCount = 3;

            // Act
            List<Employee_VM> allEmployees = _employeeManager.GetAllEmployees().ToList();

            // Assert
            Assert.IsNotNull(allEmployees, "Employee list should not be null");
            Assert.AreEqual(expectedCount, allEmployees.Count, $"Employee count should be {expectedCount}");
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// DATE: 2024-03-24
        /// Tests for Employee Manager GetAllEmployees() method 
        /// checks if an employees id is within the list.
        /// </summary>
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 
        /// <br />
        ///     Update Comments
        /// </remarks>
        [TestMethod]
        public void TestGetAllEmployeesContainsEmployeeWithID()
        {
            // Arrange
            int employeeIDToFind = 100001;

            // Act
            List<Employee_VM> allEmployees = _employeeManager.GetAllEmployees().ToList();
            Employee_VM foundEmployee = allEmployees.FirstOrDefault(e => e.Employee_ID == employeeIDToFind);

            // Assert
            Assert.IsNotNull(foundEmployee, $"Employee with ID {employeeIDToFind} should be found.");
        }



        [TestMethod]
        public void TestRemoveEmployeeRoleSucceeds()
        {
            // Arrange
            int result = 0;
            int expectedResult = 1;

            // Act
            result = _employeeManager.RemoveEmployeeRoles(2, "Maintenance");
            // Assert
            Assert.AreEqual(expectedResult, result);
            
            
        }

        [TestMethod]
        public void TestRetrieveEmployeeIDFromEmailWorks()
        {
            int result = 0;
            int expectedResult = 1;

            result = _employeeManager.RetrieveEmployeeIDFromEmail("tess@company.com");

            Assert.AreEqual(expectedResult, result);
        }

       
    }
}
// Checked by Nathan Toothaker