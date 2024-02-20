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
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Accessor to retrieve vehicle information from the database.
    /// </summary>
    ///     
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///     Update comments go here, include method or methods were changed or added 
    ///     (no other details necessary).
    ///     A new remark should be added for each update.
    /// </remarks>

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
            cmd.Parameters.Add("@Model_Lookup_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Rental", SqlDbType.Bit);

            cmd.Parameters["@VIN"].Value = vehicle.VIN;
            cmd.Parameters["@Vehicle_Number"].Value = vehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = vehicle.VehicleMileage;
            cmd.Parameters["@Model_Lookup_ID"].Value = vehicle.ModelLookupID;
            cmd.Parameters["@Vehicle_License_plate"].Value = vehicle.VehicleLicensePlate;
            cmd.Parameters["@Vehicle_Type"].Value = vehicle.VehicleType;
            cmd.Parameters["@Date_Entered"].Value = vehicle.DateEntered.Date;
            cmd.Parameters["@Description"].Value = vehicle.VehicleDescription;
            cmd.Parameters["@Rental"].Value = vehicle.Rental;

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

        public int AddModelLookup(Vehicle vehicle)
        {
            /// <summary>
            ///     Add model lookup to the database.
            /// </summary>
            /// <param name="vehicle">
            ///    The vehicle information to be added as a model lookup object.
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
            ///    CREATED: 2024-02-12
            /// </remarks>

            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_add_model_lookup";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Vehicle_Make", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Vehicle_Model", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Max_Passengers", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_Year", SqlDbType.Int);

            cmd.Parameters["@Vehicle_Make"].Value = vehicle.VehicleMake;
            cmd.Parameters["@Vehicle_Model"].Value = vehicle.VehicleModel;
            cmd.Parameters["@Max_Passengers"].Value = vehicle.MaxPassengers;
            cmd.Parameters["@Vehicle_Year"].Value = vehicle.VehicleYear;

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

        public List<Vehicle> SelectVehicleForLookupList()
        {
            /// <summary>
            ///     Accessor that allows us to pull the specific info for the Vehicle Lookup List
            /// </summary>
            /// <returns>
            ///    <see cref="List{Vehicle}">Vehicle</see> List of Vehicle objects
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            /// <br />
            ///    <see cref="ApplicationException">Exception</see>: Thrown when error encountered
            /// <br /><br />
            ///    CONTRIBUTOR: Everett DeVaux
            /// <br />
            ///    CREATED: 2024-02-10
            ///    Initial Creation
            /// <br />
            /// <br />
            ///    UPDATER: Chris Baenziger
            ///    UPDATED: 2024-02-17
            ///    Moved method comment inside of method
            /// </remarks>
            List<Vehicle> output = new List<Vehicle>();
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_all_vehicles_for_vehicle_lookup_list";
            var cmd = new SqlCommand(commandText, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        output.Add(new Vehicle()
                        {
                            VehicleMake = reader.GetString(0),
                            VehicleNumber = reader.GetString(1),
                            VehicleModel = reader.GetString(2),
                            MaxPassengers = reader.GetInt32(3),
                            VehicleMileage = reader.GetInt32(4),
                            VehicleDescription = reader.GetString(5),
                        });
                    }
                }
                else
                {
                    throw new ApplicationException("Vehicle not found.");
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
            return output;
        }
        
        public Vehicle SelectVehicleByVehicleNumber(string vehicleNumber)
        {
            /// <summary>
            ///     Get vehicle detail information from the database.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be looked up.
            /// </param>
            /// <returns>
            ///    <see cref="Vehicle">Vehicle</see>: Vehicle information.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retrieving the vehicle.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-10
            /// </remarks>
            Vehicle vehicle = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_vehicle_by_vehicle_number";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);

            cmd.Parameters["@Vehicle_Number"].Value = vehicleNumber;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    vehicle = new Vehicle();

                    vehicle.VehicleNumber = reader.GetString(0);
                    vehicle.VIN = reader.GetString(1);
                    vehicle.ModelLookupID = reader.GetInt32(2);
                    vehicle.VehicleMake = reader.GetString(3);
                    vehicle.VehicleModel = reader.GetString(4);
                    vehicle.VehicleYear = int.Parse(reader.GetString(5));
                    vehicle.VehicleMileage = reader.GetInt32(6);
                    vehicle.VehicleLicensePlate = reader.GetString(7);
                    vehicle.VehicleDescription = reader.GetString(8);
                    vehicle.DateEntered = reader.GetDateTime(9);
                    vehicle.MaxPassengers = reader.GetInt32(10);
                    vehicle.VehicleType = reader.GetString(11);
                    vehicle.Rental = reader.GetBoolean(12);

                    // [Is_Active](13)
                }
                else
                {
                    throw new ArgumentException("No matching vehicle found.");
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
            return vehicle;
        }

        public int UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            /// <summary>
            ///     Verify a data hasn't change and update the vehicle in the database.
            /// </summary>
            /// <param name="OldVehicle">
            ///    The vehicle information to be used to verify database data hasn't changed as a Vehicle object.
            /// </param>
            /// <param name="NewVehicle">
            ///    The vehicle information to be updated in the database as a Vehicle object.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: bool if the vehicle was updated.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-10
            /// </remarks>
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_update_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // primary key
            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            // old data parameters
            cmd.Parameters.Add("@OldVehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@OldVehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@OldModel_Lookup_ID", SqlDbType.Int);
            cmd.Parameters.Add("@OldVehicle_License_Plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@OldDate_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@OldVehicle_Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRental", SqlDbType.Bit);
            cmd.Parameters.Add("@OldVehicle_Year", SqlDbType.Int);
            cmd.Parameters.Add("@OldMax_Passengers", SqlDbType.Int);
            cmd.Parameters.Add("@OldVehicle_Make", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldVehicle_Model", SqlDbType.NVarChar, 255);
            // new data paremeters
            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@Model_Lookup_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_Plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Vehicle_Type", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rental", SqlDbType.Bit);
            cmd.Parameters.Add("@Vehicle_Year", SqlDbType.Int);
            cmd.Parameters.Add("@Max_Passengers", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_Make", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Vehicle_Model", SqlDbType.NVarChar, 255);

            // primary key
            cmd.Parameters["@VIN"].Value = oldVehicle.VIN;
            // old data values
            cmd.Parameters["@OldVehicle_Number"].Value = oldVehicle.VehicleNumber;
            cmd.Parameters["@OldVehicle_Mileage"].Value = oldVehicle.VehicleMileage;
            cmd.Parameters["@OldModel_Lookup_ID"].Value = oldVehicle.ModelLookupID;
            cmd.Parameters["@OldVehicle_License_Plate"].Value = oldVehicle.VehicleLicensePlate;
            cmd.Parameters["@OldDescription"].Value = oldVehicle.VehicleDescription;
            cmd.Parameters["@OldDate_Entered"].Value = oldVehicle.DateEntered.Date;
            cmd.Parameters["@OldVehicle_Type"].Value = oldVehicle.VehicleType;
            cmd.Parameters["@OldRental"].Value = oldVehicle.Rental;
            cmd.Parameters["@OldVehicle_Year"].Value = oldVehicle.VehicleYear;
            cmd.Parameters["@OldMax_Passengers"].Value = oldVehicle.MaxPassengers;
            cmd.Parameters["@OldVehicle_Make"].Value = oldVehicle.VehicleMake;
            cmd.Parameters["@OldVehicle_Model"].Value = oldVehicle.VehicleModel;
            // new data values
            cmd.Parameters["@Vehicle_Number"].Value = newVehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = newVehicle.VehicleMileage;
            cmd.Parameters["@Model_Lookup_ID"].Value = newVehicle.ModelLookupID;
            cmd.Parameters["@Vehicle_License_Plate"].Value = newVehicle.VehicleLicensePlate;
            cmd.Parameters["@Description"].Value = newVehicle.VehicleDescription;
            cmd.Parameters["@Date_Entered"].Value = newVehicle.DateEntered.Date;
            cmd.Parameters["@Vehicle_Type"].Value = newVehicle.VehicleType;
            cmd.Parameters["@Rental"].Value = newVehicle.Rental;
            cmd.Parameters["@Vehicle_Year"].Value = newVehicle.VehicleYear;
            cmd.Parameters["@Max_Passengers"].Value = newVehicle.MaxPassengers;
            cmd.Parameters["@Vehicle_Make"].Value = newVehicle.VehicleMake;
            cmd.Parameters["@Vehicle_Model"].Value = newVehicle.VehicleModel;

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


        public int SelectModelLookupID(Vehicle vehicle)
        {
            /// <summary>
            ///     Select the model lookup id matching the vehicle to be added
            /// </summary>
            /// <param name="vehicle">
            ///    The vehicle information provide the make/model/year
            /// </param>
            /// <returns>
            ///    <see cref="int">int</see>: model lookup id matching the make/model/year
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-10
            /// </remarks>
            int modelID = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_lookup_model_lookup_id_from_make_model_year";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Max_Passengers", SqlDbType.Int);
            cmd.Parameters.Add("@Year", SqlDbType.Int);
            cmd.Parameters.Add("@Make", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 255);

            // values
            cmd.Parameters["@Max_Passengers"].Value = vehicle.MaxPassengers;
            cmd.Parameters["@Year"].Value = vehicle.VehicleYear;
            cmd.Parameters["@Make"].Value = vehicle.VehicleMake;
            cmd.Parameters["@Model"].Value = vehicle.VehicleModel;

            try
            {
                conn.Open();
                modelID = Convert.ToInt32(cmd.ExecuteScalar());
                //var Reader = cmd.ExecuteReader();

                //while(Reader.Read())
                //{
                //    modelID = Reader.GetInt32(0);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return modelID;
        }

    }

}
