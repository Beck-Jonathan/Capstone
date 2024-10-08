﻿using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    ///    A Manager class for Purchase Orders Line Items
    /// </summary>
    /// <remarks>
    ///    CONTRIBUTOR: Jonathan Beck
    ///    CREATED: 2024-02-17
    /// </remarks>
    public interface IPOLineItemsManager
    {
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
        POLineItemVM LookupPOLineItemByPurchaseOrderNumberAndLineNumber(int PONumber, int lineNumber);

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
        List<POLineItemVM> LookupPOLineItemByPurchaseOrderNumber(int PONumber);

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
        int CreatePurchaseOrderLineItem(POLineItem lineItem);
    }
}
