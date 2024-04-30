using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// 
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm
    /// <br />
    /// UPDATED: 2024-02-13
    /// Changed Client_VM property from "username" to "Username"
    /// <br />
    ///     /// UPDATER: Jacob Rohr
    /// <br />
    /// UPDATED: 2024-04-12
    /// Added login property to client_vm
    /// 
    /// <br />
    /// 
    ///     
    /// </remarks>
    public partial class Client
    {
        public int ClientID { get; set; }
       [Required]
        public string GivenName { get; set; }
       [Required]
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
       [Required]
        public string Email { get; set; }
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
       [RegularExpression(@"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\(\d{3}\)\s?\d{3}-\d{4}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string TextNumber { get; set; }
       [RegularExpression(@"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\(\d{3}\)\s?\d{3}-\d{4}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string VoiceNumber { get; set; }
        public bool IsActive { get; set; }  
    }

    /// <inheritdoc/>
    public class Client_VM : Client
    {
        public string Username { get; set; }
        public IEnumerable<ClientRole> Roles { get; set; }

        public Login Login { get; set; }
        public IEnumerable<ClientDependentRole> ClientDependentRoles { get; set; }
    }
}
