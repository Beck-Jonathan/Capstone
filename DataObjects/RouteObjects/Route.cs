using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.HelperObjects;

namespace DataObjects.RouteObjects
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// 
    ///     The Route Class contains all data from the Route Table
    /// </summary>
    ///
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }
        public TimeSpan RepeatTime {  get; set; } // how often the route sends out a new vehicle (or an existing vehicle starts its second trip)
        public ActivityWeek DaysOfService { get; set; } // use accessor method to check which day, based on the bitmap
        public bool IsActive { get; set; }

        /// <summary>
        ///     Returns whether or not specific day is marked as active
        /// </summary>
        /// <param name="day">The DateTime representation of the weekday to check.</param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the day is marked as active
        /// </returns>
        /// <remarks>
        /// <br />
        /// PARAMETERS:
        /// <br />
        /// day: the datetime representation of the weekday to be checked.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public bool isActiveOnDay(DateTime day)
        {
            bool result = false;
            result = DaysOfService.isActiveOnDay(day.ToString("ddd"));
            return result;
        }
        /// <summary>
        ///     Returns whether or not specific day is marked as active
        /// </summary>
        /// <param name="day">The DayOfWeek representation of the day to check.</param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the day is marked as active
        /// </returns>
        /// <remarks>
        /// <br />
        /// PARAMETERS:
        /// <br />
        /// dayName: the DayOfWeek representation of the day to be checked.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public bool isActiveOnDay(DayOfWeek day)
        {
            bool result = false;
            result = DaysOfService.isActiveOnDay(day.ToString("d"));
            return result;
        }
        /// <summary>
        ///     Returns whether or not specific day is marked as active
        /// </summary>
        /// <param name="day">The string representation of the day to check.</param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the day is marked as active
        /// </returns>
        /// <remarks>
        /// <br />
        /// PARAMETERS:
        /// <br />
        /// day: the string representation of the day to be checked.
        /// <br />
        /// Accept dayNames are "Monday", "Mon", "Tuesday", "Tue", etc. Capitalization does not matter.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public bool isActiveOnDay(string day)
        {
            return DaysOfService.isActiveOnDay(day);
        }

    }

    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// 
    ///     Extends the <see cref="Route">Route</see> object to include the list of stops associated with the route.
    /// </summary>
    ///
    public class RouteVM : Route
    {
        public IEnumerable<RouteStopVM> RouteStops {  get; set; }
    }
}
