using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Everett DeVaux
    /// CREATED: 2024-03-24
    ///     Data access interface for accessing driver information from the database.
    /// </summary>
    /// <remarks>
    /// UPDATER: 
    /// UPDATED: YYYY-MM-DD
    /// <br />
    /// </remarks>
    public interface IDispatchAccessor
    {
        /// <summary>
        ///     Accessor that allows us to pull the specific info for the driver schedule List
        /// </summary>
        /// <returns>
        ///    <see cref="List{DriverSchedule}">DriverSchedule</see> List of Driver Schedule objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-24
        ///    Initial Creation
        /// </remarks>
        List<Dispatch> SelectDriverScheduleForList();
    }
}
