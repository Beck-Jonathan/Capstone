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
    /// AUTHOR: Jared Roberts, Isabella Rosenbohm
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
    /// <br /> <br />
    /// UPDATER: Jacob Wendt
    /// <br />
    /// UPDATED: 2024-02-20
    /// <br />
    ///    Added TestGetClientByEmailReturnsCorrectClient
    ///    Added TestGetClientByEmailThrowsExceptionWhenGivenBadData
    /// <br /> <br />
    /// UPDATER: Isabella Rosenbohm
    /// <br />
    /// UPDATED: 2024-02-21
    /// <br />
    ///    Added TestAddClientReturnsTrue and TestAddClientReturnsErrorWithDuplicateData
    ///    Rewrote TestGetClientByIDReturnsCorrectClient, TestGetClientByEmailReturnsCorrectClient,
    ///    and TestGetClientByEmailThrowsExceptionWhenGivenBadData as they were not correctly written
    /// <br /> <br />
    /// UPDATER: Michael Springer
    /// <br />
    /// UPDATED: 2024-04-25
    ///     Added TestFindCLient_ReturnsTrueWhenClientFound()  and TestFindCLient_ReturnsFalseWhenClientNotFound()
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
            // Arrange
            string expectedGivenName = "Joe";
            string expectedFamilyName = "Dirt";

            // Act
            Client client = _clientManager.GetClientById(2);

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
        public void TestAddClientReturnsTrue()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _clientManager.AddClient(
            new Client_VM
            {
                GivenName = "Ichiro",
                FamilyName = "Yamada",
                DOB = DateTime.Parse("1998-6-26"),
                Email = "iyamada@gmail.com",
                PostalCode = "88888",
                City = "Ikebukuro",
                Region = "JP-13",
                Address = "888 Tokyo Street",
                TextNumber = "543-777-7777",
                VoiceNumber = "543-555-5555",
                IsActive = true,
                Username = "iyamada26"
            });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddClientReturnsErrorWithDuplicateData()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _clientManager.AddClient(
                new Client_VM
                {
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
                });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetClientByEmailReturnsCorrectClient()
        {
            string email = "foobar@gmail.com";
            int expectedID = 1;
            int actualID = 0;

            actualID = _clientManager.GetClientByEmail(email).ClientID;

            Assert.AreEqual(expectedID, actualID);

        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetClientByEmailThrowsExceptionWhenGivenBadData()
        {
            string email = "abc@test.com";
            int acutalID = 0;

            acutalID = _clientManager.GetClientByEmail(email).ClientID; 

            // no assertion needed; should catch exception
        }
        [TestMethod]
        public void TestFindCLient_ReturnsTrueWhenClientFound()
        {
            // Arrange
            string email = "foobar@gmail.com";
            bool result = false;

            // Act
            result = _clientManager.FindClient(email);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFindCLient_ReturnsFalseWhenClientNotFound()
        {
            // Arrange
            string email = "scrandle_randle@fake.com";
            bool result = true;

            // Act
            result = _clientManager.FindClient(email);

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestDeactivateClientReturnsOne()
        {
            // Arrange
            int expected = 1;
            int actual = 0;

            // Act
            actual = _clientManager.DeactivateClient(1);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestDeactivateClientReturnsZero()
        {
            // Arrange
            int expected = 0;
            int actual = 0;

            // Act
            actual = _clientManager.DeactivateClient(17);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
    
}
