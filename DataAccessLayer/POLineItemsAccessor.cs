using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class POLineItemsAccessor : IPOLineItemsAccessor
    {
        /// <summary>
        ///     Retreive a purchase order Line Item VM from the database
        /// </summary>
        /// <param name="purchaseOrderID">
        ///    The ID of the purchsae order
        /// </param>
        ///  /// <param name="lineNumber">
        ///    The Line number of the item
        /// </param>
        /// <returns>
        ///    <see cref="POLineItemVM"</see>: The Purchase order Line Item VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public POLineItemVM GetPOLineItem(int purchaseOrderID, int lineNumber)
        {
            POLineItemVM output = new POLineItemVM();
            Parts_Inventory inventory = new Parts_Inventory();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_purchase_order_line_item";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Purchase_Order_ID", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@Purchase_Order_ID"].Value = purchaseOrderID;
            cmd.Parameters.Add("@Line_Number", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@Line_Number"].Value = lineNumber;
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
                        Parts_Inventory part = new Parts_Inventory();
                        output.PurchaseOrderID = reader.GetInt32(0);
                        output.PartsInventoryID = reader.GetInt32(1);
                        output.LineNumber = reader.GetInt32(2);
                        output.LineItemName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        output.Quantity = reader.GetInt32(4);
                        output.LineItemDescription = reader.GetString(5);
                        part.Parts_Inventory_ID = reader.GetInt32(6);
                        part.Part_Name = reader.GetString(7);
                        part.Part_Quantity = reader.GetInt32(8);
                        part.Item_Description = reader.GetString(9);
                        part.Item_Specifications = reader.GetString(10);
                        part.Part_Photo_URL = reader.GetString(11);
                        part.Ordered_Qty = reader.GetInt32(12);
                        part.Stock_Level = reader.GetInt32(13);
                        part.Is_Active = reader.IsDBNull(14);
                        output.Part = part;



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
        ///     Retreive a purchase order Line Item VM from the database
        /// </summary>
        /// <param name="purpurchaseOrderID">
        ///    The ID of the purchsae order

        /// <returns>
        ///    <see cref="List<POLineItemVM></POLineItemVM>"</see>: The List Purchase order Line
        ///    Item VM object associated with the purchsae order
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public List<POLineItemVM> GetPOLineItemsByPurchseOrder(int purpurchaseOrderID)
        {
            List<POLineItemVM> results = new List<POLineItemVM>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_purchase_order_line_item_by_purchase_Order";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Purchase_Order_ID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@Purchase_Order_ID"].Value = purpurchaseOrderID;

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        POLineItemVM output = new POLineItemVM();
                        Parts_Inventory part = new Parts_Inventory();
                        output.PurchaseOrderID = reader.GetInt32(0);
                        output.PartsInventoryID = reader.GetInt32(1);
                        output.LineNumber = reader.GetInt32(2);
                        output.LineItemName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        output.Quantity = reader.GetInt32(4);
                        output.Price = reader.GetDecimal(5);
                        output.LineItemDescription = reader.GetString(6);
                        part.Parts_Inventory_ID = reader.GetInt32(7);
                        part.Part_Name = reader.GetString(8);
                        part.Part_Quantity = reader.GetInt32(9);
                        part.Item_Description = reader.GetString(10);
                        part.Item_Specifications = reader.GetString(11);
                        part.Part_Photo_URL = reader.GetString(12);
                        part.Ordered_Qty = reader.GetInt32(13);
                        part.Stock_Level = reader.GetInt32(14);
                        part.Is_Active = reader.IsDBNull(15);
                        output.Part = part;





                        results.Add(output);



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
            return results;
        }
    }
}
