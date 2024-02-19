using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Jared Hutton, Jacob Wendt
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Represents a login providing an employee or a client access to permissions within the application
    /// </summary>
    public class Login
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int? ClientID { get; set; }
        public int? EmployeeID { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityResponse1 { get; set; }
        public string SecurityResponse2 { get; set; }
        public string SecurityResponse3 { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <inheritdoc />
    public class Login_VM : Login
    {
        public Client_VM Client { get; set; }
        public Employee_VM Employee { get; set; }
    }
}
