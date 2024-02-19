using DataObjects;
using LogicLayer;
using DataAccessFakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Messaging;

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

        //[TestMethod]
        //public void TestAddDependentThrowsExceptionWhenFailing()
        //{
            
        //}
    }
}
