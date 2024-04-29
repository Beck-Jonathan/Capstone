using DataAccessInterfaces;
using DataAccessLayer.Helpers;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RideAccessor : IRideAccessor
    {
        public int InsertRide(Ride ride)
        {
            int rideID = 0;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_insert_ride";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Client_ID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Operation", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Pickup_Location", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Dropoff_Location", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Scheduled_Pickup_Time", System.Data.SqlDbType.DateTime);

            cmd.Parameters["@Client_ID"].Value = ride.ClientID;
            cmd.Parameters["@Operation"].Value = ride.Operation;
            cmd.Parameters["@Pickup_Location"].Value = ride.PickupLocation;
            cmd.Parameters["@Dropoff_Location"].Value = ride.DropoffLocation;
            cmd.Parameters["@Scheduled_Pickup_Time"].Value = ride.ScheduledPickupTime;

            try
            {
                conn.Open();
                rideID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rideID;
        }

        public Ride_VM SelectRideById(int rideID)
        {
            Ride_VM ride = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_select_ride_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Ride_ID", System.Data.SqlDbType.Int);

            cmd.Parameters["@Ride_Id"].Value = rideID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ride = new Ride_VM
                    {
                        RideID = reader.GetInt32(0),
                        ClientID = reader.GetInt32(1),
                        Operation = reader.GetString(2),
                        DriverID = reader.GetInt32Nullable(3),
                        VIN = reader.GetStringNullable(4),
                        PickupLocation = reader.GetString(5),
                        DropoffLocation = reader.GetString(6),
                        ScheduledPickupTime = reader.GetDateTime(7),
                        IsActive = reader.GetBoolean(8)
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

            return ride;
        }

        public IEnumerable<Ride_VM> SelectRidesByClientID(int clientID)
        {
            List<Ride_VM> rides = new List<Ride_VM>();

            var conn = DBConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_select_ride_requests_by_client_id", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Client_ID", clientID);


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ride_VM ride = new Ride_VM 
                    {
                        RideID = reader.GetInt32(0),
                        ClientID = reader.GetInt32(1),
                        Operation = reader.GetString(2),
                        DriverID = reader.GetInt32Nullable(3),
                        VIN = reader.GetStringNullable(4),
                        PickupLocation = reader.GetString(5),
                        DropoffLocation = reader.GetString(6),
                        ScheduledPickupTime = reader.GetDateTime(7),
                        IsActive = reader.GetBoolean(12)
                    };
                    rides.Add(ride);
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }

            return rides;
        }

        public int UpdateRide(Ride ride)
        {
            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_update_ride";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Ride_ID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Driver_ID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@VIN", System.Data.SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Pickup_Location", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Dropoff_Location", System.Data.SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Scheduled_Pickup_Time", System.Data.SqlDbType.DateTime);

            cmd.Parameters["@Ride_ID"].Value = ride.RideID;
            if (ride.DriverID == null)
            {
                cmd.Parameters["@Driver_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@Driver_ID"].Value = ride.DriverID;
            }
            if (ride.VIN == null)
            {
                cmd.Parameters["@VIN"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@VIN"].Value = ride.VIN;
            }
            cmd.Parameters["@Pickup_Location"].Value = ride.PickupLocation;
            cmd.Parameters["@Dropoff_Location"].Value = ride.DropoffLocation;
            cmd.Parameters["@Scheduled_Pickup_Time"].Value = ride.ScheduledPickupTime;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int UpdateRideAsActive(int id, bool active)
        {
            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_update_ride_is_active";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Ride_ID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Is_Active", System.Data.SqlDbType.Bit);

            cmd.Parameters["@Ride_Id"].Value = id;
            cmd.Parameters["@Is_Active"].Value = active;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
       }
    }
}
