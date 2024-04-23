using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Everett DeVaux
    /// CREATED: 2024-03-24
    ///     Logic manager interface for working with driver schedule data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: 
    /// <br />
    /// UPDATED: YYYY-MM-DD
    /// <br />
    /// 
    ///     Initial creation
    /// </remarks>
    public interface IDriverScheduleManager
    {
        /// <summary>
        ///     Retrieves a list of Driver Schedules from the database
        /// </summary>
        /// <returns>
        ///    <see cref="DriverSchedule">DriverSchedule</see> List of Driver Schedule objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-24
        /// <br />
        ///     Initial Creation
        /// </remarks>
        List<Dispatch> GetDriverScheduleList();
    }
}
