using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    /// 
    ///     Data access class for ServiceOrder.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Steven Sanchez
    /// <br />
    /// UPDATED: 2024-02-18
    /// <br />
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-03-19
    /// <br />
    ///     Initial creation
    ///     Added UpdateServiceOrder(ServiceOrder serviceOrder)
    ///     Added SelectServiceOrderByServiceOrderID() method to complete a WO.
    /// </remarks>
    /// 
    public interface IServiceOrderAccessor
    {
        /// <summary>
        ///     Retrieves all ServiceOrder records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> List of ServiceOrder_VM objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        List<ServiceOrder_VM> GetAllServiceOrders();

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
        int UpdateServiceOrder(ServiceOrder serviceOrder);

        /// <summary>
        /// Creates a fake service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the create operation:
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-03-12</created>
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
        int CreateServiceOrder(ServiceOrder_VM serviceOrder);

        /// <summary>
        ///     Retrieves all ServiceOrder records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="ServiceOrder_VM">ServiceOrder_VM</see> ServiceOrder_VM object otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        ServiceOrder_VM SelectServiceOrderByServiceOrderID(int serviceOrderID);

        /// <summary>
        /// marks a service order as inactive in the database
        /// <br/>
        /// <br/>
        /// Max Fare 
        /// Created: 2024-04-01
        /// </summary>
        /// <param name="serviceOrder">The Service Order to Deactivate</param>
        /// <returns></returns>
        int DeactivateServiceOrder(ServiceOrder_VM serviceOrder);
        /// <summary>
        /// marks an inactive service order as active in the database
        /// <br/>
        /// <br/>
        /// Max Fare 
        /// Created: 2024-04-01
        /// </summary>
        /// <param name="serviceOrder">The Service Order to Reactivate</param>
        /// <returns></returns>
        int ActivateServiceOrder(ServiceOrder_VM serviceOrder);
    }
}
