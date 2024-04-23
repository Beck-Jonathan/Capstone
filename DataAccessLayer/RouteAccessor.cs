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

        public int InsertRoute(RouteVM route)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED: 2024-04-19
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public RouteVM selectRouteById(int routeId)
        {
            RouteVM route = null;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_route_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    route = new RouteVM()
                    {
                        RouteId = reader.GetInt32(0),
                        RouteName = reader.GetString(1),
                        StartTime = new Time(reader.GetDateTime(2)),
                        RepeatTime = reader.GetTimeSpan(3),
                        EndTime = new Time(reader.GetDateTime(4)),
                        DaysOfService = new ActivityWeek(reader.GetString(5).ToCharArray()),
                        IsActive = true
                    };
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
            return route;
        }

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

        public int UpdateRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            throw new NotImplementedException();
        }
    }
}
