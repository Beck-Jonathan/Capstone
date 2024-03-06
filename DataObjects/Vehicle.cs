using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Vehicle data object for storing vehicle information.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class Vehicle
    {
        public string VIN {  get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleMileage {  get; set; }
        public int VehicleModelID { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string VehicleType { get; set; }
        public string VehicleDescription {  get; set; }
        public bool Rental { get; set; }
        public DateTime DateEntered { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public int VehicleYear { get; set; }
        public int MaxPassengers { get; set; }
    }
}
