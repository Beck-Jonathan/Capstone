using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    /// <summary>
    ///     Generates password hashes based on provided plaintext passwords
    /// </summary>

    public interface IPasswordHasher
    {
        /// <summary>
        ///     Generates a password hash based on a password provided in plaintext
        /// </summary>
        /// <param name="password">
        ///    The plaintext password to be hashed
        /// </param>
        /// <returns>
        ///    <see cref="string">string</see>: The generated password hash
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> password: The plaintext password to be hashed
        /// </remarks>
        string HashPassword(string password);
    }
}
