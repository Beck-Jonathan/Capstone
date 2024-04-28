using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MaintenanceScheduleAccessor : IMaintenanceScheduleAccessor
    {
        ///     A method that returns scheduled sevice orders that are complete
        /// </summary>
        /// <returns>
        ///    <see cref="List{MaintenanceScheduleVM}">MaintenanceScheduleVM</see>: The list of all complete scheduled service orders.
        /// </returns>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<MaintenanceScheduleVM> SelectAllCompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> maintenanceScheduleVM = new List<MaintenanceScheduleVM>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_all_completed_scheduled_maintenance";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var maintenanceSchedule = new MaintenanceScheduleVM();
                        maintenanceSchedule.MaintenanceScheduleID = reader.GetInt32(0);
                        maintenanceSchedule.ModelID = reader.GetInt32(1);
                        maintenanceSchedule.ServiceTypeID = reader.GetString(2);
                        maintenanceSchedule.FrequencyInMonths = reader.GetInt32(3);
                        maintenanceSchedule.FrequencyInMiles = reader.GetInt32(4);
                        maintenanceSchedule.IsCompleted = reader.GetBoolean(5);
                        maintenanceSchedule.IsActive = reader.GetBoolean(6);

                        maintenanceScheduleVM.Add(maintenanceSchedule);
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

            return maintenanceScheduleVM;
        }

        ///     A method that returns scheduled sevice orders that are incomplete
        /// </summary>
        /// <returns>
        ///    <see cref="List{MaintenanceScheduleVM}">MaintenanceScheduleVM</see>: The list of all incomplete scheduled service orders.
        /// </returns>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<MaintenanceScheduleVM> SelectAllIncompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> maintenanceScheduleVM = new List<MaintenanceScheduleVM>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_all_incomplete_scheduled_maintenance";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var maintenanceSchedule = new MaintenanceScheduleVM();
                        maintenanceSchedule.MaintenanceScheduleID = reader.GetInt32(0);
                        maintenanceSchedule.ModelID = reader.GetInt32(1);
                        maintenanceSchedule.ServiceTypeID = reader.GetString(2);
                        maintenanceSchedule.FrequencyInMonths = reader.GetInt32(3);
                        maintenanceSchedule.FrequencyInMiles = reader.GetInt32(4);
                        maintenanceSchedule.IsCompleted = reader.GetBoolean(5);
                        maintenanceSchedule.IsActive = reader.GetBoolean(6);

                        maintenanceScheduleVM.Add(maintenanceSchedule);
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

            return maintenanceScheduleVM;
        }

        ///     A method that returns all scheduled sevice orders
        /// </summary>
        /// <returns>
        ///    <see cref="List{MaintenanceScheduleVM}">MaintenanceScheduleVM</see>: The list of all complete scheduled service orders.
        /// </returns>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<MaintenanceScheduleVM> SelectAllMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> maintenanceScheduleVM = new List<MaintenanceScheduleVM>();

            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_all_scheduled_maintenance";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var maintenanceSchedule = new MaintenanceScheduleVM();
                        maintenanceSchedule.MaintenanceScheduleID = reader.GetInt32(0);
                        maintenanceSchedule.ModelID = reader.GetInt32(1);
                        maintenanceSchedule.ServiceTypeID = reader.GetString(2);
                        maintenanceSchedule.FrequencyInMonths = reader.GetInt32(3);
                        maintenanceSchedule.FrequencyInMiles = reader.GetInt32(4);
                        maintenanceSchedule.IsCompleted = reader.GetBoolean(5);
                        maintenanceSchedule.IsActive = reader.GetBoolean(6);

                        maintenanceScheduleVM.Add(maintenanceSchedule);
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

            return maintenanceScheduleVM;
        }
    }
}
