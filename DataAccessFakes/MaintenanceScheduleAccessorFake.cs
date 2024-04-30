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
                IsCompleted = false,
                IsActive = true
            });
            fakeMaintenanceSchedules.Add(new MaintenanceScheduleVM()
            {
                MaintenanceScheduleID = 2,
                ModelID = 2,
                ServiceTypeID = "Tire Replacement",
                FrequencyInMonths = 12,
                FrequencyInMiles = 10000,
                IsCompleted = true,
                IsActive = false
            });
            fakeMaintenanceSchedules.Add(new MaintenanceScheduleVM()
            {
                MaintenanceScheduleID = 3,
                ModelID = 3,
                ServiceTypeID = "Window Replacement",
                FrequencyInMonths = 6,
                FrequencyInMiles = 15000,
                IsCompleted = true,
                IsActive = false
            });
        }


        public List<MaintenanceScheduleVM> SelectAllIncompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> incompleteMaintenanceSchedules = fakeMaintenanceSchedules.FindAll(m => m.IsCompleted == false);
            return incompleteMaintenanceSchedules;
        }

        public List<MaintenanceScheduleVM> SelectAllCompleteMaintenanceSchedule()
        {
            List<MaintenanceScheduleVM> completeMaintenanceSchedules = fakeMaintenanceSchedules.FindAll(m => m.IsCompleted == true);
            return completeMaintenanceSchedules;
        }

        public List<MaintenanceScheduleVM> SelectAllMaintenanceSchedule()
        {
            return fakeMaintenanceSchedules;
        }
    }
}
