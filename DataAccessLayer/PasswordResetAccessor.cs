using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Provides CRUD operations on the data source for password reset objects
    /// </summary>
    public class PasswordResetAccessor : IPasswordResetAccessor
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
        /// <br /> <br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public int InsertPasswordReset(string username, string verificationCode)
        {
            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_insert_password_reset";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Verification_Code", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Verification_Code"].Value = verificationCode;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
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
            bool passwordResetVerified = false;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_verify_password_reset";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Verification_Code", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Seconds_Before_Password_Reset_Expiry", System.Data.SqlDbType.BigInt);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@Verification_Code"].Value = verificationCode;
            cmd.Parameters["@Seconds_Before_Password_Reset_Expiry"].Value = secondsBeforePasswordResetExpiry;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    passwordResetVerified = reader.GetBoolean(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return passwordResetVerified;
        }
   }
}
