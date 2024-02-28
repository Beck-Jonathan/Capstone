using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IPOLineItemsAccessor
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
        POLineItemVM GetPOLineItem(int purchaseOrderID, int lineNumber);
        /// <summary>
        ///     Retreive all purchase order VM from the database
        /// </summary>
        /// 
        /// <returns>
        ///    <see cref="PurchaseOrderCM">List<PurchaseOrdeVM<>/see>: The Purchase order VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        List<POLineItemVM> GetPOLineItemsByPurchseOrder(int purpurchaseOrderID);
    }
}
