using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.HelperObjects;
using System.Data;

namespace DataAccessLayer
{
    public class RouteAccessor : IRouteAccessor
    {
        /// <summary>
        /// Activate a route in the database
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be activated
        /// </param>
        /// <returns>
        ///     <see cref="int">int</see>: The row count, 1 updated, else error.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be activated
        ///     Exceptions:
        ///     <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the database
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>
        public int UpdateRouteByIDAsActive(int routeId)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_activate_route_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RouteID", SqlDbType.Int);

            cmd.Parameters["@RouteID"].Value = routeId;

            try
            {
                conn.Open();
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
        /// Deactivate a route in the database
        /// </summary>
        /// <param name="routeId">
        ///     The route ID for the route to be deactivated
        /// </param>
        /// <returns>
        ///     <see cref="int">int</see>: The row count, 1 updated, else error.
        /// </returns>
        /// <remarks>
        ///     Parameters:
        ///     <see cref="RouteID">RouteID</see>: Route ID to be deactivated
        ///     Exceptions:
        ///     <see cref="SqlException">SqlException</see>: Thrown if there is a problem accessing the database
        ///     CONTRIBUTOR: Chris Baenziger
        ///     CREATED: 2024-03-02
        /// </remarks>

        public int UpdateRouteByIDAsInactive(int routeId)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_deactivate_route_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RouteID", SqlDbType.Int);

            cmd.Parameters["@RouteID"].Value = routeId;

            try
            {
                conn.Open();
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
        ///     Inserts a route and returns the ID primary key from the databse.
        /// </summary>
        /// <param name="route">The route data to be inserted.</param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the newly inserted route.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public int InsertRoute(RouteVM route)
        {
            int routeId = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_insert_route", conn);

            cmd.CommandType= CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Route_Name", route.RouteName);
            cmd.Parameters.AddWithValue("@Route_Start_Time", route.StartTime.getStorageData());
            cmd.Parameters.AddWithValue("@Route_End_Time", route.EndTime.getStorageData());
            cmd.Parameters.AddWithValue("@Route_Cycle", route.RepeatTime);
            cmd.Parameters.AddWithValue("@Days_Of_Service", route.DaysOfService.GetStorageString());

            try
            {
                conn.Open();
                routeId = Convert.ToInt32(cmd.ExecuteScalar());
            } catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return routeId;
        }
        /// <summary>
        ///     Returns a route located by ID.
        /// </summary>
        /// <param name="routeId">The ID of the route to be located.</param>
        /// <returns>
        ///    <see cref="RouteVM">RouteVM</see>: The route with the given ID
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public RouteVM selectRouteById(int routeId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Returns the list of routes.
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{RouteVM}">IEnumerable</see>: The list of routes
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public IEnumerable<RouteVM> selectRoutes()
        {
            List<RouteVM> routes = new List<RouteVM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_route";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int RouteId = reader.GetInt32(0);
                        string RouteName = reader.GetString(1);
                        Time StartTime = new Time(reader.GetDateTime(2));
                        TimeSpan RepeatTime = reader.GetTimeSpan(3);
                        Time EndTime = new Time(reader.GetDateTime(4));
                        ActivityWeek DaysOfService = new ActivityWeek(reader.GetString(5).ToCharArray());
                        bool IsActive = reader.GetBoolean(6);
                        routes.Add(new RouteVM()
                        {
                            RouteId = RouteId,
                            RouteName = RouteName,
                            StartTime = StartTime,
                            RepeatTime = RepeatTime,
                            EndTime = EndTime,
                            DaysOfService = DaysOfService,
                            IsActive = IsActive
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally { conn.Close(); }

            return routes;
        }
        /// <summary>
        ///     Updates a route record in the database.
        /// </summary>
        /// <param name="oldRoute">The route data prior to the update.</param>
        /// <param name="newRoute">The route data after the update.</param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows updated. 0 means the update failed for some reason.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when an error is caught from the database.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public int UpdateRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            int rowCount = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_update_route", conn);

            cmd.CommandType = CommandType.StoredProcedure;


            /* @RouteID INT,
	            @Old_Route_Name NVARCHAR(255),
	            @Old_Route_Start_Time DATETIME,
	            @Old_Route_End_Time DATETIME,
	            @Old_Route_Cycle TIME(7),
	            @Old_Days_Of_Service CHAR(7),
	            @New_Route_Name NVARCHAR(255),
	            @New_Route_Start_Time DATETIME,
	            @New_Route_End_Time DATETIME,
	            @New_Route_Cycle TIME(7),
	            @New_Days_Of_Service CHAR(7) */

            cmd.Parameters.AddWithValue("@RouteID", newRoute.RouteId);
            cmd.Parameters.AddWithValue("@Old_Route_Name", oldRoute.RouteName);
            cmd.Parameters.AddWithValue("@Old_Route_Start_Time", oldRoute.StartTime.getStorageData());
            cmd.Parameters.AddWithValue("@Old_Route_End_Time", oldRoute.EndTime.getStorageData());
            cmd.Parameters.AddWithValue("@Old_Route_Cycle", oldRoute.RepeatTime);
            cmd.Parameters.AddWithValue("@Old_Days_Of_Service", oldRoute.DaysOfService.GetStorageString());
            cmd.Parameters.AddWithValue("@New_Route_Name", newRoute.RouteName);
            cmd.Parameters.AddWithValue("@New_Route_Start_Time", newRoute.StartTime.getStorageData());
            cmd.Parameters.AddWithValue("@New_Route_End_Time", newRoute.EndTime.getStorageData());
            cmd.Parameters.AddWithValue("@New_Route_Cycle", newRoute.RepeatTime);
            cmd.Parameters.AddWithValue("@New_Days_Of_Service", newRoute.DaysOfService.GetStorageString());

            try
            {
                conn.Open();
                rowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return rowCount;
        }
    }
}
