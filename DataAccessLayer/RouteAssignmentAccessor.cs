using DataAccessInterfaces;
using DataObjects.HelperObjects;
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
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Gets Data for Route Assignments using the database 
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public class RouteAssignmentAccessor : IRouteAssignmentAccessor
    {
        public IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID)
        {
            List<Route_Assignment_VM> route_Assignments = new List<Route_Assignment_VM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_get_assigned_routes";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DriverID", Driver_ID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        route_Assignments.Add(new Route_Assignment_VM()
                        {
                            Assignment_ID = reader.GetInt32(0),
                            Route_ID = reader.GetInt32(1),
                            routeVM = new RouteVM
                            {

                                RouteName = reader.GetString(2),
                                StartTime = new Time(reader.GetDateTime(3)),
                                EndTime = new Time(reader.GetDateTime(4)),
                            },
                            routeStopVM = new RouteStopVM
                            {
                                StopNumber = reader.GetInt32(5),
                            },
                            stop = new Stop
                            {
                                StreetAddress = reader.GetString(6),
                                ZIPCode = reader.GetString(7),
                                Latitude = reader.GetDecimal(8),
                                Longitude = reader.GetDecimal(9)
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

            return route_Assignments;
        }
    }
}
