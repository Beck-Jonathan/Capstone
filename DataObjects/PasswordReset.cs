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
    /// CREATED: 2024-02-24
    /// <br />
    ///     Represents a password reset for failed authentication
    /// </summary>
    public class PasswordReset
    {
        public int PasswordResetId { get; set; }
        public string Username { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string VerificationCode { get; set; }
        public bool IsActive { get; set; }
    }

    /// <inheritdoc />
    public class PasswordReset_VM : PasswordReset
    {
        public Login_VM Login { get; set; }
    }
}
