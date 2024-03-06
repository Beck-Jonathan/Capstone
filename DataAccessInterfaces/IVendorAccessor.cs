using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{ /// <summary>
  /// AUTHOR: Jonathan Beck
  /// CREATED: 2024-03-03
  ///    Data Access interface for vendors
  /// </summary>
  /// <remarks>
    public interface IVendorAccessor
    {
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

        VendorVM selectVendorByVendorID(int VendorID);
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
        List<VendorVM> selectAllVendors();

    }
}
