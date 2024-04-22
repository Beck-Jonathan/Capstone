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
    ///     Vehicle Unavailability Model
    /// </summary>
    public class VehicleUnavailability
    {
        public int Unavailability_ID { get; set; }
        public string VIN { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public bool IsActive { get; set; }
    }
}
