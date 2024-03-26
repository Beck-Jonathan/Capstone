using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    ///    A Manager class for Purchase Orders
    /// </summary>
    /// <remarks>
    ///    CONTRIBUTOR: Jonathan Beck
    ///    CREATED: 2024-02-17
    /// </remarks>
    public class PurchaseOrderManager : IPurchase_OrderManager
    {
        private IPurchase_OrderAccessor _OrderAccessor = null;
        private IPOLineItemsAccessor _pOLineItemsAccessor = null;
        public PurchaseOrderManager()
        {

            _OrderAccessor = new Purchase_OrderAccessor(); //use the database
            _pOLineItemsAccessor = new POLineItemsAccessor();
        }

        public PurchaseOrderManager(IPurchase_OrderAccessor purchase_OrderAccessor, IPOLineItemsAccessor pOLineItemsAccessor)
        {
            _OrderAccessor = purchase_OrderAccessor; //use data access fakes
            _pOLineItemsAccessor = pOLineItemsAccessor;
        }
        /// <summary>
        ///     Retreive a purchase order VM from the database
        /// </summary>
        /// <param name="id">
        ///    The ID of the purchsae order
        /// </param>
        /// <returns>
        ///    <see cref="PurchaseOrderCM">PurchaseOrdeVM</see>: The Purchase order VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public Purchase_OrderVM LookupPurchaseOrderByPurchaseOrderNumber(int id)
        {
            Purchase_OrderVM purchase_Order = null;
            try
            {
                purchase_Order = _OrderAccessor.GetPurchaseOrderByID(id);
                purchase_Order.pOLineItems = _pOLineItemsAccessor.GetPOLineItemsByPurchseOrder(purchase_Order.Purchase_Order_ID);
            }
            catch (Exception)
            {

                throw;
            }
            return purchase_Order;
        }
        /// <summary>
        ///     Retreive all purchase order VM from the database within a given date range
        /// </summary>
        ///  <param name="startDate">
        ///   The start date to look up by
        /// </param>
        ///  <param name="endDate">
        ///    the end date to look up by
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="PurchaseOrderVM">List<PurchaseOrdeVM<>/see>: The Purchase order VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public List<Purchase_OrderVM> LookupPurchaseOrderByDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) { throw new ArgumentException("Start Has to be before begin"); }
            List<Purchase_OrderVM> purchase_orders = new List<Purchase_OrderVM>();
            try
            {
                purchase_orders = _OrderAccessor.GetPurchaseOrderByDateRange(startDate, endDate);
                foreach (Purchase_OrderVM purchase_Order in purchase_orders)
                {
                    purchase_Order.pOLineItems = _pOLineItemsAccessor.GetPOLineItemsByPurchseOrder(purchase_Order.Purchase_Order_ID);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return purchase_orders;
        }

        /// <summary>
        ///     Creates the purchase order
        /// </summary>
        /// <param cref="Purchase_OrderVM" name="purchaseOrder">
        ///    The Purchase order to add to the database
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the purchase order
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        public int CreatePurchaseOrder(Purchase_OrderVM purchaseOrder)
        {
            int result = 0;
            try
            {
                int PurchaseOrderID = _OrderAccessor.InsertPurchaseOrder(purchaseOrder);
                foreach (POLineItem POLine in purchaseOrder.pOLineItems)
                {
                    POLine.PurchaseOrderID = PurchaseOrderID;
                    result += _pOLineItemsAccessor.InsertPOLineItem(POLine);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return result;
        }
    }

}
