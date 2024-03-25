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
    /// <inheritdoc/>

    public class VehicleAccessor : IVehicleAccessor
    {

        public int AddVehicle(Vehicle vehicle)
        {
            int rows = 0;
            
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_add_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_Model_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Type_ID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Rental", SqlDbType.Bit);

            cmd.Parameters["@VIN"].Value = vehicle.VIN;
            cmd.Parameters["@Vehicle_Number"].Value = vehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = vehicle.VehicleMileage;
            cmd.Parameters["@Vehicle_Model_ID"].Value = vehicle.VehicleModelID;
            cmd.Parameters["@Vehicle_License_plate"].Value = vehicle.VehicleLicensePlate;
            cmd.Parameters["@Vehicle_Type_ID"].Value = vehicle.VehicleType;
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
            cmd.Parameters["@Vehicle_Model_ID"].Value = vehicle.VehicleModelID;
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
                            VehicleModelID = reader.GetInt32(2),
                            MaxPassengers = reader.GetInt32(3),
                            VehicleMileage = reader.GetInt32(4),
                            VehicleDescription = reader.GetString(5),
                            VIN = reader.GetString(9),
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
                    vehicle.VehicleModelID = reader.GetInt32(2);
                    vehicle.VehicleMake = reader.GetString(3);
                    vehicle.VehicleModel = reader.GetString(4);
                    vehicle.VehicleYear = reader.GetInt32(5);
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
            cmd.Parameters.Add("@OldVehicle_Model_ID", SqlDbType.Int);
            cmd.Parameters.Add("@OldVehicle_License_Plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@OldDate_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@OldVehicle_Type_ID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRental", SqlDbType.Bit);
            cmd.Parameters.Add("@OldMax_Passengers", SqlDbType.Int);
            // new data paremeters
            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_Model_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_Plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Vehicle_Type_ID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rental", SqlDbType.Bit);
            cmd.Parameters.Add("@Max_Passengers", SqlDbType.Int);

            // primary key
            cmd.Parameters["@VIN"].Value = oldVehicle.VIN;
            // old data values
            cmd.Parameters["@OldVehicle_Number"].Value = oldVehicle.VehicleNumber;
            cmd.Parameters["@OldVehicle_Mileage"].Value = oldVehicle.VehicleMileage;
            cmd.Parameters["@OldVehicle_Model_ID"].Value = oldVehicle.VehicleModelID;
            cmd.Parameters["@OldVehicle_License_Plate"].Value = oldVehicle.VehicleLicensePlate;
            cmd.Parameters["@OldDescription"].Value = oldVehicle.VehicleDescription;
            cmd.Parameters["@OldDate_Entered"].Value = oldVehicle.DateEntered.Date;
            cmd.Parameters["@OldVehicle_Type_ID"].Value = oldVehicle.VehicleType;
            cmd.Parameters["@OldRental"].Value = oldVehicle.Rental;
            cmd.Parameters["@OldMax_Passengers"].Value = oldVehicle.MaxPassengers;
            // new data values
            cmd.Parameters["@Vehicle_Number"].Value = newVehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = newVehicle.VehicleMileage;
            cmd.Parameters["@Vehicle_Model_ID"].Value = newVehicle.VehicleModelID;
            cmd.Parameters["@Vehicle_License_Plate"].Value = newVehicle.VehicleLicensePlate;
            cmd.Parameters["@Description"].Value = newVehicle.VehicleDescription;
            cmd.Parameters["@Date_Entered"].Value = newVehicle.DateEntered.Date;
            cmd.Parameters["@Vehicle_Type_ID"].Value = newVehicle.VehicleType;
            cmd.Parameters["@Rental"].Value = newVehicle.Rental;
            cmd.Parameters["@Max_Passengers"].Value = newVehicle.MaxPassengers;

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


        public int DeactivateVehicle(Vehicle vehicle)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_deactivate_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Vehicle_Number", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_Model_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Vehicle_License_plate", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@Vehicle_Type_ID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Date_Entered", SqlDbType.Date);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Rental", SqlDbType.Bit);

            cmd.Parameters["@VIN"].Value = vehicle.VIN;
            cmd.Parameters["@Vehicle_Number"].Value = vehicle.VehicleNumber;
            cmd.Parameters["@Vehicle_Mileage"].Value = vehicle.VehicleMileage;
            cmd.Parameters["@Vehicle_Model_ID"].Value = vehicle.VehicleModelID;
            cmd.Parameters["@Vehicle_License_plate"].Value = vehicle.VehicleLicensePlate;
            cmd.Parameters["@Vehicle_Type_ID"].Value = vehicle.VehicleType;
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

    }
}
