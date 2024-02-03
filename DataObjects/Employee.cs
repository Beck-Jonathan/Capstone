using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
    }

    public class Employee_VM : Employee
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}
