﻿using DataAccessInterfaces;
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
    /// CREATED: 2024-02-26
    /// <br />
    /// 
    ///     Data accessor class for ServiceLineItems.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: 
    /// <br />
    /// UPDATED:
    /// <br />
    ///     Initial creation
    /// </remarks>
    public class ServiceOrderLineItemsAccessor : IServiceOrderLineItemsAccessor
    {
        /// <summary>
        ///     Retrieves all ServiceOrderLineItems records from the database
        /// </summary>
        /// <returns>
        ///    List of <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see>'s otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-26
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<ServiceOrderLineItems> GetAllServiceOrderLineItems()
        {
            List<ServiceOrderLineItems> serviceOrderLineItems = new List<ServiceOrderLineItems>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_service_line_items";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceOrderLineItems serviceOrderLineItem = new ServiceOrderLineItems()
                    {
                        Service_Order_ID = reader.GetInt32(0),
                        Service_Order_Version = reader.GetInt32(1),
                        Parts_Inventory_ID = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3)
                    };
                    serviceOrderLineItems.Add(serviceOrderLineItem);
                }

                if (serviceOrderLineItems == null)
                {
                    throw new ArgumentException("No service order line item records found");
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
            return serviceOrderLineItems;
        }

        /// <summary>
        /// Inserts a new record into the service order line item table
        /// <br />
        /// <br />
        ///    Creator: Max Fare
        /// <br />
        ///    CREATED: 2024-04-05
        /// </summary>
        /// <param name="item">The line item to add</param>
        /// <returns>The number of rows affected</returns>
        public int InsertServiceOrderLineItem(ServiceOrderLineItems_VM item)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_service_order_line_item";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Service_Order_ID", item.Service_Order_ID);
            cmd.Parameters.AddWithValue("@Service_Order_Version", item.Service_Order_Version);
            cmd.Parameters.AddWithValue("@Parts_Inventory_ID", item.Parts_Inventory_ID);
            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
            return rows;
        }
    }
}
