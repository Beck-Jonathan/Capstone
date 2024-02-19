using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class ClientManager_Test
    {
        private IClientManager _clientManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _clientManager = new ClientManager();
        }

        [TestMethod]
        public void TestGetClientByIDReturnsCorrectClient()
        {
            IEnumerable<Client_VM> testClientData = new List<Client_VM>()
            {
                new Client_VM()
                {
                    ClientID = 100006,
                    GivenName = "Joseph",
                    FamilyName = "Joestar"
                },

                new Client_VM()
                {
                    ClientID = 123456,
                    GivenName = "Wrong",
                    FamilyName = "Name"
                }
            };

            ClientAccessorFake clientAccessorFake = new ClientAccessorFake(testClientData);
            _clientManager = new ClientManager(clientAccessorFake);

            // Arrange
            string expectedGivenName = "Joseph";
            string expectedFamilyName = "Joestar";

            // Act
            Client client = _clientManager.GetClientById(100006);

            // Assert
            Assert.AreEqual(expectedGivenName, client.GivenName);
            Assert.AreEqual(expectedFamilyName, client.FamilyName);
        }

    }
}
