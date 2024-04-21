using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.RouteObjects
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// <br />
    /// CREATED: 2024-03-24
    /// <br />
    /// The Route Assignment Class contains all data from the Route Assignment Table
    /// </summary>
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public class Route_Assignment
    {
        public int Assignment_ID { get; set; }
        public int DriverID { get; set; }
        public int Route_ID { get; set; }
        public string VIN_Number { get; set; }
        public DateTime Date_Assignment_Started { get; set; }
        public DateTime? Date_Assignment_Ended { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// Extends the <see cref="Route_Assignment">Route Assignment</see> object to include the list of 
    /// stops associated with the route and list of routes
    /// </summary>
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public class Route_Assignment_VM : Route_Assignment
    {
        public RouteVM routeVM { get; set; }
        public RouteStopVM routeStopVM { get; set; }
        public Stop stop { get; set; }
    }
}
