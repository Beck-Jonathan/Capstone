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

        public bool isActiveOnDay(DateTime day)
        {
            bool result = false;
            result = DaysOfService.isActiveOnDay(day.ToString("ddd"));
            return result;
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
