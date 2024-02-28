using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Provides CRUD operations on the data source for password reset objects
    /// </summary>
    public interface IPasswordResetAccessor
    {
        /// <summary>
        ///     Inserts new password reset object
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to reset their password
        /// </param>
        /// <param name="verificationCode">
        ///    The verification code which the user will use to verify their identity
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows affected by the insert operation
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> verificationCode: The verification code which the user will use to verify their identity
        /// </remarks>

        int InsertPasswordReset(string username, string verificationCode);

        /// <summary>
        ///     Accepts a username, email, and verification code to verify a password reset
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to reset their password
        /// </param>
        /// <param name="email">
        ///    The email of the user attempting to reset their password
        /// </param>
        /// <param name="verificationCode">
        ///    The verification code which the user is providing to verify their identity
        /// </param>
        /// <param name="secondsBeforePasswordResetExpiry">
        ///    The number of seconds that may pass before a password reset attempt expires
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether the password reset was verified
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> email: The email given by the user
        /// <br />
        ///    <see cref="string">verificationCode</see> verificationCode: The verification code given by the user
        /// <br />
        ///    <see cref="long">secondsBeforePasswordResetExpiry</see> verificationCode:
        ///    The number of seconds that may pass before a password reset attempt expires
        /// </remarks>
       bool VerifyPasswordReset(
            string username,
            string email,
            string verificationCode,
            long secondsBeforePasswordResetExpiry);
    }
}
