using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace LogicLayerTests
{
    [TestClass]
    public class PurchaseOrderTests
    {
        IPurchase_OrderManager _mgr = null;
        [TestInitialize]
        public void testSetup()
        {
            _mgr = new PurchaseOrderManager(new Purchase_Order_Fakes(), new POLineItems_Fakes(),new Parts_Inventory_Fakes());

        }
        [TestMethod]
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17

        public void TestLookupPurchaseOrderByPurchaseOrderNumberGrabsCorrectPO()
        {
            //arrange
            DateTime expected = new DateTime(2022, 06, 05);
            DateTime actual = DateTime.Now;
            int PurchaseOrderID = 1;
            //act
            actual = _mgr.LookupPurchaseOrderByPurchaseOrderNumber(1).Purchase_Order_Date;
            //assert
            Assert.AreEqual(expected, actual);
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLookupPurchaseOrderByPurchaseOrderNumberFailswithInvalidPO()
        {

            //arrange
            int purchaseOrderNumber = 6;
            Purchase_Order purchase_Order = new Purchase_Order();
            //act
            purchase_Order = _mgr.LookupPurchaseOrderByPurchaseOrderNumber(purchaseOrderNumber);
            //assert - nothing to do

        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]
        public void TestLookupPurchaseOrderByDateRangeGetsCorrectResults()
        {
            //arrange
            DateTime start = new DateTime(2022, 5, 10);
            DateTime end = new DateTime(2022, 8, 10);
            List<Purchase_OrderVM> results = new List<Purchase_OrderVM>();
            int expected = 2;
            int actual = 0;
            //act
            results = _mgr.LookupPurchaseOrderByDateRange(start, end);
            actual = results.Count;
            //assert
            Assert.AreEqual(expected, actual);
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLookupPurchaseOrderByDateRangeFailsWithBadDates()
        {

            //arrange
            DateTime start = DateTime.Now;
            DateTime end = new DateTime(2000, 4, 10);
            List<Purchase_OrderVM> results = new List<Purchase_OrderVM>();
            //act
            results = _mgr.LookupPurchaseOrderByDateRange(start, end);
            //assert --nothing to do
        }
        //Created By: Jonathan Beck
        //Creation Date: 2024-02-17
        [TestMethod]

        public void TestLookupPurchaseOrderByDateRangeReturnsNothingWhenNoRecordsArePresent()
        {
            //arrange
            DateTime start = DateTime.MinValue;
            DateTime end = new DateTime(2000, 4, 10);
            List<Purchase_OrderVM> results = new List<Purchase_OrderVM>();
            int expected = 0;

            //act
            results = _mgr.LookupPurchaseOrderByDateRange(start, end);
            int actual = results.Count;
            //assert
            Assert.AreEqual(expected, actual);
        }

        //Created By: Jonathan Beck
        //Creation Date: 2024-03-18
        [TestMethod]

        public void TestInsertPurchaseOrderReturnsCorrectPOAndUpdatesQauntity()
        {
            int expected = 4;
            
            Purchase_OrderVM purchase_Order = new Purchase_OrderVM();
            POLineItemVM line1 = new POLineItemVM();
            line1.Quantity = 1;
            line1.PartsInventoryID = 1;
            POLineItemVM line2 = new POLineItemVM();
            line2.Quantity = 3;
            line2.PartsInventoryID = 2;
            POLineItemVM line3 = new POLineItemVM();
            line3.Quantity = 5;
            line3.PartsInventoryID = 3;
            purchase_Order.pOLineItems = new List<POLineItemVM>();
            purchase_Order.pOLineItems.Add(line1);
            purchase_Order.pOLineItems.Add(line2);
            purchase_Order.pOLineItems.Add(line3);

            int actual = _mgr.CreatePurchaseOrder(purchase_Order);
            
            Assert.AreEqual(expected, actual);

        }
    }
}
