using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
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
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    ///     Initial creation
    /// </remarks>
    public class ServiceOrderAccessor : IServiceOrderAccessor
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
        public List<ServiceOrder_VM> GetAllServiceOrders()
        {
            List<ServiceOrder_VM> serviceOrders = new List<ServiceOrder_VM>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_active_service_orders";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
                    {
                        VIN = reader.GetString(0),
                        Service_Order_ID = reader.GetInt32(1),
                        Critical_Issue = reader.GetBoolean(2),
                        Service_Type_ID = reader.GetString(3),
                        Service_Description = reader.GetString(4)
                    };
                    serviceOrders.Add(serviceOrder);
                }

                if (serviceOrders.Count == 0)
                {
                    throw new ArgumentException("No service order records found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return serviceOrders;
        }
    }
}
