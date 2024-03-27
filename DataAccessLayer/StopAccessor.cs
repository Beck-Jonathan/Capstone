using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
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

        public int UpdateStop(Stop oldStop, Stop newStop)
        {
            throw new NotImplementedException();
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
