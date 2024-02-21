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
        private List<ServiceOrder> _updatedServiceOrders = new List<ServiceOrder>();

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

        /// <summary>
        /// Updates a fake service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the updated details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the update operation:
        /// </returns>
        /// <remarks>
        ///     If the provided <paramref name="serviceOrder"/> is null, an <see cref="ArgumentNullException"/> is thrown.
        ///     The method searches for the fake service order based on the provided Service_Order_ID.
        ///     If found, it updates the fake service order with the new values.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="serviceOrder"/> is null.</exception>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-02-18</created>
        /// <updated>yyyy-MM-dd</updated>
        /// <update>
        /// <summary>
        /// Update comments go here.
        /// </summary>
        /// <remarks>
        /// Explain what you changed in this method.
        /// A new remark should be added for each update to this method.
        /// </remarks>
        /// </update>

        public int UpdateServiceOrder(ServiceOrder serviceOrder)
        {
            if (serviceOrder == null)
            {
                throw new ArgumentNullException(nameof(serviceOrder), "Service order cannot be null.");
            }

            // Find the corresponding fake service order and update it
            var fakeServiceOrder = _fakeServiceOrders.Find(so => so.Service_Order_ID == serviceOrder.Service_Order_ID);
            if (fakeServiceOrder != null)
            {
                // Update the fake service order with the new values
                fakeServiceOrder.VIN = serviceOrder.VIN;
                fakeServiceOrder.Critical_Issue = serviceOrder.Critical_Issue;
                fakeServiceOrder.Service_Type_ID = serviceOrder.Service_Type_ID;
                fakeServiceOrder.Service_Description = serviceOrder.Service_Description;

                // Keep track of the updated service order
                _updatedServiceOrders.Add(serviceOrder);

                // Return 1 to indicate success (assuming the update was successful)
                return 1;
            }

            // Return 0 to indicate failure (service order not found)
            return 0;
        }
    }
}
