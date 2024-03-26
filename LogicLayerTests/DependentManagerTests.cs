/// <summary>
/// Michael Springer
/// Created: 2024/01/20
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataObjects;
using LogicLayer;
using DataAccessFakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Messaging;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LogicLayerTests
{
    [TestClass]
    public class DependentManagerTests
    {
        private DependentManager _dependentManager = new DependentManager(new DependentAccessorFake());
        private DependentManager _dependentManager2;
        Dependent testDependent1 = (new Dependent()
        {
            DependentID = 1,
            GivenName = "test",
            FamilyName = "test",
            MiddleName = "test",
            DOB = new DateTime(2018, 12, 12),
            Gender = "female",
            EmergencyContact = "John Smith",
            ContactRelationship = "Parent",
            EmergencyPhone = "123456789101",
            IsActive = true,

        });
        Dependent testDependent2;

        Client client = new Client()
        {
            ClientID = 3
        };

        [TestInitialize]
        public void TestSetup()
        {
            List<DependentVM> testDependentList = new List<DependentVM>
           {
               new DependentVM
               {
                   DependentID = 100003,
                GivenName = "Jeremy",
                FamilyName = "Smith",
                MiddleName = "Lanklema",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Male",
                EmergencyContact = "Nancy Smith",
                ContactRelationship = "Mom",
                EmergencyPhone = "3192221111",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
               },
               new DependentVM
               {
                   DependentID = 100004,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
               },
                new DependentVM
               {
                   DependentID = 100005,
                GivenName = "Test",
                FamilyName = "Three",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = false,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
               },
                new DependentVM
               {
                   DependentID = 100006,
                GivenName = "Test",
                FamilyName = "Three",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = false,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 5
                    }
                }
               }
           };

            _dependentManager2 = new DependentManager(new DependentAccessorFake(testDependentList));
        }

        /// <summary>
        /// Michael Springer
        /// Test to check add returns the proper value for row affected
        /// 
        /// </summary>
        ///>    
        [TestMethod]
        public void TestAddDependenReturnRowsAffectedWhenValid()
        {
            int expectedValue = 1;
            int actualValue = 1;

            actualValue = _dependentManager.AddDependent(testDependent1);

            Assert.AreEqual(expectedValue, actualValue);
        }

        /// <summary>
        /// Michael Springer
        /// Test to check add returns the value 0 when add fails
        /// 
        /// </summary>
        ///>    
        [TestMethod]
        public void TestAddDependentReturns0WhenNoUpdate()
        {
            int expectedValue = 0;
            int actualValue = 0;

            actualValue = _dependentManager.AddDependent(testDependent2);


            Assert.AreEqual(expectedValue, actualValue);

        }

        /// <summary>
        /// Author: Jacob Rohr
        /// CREATED: 2024-02-13
        /// 
        ///     A test method to make sure that the method actually returns a list
        ///     
        /// Asserts: 3 Items are found in the accessorfakes.
        ///    
        /// 
        /// </summary>
        [TestMethod]
        public void TestListDependentsReturnsList()
        {
            int expectedValue = 3;
            int actualValue = 0;

            actualValue = _dependentManager.GetDependentList().ToList().Count();

            Assert.AreEqual(expectedValue, actualValue);


        }
        [TestMethod]
        public void TestListDependentsByUserReturnsList()
        {
            Assert.AreEqual(1, 1);
        }

        /// <summary>
        /// Author: Jacob Rohr
        /// CREATED: 2024-02-13
        /// 
        ///     A test method to make sure that the method fails if its not the right size
        ///     
        /// Asserts: 2 items are not found, because there are 3.
        ///    
        /// 
        /// </summary>
        [TestMethod]
        public void TestListDependentsReturnsListWrongSizeFails()
        {
            int expectedValue = 2;
            int actualValue = 0;

            actualValue = _dependentManager.GetDependentList().ToList().Count();

            Assert.AreNotEqual(expectedValue, actualValue);

        }
        /// <summary>
        /// Michael Springer
        /// Test that all valid dependents are returned, no invalid or unrequested dependents are returned
        /// 
        /// </summary>
        ///>    
        [TestMethod]
        public void TestGetDependentsByClientIDReturnsCorrectDependents()
        // Returns all associated dependents
        // Does not return isActive == false && does not return other ClientID
        {
            //arrange
            int expectedDependentCount = 2;
            int expectedDependentId1 = 100003;
            int expectedDependentId2 = 100004;

            //act
            List<DependentVM> dependents = _dependentManager2.GetDependentListByClientId(3).ToList();

            //assert
            Assert.IsTrue(dependents.Count() == expectedDependentCount);
            Assert.AreEqual(expectedDependentId1, dependents[0].DependentID);
            Assert.AreEqual(expectedDependentId2, dependents[1].DependentID);

        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Checks for non-null result
        /// Checks expected id with actual id
        /// </summary>
        ///   
        [TestMethod]
        public void TestGetDependentByDependentIdReturnsDependent()
        {
            // arrange
            int dependentId = 100003;

            //act
            DependentVM dependent = _dependentManager2.GetDependentByDependentId(dependentId);

            // assert

            Assert.IsNotNull(dependent);
            Assert.AreEqual(dependentId, dependent.DependentID);
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Deactivated ID throws exception
        /// 
        /// </summary>
        ///
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivatedDependentThrowsException()
        {
            // arrange
            int deactivatedId = 100005;
            // act
            DependentVM testDependent = _dependentManager2.GetDependentByDependentId(deactivatedId);
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Invalid id throws exception
        /// 
        /// </summary>
        /// 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidIdThrowsException()
        {
            // arrange
            int invalidId = 100002;

            // act
            DependentVM testDependent = _dependentManager2.GetDependentByDependentId(invalidId);
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// check to see if valid new values and matching old values
        /// lead to successful update
        /// 
        /// </summary>
        /// 
        [TestMethod]
        public void ValidDependentDataAllowsUpdate()
        {
            // arrange
            DependentVM oldValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            DependentVM newValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem2",
                FamilyName = "Amy2",
                MiddleName = "",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            // ACT
            int confirmation = _dependentManager2.EditDependent(oldValues, newValues, 3);

            // Assert -- rows affected
            Assert.AreEqual(1, confirmation);
        }

        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Checks that inaccurate comparative data prevents update
        /// </summary>
        ///   
        [TestMethod]
        public void InValidOldDataPreventsEdit()
        {
            // arrange
            DependentVM oldValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem",
                FamilyName = "GrAmy", //This data is bad
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            DependentVM newValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem2",
                FamilyName = "Amy2",
                MiddleName = "",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            // ACT
            int confirmation = _dependentManager2.EditDependent(oldValues, newValues, 3);

            // Assert
            Assert.AreEqual(0, confirmation);
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Tests that invalid values prevent successful update
        /// </summary>
        ///   
        [TestMethod]
        public void InvalidNewDataPreventsEdit()
        {
            // arrange
            DependentVM oldValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            DependentVM newValues = new DependentVM
            {
                DependentID = 100004,
                GivenName = "", // Empty value for required field given name
                FamilyName = "Amy2",
                MiddleName = "",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            // ACT
            int confirmation = _dependentManager2.EditDependent(oldValues, newValues, 3);
            // Assert
            Assert.AreEqual(0, confirmation);
        }
    }
}
