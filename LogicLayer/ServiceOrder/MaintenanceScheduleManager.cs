using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.ServiceOrder
{
    public class MaintenanceScheduleManager : IMaintenanceScheduleManager
    {
        IMaintenanceScheduleAccessor _maintenanceAccessor = null;
        public MaintenanceScheduleManager()
        {
            _maintenanceAccessor = new MaintenanceScheduleAccessor();
        }

        public MaintenanceScheduleManager(IMaintenanceScheduleAccessor maintenanceAccessor)
        {
            _maintenanceAccessor = maintenanceAccessor;
        }

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
        public List<MaintenanceScheduleVM> GetAllCompleteMaintenanceSchedules()
        {
            List<MaintenanceScheduleVM> result = null;
            try
            {
                result = _maintenanceAccessor.SelectAllCompleteMaintenanceSchedule();
                if (result.Count() == 0)
                {
                    throw new ArgumentException("No Maintenance was found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Scheduled Maintenance was found", ex);
            }
            return result;
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
        public List<MaintenanceScheduleVM> GetAllIncompleteMaintenanceSchedules()
        {
            List<MaintenanceScheduleVM> result = null;
            try
            {
                result = _maintenanceAccessor.SelectAllIncompleteMaintenanceSchedule();
                if (result.Count() == 0)
                {
                    throw new ArgumentException("No Maintenance was found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Scheduled Maintenance was found", ex);
            }
            return result;
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
        public List<MaintenanceScheduleVM> GetAllMaintenanceSchedules()
        {
            List<MaintenanceScheduleVM> result = null;
            try
            {
                result = _maintenanceAccessor.SelectAllMaintenanceSchedule();
                if (result.Count() == 0)
                {
                    throw new ArgumentException("No Maintenance was found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Scheduled Maintenance was found", ex);
            }
            return result;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-03-03
        /// sends the given object to the database to be added as a new record
        /// </summary>
        /// <param name="maintenance">The data to add the database</param>
        /// <returns>the id number for the new object</returns>
        public int AddScheduledMaintenance(MaintenanceScheduleVM maintenance)
        {
            int result = -1;
            try
            {
                result = _maintenanceAccessor.CreateMaintenanceSchedule(maintenance);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
