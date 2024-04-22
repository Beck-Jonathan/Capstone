using DataAccessInterfaces;
using DataObjects;
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
    /// AUTHOR: Everett DeVaux
    /// <br />
    /// CREATED: 2024-03-24
    /// <br />
    /// 
    ///     Data access class for Driver.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 
    /// <br />
    ///     Initial creation
    ///     <br />
    /// </remarks>
    public class DispatchAccessor : IDispatchAccessor
    {
        /// <summary>
        ///     Retrieves all Drivers and their schedules from the database.
        /// </summary>
        /// <returns>
        ///    List of <see cref="List{DriverSchedule}">DriverSchedule</see> objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-24
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        public List<Dispatch> SelectDriverScheduleForList()
        {
            List<Dispatch> output = new List<Dispatch>();
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_driver_and_schedule";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(8) && !reader.IsDBNull(9))
                        {
                            Dispatch driverSchedule = new Dispatch()
                            {
                                EmployeeID = reader.GetInt32(0),
                                DriverLicenseClassID = reader.GetString(1),
                                DriverID = reader.GetInt32(2),
                                ScheduleID = reader.GetString(3),
                                WeekDays = reader.GetString(4),
                                StartTime = reader.GetDateTime(5),
                                EndTime = reader.GetDateTime(6),
                                StartDate = reader.GetDateTime(7),
                                EndDate = reader.GetDateTime(8),
                                Notes = reader.GetString(9),
                                isActive = reader.GetBoolean(10)
                            };
                            output.Add(driverSchedule);
                        }

                        else if (reader.IsDBNull(8) && !reader.IsDBNull(9))
                        {
                            Dispatch driverSchedule = new Dispatch()
                            {
                                EmployeeID = reader.GetInt32(0),
                                DriverLicenseClassID = reader.GetString(1),
                                DriverID = reader.GetInt32(2),
                                ScheduleID = reader.GetString(3),
                                WeekDays = reader.GetString(4),
                                StartTime = reader.GetDateTime(5),
                                EndTime = reader.GetDateTime(6),
                                StartDate = reader.GetDateTime(7),
                                Notes = reader.GetString(9),
                                isActive = reader.GetBoolean(10)
                            };
                            output.Add(driverSchedule);
                        }
                        else if (!reader.IsDBNull(8) && reader.IsDBNull(9))
                        {
                            Dispatch driverSchedule = new Dispatch()
                            {
                                EmployeeID = reader.GetInt32(0),
                                DriverLicenseClassID = reader.GetString(1),
                                DriverID = reader.GetInt32(2),
                                ScheduleID = reader.GetString(3),
                                WeekDays = reader.GetString(4),
                                StartTime = reader.GetDateTime(5),
                                EndTime = reader.GetDateTime(6),
                                StartDate = reader.GetDateTime(7),
                                EndDate = reader.GetDateTime(8),
                                isActive = reader.GetBoolean(10)
                            };
                            output.Add(driverSchedule);
                        }
                        else
                        {
                            Dispatch driverSchedule = new Dispatch()
                            {
                                EmployeeID = reader.GetInt32(0),
                                DriverLicenseClassID = reader.GetString(1),
                                DriverID = reader.GetInt32(2),
                                ScheduleID = reader.GetString(3),
                                WeekDays = reader.GetString(4),
                                StartTime = reader.GetDateTime(5),
                                EndTime = reader.GetDateTime(6),
                                StartDate = reader.GetDateTime(7),
                                isActive = reader.GetBoolean(10)
                            };
                            output.Add(driverSchedule);
                        }
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
    }
}
