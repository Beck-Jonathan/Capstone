using DataAccessInterfaces;
using DataAccessLayer;
using LogicLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    ///     Performs password reset-related operations
    /// </summary>
    public interface IPasswordResetManager
    {
        void BeginPasswordReset(string username);
        bool VerifyPasswordReset(string username, string email, string verificationCode);
    }

    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Performs password reset-related operations
    /// </summary>
    public class PasswordResetManager : IPasswordResetManager
    {
        private IVerificationCodeGenerator _verificationCodeGenerator;
        private IPasswordResetAccessor _passwordResetAccessor;
        private long _secondsBeforePasswordResetExpiry;

        /// <summary>
        ///     Instantiates a PasswordResetManager
        /// </summary>
        /// <param name="verificationCodeGenerator">
        ///    The utility used to generate multi-factor verification codes
        /// </param>
        /// <param name="secondsBeforePasswordResetExpiry">
        ///    The number of seconds before a password reset expires
        /// </param>
        /// <returns>
        ///    <see cref="PasswordResetManager">PasswordResetManager</see>: A PasswordResetManager object
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="IVerificationCodeGenerator">IVerificationCodeGenerator</see>
        ///    verificationCodeGenerator: The utility used to generate multi-factor verification codes
        /// <br />
        ///    <see cref="long">long</see>
        ///    secondsBeforePasswordResetExpiry: The number of seconds before a password reset expires
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
       public PasswordResetManager(
           IVerificationCodeGenerator verificationCodeGenerator,
           long secondsBeforePasswordResetExpiry)
        {
            _verificationCodeGenerator = verificationCodeGenerator;
            _secondsBeforePasswordResetExpiry = secondsBeforePasswordResetExpiry;
            _passwordResetAccessor = new PasswordResetAccessor();
        }

        /// <summary>
        ///     Instantiates a PasswordResetManager
        /// </summary>
        /// <param name="verificationCodeGenerator">
        ///    The utility used to generate multi-factor verification codes
        /// </param>
        /// <param name="secondsBeforePasswordResetExpiry">
        ///    The number of seconds before a password reset expires
        /// </param>
        /// <param name="passwordResetAccessor">
        ///     The accessor used to send and retrieve password reset objects to/from the data source
        /// </param>
        /// <returns>
        ///    <see cref="PasswordResetManager">PasswordResetManager</see>: A PasswordResetManager object
        /// </returns>
        /// <br /><br />
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="IVerificationCodeGenerator">IVerificationCodeGenerator</see>
        ///    verificationCodeGenerator: The utility used to generate multi-factor verification codes
        /// <br />
        ///    <see cref="IPasswordResetAccessor">IPasswordResetAccessor</see> passwordResetAccessor:
        ///    The accessor used to send and retrieve password reset objects to/from the data source
        /// <br />
        ///    <see cref="long">long</see>
        ///    secondsBeforePasswordResetExpiry: The number of seconds before a password reset expires
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public PasswordResetManager(
            IVerificationCodeGenerator verificationCodeGenerator,
            long secondsBeforePasswordResetExpiry,
            IPasswordResetAccessor passwordResetAccessor)
        {
            _verificationCodeGenerator = verificationCodeGenerator;
            _passwordResetAccessor = passwordResetAccessor;
            _secondsBeforePasswordResetExpiry = secondsBeforePasswordResetExpiry;
        }

        /// <summary>
        ///     Begins a new password reset process
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to reset their password
        /// </param>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public void BeginPasswordReset(string username)
        {
            string verificationCode = _verificationCodeGenerator.GenerateCode();

            _passwordResetAccessor.InsertPasswordReset(username, verificationCode);
        }

        /// <summary>
        ///     Acceptsa username, email, and verification code to verify a password reset
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
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public bool VerifyPasswordReset(string username, string email, string verificationCode)
        {
            return _passwordResetAccessor.VerifyPasswordReset(
                username,
                email,
                verificationCode,
                _secondsBeforePasswordResetExpiry);
        }
    }
}
