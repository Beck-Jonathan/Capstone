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
    ///     Data access interface for accessing vehicle information from the database.
    /// </summary>
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// UPDATED: 2024-02-13
    /// <br />
    /// </remarks>
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added method for deactivate vehicle.
    /// </remarks>
    /// <remarks>
    /// UPDATER: Jacob Rohr
    /// UPDATED: 2024-04-01
    /// Removed inheritdoc and migrated Interface file comments. 
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-04-25
    /// Added add checklist 
    /// </remarks>
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
            if (vehicle.VehicleType.isNotEmptyOrNull())
            {
                cmd.Parameters["@Vehicle_Type_ID"].Value = vehicle.VehicleType;
            }
            else
            {
                cmd.Parameters["@Vehicle_Type_ID"].Value = DBNull.Value;
            }
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
        /// <summary>
        ///     Get all service orders for a specificed vehicle
        /// </summary>
        /// <param name="VIN">
        ///    The VIN to get associated service orders for..
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="ServiceOrder_VM">List:ServiceOrder_VM</see>: a list of service orders related to the vehicle
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-04-13
        /// </remarks>
        public List<ServiceOrder_VM> SelectServiceOrdersByVin(String VIN)
        {
            List<ServiceOrder_VM> output = new List<ServiceOrder_VM>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_VIN_Service_Order";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar,17);


            // values
            cmd.Parameters["@VIN"].Value = VIN;


            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        ServiceOrder_VM _Service_Order = new ServiceOrder_VM();
                        _Service_Order.Service_Order_ID = reader.GetInt32(0);
                        _Service_Order.Service_Order_Version = reader.GetInt32(1);
                        _Service_Order.VIN = reader.GetString(2);
                        _Service_Order.Service_Type_ID = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        _Service_Order.Created_By_Employee_ID = reader.GetInt32(4);
                        _Service_Order.Serviced_By_Employee_ID = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        _Service_Order.Date_Started = reader.GetDateTime(6);
                        _Service_Order.Date_Finished = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7);
                        _Service_Order.Is_Active = reader.GetBoolean(8);
                        _Service_Order.Critical_Issue = reader.GetBoolean(9);
                        output.Add(_Service_Order);
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

        /// <summary>
        ///     Retrieves a Vehicle record by the VIN from the database
        /// </summary>
        /// <returns>
        ///    A <see cref="Vehicle">Vehicle</see> object otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="SqlException">SqlException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-24
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public Vehicle SelectVehicleByVIN(string VIN)
        {
            Vehicle returnVehicle = new Vehicle();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_vehicle_by_vin";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VIN", VIN);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle()
                    {
                        VehicleNumber = reader.GetString(0),
                        VehicleMileage = reader.GetInt32(1),
                        VehicleLicensePlate = reader.GetString(2),
                        VehicleModelID = reader.GetInt32(3),
                        VehicleType = reader.GetString(4),
                        DateEntered = reader.GetDateTime(5),
                        VehicleDescription = reader.GetString(6),
                        MaintenanceNotes = reader.GetString(7),
                        Rental = reader.GetBoolean(8),
                        Is_Active = reader.GetBoolean(9)
                    };
                    returnVehicle = vehicle;
                }

                if (returnVehicle == null)
                {
                    throw new ArgumentException("No vehicle record found");
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
            return returnVehicle;
        }

        public int AddVehicleChecklist(VehicleChecklist checklist)
        {
            int checklistID = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_vehicle_checklist";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Employee_ID", checklist.EmployeeID);
            cmd.Parameters.AddWithValue("@VIN", checklist.VIN);
            cmd.Parameters.AddWithValue("@Date", checklist.ChecklistDate);
            cmd.Parameters.AddWithValue("@Clean", checklist.Clean);
            cmd.Parameters.AddWithValue("@Pedals", checklist.Pedals);
            cmd.Parameters.AddWithValue("@Dash", checklist.Dash);
            cmd.Parameters.AddWithValue("@Steering", checklist.Steering);
            cmd.Parameters.AddWithValue("@AC_Heat", checklist.AC_Heat);
            cmd.Parameters.AddWithValue("@Mirror_DS", checklist.MirrorDS);
            cmd.Parameters.AddWithValue("@Mirror_PS", checklist.MirrorPS);
            cmd.Parameters.AddWithValue("@Mirror_RV", checklist.MirrorRV);
            cmd.Parameters.AddWithValue("@Cosmetic", checklist.Cosmetic);
            cmd.Parameters.AddWithValue("@Tire_Pressure_DF", checklist.Tire_Pressure_DF);
            cmd.Parameters.AddWithValue("@Tire_Pressure_PF", checklist.Tire_Pressure_PF);
            cmd.Parameters.AddWithValue("@Tire_Pressure_DR", checklist.Tire_Pressure_DR);
            cmd.Parameters.AddWithValue("@Tire_Pressure_PR", checklist.Tire_Pressure_PR);
            cmd.Parameters.AddWithValue("@Blinker_DF", checklist.Blinker_DF);
            cmd.Parameters.AddWithValue("@Blinker_PF", checklist.Blinker_PF);
            cmd.Parameters.AddWithValue("@Blinker_DR", checklist.Blinker_DR);
            cmd.Parameters.AddWithValue("@Blinker_PR", checklist.Blinker_PR);
            cmd.Parameters.AddWithValue("@Breaklight_DR", checklist.Breaklight_DR);
            cmd.Parameters.AddWithValue("@Breaklight_PR", checklist.Breaklight_PR);
            cmd.Parameters.AddWithValue("@Headlight_DS", checklist.Headlight_Driver);
            cmd.Parameters.AddWithValue("@Headlight_PS", checklist.Headlight_Passenger);
            cmd.Parameters.AddWithValue("@Taillight_DS", checklist.TailLight_Driver);
            cmd.Parameters.AddWithValue("@Taillight_PS", checklist.TailLight_Passenger);
            cmd.Parameters.AddWithValue("@Wiper_DS", checklist.Wiper_Driver);
            cmd.Parameters.AddWithValue("@Wiper_PS", checklist.Wiper_Passenger);
            cmd.Parameters.AddWithValue("@Wiper_R", checklist.Wiper_Rear);
            cmd.Parameters.AddWithValue("@Seat_Belts", checklist.SeatBelts);
            cmd.Parameters.AddWithValue("@Fire_Extinguisher", checklist.FireExtinguisher);
            cmd.Parameters.AddWithValue("@Airbags", checklist.Airbags);
            cmd.Parameters.AddWithValue("@First_Aid", checklist.FirstAid);
            cmd.Parameters.AddWithValue("@Emergency_Kit", checklist.EmergencyKit);
            cmd.Parameters.AddWithValue("@Mileage", checklist.Mileage);
            cmd.Parameters.AddWithValue("@Fuel_Level", checklist.FuelLevel);
            cmd.Parameters.AddWithValue("@Brakes", checklist.Breaks);
            cmd.Parameters.AddWithValue("@Accelerator", checklist.Accelerator);
            cmd.Parameters.AddWithValue("@Clutch", checklist.Clutch);
            cmd.Parameters.AddWithValue("@Notes", checklist.Notes);

            try
            {
                conn.Open();
                checklistID = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return checklistID;
        }

        /// <summary>
        ///     Retrieves VIN/Vehicle number tuples to fill drop downs
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle}">Vehicle</see> List of Vin/Vehicle Number tuples for drop downs
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    
        /// <br />
        ///    CREATED: 2024-04-22
        /// <br />
        ///     Initial Creation
        /// <br />
        ///    Creator: Jonathan Beck
        /// <br />
        ///    
        /// <br />
        ///    
        /// </remarks>
        public List<Vehicle> selectVehicleTuplesForDropDown()
        {
            List<Vehicle> output = new List<Vehicle>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_vehicle_vin_and_number_for_dropdown";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Vehicle = new Vehicle();
                        _Vehicle.VIN = reader.GetString(0);
                        _Vehicle.VehicleNumber = reader.GetString(1);

                        output.Add(_Vehicle);
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
    }
}
