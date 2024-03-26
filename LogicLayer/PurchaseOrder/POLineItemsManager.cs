using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class POLineItemsManager : IPOLineItemsManager
    {
        private IPOLineItemsAccessor _LineItemAccessor = null;
        public POLineItemsManager()
        {

            _LineItemAccessor = new POLineItemsAccessor(); //use the database
        }

        public POLineItemsManager(IPOLineItemsAccessor pOLineItemsAccessor)
        {
            _LineItemAccessor = pOLineItemsAccessor; //use data access fakes
        }

        /// <summary>
        ///     Retreive a purchase order Line Item VM from the database
        /// </summary>
        /// <param name="purpurchaseOrderID">
        ///    The ID of the purchsae order

        /// <returns>
        ///    <see cref="List<POLineItemVM></POLineItemVM>"</see>: The List Purchase order Line
        ///    Item VM object associated with the purchsae order
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public List<POLineItemVM> LookupPOLineItemByPurchaseOrderNumber(int PONumber)
        {
            List<POLineItemVM> results = new List<POLineItemVM>();
            try
            {
                results = _LineItemAccessor.GetPOLineItemsByPurchseOrder(PONumber);
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }
        /// <summary>
        ///     Retreive a purchase order Line Item VM from the database
        /// </summary>
        /// <param name="purchaseOrderID">
        ///    The ID of the purchsae order
        /// </param>
        ///  /// <param name="lineNumber">
        ///    The Line number of the item
        /// </param>
        /// <returns>
        ///    <see cref="POLineItemVM"</see>: The Purchase order Line Item VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public POLineItemVM LookupPOLineItemByPurchaseOrderNumberAndLineNumber(int PONumber, int lineNumber)
        {
            POLineItemVM _lineitem = new POLineItemVM();
            try
            {
                _lineitem = _LineItemAccessor.GetPOLineItem(PONumber, lineNumber);
            }
            catch (Exception)
            {

                throw;
            }
            return _lineitem;
        }

        /// <summary>
        ///     Inserts a purchase order line item
        /// </summary>
        /// <param cref="POLineItem" name="lineItem">
        ///    The line item that will be added to the order
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="int">int</see>: The line number of the item inserted
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        public int CreatePurchaseOrderLineItem(POLineItem lineItem)
        {
            int result = 0;
            try
            {
                result = _LineItemAccessor.InsertPOLineItem(lineItem);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
