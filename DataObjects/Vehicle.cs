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
    /// Updated 2024-04-13 Jonathan Beck
    /// <br />
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-03-24
    /// <br />
    ///     Initial creation
    ///     Added MaintenanceNotes and Is_Active properties
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
        public string MaintenanceNotes { get; set; }
        public bool Rental { get; set; }
        public DateTime DateEntered { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public int VehicleYear { get; set; }
        public int MaxPassengers { get; set; }
        public bool Is_Active { get; set; }
    }
    
    public class Vehicle_CM : Vehicle { 
        //added Jonathan Beck 4/13/2024
    public List<ServiceOrder_VM> ServiceOrders { get; set; }
    
    }
}
