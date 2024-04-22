using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Everett DeVaux
    /// <br />
    /// CREATED: 2024-03-24
    /// <br />
    ///     Fake Driver data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// Initial creation
    /// </remarks>
    public class DispatchAccessorFake : IDispatchAccessor
    {
        private List<Dispatch> _fakeDriverSchedule = new List<Dispatch>();

        public DispatchAccessorFake()
        {
            _fakeDriverSchedule.Add(new Dispatch()
            {
                EmployeeID = 100000,
                DriverLicenseClassID = "D",
                DriverID = 100000,
                ScheduleID = "SCH1",
                WeekDays = "1111111",
                StartTime = DateTime.Parse("10:00:00"),
                EndTime = DateTime.Parse("12:00:00"),
                StartDate = DateTime.Parse("2024-03-01"),
                EndDate = DateTime.Parse("2024-05-15"),
                Notes = "Notes...",
                isActive = true,

            });
        }

        /// <summary>
        ///     Returns all fake DriveSchedule records
        /// </summary>

        /// <returns>
        ///    <see cref="List{DriverSchedule}">DriverSchedule</see> The list of all fake DriverSchedule objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-24
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public List<Dispatch> SelectDriverScheduleForList()
        {
            return _fakeDriverSchedule;
        }
    }
}