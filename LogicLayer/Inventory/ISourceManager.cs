using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Inventory
{
    public interface ISourceManager
    {
        /// <summary>
        ///     Gets the  most recent price of an item from a specific vendor
        /// </summary>
        /// <param name="Vendor_ID">
        ///    The vendor ID that supplies this part inventory
        /// </param>
        /// <param name="Parts_inventory_id">
        ///    Our Part Inventory number
        /// </param>
        /// <returns>
        ///    <see cref="Source">source</see>: The source object that contains various information
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        Source LookupSourceByVendorIDandPartsInventoryID(int Vendor_Id, int Parts_inventory_id);

    }

}
