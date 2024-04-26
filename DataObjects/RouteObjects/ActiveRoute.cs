using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.RouteObjects
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-04-20
    ///     Object for holding a currently active route.
    /// </summary>
    public class ActiveRoute
    {
        [Required]
        [DisplayName("Assigned Route")]
        public int AssignmentID { get; set; }

        [Required]
        [DisplayName("Driver")]
        public int DriverID { get; set; }

        [Required]
        [DisplayName("Vehicle")]
        public string VIN { get; set; }
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }
    }
}
