using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ClientDependentRole
    {
        public int ClientID { get; set; }
        public int DependentID { get; set; }
        public string Relationship { get; set; }
        public bool IsActive { get; set; }

    }
    public class ClientDependentRole_VM : ClientDependentRole
    {
        public Client client { get; set; }
        public Dependent dependent { get; set; }
    }
}
