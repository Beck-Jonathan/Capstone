using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
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
    }
}
