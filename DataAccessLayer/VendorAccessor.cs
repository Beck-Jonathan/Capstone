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
{ /// <summary>
  /// AUTHOR: Jonathan Beck
  /// CREATED: 2024-03-03
  ///    Data Access class for vendors
  /// </summary>
  /// <remarks>
    public class VendorAccessor : IVendorAccessor
    {
        /// <summary>
        ///     Returns all Vendor objects
        /// </summary>
        /// 
        /// 
        /// <returns>
        ///    <see cref="List{Vendor}">List<Vendor</see>: A list of vendor objects.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retreiving the vendors
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-10
        /// </remarks>
        public List<VendorVM> selectAllVendors()
        {
            List<VendorVM> output = new List<VendorVM>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_active_vendors";
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
                        var _Vendor = new VendorVM();
                        _Vendor.Vendor_ID = reader.GetInt32(0);
                        _Vendor.Vendor_Name = reader.GetString(1);
                        _Vendor.Vendor_Contact_Given_Name = reader.GetString(2);
                        _Vendor.Vendor_Contact_Family_Name = reader.GetString(3);
                        _Vendor.Vendor_Contact_Phone_Number = reader.GetString(4);
                        _Vendor.Vendor_Contact_Email = reader.GetString(5);
                        _Vendor.Vendor_Phone_Number = reader.GetString(6);
                        _Vendor.Vendor_Address = reader.GetString(7);
                        _Vendor.Vendor_Address2 = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        _Vendor.Vendor_City = reader.GetString(9);
                        _Vendor.Vendor_State = reader.GetString(10);
                        _Vendor.Vendor_Country = reader.GetString(11);
                        _Vendor.Vendor_Zip = reader.GetString(12);
                        _Vendor.Preferred = reader.GetBoolean(13);

                        output.Add(_Vendor);
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
        ///     Get vendor detail from the database
        /// </summary>
        /// <param name="VendorID">
        ///    The vendorid  to be looked up.
        /// </param>
        /// <returns>
        ///    <see cref="Vendor">Vehicle</see>: Vendor data object.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retrieving the vehicle.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-10
        /// </remarks>
        public VendorVM selectVendorByVendorID(int Vendor_ID)
        {
            VendorVM output = new VendorVM();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_vendor_by_id";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@Vendor_ID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@Vendor_ID"].Value = Vendor_ID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.Vendor_ID = reader.GetInt32(0);
                        output.Vendor_Name = reader.GetString(1);
                        output.Vendor_Contact_Given_Name = reader.GetString(2);
                        output.Vendor_Contact_Family_Name = reader.GetString(3);
                        output.Vendor_Contact_Phone_Number = reader.GetString(4);
                        output.Vendor_Contact_Email = reader.GetString(5);
                        output.Vendor_Phone_Number = reader.GetString(6);
                        output.Vendor_Address = reader.GetString(7);
                        output.Vendor_Address2 = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        output.Vendor_City = reader.GetString(9);
                        output.Vendor_State = reader.GetString(10);
                        output.Vendor_Country = reader.GetString(11);
                        output.Vendor_Zip = reader.GetString(12);
                        output.Preferred = reader.GetBoolean(13);


                    }
                    else
                    {
                        throw new ArgumentException("Vendor not found");
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
