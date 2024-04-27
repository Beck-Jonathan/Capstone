using DataAccessInterfaces;
using DataAccessLayer.Helpers;
using DataObjects.Assignment;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        ///     Add Route assignment to database
        /// </summary>
        /// <param name="driverID">
        ///    The ID of the driver to be assigned to the route assignment
        /// </param>
        ///  <param name="vin">
        ///    VIN of vehicle to be added to route assignment
        /// </param>
        /// <param name="routeID">
        ///    ID of route to be added to route assignment
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <returns>
        ///    int, number of rows returned by database call
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">SystemException</see>: Thrown when no records returned.
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public int AddRouteAssignment(int driverID, string vin, int routeID, DateTime start, DateTime end)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_create_route_assignment";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Driver_ID", SqlDbType.Int).Value = driverID;
            cmd.Parameters.Add("@p_VIN", SqlDbType.NVarChar, 17).Value = vin;
            cmd.Parameters.Add("@p_Route_ID", SqlDbType.Int).Value = routeID;
            cmd.Parameters.Add("@p_Date_Assignment_Started", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_Date_Assignment_Ended", SqlDbType.DateTime).Value = end;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ArgumentException("Vehicle not added");
                }
            }
            catch (Exception ex)
            {
                throw new SystemException("Database error", ex);
            }
            return rows;
        }

        /// <summary>
        ///     Add Vehicle and Driver unavailability records to database
        /// </summary>
        ///  <param name="vin">
        ///    VIN of vehicle to be added to route assignment
        /// </param>
        /// <param name="driverID">
        ///    The ID of the driver to be assigned to the route assignment
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="reason">
        ///    Explaination of unavailability being placed
        /// </param>
        /// <returns>
        ///    int Number of rows returned(should be 2)
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">SystemException</see>: No rows returned.
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public int AddVehicleAndDriverUnavailabilities(string vin, int driverID, DateTime start, DateTime end, string reason)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_create_driver_and_vehicle_unavailability";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_VIN", SqlDbType.NVarChar, 17).Value = vin;
            cmd.Parameters.Add("@p_Driver_ID", SqlDbType.Int).Value = driverID;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@p_Reason", SqlDbType.NVarChar, 1000).Value = reason;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ArgumentException("Unavailabilities not added");
                }
            }
            catch (Exception ex)
            {
                throw new SystemException("Database error", ex);
            }
            return rows;


        }

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

        /// <summary>
        ///     Get available drivers from the database that meet the criteria of the parameters
        /// </summary>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="passengerCount">
        ///    Number of anticipated passengers
        /// </param>
        /// <returns>
        ///    List of driver objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">SystemException</see>: Thrown when no records returned from call.
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<Driver> GetAvailableDrivers(DateTime start, DateTime end, int passengerCount)
        {
            List<Driver> assignments = new List<Driver>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_available_drivers_by_date_and_max_capacity";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@p_Capacity", SqlDbType.Int).Value = passengerCount;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        assignments.Add(new Driver()
                        {
                            Employee_ID = reader.GetInt32(0),
                            Given_Name = reader.GetString(1),
                            Family_Name = reader.GetString(2),
                            Driver_License_Class_ID = reader.GetString(3),
                            Max_Passenger_Count = reader.GetInt32(4)
                        });
                    };
                }
                if (assignments.Count == 0)
                {
                    throw new ArgumentException("No records found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database Error", ex);
            }

            return assignments;
        }

        /// <summary>
        ///     Get available drivers by date
        /// </summary>
        /// <param name="start">
        ///    DateTime start date
        /// </param>
        /// <param name="end">
        ///   DateTime end date
        /// </param>
        /// <returns>
        ///    List<Driver> List of driver objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-25
        /// </remarks>
        public List<Driver> GetAvailableDriversByDate(DateTime start, DateTime end)
        {
            List<Driver> assignments = new List<Driver>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_available_drivers_by_date";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        assignments.Add(new Driver()
                        {
                            Employee_ID = reader.GetInt32(0),
                            Given_Name = reader.GetString(1),
                            Family_Name = reader.GetString(2),
                            Driver_License_Class_ID = reader.GetString(3),
                            Max_Passenger_Count = reader.GetInt32(4)
                        });
                    };
                }
                if (assignments.Count == 0)
                {
                    throw new ArgumentException("No records found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database Error", ex);
            }

            return assignments;
        }

        /// <summary>
        ///     Get available vehicles from the database that meet the criteria of the parameters
        /// </summary>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="passengerCount">
        ///    Number of anticipated passengers
        /// </param>
        /// <returns>
        ///    List of vehicle assignment objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">SystemException</see>: Thrown when no records returned from call.
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<VehicleAssignment> GetAvailableVehicles(DateTime start, DateTime end, int passengerCount)
        {
            List<VehicleAssignment> assignments = new List<VehicleAssignment>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_available_vehicles_by_date_and_max_capacity";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@p_Capacity", SqlDbType.Int).Value = passengerCount;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        assignments.Add(new VehicleAssignment()
                        {
                            VIN = reader.GetString(0),
                            Name = reader.GetString(1),
                            Make = reader.GetString(2),
                            Max_Passengers = reader.GetInt32(3)
                        });
                    };
                }
                if (assignments.Count == 0)
                {
                    throw new ArgumentException("No records found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database Error", ex);
            }

            return assignments;
        }

        /// <summary>
        ///     Get available vehicles by date
        /// </summary>
        /// <param name="start">
        ///    DateTime start date
        /// </param>
        /// <param name="end">
        ///   DateTime end date
        /// </param>
        /// <returns>
        ///    List<VehicleAssignment> List of vehicle assignments 
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-25
        /// </remarks>
        public List<VehicleAssignment> GetAvailableVehiclesByDate(DateTime start, DateTime end)
        {
            List<VehicleAssignment> assignments = new List<VehicleAssignment>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_available_vehicles_by_date";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        assignments.Add(new VehicleAssignment()
                        {
                            VIN = reader.GetString(0),
                            Name = reader.GetString(1),
                            Make = reader.GetString(2),
                            Max_Passengers = reader.GetInt32(3)
                        });
                    };
                }
                if (assignments.Count == 0)
                {
                    throw new ArgumentException("No records found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database Error", ex);
            }

            return assignments;
        }

        /// <summary>
        ///     Get the driver for a route assignment
        /// </summary>
        /// <param name="routeID">
        ///    ID of the route assignment
        /// </param>
        /// <returns>
        ///    Driver object of found driver
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-25
        /// </remarks>
        public Driver GetRouteAssignmentDriverByRouteAssignmentID(int routeID)
        {
            Driver driver = null;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_route_assignment_driver_by_route_assignment_id";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Route_Assignment_ID", SqlDbType.Int).Value = routeID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        driver = new Driver()
                        {
                            Employee_ID = reader.GetInt32(0),
                            Given_Name = reader.GetString(1),
                            Family_Name = reader.GetString(2),
                            Driver_License_Class_ID = reader.GetString(3),
                            Max_Passenger_Count = reader.GetInt32(4)
                        };

                    }
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Error accessing data", ex);
            }
            finally
            {
                conn.Close();
            }

            return driver;
        }

        /// <summary>
        ///     Get route assignments from the database that meet the criteria of the parameters
        /// </summary>
        /// <param name="routeID">
        ///    ID of the route
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <returns>
        ///    List of vehicle assignment objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<Route_Assignment> GetRouteAssignmentsByRouteIDAndDate(int routeID, DateTime start, DateTime end)
        {
            List<Route_Assignment> assignments = new List<Route_Assignment>();
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_get_route_assignments_by_route_id_and_dates";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Start_Date", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@p_End_Date", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@p_Route_ID", SqlDbType.Int).Value = routeID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        assignments.Add(new Route_Assignment()
                        {
                            Assignment_ID = reader.GetInt32(0),
                            DriverID = reader.GetInt32(1),
                            Route_ID = reader.GetInt32(2),
                            VIN_Number = reader.GetString(3),
                            Date_Assignment_Started = reader.GetDateTime(4),
                            Date_Assignment_Ended = reader.GetDateTimeNullable(5),
                            IsActive = reader.GetBoolean(6)

                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Error accessing data", ex);
            }
            finally
            {
                conn.Close();
            }

            return assignments;
        }

        /// <summary>
        ///     Update the driver of a Route_Assignment
        /// </summary>
        /// <param name="routeAssignmentID">
        ///    ID of the route assignment
        /// </param>
        /// <param name="driverID">
        ///    ID of the driver
        /// </param>
        /// <returns>
        ///    Int number of rows affected
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-25
        /// </remarks>
        public int UpdateRouteAssignmentDriver(int routeAssignmentID, int driverID)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_update_route_assignment_driver";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Assignment_ID", SqlDbType.Int).Value = routeAssignmentID;
            cmd.Parameters.Add("@p_Driver_ID", SqlDbType.Int).Value = driverID;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ArgumentException("Route assignment not updated");
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
            return rows;
        }

        /// <summary>
        ///     Update the vehicle of a Route_Assignment
        /// </summary>
        /// <param name="routeAssignmentID">
        ///    ID of the route assignment
        /// </param>
        /// <param name="vin">
        ///    VIN of the vehicle
        /// </param>
        /// <returns>
        ///    Int number of rows affected
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-25
        /// </remarks>
        public int UpdateRouteAssignmentVehicle(int routeAssignmentID, string vin)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_update_route_assignment_vehicle";

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@p_Assignment_ID", SqlDbType.Int).Value = routeAssignmentID;
            cmd.Parameters.Add("@p_VIN", SqlDbType.NVarChar, 17).Value = vin;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ArgumentException("Route assignment not updated");
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
            return rows;
        }
    }
}
