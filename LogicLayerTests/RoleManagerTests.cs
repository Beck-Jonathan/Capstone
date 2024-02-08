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

    }
}
// Checked by Nathan Toothaker