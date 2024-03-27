using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayer.PartsRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class PartsRequestManagerTests
    {
        PartsRequestManager _partsRequestManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _partsRequestManager = new PartsRequestManager(new PartsRequestAccessorFake());
        }


        /// <summary>
        ///  Tests that the GetAllPartsRequests() method passes when a valid service order is provided.      
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        [TestMethod]
        public void TestGetAllPartsRequestCountPasses()
        {
            int expected = 3;
            int actual = _partsRequestManager.GetAllPartsRequests().Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///  Tests that the GetPartsRequestDetails() method passes when a valid service order is provided.      
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        [TestMethod]
        public void TestGetPartsRequestDetailsPasses()
        {
            object expected = new Parts_Request().GetType();
            object actual = _partsRequestManager.GetPartsRequestDetails(100000).GetType();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///  deactivates a parts request and saves the request in the deactivated list
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-26
        /// <br />
        [TestMethod]
        public void DeactivatePartsRequestSuccess()
        {
            Assert.IsTrue(_partsRequestManager.DeactivatePartsRequest(100002));
        }

        /// <summary>
        ///  deactivates
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-26
        /// <br />
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeactivatePartsRequestFailed()
        {
            _partsRequestManager.DeactivatePartsRequest(1);
        }
    }
}
