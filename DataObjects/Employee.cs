using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: James Williams, Jared Hutton, Parker Svoboda, Jacob Wendt
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Represents an employee within the organization
    /// </summary>
    /// <br /><br />
    ///    UPDATER: Steven Sanchez
    /// <br />
    ///    UPDATED: 2024-02-27
    /// <br />
    ///     added Login to the Employee_VM
    /// </remarks>
    public class Employee
    {
        public int Employee_ID { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public bool Is_Active { get; set; }
    }

    /// <inheritdoc />
    public class Employee_VM : Employee
    {
        public Login Login { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
