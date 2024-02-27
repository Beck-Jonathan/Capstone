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
        ///     calls the sp_update_service_order stored procedure
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

            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_update_service_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.AddWithValue("@Service_Order_ID", serviceOrder.Service_Order_ID);
            // cmd.Parameters.AddWithValue("@VIN", serviceOrder.VIN);
            cmd.Parameters.AddWithValue("@Critical_Issue", Convert.ToInt32(serviceOrder.Critical_Issue));
            cmd.Parameters.AddWithValue("@Service_Type_ID", serviceOrder.Service_Type_ID);
            cmd.Parameters.AddWithValue("@Service_Description", serviceOrder.Service_Description);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
