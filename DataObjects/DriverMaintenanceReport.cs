using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    /// 
    ///     Data Class for Maintenance Reports
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: xx-xx-xx
    /// <br />
    ///     
    /// </remarks>
    public class DriverMaintenanceReport
    {
        [Display(Name = "Maintance Report ID")]
        [Required(ErrorMessage = "Please enter Driver_Maintenance_Report_ID ")]
        public int DriverMaintenanceReportID { set; get; }
        [Display(Name = "Driver ID")]
        [Required(ErrorMessage = "Please enter Driver ID ")]
        public int DriverID { set; get; }
        [Display(Name = "Date_Time")]
        [Required(ErrorMessage = "Please enter The Date and Time of the event ")]

        public DateTime DateTime { set; get; }
        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Please enter VIN ")]
        [StringLength(17)]
        public string VIN { set; get; }
        [Display(Name = "Severity")]
        [Required(ErrorMessage = "Please enter the Severity ")]
        [StringLength(20)]
        public string Severity { set; get; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a Description ")]
        [StringLength(250)]
        public string Description { set; get; }
        [Display(Name = "Most Recent")]
        public bool Is_Active { set; get; }

    }
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    /// 
    ///     VM Class for Maintenance Reports
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: xx-xx-xx
    /// <br />
    ///     
    /// </remarks>
    public class DriverMaintenanceReportVM : DriverMaintenanceReport
    {
        public Vehicle Vehicle { set; get; }
        public Employee Employee { set; get; }

    }
}
