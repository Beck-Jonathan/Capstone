using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer.Vendor
{
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// CREATED: 2024-03-03
    ///   Vendor Manager class
    /// </summary>
    /// <remarks>
    public class VendorManager : IVendorManager
    {
        IVendorAccessor vendorAccessor;

        public VendorManager()
        {
            //use the database
            vendorAccessor = new VendorAccessor();
        }

        public VendorManager(IVendorAccessor vendorAccessor)
        {
            //use data access fakes
            this.vendorAccessor = vendorAccessor;
        }
        /// <summary>
        ///     Returns all Vendor objects
        /// </summary>
        /// 
        /// 
        /// <returns>
        ///    <see cref="List{Vendor}">List<Vendor</see>: A list of vendor objects.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retreiving the vendors
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-10
        /// </remarks>
        public List<VendorVM> getAllVendors()
        {
            List<VendorVM> results;
            try
            {
                results = vendorAccessor.selectAllVendors();
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }
        /// <summary>
        ///     Get vendor detail from the database
        /// </summary>
        /// <param name="VendorID">
        ///    The vendorid  to be looked up.
        /// </param>
        /// <returns>
        ///    <see cref="Vendor">Vehicle</see>: Vendor data object.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retrieving the vehicle.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-10
        /// </remarks>
        public VendorVM getVendorByVendorID(int VendorID)
        {
            VendorVM result;
            try
            {
                result = vendorAccessor.selectVendorByVendorID(VendorID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
