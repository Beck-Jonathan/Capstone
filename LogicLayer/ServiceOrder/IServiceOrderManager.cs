using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Ben Collins, Steven Sanchez
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    /// 
    ///     Manager class for ServiceOrder that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-2-20
    /// <br />
    ///     Initial creation
    ///     <br/>
    ///     Added SelectServiceOrderByServiceOrderID to populate the CompleteWorkOrderPage with options.
    /// </remarks>
    public interface IServiceOrderManager
    {
        /// <summary>
        ///     Retrieves all ServiceOrder_VM records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> List of ServiceOrder_VM objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
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
        List<ServiceOrder_VM> GetALlServiceOrders();

        /// <summary>
        /// Updates a service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the updated details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the update operation:
        /// </returns>
        /// <remarks>
        ///     If the provided <paramref name="serviceOrder"/> is null, an <see cref="ArgumentNullException"/> is thrown.
        ///     The method searches for the service order based on the provided Service_Order_ID.
        ///     If found, it updates the service order with the new values.
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
        /// Creates a service order with the provided details.
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
        bool CreateServiceOrder(ServiceOrder_VM serviceOrder);

        /// <summary>
        ///     Retrieves all ServiceOrder_VM records from the ServiceOrderAccessor and,
        ///     <br/>
        ///     the Vehicle, ServiceOrderLineItems, and Parts_Inventory from their 
        ///     <br/>
        ///     respective managers
        /// </summary>
        /// <returns>
        ///    <see cref="ServiceOrder_VM">ServiceOrder_VM</see> ServiceOrder_VM object
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
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
        /// Completes a service order by enacting changes to inventory 
        /// <br/>
        /// numbers and marking the order as completed.
        /// <br/>
        /// <br/>
        /// Max Fare 
        /// Created: 2024-04-01
        /// </summary>
        /// <param name="serviceOrder">The order to complete</param>
        /// <returns><see cref="int">A number identifying the result of attempted changes</see></returns>
        int CompleteServiceOrder(ServiceOrder_VM serviceOrder);
    }
}
