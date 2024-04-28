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
        public string VIN { get; set; }
        public DateTime ChecklistDate { get; set; }
        public bool Clean { get; set; }
        public bool Pedals { get; set; }
        public bool Dash { get; set; }
        public bool Steering { get; set; }
        public bool AC_Heat { get; set; }

        public bool MirrorDS { get; set; }
        public bool MirrorPS { get; set; }
        public bool MirrorRV { get; set; }

        public string Cosmetic { get; set; }

        public bool Tire_Pressure_DF { get; set; }
        public bool Tire_Pressure_PF { get; set; }
        public bool Tire_Pressure_DR { get; set; }
        public bool Tire_Pressure_PR { get; set; }

        public bool Blinker_DF { get; set; }
        public bool Blinker_PF { get; set; }
        public bool Blinker_DR { get; set; }
        public bool Blinker_PR { get; set; }

        public bool Breaklight_DR { get; set; }
        public bool Breaklight_PR { get; set; }

        public bool Headlight_Driver { get; set; }
        public bool Headlight_Passenger { get; set; }

        public bool TailLight_Driver { get; set; }
        public bool TailLight_Passenger { get; set; }

        public bool Wiper_Driver { get; set; }
        public bool Wiper_Passenger { get; set; }
        public bool Wiper_Rear { get; set; }

        public bool SeatBelts { get; set; }
        public bool FireExtinguisher { get; set; }
        public bool Airbags { get; set; }
        public bool FirstAid { get; set; }
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
