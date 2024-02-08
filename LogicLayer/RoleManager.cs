using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <inheritdoc/> 
    public class RoleManager : IRoleManager
    {
        IRoleAccessor _roleAccessor = null;

        //default constructor
        public RoleManager()
        {
            _roleAccessor = new RoleAccessor();
        }

        //parameterized constructor allowing use of fake data for unit testing
        public RoleManager(IRoleAccessor roleAccessor)
        {
            _roleAccessor = roleAccessor;
        }

      
        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<Role> roles = null;

            try
            {
                roles = _roleAccessor.GetAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting roles", ex);
            }
            return roles;
        }
    }
}

// Checked by Nathan Toothaker
