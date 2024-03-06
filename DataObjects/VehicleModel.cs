using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Represents a vehicle model which vehicles can be assigned
    /// </summary>
    public class VehicleModel
    {
        public int VehicleModelID { get; set; }
        public string VehicleTypeID { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public int MaxPassengers { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
