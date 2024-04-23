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
        public int ActivateRouteStop(RouteStopVM routeStopVM)
        {
            throw new NotImplementedException();
        }

        public int DeactivateRouteStop(RouteStopVM routeStopVM)
        {
            throw new NotImplementedException();
        }

        public int InsertRouteStop(RouteStopVM routeStopVM)
        {
            throw new NotImplementedException();
        }


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

        public int UpdateRouteStop(RouteStopVM oldRouteStopVM, RouteStopVM newRouteStopVM)
        {
            throw new NotImplementedException();
        }
    }
}
