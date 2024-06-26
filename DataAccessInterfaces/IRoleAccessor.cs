﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    /// Database access class for Role
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    /// initial creation
    /// </remarks>
    public interface IRoleAccessor
    {
        /// <summary>
        ///     Method to retrieve all roles in the database
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{Role}">IEnumerable{Role}</see>: A list of roles
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when no roles retrieved from database.
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: 2024-02-01
        /// <br />
        ///     Initial Creation
        /// </remarks>
        IEnumerable<Role> GetAllRoles();


        /// <summary>
        ///     Method to retrieve all roles in the database
        /// </summary>
        /// <returns>
        ///    <see cref="int">int</see>: number of roles created.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when no role could not be added.
        /// <br /><br />
        ///    CONTRIBUTOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-02-25
        /// <br /><br />
        /// </remarks>
        int CreateRole(Role role);
       
    }
}
// Checked by Nathan Toothaker