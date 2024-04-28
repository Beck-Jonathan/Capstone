using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Jonathan Beck
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    /// 
    ///     Accessor Fakes Class for Maintenance Reports
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: xx-xx-xx
    /// <br />
    ///     
    /// </remarks>
    public class DriverMaintenanceReportFakes : IDriverMaintenanceReportAccessor
    {
        List<DriverMaintenanceReport> _reports;
        //Constructor adds 3 generic reports
        /// <summary>
        /// Jonathan Beck 2024-04-17
        /// </summary>
        public DriverMaintenanceReportFakes()
        {
            _reports = new List<DriverMaintenanceReport>() {
        new DriverMaintenanceReport() {DriverMaintenanceReportID=1, DriverID=1, Description="Hit a Deer."},
        new DriverMaintenanceReport() {DriverMaintenanceReportID=2,DriverID=1},
        new DriverMaintenanceReport() {DriverMaintenanceReportID=3, DriverID = 2},
        };
        }
        /// <summary>
        /// Select by id, Jonathan Beck 2024-04-23
        /// </summary>

        public DriverMaintenanceReportVM SelectAllDriverMaintenanceReportsById(int ReportID)
        {
            DriverMaintenanceReportVM result = null;
            foreach (DriverMaintenanceReport report in _reports)
            {
                if (report.DriverMaintenanceReportID == ReportID)
                {
                    result = new DriverMaintenanceReportVM();
                    result.DriverMaintenanceReportID = report.DriverMaintenanceReportID;
                    result.DriverID = report.DriverID;
                    result.Description = report.Description;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// Insert Method Jonathan Beck 2024-04-17
        /// </summary>
        public int insertDriverMaintenanceReport(DriverMaintenanceReport _driver_maintenance_report)
        {
            int start_size = _reports.Count();
            _reports.Add(_driver_maintenance_report);
            int end_size = _reports.Count();
            return end_size - start_size;
        }
        /// <summary>
        /// Select all method Jonathan Beck 2024-04-23
        /// </summary>
        public List<DriverMaintenanceReportVM> SelectActiveDriverMaintenaceReports()
        {
            List<DriverMaintenanceReportVM> results = new List<DriverMaintenanceReportVM>();
            foreach (DriverMaintenanceReport report in _reports)
            {
                results.Add(report as DriverMaintenanceReportVM);
            }
            return results;
        }
        /// <summary>
        /// Select by driver method Jonathan Beck 2024-04-23
        /// </summary>
        public List<DriverMaintenanceReportVM> SelectAllDriverMaintenanceReportsByEmployeeID(int DriverID)
        {
            List<DriverMaintenanceReportVM> results = new List<DriverMaintenanceReportVM>();
            foreach (DriverMaintenanceReport report in _reports)
            {
                if (report.DriverID == DriverID)
                {
                    results.Add(report as DriverMaintenanceReportVM);
                }
            }
            return results;
        }
    }
}
