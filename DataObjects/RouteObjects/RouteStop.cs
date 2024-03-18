using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.RouteObjects
{   
     /// <summary>
     /// AUTHOR: Nathan Toothaker
     /// <br />
     /// CREATED: 2024-03-04
     /// <br />
     /// 
     ///     The RouteStop class is to represent the data contained in the RouteStop table.
     /// </summary>
     /// 
    public class RouteStop
    {
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int StopNumber {  get; set; }
        public TimeSpan OffsetFromRouteStart { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// 
    ///     The RouteStopVM class extends the <see cref="RouteStop">RouteStop</see> class and includes the Stop object contained.
    /// </summary>
    /// 
    public class RouteStopVM : RouteStop
    {
        public Stop stop { get; set; }
    }
}
