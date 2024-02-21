using DataObjects;
using LogicLayer;
using DataAccessFakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Messaging;
using System.Linq;

namespace LogicLayerTests
{
    [TestClass]
    public class DependentManagerTests
    {
        DependentManager _dependentManager = new DependentManager(new DependentAccessorFake());

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
        

        [TestMethod]
        public void TestAddDependenReturnRowsAffectedWhenValid()
        {
            int expectedValue = 1;
            int actualValue = 1;

           actualValue = _dependentManager.AddDependent(testDependent1);

            Assert.AreEqual(expectedValue, actualValue);
        }

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

        //[TestMethod]
        //public void TestAddDependentThrowsExceptionWhenFailing()
        //{

        //}
    }
}
