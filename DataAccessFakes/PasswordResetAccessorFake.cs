using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Provides access to test data stored in memory representing the password reset table
    /// </summary>
   public class PasswordResetAccessorFake : IPasswordResetAccessor
    {
        List<PasswordReset_VM> _fakePasswordResetData;

        /// <summary>
        ///     Instantiates a fake password reset accessor.
        ///     Accepts a collection of password reset objects mimicking a data source.
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public PasswordResetAccessorFake(List<PasswordReset_VM> fakePasswordResetData)
        {
            _fakePasswordResetData = fakePasswordResetData;
        }

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
        /// <br /> <br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public int InsertPasswordReset(string username, string verificationCode)
        {
            _fakePasswordResetData.Add(new PasswordReset_VM
            {
                Username = username,
                VerificationCode = verificationCode,
                RequestDateTime = DateTime.Now,
                IsActive = true
            });

            return 1;
        }

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
        /// <br /> <br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public bool VerifyPasswordReset(
            string username,
            string email,
            string verificationCode,
            long secondsBeforePasswordResetExpiry)
        {
            return _fakePasswordResetData.Any(passwordReset => 
                passwordReset.Username == username &&
                ((passwordReset.Login.Employee != null && passwordReset.Login.Employee.Email == email) ||
                (passwordReset.Login.Client != null && passwordReset.Login.Client.Email == email)) &&
                passwordReset.VerificationCode == verificationCode &&
                (DateTime.Now - passwordReset.RequestDateTime).Seconds < secondsBeforePasswordResetExpiry);
        }
    }
}
