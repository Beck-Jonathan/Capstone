using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using System.ComponentModel;
using LogicLayer.Client;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Michael Springer
    /// <br />
    /// CREATED: 2024-02-25
    /// <br />
    /// 
    ///     Tests for handling clientdependentroles
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER:
    /// <br />
    ///    UPDATED:
    /// <br />
    /// </remarks>
    /// 
    [TestClass]
    public class ClientDependentRoleManagerTests
    {
        private ClientDependentRoleManager _clientDependentRoleManager;

        [TestInitialize]
        public void TestSetup()
        {
            List<ClientDependentRole_VM> testRoleList = new List<ClientDependentRole_VM>
            {
                new ClientDependentRole_VM
                {
                    ClientID = 1,
                    DependentID = 1,
                    Relationship = "Parent",
                    IsActive = true
                },
                new ClientDependentRole_VM
                {
                    ClientID = 1,
                    DependentID = 2,
                    Relationship = "Parent",
                    IsActive = true
                },
                new ClientDependentRole_VM
                {
                    ClientID = 1,
                    DependentID = 3,
                    Relationship = "Parent",
                    IsActive = true
                },
                new ClientDependentRole_VM
                {
                    ClientID = 2,
                    DependentID = 4,
                    Relationship = "Legal Custodian",
                    IsActive = true
                },
                new ClientDependentRole_VM
                {
                    ClientID = 3,
                    DependentID = 5,
                    Relationship = "Power of Attorney",
                    IsActive = true
                },
                new ClientDependentRole_VM
                {
                    ClientID = 4,
                    DependentID = 6,
                    Relationship = "Power of Attorney",
                    IsActive = false
                },
                new ClientDependentRole_VM
                {
                    ClientID = 4,
                    DependentID = 7,
                    Relationship = "Legal Custodian",
                    IsActive = false
                }
            };
            _clientDependentRoleManager = new ClientDependentRoleManager(new ClientDependentRoleAccessorFake(testRoleList));
        }
        /// <summary>
        /// Michael Springer
        /// Test get method returns correct number of roles
        /// 
        /// </summary>
        ///>    
        [TestMethod]
        public void GetClientDependentRolesByClientReturnsCorrectRoles()
        {
            //arrange
            int expectedCount = 3;
            int actualCount = 1;

            //act
            actualCount = _clientDependentRoleManager.GetClientDependentRolesByClient(1).Count();

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Michael Springer
        /// Test to check add returns empty Ienumerable when client has no dependents
        /// 
        /// </summary>
        ///>    
        [TestMethod]
        public void GetClientDependentRoleByIdReturnsEmptyWhenNoRecord()
        {
            //arrange
            IEnumerable<ClientDependentRole_VM> actual;

            //act
            //Passing a Client ID without an associated dependent
            actual = _clientDependentRoleManager.GetClientDependentRolesByClient(8);

            //assert
            Assert.IsTrue(actual.Count() == 0);
        }
    }
}
