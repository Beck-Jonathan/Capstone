using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Inventory
{
    /// <summary> 
    /// Initial version created by Jonathan Beck
    /// Basic template for the source mangaer.
    /// Date : 3/18/2024
    /// </summary>
    public class SourceManager : ISourceManager
    {
        private ISourceAccessor sourceAccessor = null;
        public SourceManager()
        {

            sourceAccessor = new SourceAccessor(); //use the database
        }

        public SourceManager(ISourceAccessor _sourceAcessor)
        {
            sourceAccessor = _sourceAcessor; //use data access fakes
        }

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
        public Source LookupSourceByVendorIDandPartsInventoryID(int Vendor_Id, int Parts_inventory_id)
        {
            Source _source = new Source();
            try
            {
                _source = sourceAccessor.getSourceByVendorIDandPartsInventoryId(Vendor_Id, Parts_inventory_id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _source;
        }
    }
}
