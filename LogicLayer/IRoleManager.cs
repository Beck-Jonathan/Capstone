using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    /// 
    ///     Manager class for Role that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: James Williams
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    ///  Initial creation
    /// </remarks>
    public interface IRoleManager
    {
        /// <summary>
        ///     Method to retreive all roles from database.
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{Role}">IEnumerable{Role}</see>: List of employee roles
        /// </returns>
        /// <remarks>
        ///
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when no roles returned
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: 
        /// <br />
        ///    UPDATED: 2024-02-01
        /// <br />
        ///    Initial Creation
        /// </remarks>
        IEnumerable<Role> GetAllRoles();
    }
}
// Checked by Nathan Toothaker