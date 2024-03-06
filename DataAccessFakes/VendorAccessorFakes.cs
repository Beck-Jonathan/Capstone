using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// CREATED: 2024-03-03
    ///    Data acecss fakes for vendors
    /// </summary>
    /// <remarks>
    public class VendorAccessorFakes : IVendorAccessor
    {
        public List<VendorVM> Fakevendors = new List<VendorVM>();

        public VendorAccessorFakes()
        {
            VendorVM VendorOne = new VendorVM();
            VendorOne.Vendor_ID = 1;
            VendorOne.Vendor_City = "Cedar Rapids";
            VendorVM VendorTwo = new VendorVM();
            VendorTwo.Vendor_ID = 2;
            VendorTwo.Vendor_City = "Iowa City";
            VendorVM VendorThree = new VendorVM();
            VendorThree.Vendor_ID = 3;
            VendorThree.Vendor_City = "Ames";
            Fakevendors.Add(VendorOne);
            Fakevendors.Add(VendorTwo);
            Fakevendors.Add(VendorThree);
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

        public List<VendorVM> selectAllVendors()
        {
            return Fakevendors;
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
        public VendorVM selectVendorByVendorID(int VendorID)
        {
            VendorVM result = null;
            foreach (VendorVM test in Fakevendors)
            {
                if (test.Vendor_ID == VendorID)
                {
                    result = test;
                    break;
                }
            }
            if (result == null) { throw new ApplicationException("Vendor not found"); }
            return result;
        }
    }
}
