using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.AppData
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Static application data describing currently authenticated user
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// CREATED: 2024-02-24
        /// <br />
        ///     The number of seconds that can pass before a password reset expires
        /// </summary>
        public static long SecondsBeforePasswordResetExpiry { get; set; } = 3600;

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// CREATED: 2024-02-01
        /// <br />
        ///     The currently authenticated user if that user is a client
        /// </summary>
        public static Client_VM AuthenticatedClient { get; set; } = null;

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// CREATED: 2024-02-01
        /// <br />
        ///     The currently authenticated user if that user is an employee
        /// </summary>
        public static Employee_VM AuthenticatedEmployee { get; set; } = null;

        /// <summary>
        ///     Determines whether the currently authenticated user - if one is authenticated - has a certain role
        /// </summary>
        /// <param name="roleName">
        ///    The name of the role which a user may have
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether the authenticated user has a role; false if no user is authenticated
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> roleName: The name of the role which a user may have
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public static bool HasRole(string roleName)
        {
            bool roleFound = false;

            if (AuthenticatedClient != null)
            {
                roleFound = AuthenticatedClient.Roles.Any(role => role.ClientRoleID == roleName);
            }
            else if (AuthenticatedEmployee != null)
            {
                roleFound = AuthenticatedEmployee.Roles.Any(role => role.RoleID == roleName);
            }

            return roleFound;
        }
    }
}

