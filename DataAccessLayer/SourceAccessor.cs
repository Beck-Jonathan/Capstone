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
    public class SourceAccessor : ISourceAccessor
    {
        /// <summary>
        ///     Gets the  most recent price of an item from a specific vendor
        /// </summary>
        /// <param name="Vendor_ID">
        ///    The vendor ID that supplies this part inventory
        /// </param>
        /// <param name="Parts_inventory_id">
        ///    Our Part Inventory number
        /// </param>
        /// <returns>
        ///    <see cref="Source">source</see>: The source object that contains various information
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        public Source getSourceByVendorIDandPartsInventoryId(int Vendor_Id, int Parts_inventory_id)
        {
            Source output = new Source();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_part_by_vendor_ID_and_part_number";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Vendor_Id", SqlDbType.Int);
            cmd.Parameters.Add("@Parts_inventory_id", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@Vendor_Id"].Value = Vendor_Id;
            cmd.Parameters["@Parts_inventory_id"].Value = Parts_inventory_id;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.Vendor_Id = reader.GetInt32(0);
                        output.Parts_inventory_id = reader.GetInt32(1);
                        output.Vendor_Part_Number = reader.GetString(2);
                        output.Estimated_delivery_time_days = reader.GetInt32(3);
                        output.Part_Price = reader.GetDecimal(4);
                        output.Minimum_order_Qty = reader.GetInt32(5);
                        output.Active = reader.GetBoolean(6);

                    }
                    else
                    {
                        throw new ArgumentException("Source not found");
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
            return output;
        }
    }
}
