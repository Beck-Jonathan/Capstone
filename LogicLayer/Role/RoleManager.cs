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
        RoleManager _roleManager = null;
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

        public int CreateRole(Role role)
        {
            int roleCount = 0;

            try
            {

                roleCount = _roleAccessor.CreateRole(role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Couldn't add role", ex);
            }
            return roleCount;
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
