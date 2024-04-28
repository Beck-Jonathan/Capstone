using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class MaintenanceSchedule
    {
        public int MaintenanceScheduleID { get; set; }
        public int ModelID { get; set; }
        public string ServiceTypeID { get; set; }
        public int FrequencyInMonths { get; set; }
        public int FrequencyInMiles { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
    }

    public class MaintenanceScheduleVM : MaintenanceSchedule
    {
        public VehicleModel Model { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
