using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jared Roberts, Isabella Rosenbahm
    /// <br />
    /// CREATED: 2024-02-11
    /// <br />
    ///    ClientManager Unit tests. Uses ClientAccessorFake for data fakes
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Jared Roberts
    /// <br />
    /// UPDATED: 2024-02-11
    /// <br />
    ///    Added TestGetClientByIDReturnsCorrectClient and TestEditClientWorksCorrectly
    /// </remarks>
    /// <remarks>
    /// UPDATER: Jacob Wendt
    /// <br />
    /// UPDATED: 2024-02-20
    /// <br />
    ///    Added TestGetClientByEmailReturnsCorrectClient
    ///    Added TestGetClientByEmailThrowsExceptionWhenGivenBadData
    /// </remarks>
    [TestClass]
    public class ClientManagerTests
    {
        private ClientManager _clientManager;

        [TestInitialize]
        public void TestSetup()
        {
            List<Client_VM> testClientList = new List<Client_VM>
            {
                new Client_VM
                {
                    ClientID = 1,
                    GivenName = "Foo",
                    FamilyName = "Bar",
                    DOB = DateTime.Parse("1905-1-2"),
                    Email = "foobar@gmail.com",
                    PostalCode = "12345",
                    City = "Fake City",
                    Region = "US-NV",
                    Address = "123 Fake Street",
                    TextNumber = "123-123-1234",
                    VoiceNumber = "321-321-4321",
                    IsActive = true,
                    Username = "foobar123"
                },
                new Client_VM
                {
                    ClientID = 2,
                    GivenName = "Joe",
                    FamilyName = "Dirt",
                    DOB = DateTime.Parse("1950-5-3"),
                    Email = "joedirt@gmail.com",
                    PostalCode = "54321",
                    City = "Faker City",
                    Region = "US-AK",
                    Address = "123 Phony Road",
                    TextNumber = "523-123-1235",
                    VoiceNumber = "721-321-4328",
                    IsActive = false,
                    Username = "joedirt35"
                }
            };
            _clientManager = new ClientManager(new ClientAccessorFake(testClientList));
        }

        [TestMethod]
        public void GetAllClientsReturnsCompleteListOfClients()
        {
            //arrange
            int expectedCount = 2;
            int actualCount = 0;

            //act 
            actualCount = _clientManager.GetAllClients().ToList().Count();

            //assert 
            Assert.AreEqual(expectedCount, actualCount);
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

        [TestMethod]
        public void TestEditClientWorksCorrectly()
        {

            // Arrange
            int expectedResult = 1;
            int actualResult = 0;

            Client_VM oldClient = new Client_VM()
            {
                ClientID = 1,
                GivenName = "Foo",
                FamilyName = "Bar",
                MiddleName = "Soul",
                DOB = DateTime.Parse("1905-1-2"),
                Email = "foobar@gmail.com",
                PostalCode = "12345",
                City = "Fake City",
                Region = "US-NV",
                Address = "123 Fake Street",
                TextNumber = "123-123-1234",
                VoiceNumber = "321-321-4321",
                IsActive = true,
            };
            Client_VM newClient = new Client_VM()
            {
                ClientID = 2,
                GivenName = "Joe",
                FamilyName = "Dirt",
                MiddleName = "Pit",
                DOB = DateTime.Parse("1950-5-3"),
                Email = "joedirt@gmail.com",
                PostalCode = "54321",
                City = "Faker City",
                Region = "US-AK",
                Address = "123 Phony Road",
                TextNumber = "523-123-1235",
                VoiceNumber = "721-321-4328",
                IsActive = false,
            };
            // Act
            actualResult = _clientManager.EditClient(newClient);

            // If pass means updated successfully
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestGetClientByEmailReturnsCorrectClient()
        {
            IEnumerable<Client_VM> testClientData = new List<Client_VM>()
            {
                new Client_VM()
                {
                    ClientID = 158789,
                    GivenName = "Joseph",
                    FamilyName = "Joestar",
                    Email = "jstar@gmail.com"
                },

                new Client_VM()
                {
                    ClientID = 128749,
                    GivenName = "Wrong",
                    FamilyName = "Name",
                    Email = "wname@gmail.com"
                }
            };

            ClientAccessorFake clientAccessorFake = new ClientAccessorFake(testClientData);
            _clientManager = new ClientManager(clientAccessorFake);

            // Arrange
            string expectedGivenName = "Joseph";
            string expectedFamilyName = "Joestar";

            // Act
            Client client = _clientManager.GetClientByEmail("jstar@gmail.com");

            // Assert
            Assert.AreEqual(expectedGivenName, client.GivenName);
            Assert.AreEqual(expectedFamilyName, client.FamilyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetClientByEmailThrowsExceptionWhenGivenBadData()
        {
            IEnumerable<Client_VM> testClientData = new List<Client_VM>()
            {
                new Client_VM()
                {
                    ClientID = 158789,
                    GivenName = "Joseph",
                    FamilyName = "Joestar",
                    Email = "jstar@gmail.com"
                },

                new Client_VM()
                {
                    ClientID = 128749,
                    GivenName = "Wrong",
                    FamilyName = "Name",
                    Email = "wname@gmail.com"
                }
            };

            ClientAccessorFake clientAccessorFake = new ClientAccessorFake(testClientData);
            _clientManager = new ClientManager(clientAccessorFake);

            // Arrange
            // string expectedGivenName = "Joseph";
            // string expectedFamilyName = "Joestar";

            // Act
            Client client = _clientManager.GetClientByEmail("jtar@gmail.com");

            // Assert
            // Assert.ThrowsException<ArgumentException>(() => _clientManager.GetClientByEmail("jtar@gmail.com"));
        }
    }
}
