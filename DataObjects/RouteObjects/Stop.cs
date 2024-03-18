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
    ///     The stop class contains all data from the Stop table in the database.
    ///     This represents a location you could find on a map, where a vehicle could stop for pick-up or drop-off.
    /// </summary>
    ///
    public class Stop
    {
        public int StopId { get; set; }
        public string StreetAddress {  get; set; }
        public string ZIPCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsActive {  get; set; }
    }
}
