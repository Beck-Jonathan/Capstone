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
    public class Client
    {
        public int ClientID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string TextNumber { get; set; }
        public string VoiceNumber { get; set; }
        public bool IsActive { get; set; }
    }

    /// <inheritdoc/>
    public class Client_VM : Client
    {
        public string Username { get; set; }
        public IEnumerable<ClientRole> Roles { get; set; }
    }
}
