using DataObjects;
using System;
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
    public interface IServiceOrderLineItemsManager
    {
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
        List<ServiceOrderLineItems> GetServiceOrderLineItems();
    }
}
