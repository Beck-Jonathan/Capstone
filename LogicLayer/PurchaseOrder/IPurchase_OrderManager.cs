﻿using DataObjects;
using System;
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
    public interface IPurchase_OrderManager
    {
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
        Purchase_OrderVM LookupPurchaseOrderByPurchaseOrderNumber(int Purchase_OrderID);
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
        List<Purchase_OrderVM> LookupPurchaseOrderByDateRange(DateTime startDate, DateTime endDate);

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
        int CreatePurchaseOrder(Purchase_OrderVM purchaseOrder);

    }
}
