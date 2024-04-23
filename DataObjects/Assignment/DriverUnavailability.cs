using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    ///     Driver Unavailability Model
    /// </summary>
    public class DriverUnavailability
    {
        public int UnavailableID { get; set; }
        public int DriverID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
