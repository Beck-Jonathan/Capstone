using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Login
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }
    }

    public class Login_VM : Login
    {
        public Client_VM Client { get; set; }
        public Employee_VM Employee { get; set; }
    }
}
