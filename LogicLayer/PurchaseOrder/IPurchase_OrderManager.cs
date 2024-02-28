using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
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


    }
}
