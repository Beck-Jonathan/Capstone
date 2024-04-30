using DataObjects.HelperObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Jared Hutton, Jacob Wendt
    /// <br />
    /// CREATED: 2024-04-23
    /// <br />
    ///     Represents a ride schedule by a client and fulfilled by a driver
    /// </summary>
    public class Ride
    {
        public int RideID { get; set; }

        public int ClientID { get; set; }

        public int? DriverID { get; set; }

        public string Operation { get; set; }

        public string VIN { get; set; }

        [Required(ErrorMessage = "Scheduled location is required")]
        [DisplayName("Pickup Location")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Dropoff location is required")]
        [DisplayName("Dropoff Location")]
        public string DropoffLocation { get; set; }

        public DateTime ScheduledPickupTime { get; set; }

        public bool IsActive { get; set; }
    }

    public class Ride_VM : Ride
    {
        public Employee_VM Driver { get; set; }

        [Required(ErrorMessage ="Scheduled date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}")]
        [DisplayName("Date")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage ="Scheduled time is required")]
        [DataType(DataType.Time)]
        [DisplayName("Time")]
        public TimeSpan ScheduledTime { get; set; }

        // This method allows us to separate the date and time inputs in the view
        public void CalculatePickupTime()
        {
            int scheduledYear = ScheduledDate.Year;
            int scheduledMonth = ScheduledDate.Month;
            int scheduledDay = ScheduledDate.Day;

            int scheduledHour = ScheduledTime.Hours;
            int scheduledMinute = ScheduledTime.Minutes;
            int scheduledSecond = ScheduledTime.Seconds;

            ScheduledPickupTime = new DateTime(
                scheduledYear,
                scheduledMonth,
                scheduledDay,
                scheduledHour,
                scheduledMinute,
                scheduledSecond);
        }
    }
}
