using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Stop accessor
    /// </summary>
    public class StopAccessor : IStopAccessor
    {
        /// <summary>
        ///     Add a stop to database.
        /// </summary>
        /// <param name="Stop">
        ///    The stop information to be added.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The stop number that was added.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="Stop">Stop</see> The stop information to be added.
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-03-26
        /// </remarks>
        public int InsertStop(Stop stop)
        {
            int stopID = 0;
            
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_stop";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Street_Address", stop.StreetAddress);
            cmd.Parameters.AddWithValue("@Zip_Code", stop.ZIPCode);
            cmd.Parameters.AddWithValue("@Latitude", stop.Latitude);
            cmd.Parameters.AddWithValue("@Longitude", stop.Longitude);

            try
            {
                conn.Open();
                stopID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return stopID;
        }

        public Stop SelectStopByID(int stopID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Select all stops from the database.
        /// </summary>
        /// <returns>
        ///    <see cref="List</Stop>">List</see>: The list of all stops.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem retrieving from the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-03-26
        /// </remarks>
        public List<Stop> SelectStops()
        {
            List<Stop> stops = null;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_stops";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    stops = new List<Stop>();
                    while (reader.Read())
                    {
                        stops.Add(new Stop()
                        {
                            StopId = reader.GetInt32(0),
                            StreetAddress = reader.GetString(1),
                            ZIPCode = reader.GetString(2),
                            Latitude = reader.GetDecimal(3),
                            Longitude = reader.GetDecimal(4),
                            IsActive = reader.GetBoolean(5)
                        });
                    }
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

            return stops;
        }
        /// <summary>
        ///     Edit a stop in the database
        /// </summary>
        /// <param name="_oldStop">
        ///    The stop to be edited.
        /// </param>
        /// <param name="_newStop">
        ///    The new info for the stop.
        /// </param>
        /// <returns>
        ///    <see cref="int">bool</see>: The number of stops updated. Should be 1 or 0.
        /// </returns>
        /// <remarks>
        ///    Parameters <br/>:
        ///    <see cref="Stop">_oldStop</see> The stop to be edited. <br/>
        ///    <see cref="Stop">_newStop</see>  The new info for the stop. <br/>
        ///    Exceptions: <br/>
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-04-02
        /// </remarks>

        public int UpdateStop(Stop _oldStop, Stop _newStop)
        {
            int rows = 0;
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_stop";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldStop_ID", SqlDbType.Int);
            cmd.Parameters.Add("@oldStreet_Address", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@newStreet_Address", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@oldZip_Code", SqlDbType.VarChar, 5);
            cmd.Parameters.Add("@newZip_Code", SqlDbType.VarChar, 5);
            cmd.Parameters.Add("@oldLatitude", SqlDbType.Decimal);
            cmd.Parameters.Add("@newLatitude", SqlDbType.Decimal);
            cmd.Parameters.Add("@oldLongitude", SqlDbType.Decimal);
            cmd.Parameters.Add("@newLongitude", SqlDbType.Decimal);
            cmd.Parameters.Add("@oldIs_Active", SqlDbType.Bit);
            cmd.Parameters.Add("@newIs_Active", SqlDbType.Bit);

            //We need to set the parameter values
            cmd.Parameters["@oldStop_ID"].Value = _oldStop.StopId;
            cmd.Parameters["@oldStreet_Address"].Value = _oldStop.StreetAddress;
            cmd.Parameters["@newStreet_Address"].Value = _newStop.StreetAddress;
            cmd.Parameters["@oldZip_Code"].Value = _oldStop.ZIPCode;
            cmd.Parameters["@newZip_Code"].Value = _newStop.ZIPCode;
            cmd.Parameters["@oldLatitude"].Value = _oldStop.Latitude;
            cmd.Parameters["@newLatitude"].Value = _newStop.Latitude;
            cmd.Parameters["@oldLongitude"].Value = _oldStop.Longitude;
            cmd.Parameters["@newLongitude"].Value = _newStop.Longitude;
            cmd.Parameters["@oldIs_Active"].Value = _oldStop.IsActive;
            cmd.Parameters["@newIs_Active"].Value = _newStop.IsActive;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    //treat failed update as exception 
                    throw new ArgumentException("invalid values, update failed");
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
            return rows;
        }

        public int UpdateStopByIDAsActive(int stopID)
        {
            throw new NotImplementedException();
        }

        public int UpdateStopByIDAsInactive(int stopID)
        {
            throw new NotImplementedException();
        }
    }
}
