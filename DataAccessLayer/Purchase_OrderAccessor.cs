using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Purchase_OrderAccessor : IPurchase_OrderAccessor
    {


        /// <summary>
        ///     Retreive a purchase order VM from the database
        /// </summary>
        /// <param name="id">
        ///    The ID of the purchsae order
        /// </param>
        /// <returns>
        ///    <see cref="PurchaseOrderVM">PurchaseOrdeVM</see>: The Purchase order VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public Purchase_OrderVM GetPurchaseOrderByID(int Purchase_Order_ID)
        {
            Purchase_OrderVM output = new Purchase_OrderVM();
            var _vendor = new Vendor();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_purchase_order_by_purchase_order_ID";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Purchase_Order_ID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@Purchase_Order_ID"].Value = Purchase_Order_ID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.Purchase_Order_ID = reader.GetInt32(0);
                        output.Vendor_ID = reader.GetInt32(1);
                        output.Purchase_Order_Date = reader.GetDateTime(2);
                        output.Delivery_Address = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        output.Delivery_Address2 = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        output.Delivery_City = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        output.Delivery_State = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        output.Delivery_Country = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        output.Delivery_Zip = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        output.Is_Active = reader.GetBoolean(9);
                        _vendor.Vendor_ID = reader.GetInt32(10);
                        _vendor.Vendor_Name = reader.GetString(11);
                        _vendor.Vendor_Contact_Given_Name = reader.GetString(12);
                        _vendor.Vendor_Contact_Family_Name = reader.GetString(13);
                        _vendor.Vendor_Contact_Phone_Number = reader.GetString(14);
                        _vendor.Vendor_Contact_Email = reader.GetString(15);
                        _vendor.Vendor_Phone_Number = reader.GetString(16);
                        _vendor.Vendor_Address = reader.GetString(17);
                        _vendor.Vendor_Address2 = reader.GetString(18);
                        _vendor.Vendor_City = reader.GetString(19);
                        _vendor.Vendor_State = reader.GetString(20);
                        _vendor.Vendor_Country = reader.GetString(21);
                        _vendor.Vendor_Zip = reader.GetString(22);
                        _vendor.Preferred = reader.GetBoolean(23);
                        _vendor.Is_Active = reader.GetBoolean(24);
                        output.vendor = _vendor;

                    }
                    else
                    {
                        throw new ArgumentException("Purchase_Order not found");
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
        ///     Retreive all purchase order VM from the database within a given date range
        /// </summary>
        ///  <param name="startDate">
        ///   The start date to look up by
        /// </param>
        ///  <param name="endDate">
        ///    the end date to look up by
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="PurchaseOrderVM">List<PurchaseOrdeVM<>/see>: The Purchase order VM object
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-02-17
        /// </remarks>
        public List<Purchase_OrderVM> GetPurchaseOrderByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Purchase_OrderVM> output = new List<Purchase_OrderVM>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_purchase_orders_by_date_range";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Start_Date", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@Start_Date"].Value = startDate;
            cmd.Parameters.Add("@End_Date", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@End_Date"].Value = endDate;
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
                        var _Purchase_Order = new Purchase_OrderVM();
                        var _vendor = new Vendor();
                        _Purchase_Order.Purchase_Order_ID = reader.GetInt32(0);
                        _Purchase_Order.Vendor_ID = reader.GetInt32(1);
                        _Purchase_Order.Purchase_Order_Date = reader.GetDateTime(2);
                        _Purchase_Order.Delivery_Address = reader.IsDBNull(8) ? "" : reader.GetString(3);
                        _Purchase_Order.Delivery_Address2 = reader.IsDBNull(8) ? "" : reader.GetString(4);
                        _Purchase_Order.Delivery_City = reader.IsDBNull(8) ? "" : reader.GetString(5);
                        _Purchase_Order.Delivery_State = reader.IsDBNull(8) ? "" : reader.GetString(6);
                        _Purchase_Order.Delivery_Country = reader.IsDBNull(8) ? "" : reader.GetString(7);
                        _Purchase_Order.Delivery_Zip = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        _Purchase_Order.Is_Active = reader.GetBoolean(9);
                        _vendor.Vendor_ID = reader.GetInt32(10);
                        _vendor.Vendor_Name = reader.GetString(11);
                        _vendor.Vendor_Contact_Given_Name = reader.GetString(12);
                        _vendor.Vendor_Contact_Family_Name = reader.GetString(13);
                        _vendor.Vendor_Contact_Phone_Number = reader.GetString(14);
                        _vendor.Vendor_Contact_Email = reader.GetString(15);
                        _vendor.Vendor_Phone_Number = reader.GetString(16);
                        _vendor.Vendor_Address = reader.GetString(17);
                        _vendor.Vendor_Address2 = reader.GetString(18);
                        _vendor.Vendor_City = reader.GetString(19);
                        _vendor.Vendor_State = reader.GetString(20);
                        _vendor.Vendor_Country = reader.GetString(21);
                        _vendor.Vendor_Zip = reader.GetString(22);
                        _vendor.Preferred = reader.GetBoolean(23);
                        _vendor.Is_Active = reader.GetBoolean(24);
                        _Purchase_Order.vendor = _vendor;
                        output.Add(_Purchase_Order);
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
        ///     Creates the purchase order
        /// </summary>
        /// <param cref="Purchase_OrderVM" name="purchaseOrder">
        ///    The Purchase order to add to the database
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the purchase order
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        public int InsertPurchaseOrder(Purchase_Order _purchase_order)
        {
            int InsertID = 0;
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_purchase_order";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command

            cmd.Parameters.Add("@Vendor_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Purchase_Order_Date", SqlDbType.DateTime);
            cmd.Parameters.Add("@Delivery_Address", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Delivery_Address2", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Delivery_City", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Delivery_State", SqlDbType.NVarChar, 2);
            cmd.Parameters.Add("@Delivery_Country", SqlDbType.NVarChar, 3);
            cmd.Parameters.Add("@Delivery_Zip", SqlDbType.NVarChar, 9);


            //We need to set the parameter values

            cmd.Parameters["@Vendor_ID"].Value = _purchase_order.Vendor_ID;
            cmd.Parameters["@Purchase_Order_Date"].Value = _purchase_order.Purchase_Order_Date;
            cmd.Parameters["@Delivery_Address"].Value = _purchase_order.Delivery_Address;
            cmd.Parameters["@Delivery_Address2"].Value = _purchase_order.Delivery_Address2;
            cmd.Parameters["@Delivery_City"].Value = _purchase_order.Delivery_City;
            cmd.Parameters["@Delivery_State"].Value = _purchase_order.Delivery_State;
            cmd.Parameters["@Delivery_Country"].Value = _purchase_order.Delivery_Country;
            cmd.Parameters["@Delivery_Zip"].Value = _purchase_order.Delivery_Zip;

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                InsertID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return InsertID;
        }
        /// <summary>
        ///     Inserts a purchase order line item
        /// </summary>
        /// <param cref="POLineItem" name="lineItem">
        ///    The line item that will be added to the order
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="int">int</see>: The line number of the item inserted
        /// </returns>
        /// 
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-03-18
        /// </remarks>
        public int InsertPOLineItem(POLineItem _purchase_order_line_item)
        {
            int rows = 0;
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_purchase_order_line_item";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Purchase_Order_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Parts_Inventory_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Line_Number", SqlDbType.Int);
            cmd.Parameters.Add("@Line_Item_Name", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@Line_Item_Qty", SqlDbType.Int);
            cmd.Parameters.Add("@Line_Item_Price", SqlDbType.Int);
            cmd.Parameters.Add("@Line_Item_Description", SqlDbType.NVarChar, 100);


            //We need to set the parameter values
            cmd.Parameters["@Purchase_Order_ID"].Value = _purchase_order_line_item.PurchaseOrderID;
            cmd.Parameters["@Parts_Inventory_ID"].Value = _purchase_order_line_item.PartsInventoryID;
            cmd.Parameters["@Line_Number"].Value = _purchase_order_line_item.LineNumber;
            cmd.Parameters["@Line_Item_Name"].Value = _purchase_order_line_item.LineItemName;
            cmd.Parameters["@Line_Item_Qty"].Value = _purchase_order_line_item.Quantity;
            cmd.Parameters["@Line_Item_Price"].Value = _purchase_order_line_item.Price;
            cmd.Parameters["@Line_Item_Description"].Value = _purchase_order_line_item.LineItemDescription;

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


    }
}
