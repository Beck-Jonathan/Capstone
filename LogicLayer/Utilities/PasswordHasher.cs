using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Generates password hashes based on provided plaintext passwords
    /// </summary>
    public class PasswordHasher : IPasswordHasher
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
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public string HashPassword(string password)
        {
            byte[] hashBytes;

            using (SHA256 sha256Hasher = SHA256.Create())
            {
                hashBytes = sha256Hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte hexByte in hashBytes)
            {
                stringBuilder.Append(hexByte.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
