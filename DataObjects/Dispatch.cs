using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Dispatch
    {
        public int EmployeeID { get; set; }
        public string DriverLicenseClassID { get; set; }
        public int DriverID { get; set; }
        public string ScheduleID { get; set; }
        public string WeekDays { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public bool isActive { get; set; }
    }
}
