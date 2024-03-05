using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    /// Role Manager unit tests. Uses RoleAccessorFakes.cs for data fakes
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
    public class RoleManagerTests
    {
        RoleManager _roleManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            _roleManager = new RoleManager(new RoleAccessorFake());
        }
        [TestMethod]
        public void TestGetAllRolesCountPasses()
        {
            int expectedCount = 3;
            int actualCount = 0;

            foreach (var role in _roleManager.GetAllRoles())
            {
                actualCount++;
            }

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestGetAllRolesCountFails()
        {
            int expectedCount = 2;
            int actualCount = 0;


            Assert.AreNotEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestCreateRolePassesWithProperRoleParameter()
        {
            int expectedCount = 1;
            int actualCount = 0;

            var role = new Role() { RoleID = "Cleaner", RoleDescription = "Something interesting", IsActive = true };
            actualCount = _roleManager.CreateRole(role);

            Assert.AreEqual(expectedCount, actualCount);
            
        }

        [TestMethod]
        public void TestCreateRoleFailsWhenGivenNullParamater()
        {

            Assert.ThrowsException<ApplicationException>(() => _roleManager.CreateRole(null));
            
        }

        [TestMethod] 
        public void TestCreateRoleFailsWhenGivenDuplicateRoleID()
        {

            var role = new Role() { RoleID = "Administrator", RoleDescription = "Something interesting", IsActive = true };

            Assert.ThrowsException<ApplicationException>(() => _roleManager.CreateRole(role));
        }
        [TestMethod]
        public void TestCreateRolePassesWithOutDescription()
        {
            int expectedCount = 1;
            int actualCount = 0;

            var role = new Role() { RoleID = "Flargle", IsActive = true };
            actualCount = _roleManager.CreateRole(role);

            Assert.AreEqual(expectedCount, actualCount);

        }
        [TestMethod]
        public void TestCreateRolePassesWithoutDescriptionOrActive()
        {
            int expectedCount = 1;
            int actualCount = 0;

            var role = new Role() { RoleID = "Foo"};
            actualCount = _roleManager.CreateRole(role);

            Assert.AreEqual(expectedCount, actualCount);
        }

    }
}
// Checked by Nathan Toothaker
// Checked by Michael Springer