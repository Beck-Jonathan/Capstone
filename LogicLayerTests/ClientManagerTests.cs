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
                    username = "foobar123"
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
                    username = "joedirt35"
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
    }
}
