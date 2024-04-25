using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-26
    /// <br />
    ///     Manager class for ServiceOrderLineItems that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-2-27
    /// <br />
    ///     Initial creation
    ///     Added GetServiceOrderLineItems() to populate the vehicle object in the ServiceOrderManager.
    /// </remarks>
    public class ServiceOrderLineItemsManager : IServiceOrderLineItemsManager
    {
        // Global Variables
        IServiceOrderLineItemsAccessor _serviceOrderLineItemsAccessor;

        // Default Constructor
        public ServiceOrderLineItemsManager()
        {
            _serviceOrderLineItemsAccessor = new ServiceOrderLineItemsAccessor();
        }
        
        // Parameterized Constructor
        public ServiceOrderLineItemsManager(IServiceOrderLineItemsAccessor serviceOrderLineItemsAccessor)
        {
            _serviceOrderLineItemsAccessor = serviceOrderLineItemsAccessor;
        }

        /// <summary>
        ///     Retrieves all ServiceOrderLineItems records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrderLineItmes_VM}">ServiceOrderLineItems</see> List of ServiceOrderLineItems objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-27
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<ServiceOrderLineItems> GetServiceOrderLineItems()
        {
            List<ServiceOrderLineItems> result = new List<ServiceOrderLineItems>();
            try
            {
                result = _serviceOrderLineItemsAccessor.GetAllServiceOrderLineItems();          
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Stored Procedure Problem.", ex);
            }
            return result;
        }

        /// <summary>
        /// Adds a line item to an existing Service Order
        /// <br/>
        /// <br/>
        /// Creator: Max Fare
        /// <br/>
        /// Date Created: 2024-04-05
        /// </summary>
        /// <param name="item"></param>
        /// <returns>An int to be used as a success indicator</returns>
        /// <exception cref="ApplicationException"></exception>
        public int AddServiceOrderLineItem(ServiceOrderLineItems_VM item)
        {
            int result = 0;
            try
            {
                result = _serviceOrderLineItemsAccessor.InsertServiceOrderLineItem(item);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Stored Procedure Problem", ex);
            }
            return result;
        }
    }
}
