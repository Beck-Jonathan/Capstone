using DataAccessInterfaces;
using DataAccessLayer;
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
    /// CREATED: 2024-02-10
    /// <br />
    /// 
    ///     Manager class for ServiceOrder that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    ///     Initial creation
    /// </remarks>
    public class ServiceOrderManager : IServiceOrderManager
    {
        IServiceOrderAccessor _serviceOrderAccessor = null;


        // Default Constuctor
        public ServiceOrderManager()
        {
            _serviceOrderAccessor = new ServiceOrderAccessor();
        }

        // Parametized constructor to allow use of fake data
        public ServiceOrderManager(IServiceOrderAccessor serviceOrderAccessor)
        {
            _serviceOrderAccessor = serviceOrderAccessor;
        }


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
        public List<ServiceOrder_VM> GetALlServiceOrders()
        {
            List<ServiceOrder_VM> serviceOrders = null;

            try
            {
                serviceOrders = _serviceOrderAccessor.GetAllServiceOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serviceOrders;
        }
    }
}
