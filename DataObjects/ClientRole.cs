using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Isabella Rosenbohm, Jared Hutton, Jacob Wendt, Jared Roberts
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Represents a client using the services provided by the organization
    /// </summary>
    public class ClientRole
    {
        public string ClientRoleID { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
    }

    /// <inheritdoc/>
    public class ClientRole_VM : ClientRole
    {
        public IEnumerable<Client_VM> Clients { get; set; }
    }
}
