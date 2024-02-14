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
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    ///     Fake ServiceOrder data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// Initial creation
    /// </remarks>
    public class ServiceOrderAccessorFakes : IServiceOrderAccessor
    {
        private List<ServiceOrder_VM> _fakeServiceOrders = new List<ServiceOrder_VM>();

        public ServiceOrderAccessorFakes()
        {
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "2GNALDEK9C6340800",
                Service_Order_ID = 100000,
                Critical_Issue = true,
                Service_Type_ID = "Windshield Wiper Replacement",
                Service_Description = "Replace the windshield wipers with OEM wipers"
            });            
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437",
                Service_Order_ID = 100001,
                Critical_Issue = false,
                Service_Type_ID = "Brake Pad Replacement",
                Service_Description = "Replace the brake pads with OEM pads"
            });
        }
        

        /// <summary>
        ///     Returns all fake ServiceOrder_VM records
        /// </summary>

        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> The list of all fake ServiceOrder_VM objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public List<ServiceOrder_VM> GetAllServiceOrders()
        {
            return _fakeServiceOrders;
        }
    }
}
