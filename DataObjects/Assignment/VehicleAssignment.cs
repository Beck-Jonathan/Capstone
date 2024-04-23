using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Assignment
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    ///    Vehicle Assignment Model
    /// </summary>
    public class VehicleAssignment
    {
        public string VIN { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public int Max_Passengers { get; set; }
    }
}
