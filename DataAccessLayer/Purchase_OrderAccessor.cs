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


    }
}
