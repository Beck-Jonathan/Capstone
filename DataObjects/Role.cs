using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Represents a role exposing permissions for employees
    /// </summary>
    public class Role
    {
        public string RoleID { get; set; }
        public bool IsActive { get; set; }
    }

    /// <inheritdoc/>
    public class Role_VM : Role
    {
        public List<Employee_VM> Employees { get; set; } 
    }
}
