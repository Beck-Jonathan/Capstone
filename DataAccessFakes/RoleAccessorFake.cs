using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{

    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-02
    /// <br />
    /// 
    ///   Fake data for the Role class
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: James Williams
    /// <br />
    /// UPDATED: 2024-02-04
    /// <br />
    /// initial creation
    /// </remarks>
    /// 

    public class RoleAccessorFake : IRoleAccessor
    {


        private List<Role> _roles = new List<Role>();
        public RoleAccessorFake()
        {
            _roles.Add(new Role() { Role_ID = "Administrator", Is_Active = true });
            _roles.Add(new Role() { Role_ID = "Maintenance", Is_Active = true });
            _roles.Add(new Role() { Role_ID = "Fleet Manager", Is_Active = true });
            // purposeful duplicate entry to check that GetAllRoles() only returns unique Role_ID elements
            _roles.Add(new Role() { Role_ID = "Fleet Manager", Is_Active = true });

        }

        /// <summary>
        ///     Returns all fake roles
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>
        ///    <see cref="bool">true</see> if the email address is valid; otherwise, <see cref="ArgumentException">exeception</see>.
        /// </returns>
        /// 

        /// <summary>
        ///    Returns all fake roles
        /// </summary>    
        /// <returns>
        ///    <see cref="IEnumerable{Role}"></see>: List of roles
        /// </returns>
        /// <remarks>
        ///   
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when no roles found.
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>

        public IEnumerable<Role> GetAllRoles()
        {
            //Only return unique roles. This LINQ query groups all of the similar Role_ID properties and then only returns
            //the first element of each group.
            List<Role> roles = _roles.GroupBy(r => r.Role_ID).Select(group => group.First()).ToList();

            if (roles.Count < 1)
            {
                throw new ArgumentException("No roles found");
            }

            return roles;

        }

      
    }
}
// Checked by Nathan Toothaker, update comments