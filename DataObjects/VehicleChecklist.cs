using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-04-20
    ///     Vehicle checklist data object for storing checklist information.
    /// </summary>
    public class VehicleChecklist
    {
        public int ChecklistID { get; set; }

        [Required]
        [DisplayName("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        [DisplayName("Vehicle Number")]
        public string VIN { get; set; }
        [DisplayName("Checklist Date")]
        public DateTime ChecklistDate { get; set; }
        public bool Clean { get; set; }
        public bool Pedals { get; set; }
        public bool Dash { get; set; }
        public bool Steering { get; set; }
        [DisplayName("Climate Control System")]
        public bool AC_Heat { get; set; }

        [DisplayName("Driver Side Mirror")]
        public bool MirrorDS { get; set; }
        [DisplayName("Passenger Side Mirror")]
        public bool MirrorPS { get; set; }
        [DisplayName("Rearview Mirror")]
        public bool MirrorRV { get; set; }

        public string Cosmetic { get; set; }

        [DisplayName("Driver Front Tire Pressure")]
        public bool Tire_Pressure_DF { get; set; }
        [DisplayName("Passenger Front Tire Pressure")]
        public bool Tire_Pressure_PF { get; set; }
        [DisplayName("Driver Rear Tire Pressure")]
        public bool Tire_Pressure_DR { get; set; }
        [DisplayName("Passenger Rear Tire Pressure")]
        public bool Tire_Pressure_PR { get; set; }

        [DisplayName("Driver Front Turn Signal")]
        public bool Blinker_DF { get; set; }
        [DisplayName("Passenger Front Turn Signal")]
        public bool Blinker_PF { get; set; }
        [DisplayName("Driver Rear Turn Signal")]
        public bool Blinker_DR { get; set; }
        [DisplayName("Passenger Rear Turn Signal")]
        public bool Blinker_PR { get; set; }

        [DisplayName("Driver Side Break Light")]
        public bool Breaklight_DR { get; set; }
        [DisplayName("Passenger Side Break Light")]
        public bool Breaklight_PR { get; set; }

        [DisplayName("Driver Side Headlight")]
        public bool Headlight_Driver { get; set; }
        [DisplayName("Passenger Side Headlight")]
        public bool Headlight_Passenger { get; set; }

        [DisplayName("Driver Side Tail Light")]
        public bool TailLight_Driver { get; set; }
        [DisplayName("Passenger Side Tail Light")]
        public bool TailLight_Passenger { get; set; }

        [DisplayName("Driver Wiper")]
        public bool Wiper_Driver { get; set; }
        [DisplayName("Passenger Wiper")]
        public bool Wiper_Passenger { get; set; }
        [DisplayName("Rear Wiper")]
        public bool Wiper_Rear { get; set; }

        [DisplayName("Seat Belts")]
        public bool SeatBelts { get; set; }
        [DisplayName("FIre Extinguisher")]
        public bool FireExtinguisher { get; set; }
        public bool Airbags { get; set; }
        [DisplayName("First Aid Kit")]
        public bool FirstAid { get; set; }
        [DisplayName("Emergency Kit")]
        public bool EmergencyKit { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        [DisplayName("Fuel Level")]
        public decimal FuelLevel { get; set; }
        public bool Breaks { get; set; }
        public bool Accelerator { get; set; }
        public bool Clutch { get; set; }
        public string Notes { get; set; }

    }
}
