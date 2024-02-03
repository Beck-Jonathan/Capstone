using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ClientRole
    {
        public string ClientRoleID { get; set; }
        public string RoleDescription { get; set; }
    }

    public class ClientRole_VM
    {
        public IEnumerable<Client_VM> Clients { get; set; }
    }
}
