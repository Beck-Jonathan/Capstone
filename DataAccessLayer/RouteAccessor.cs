using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.HelperObjects;

namespace DataAccessLayer
{
    public class RouteAccessor : IRouteAccessor
    {
        public int ActivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int DeactivateRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public int InsertRoute(RouteVM route)
        {
            throw new NotImplementedException();
        }

        public RouteVM selectRouteById(int routeId)
        {
            throw new NotImplementedException();
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
