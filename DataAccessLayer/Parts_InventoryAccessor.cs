/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// IAccessor for Parts_Inventory
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
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
    //needs commment
    public class Parts_InventoryAccessor : IParts_InventoryAccessor
    {
        ///<inheritdoc/>
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retrieves Part_Inventory by Part_InventoryID
        /// </summary>
        ///<Throws>Argument Exception if item not found</Throws>
        /// <remarks>
        /// Jonsthan Beck
        /// Updated: 2024/02/06
        /// </remarks>
        public Parts_Inventory selectParts_InventoryByPrimaryKey(int Parts_Inventory_ID)
        {
            Parts_Inventory output = new Parts_Inventory();
            // start with a connection object 
            //needs sql connection provider
            var conn = DBConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_select_part_by_part_id";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Parts_Inventory_ID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@Parts_Inventory_ID"].Value = Parts_Inventory_ID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.Parts_Inventory_ID = reader.GetInt32(0);
                        output.Part_Name = reader.GetString(1);
                        output.Part_Quantity = reader.GetInt32(2);
                        output.Item_Description = reader.GetString(3);
                        output.Item_Specifications = reader.GetString(4);
                        output.Part_Photo_URL = reader.GetString(5);
                        output.Ordered_Qty = reader.GetInt32(6);
                        output.Stock_Level = reader.GetInt32(7);
                        output.Is_Active = reader.GetBoolean(8);

                    }
                    else
                    {
                        throw new ArgumentException("Oops");
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
        public List<Parts_Inventory> selectAllParts_Inventory()
        {
            List<Parts_Inventory> output = new List<Parts_Inventory>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_all_parts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Parts_Inventory = new Parts_Inventory();
                        _Parts_Inventory.Parts_Inventory_ID = reader.GetInt32(0);
                        _Parts_Inventory.Part_Name = reader.GetString(1);
                        _Parts_Inventory.Part_Quantity = reader.GetInt32(2);
                        _Parts_Inventory.Item_Description = reader.GetString(3);
                        _Parts_Inventory.Item_Specifications = reader.GetString(4);
                        _Parts_Inventory.Part_Photo_URL = reader.GetString(5);
                        _Parts_Inventory.Ordered_Qty = reader.GetInt32(6);
                        _Parts_Inventory.Stock_Level = reader.GetInt32(7);
                        _Parts_Inventory.Is_Active = reader.GetBoolean(8);
                        output.Add(_Parts_Inventory);
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

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-25
        /// Selects all active inventory parts
        /// </summary>
        /// <returns>All active parts</returns>
        public List<Parts_Inventory> selectParts_Inventory()
        {
            List<Parts_Inventory> output = new List<Parts_Inventory>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_parts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Parts_Inventory = new Parts_Inventory();
                        _Parts_Inventory.Parts_Inventory_ID = reader.GetInt32(0);
                        _Parts_Inventory.Part_Name = reader.GetString(1);
                        _Parts_Inventory.Part_Quantity = reader.GetInt32(2);
                        _Parts_Inventory.Item_Description = reader.GetString(3);
                        _Parts_Inventory.Item_Specifications = reader.GetString(4);
                        _Parts_Inventory.Part_Photo_URL = reader.GetString(5);
                        _Parts_Inventory.Ordered_Qty = reader.GetInt32(6);
                        _Parts_Inventory.Stock_Level = reader.GetInt32(7);
                        _Parts_Inventory.Is_Active = reader.GetBoolean(8);
                        output.Add(_Parts_Inventory);
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

        public int UpdateParts_Inventory(Parts_Inventory oldPart, Parts_Inventory newPart)
        {
            int rows = 0;
            // start with a connection object 
            //needs sql connection provider
            var conn = DBConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_part";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@part_id", SqlDbType.Int);

            cmd.Parameters.Add("@new_Part_Name", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@new_Part_Qty", SqlDbType.Int);
            cmd.Parameters.Add("@new_Item_Description", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@new_Item_Specifications", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@new_Part_Photo_URL", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@new_ordered_qty", SqlDbType.Int);
            cmd.Parameters.Add("@new_stock_lvl", SqlDbType.Int);

            cmd.Parameters.Add("@old_Part_Name", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@old_Part_Qty", SqlDbType.Int);
            cmd.Parameters.Add("@old_Item_Description", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@old_Item_Specifications", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@old_Part_Photo_URL", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@old_ordered_qty", SqlDbType.Int);
            cmd.Parameters.Add("@old_stock_lvl", SqlDbType.Int);
           
            //We need to set the parameter values
            cmd.Parameters["@part_id"].Value = oldPart.Parts_Inventory_ID;

            cmd.Parameters["@new_Part_Name"].Value = newPart.Part_Name;
            cmd.Parameters["@new_Part_Qty"].Value = newPart.Part_Quantity;
            cmd.Parameters["@new_Item_Description"].Value = newPart.Item_Description;
            cmd.Parameters["@new_Item_Specifications"].Value = newPart.Item_Specifications;
            cmd.Parameters["@new_Part_Photo_URL"].Value = newPart.Part_Photo_URL;
            cmd.Parameters["@new_ordered_qty"].Value = newPart.Ordered_Qty;
            cmd.Parameters["@new_stock_lvl"].Value = newPart.Stock_Level;

            cmd.Parameters["@old_Part_Name"].Value = oldPart.Part_Name;
            cmd.Parameters["@old_Part_Qty"].Value = oldPart.Part_Quantity;
            cmd.Parameters["@old_Item_Description"].Value = oldPart.Item_Description;
            cmd.Parameters["@old_Item_Specifications"].Value = oldPart.Item_Specifications;
            cmd.Parameters["@old_Part_Photo_URL"].Value = oldPart.Part_Photo_URL;
            cmd.Parameters["@old_ordered_qty"].Value = oldPart.Ordered_Qty;
            cmd.Parameters["@old_stock_lvl"].Value = oldPart.Stock_Level;

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-23
        /// Deactivates the given part by changing the Is_Active field to false
        /// </summary>
        /// <param name="part"></param>
        /// <returns>the number of rows affected</returns>
        /// <exception cref="ArgumentException">If no change is made</exception>
        /// <remarks>
        /// 
        /// </remarks>
        public int DeactivateParts_Inventory(Parts_Inventory part)
        {
            int rows = 0;
            // start with a connection object 
            //needs sql connection provider
            var conn = DBConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_deactivate_part";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@part_id", SqlDbType.Int);
            // give the parameter a value
            cmd.Parameters["@part_id"].Value = part.Parts_Inventory_ID;
            try
            {
                conn.Open();
                rows = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        ///     Selects all parts compatible with a given vehicle model
        /// </summary>
        /// <param name="vehicleModelId">
        ///    The ID of the vehicle model
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{Parts_Inventory}">IEnumerable<Parts_Inventory></Parts_Inventory></see>: The parts compativle with the given vehicle model
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> vehicleModelId: The ID of the vehicle model
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-22
        /// </remarks>
        public IEnumerable<Parts_Inventory> SelectPartsCompatibleWithVehicleModelId(int vehicleModelId)
        {
            List<Parts_Inventory> output = new List<Parts_Inventory>();
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_parts_compatible_with_vehicle_model_id";
            var cmd = new SqlCommand(commandText, conn);

            cmd.Parameters.Add("@Vehicle_Model_Id", SqlDbType.Int);

            cmd.Parameters["@Vehicle_Model_Id"].Value = vehicleModelId;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Parts_Inventory = new Parts_Inventory();
                        _Parts_Inventory.Part_Name = reader.GetString(0);
                        _Parts_Inventory.Part_Quantity = reader.GetInt32(1);
                        _Parts_Inventory.Parts_Inventory_ID = reader.GetInt32(2);
                        output.Add(_Parts_Inventory);
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

        /// <summary>
        ///     Retrieves all parts compatible with a given vehicle model
        /// </summary>
        /// <param name="model_ID">
        ///    The ID of the vehicle model
        /// </param>
        /// <param name="part_ID">
        ///    The ID of the part
        /// </param>
        /// <returns>
        ///    <see cref="Int">Int</see>: 1 if the part compatibility was removed
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> vehicleModelId: The ID of the vehicle model
        ///   <see cref="int">int</see> Parts_InventoryID: The ID of the part
        ///   /// <br /><br />
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>
        /// </remarks>

        public int DeleteModelCompatibility(int vehicleModelId, int Parts_InventoryID)
        {
            int rows = 0;
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Model_Compatibility";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Model_Lookup_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Parts_Inventory_ID", SqlDbType.Int);
            

            //We need to set the parameter values
            cmd.Parameters["@Model_Lookup_ID"].Value = vehicleModelId;
            cmd.Parameters["@Parts_Inventory_ID"].Value = Parts_InventoryID;

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        ///     Adds a compatible inventory part to a vehicle model
        /// </summary>
        /// <param name="model_ID">
        ///    The ID of the vehicle model
        /// </param>
        /// <param name="part_ID">
        ///    The ID of the part
        /// </param>
        /// <returns>
        ///    <see cref="Int">Int</see>: 1 if the part compatibility was removed
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> vehicleModelId: The ID of the vehicle model
        ///   <see cref="int">int</see> Parts_InventoryID: The ID of the part
        ///   /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-03-26
        /// </remarks>
        /// </remarks>
        public int AddModelCompatibility(int vehicleModelID, int partsInventoryID)
        {
            int rows = 0;
            
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_add_compatible_part";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Vehicle_Model_ID", SqlDbType.Int).Value = vehicleModelID;
            cmd.Parameters.Add("@p_Part_Inventory_ID", SqlDbType.Int).Value = partsInventoryID;
            

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing sp_add_compatible_part",ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        // Reviewed By: John Beck
    }
}
