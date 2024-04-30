using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;

namespace DataAccessLayer
{
    public class ActiveRouteAccessor : IActiveRouteAccessor
    {
        public int AddActiveRoute(ActiveRoute route)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_route_fulfillment";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Assignment_ID", route.AssignmentID);
            cmd.Parameters.AddWithValue("@Driver_ID", route.DriverID);
            cmd.Parameters.AddWithValue("@VIN", route.VIN);
            cmd.Parameters.AddWithValue("@Start_Time", route.StartTime);

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

        public int EndActiveRoute(ActiveRoute route)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_update_route_fulfillment_end_time";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Assignment_ID", route.AssignmentID);
            cmd.Parameters.AddWithValue("@Driver_ID", route.DriverID);
            cmd.Parameters.AddWithValue("@VIN", route.VIN);
            cmd.Parameters.AddWithValue("@End_Time", route.EndTime);

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
    }
}
