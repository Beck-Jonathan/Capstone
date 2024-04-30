using DataAccessInterfaces;
using DataObjects;
using DataObjects.RouteObjects;
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
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-26
    /// Handles the Database Logic for the Route Stop table.
    /// </summary>
    public class RouteStopAccessor : IRouteStopAccessor
    {
        /// <summary>
        /// AUTHOR: Nathan Toothaker <br />
        /// DATE: 2024-04-23<br /> <br />
        /// Deletes a RouteStop entry from the database. <br />
        /// Doesn't remove the Route or Stop data, just the data about the relationship between the two. <br />
        /// Throws an exception when the database connection fails.
        /// </summary>
        /// <param name="routeStopVM">The routeStop data to be removed.</param>
        /// <returns><see cref="int">int</see>: the number of rows deleted (should be 1 if delete succeeded.)</returns>

        public int DeleteRouteStop(RouteStopVM routeStopVM)
        {
            int rowCount = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_route_stop", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_route_stop_id", routeStopVM.RouteStopId);
            cmd.Parameters.AddWithValue("@p_Route_Id", routeStopVM.RouteId);
            cmd.Parameters.AddWithValue("@p_Stop_Id", routeStopVM.StopId);
            cmd.Parameters.AddWithValue("@p_ordinal", routeStopVM.StopNumber);
            cmd.Parameters.AddWithValue("@p_Start_Offset", routeStopVM.OffsetFromRouteStart);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return rowCount;
        }

        /// <summary>
        /// AUTHOR: Nathan Toothaker <br />
        /// DATE: 2024-04-23<br /> <br />
        /// Inserts a RouteStop entry into the database. <br />
        /// Throws an exception when the database connection fails, or if a key is violated.
        /// </summary>
        /// <param name="routeStopVM">The routeStop data to be removed.</param>
        /// <returns><see cref="int">int</see>: the ID of the newly inserted RouteStop record.</returns>
        public int InsertRouteStop(RouteStopVM routeStopVM)
        {

            
            int routeStopId = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_insert_route_stop", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_Route_Id", routeStopVM.RouteId);
            cmd.Parameters.AddWithValue("@p_Stop_Id", routeStopVM.StopId);
            cmd.Parameters.AddWithValue("@p_Route_Stop_Number", routeStopVM.StopNumber);
            cmd.Parameters.AddWithValue("@p_Start_Offset", routeStopVM.OffsetFromRouteStart);

            try
            {
                conn.Open();
                routeStopId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return routeStopId;
            
        }

        /// <summary>
        /// AUTHOR: Nathan Toothaker <br />
        /// DATE: 2024-04-23<br /> <br />
        /// Retrieves all RouteStop entries from the database for a given Route ID. <br />
        /// Throws an exception when the database connection fails.
        /// </summary>
        /// <param name="routeId">The route ID.</param>
        /// <returns><see cref="IEnumerable">IEnumerable</see>: the RouteStop records associated with the route.</returns>

        public IEnumerable<RouteStopVM> selectRouteStopByRouteId(int routeId)
        {
            List<RouteStopVM> routeStops = new List<RouteStopVM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_route_stops_by_route_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Route_Id", routeId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetInt32(0);
                        reader.GetInt32(1);
                        reader.GetInt32(2);
                        reader.GetTimeSpan(3);
                        reader.GetBoolean(4);
                        reader.GetString(5);
                        reader.GetString(6);
                        reader.GetDecimal(7);
                        reader.GetDecimal(8);
                        routeStops.Add(new RouteStopVM()
                        {
                            RouteStopId = reader.GetInt32(9),
                            RouteId = reader.GetInt32(0),
                            StopId = reader.GetInt32(1),
                            StopNumber = reader.GetInt32(2),
                            OffsetFromRouteStart = reader.GetTimeSpan(3),
                            IsActive = reader.GetBoolean(4),
                            stop = new Stop()
                            {
                                StopId = reader.GetInt32(1),
                                StreetAddress = reader.GetString(5),
                                ZIPCode = reader.GetString(6),
                                Latitude = reader.GetDecimal(7),
                                Longitude = reader.GetDecimal(8)
                            }
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return routeStops;
        }

        /// <summary>
        /// AUTHOR: Nathan Toothaker <br />
        /// DATE: 2024-04-23<br /> <br />
        /// Updates only the ordinal of a routestop relationship.<br />
        /// Throws an exception when the database connection fails.
        /// </summary>
        /// <param name="routeStopVM">The routeStop with the updated ordinal.</param>
        /// <returns><see cref="IEnumerable">int</see>: The number of rows updated.</returns>
        public int UpdateOrdinal(RouteStopVM routeStopVM)
        {
            int rowCount = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_update_ordinal", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_route_stop_id", routeStopVM.RouteStopId);
            cmd.Parameters.AddWithValue("@p_Route_Id", routeStopVM.RouteId);
            cmd.Parameters.AddWithValue("@p_Stop_Id", routeStopVM.StopId);
            cmd.Parameters.AddWithValue("@p_new_ordinal", routeStopVM.StopNumber);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return rowCount;
        }

        public int UpdateRouteStop(RouteStopVM oldRouteStopVM, RouteStopVM newRouteStopVM)
        {
            throw new NotImplementedException();
        }
    }
}
