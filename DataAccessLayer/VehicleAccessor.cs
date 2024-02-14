using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Accessor to retrieve vehicle information from the database.
    /// </summary>

    public class VehicleAccessor : IVehicleAccessor
    {
        
        public int AddVehicle(Vehicle vehicle)
        {
            /// <summary>
            ///     Add vehicle to the database.
            /// </summary>
            /// <param name="vehicle">
            ///    The vehicle information to be added as a Vehicle object.
            /// </param>
            /// <returns>
            ///    <see cref="int">int</see>: The row count, 1 added, 0 error adding.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="Vehicle">Vehicle</see> vehicle: The vehicle information to be added.
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-01
            /// </remarks>
            int rows = 0;
            
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_add_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Max_Passengers", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);

            cmd.Parameters["@VIN"].Value = vehicle.VIN;
            cmd.Parameters["@Vehicle_Number"].Value = vehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = vehicle.VehicleMileage;
            cmd.Parameters["@Vehicle_License_plate"].Value = vehicle.VehicleLicensePlate;
            cmd.Parameters["@Vehicle_Type"].Value = vehicle.VehicleType;
            cmd.Parameters["@Date_Entered"].Value = DateTime.Now;
            cmd.Parameters["@Max_Passengers"].Value = vehicle.MaxPassengers;
            cmd.Parameters["@Description"].Value = vehicle.VehicleDescription;

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

        public List<string> SelectVehicleMakes()
        {
            /// <summary>
            ///     Get a list of vehicle makes from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle makes.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            List<string> makes = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_vehicle_makes";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    makes = new List<string>();
                    while (reader.Read())
                    {
                        makes.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No vehicle makes found.");
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

            return makes;

        }
    

        public List<string> SelectVehicleModels()
        {
            /// <summary>
            ///     Get a list of vehicle models from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle models.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            List<string> models = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_vehicle_models";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    models = new List<string>();
                    while (reader.Read())
                    {
                        models.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No vehicle models found.");
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

            return models;

        }
    

        public List<string> SelectVehicleTypes()
        {
            /// <summary>
            ///     Get a list of vehicle types from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle types.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            List<string> types = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_vehicle_types";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    types = new List<string>();
                    while (reader.Read())
                    {
                        types.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No vehicle types found.");
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

            return types;

        }
    }
}
