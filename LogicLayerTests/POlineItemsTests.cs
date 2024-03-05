using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class POlineItemsTests
    {
        POLineItemsManager _mgr = null;
        [TestInitialize]
        public void testSetup()
        {
            _mgr = new POLineItemsManager(new POLineItems_Fakes());

        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]
        public void TestLookupPOLineItemByPurchaseOrderNumberAndLineNumberGetsCorrectLineItem()
        {
            //arrange
            int purchase_Order = 1;
            int line_no = 1;
            int expected = 5;
            int actual = 0;

            //act
            actual = _mgr.LookupPOLineItemByPurchaseOrderNumberAndLineNumber(purchase_Order, line_no).Quantity;

            //assert
            Assert.AreEqual(expected, actual);
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLookupPOLineItemByPurchaseOrderNumberAndLineNumberFailesWithBadData()
        {
            //arrage
            int Purchase_Order = 0;
            int line_No = 0;
            //act
            POLineItem result = _mgr.LookupPOLineItemByPurchaseOrderNumberAndLineNumber(Purchase_Order, line_No);

            //assert - nothing to do
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17

        public void TestLookupPOLineItemByPurchaseOrderGetsCorrecCount()
        {
            //arrange
            int purchase_Order = 1;
            int actual = 0;
            int expected = 2;

            //act
            actual = _mgr.LookupPOLineItemByPurchaseOrderNumber(purchase_Order).Count;

            //assert
            Assert.AreEqual(expected, actual);
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLookupPOLineItemByPurchaseOrderNumberFailesWithBadData()
        {
            //arrage
            int Purchase_Order = 0;

            //act
            int result = _mgr.LookupPOLineItemByPurchaseOrderNumber(Purchase_Order).Count;

            //assert - nothing to do
        }
    }
}
