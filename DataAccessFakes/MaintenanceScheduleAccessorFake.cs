using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class MaintenanceScheduleAccessorFake : IMaintenanceScheduleAccessor
    {
        private List<MaintenanceScheduleVM> fakeMaintenanceSchedules = new List<MaintenanceScheduleVM>();

        public MaintenanceScheduleAccessorFake()
        {
            fakeMaintenanceSchedules.Add(new MaintenanceScheduleVM()
            {
                MaintenanceScheduleID = 1,
                ModelID = 1,
                ServiceTypeID = "Oil Change",
                FrequencyInMonths = 6,
                FrequencyInMiles = 5000,
                TimeLastCompleted = DateTime.Now,
                IsActive = true
            });
            fakeMaintenanceSchedules.Add(new MaintenanceScheduleVM()
            {
                MaintenanceScheduleID = 2,
                ModelID = 2,
                ServiceTypeID = "Tire Replacement",
                FrequencyInMonths = 12,
                FrequencyInMiles = 10000,
                TimeLastCompleted = DateTime.Now,
                IsActive = false
            });
            fakeMaintenanceSchedules.Add(new MaintenanceScheduleVM()
            {
                MaintenanceScheduleID = 3,
                ModelID = 3,
                ServiceTypeID = "Window Replacement",
                FrequencyInMonths = 6,
                FrequencyInMiles = 15000,
                TimeLastCompleted = DateTime.Now,
                IsActive = false
            });
        }


        public List<MaintenanceScheduleVM> SelectAllIncompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> incompleteMaintenanceSchedules = fakeMaintenanceSchedules.FindAll(m => m.IsActive == false);
            return incompleteMaintenanceSchedules;
        }

        public List<MaintenanceScheduleVM> SelectAllCompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> completeMaintenanceSchedules = fakeMaintenanceSchedules.FindAll(m => m.IsActive == true);
            return completeMaintenanceSchedules;
        }

        public List<MaintenanceScheduleVM> SelectAllMaintenanceSchedule()
        {
            return fakeMaintenanceSchedules;
        }

        public int CreateMaintenanceSchedule(MaintenanceScheduleVM maintenance)
        {
            int result = -1;
            try
            {
                //check that all required values are not null
                if (maintenance.ModelID != null && maintenance.ServiceTypeID != null
                    && maintenance.FrequencyInMonths != null)
                {
                    //give object an ID and add to list
                    maintenance.MaintenanceScheduleID = fakeMaintenanceSchedules[fakeMaintenanceSchedules.Count - 1].MaintenanceScheduleID + 1;
                    fakeMaintenanceSchedules.Add(maintenance);
                    result = maintenance.MaintenanceScheduleID;
                }
                else
                {
                    throw new ArgumentException("All required fields cannot be null.");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;

        }
    }
}
