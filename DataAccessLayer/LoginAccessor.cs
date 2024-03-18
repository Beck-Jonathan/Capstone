using DataAccessInterfaces;
using DataAccessLayer.Helpers;
using DataObjects;
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
    /// CREATED: 2024-02-01
    /// <br />
    ///     Provides CRUD operations on the data source for login data
    /// </summary>
    /// <remarks>
    /// UPDATER: Jared Hutton
    /// <br />
    /// UPDATED: 2024-02-16
    /// <br />
    ///  Add AuthenticateEmployee method
    /// <br /> <br />
    /// UPDATER: Jared Hutton
    /// <br />
    /// UPDATED: 2024-02-24
    /// <br />
    ///  Add UpdateLoginPasswordHash and GetLoginEmailByUsername methods
    /// </remarks>
    public class LoginAccessor : ILoginAccessor
    {
        /// <summary>
        ///     Authenticates given username and password hash and retrieves the authenticated employee data
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="Employee_VM">Employee_VM</see>: The authenticated employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The password hash generated on the password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-16
        /// </remarks>
        /// /// <br /><br />
        ///    UPDATER: Steven Sanchez
        /// <br />
        ///    UPDATED: 2024-02-27
        /// <br />
        ///     added new Login object for username
        /// </remarks>
        public Employee_VM AuthenticateEmployee(string username, string passwordHash)
        {
            Employee_VM employee = null;
            List<Role_VM> roles = new List<Role_VM>();

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_employee";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    if (!reader.IsDBNull(0))
                    {
                        roles.Add(new Role_VM
                        {
                            RoleID = reader.GetString(0)
                        });
                    }

                    employee = new Employee_VM
                    {
                        Employee_ID = reader.GetInt32(1),
                        Given_Name = reader.GetString(2),
                        Family_Name = reader.GetString(3),
                        Address = reader.GetString(4),
                        Address2 = reader.GetStringNullable(5),
                        City = reader.GetString(6),
                        State = reader.GetString(7),
                        Country = reader.GetString(8),
                        Zip = reader.GetString(9),
                        Phone_Number = reader.GetString(10),
                        Email = reader.GetString(11),
                        Position = reader.GetString(12),
                        Login = new Login()
                        {
                            Username = reader.GetString(13),
                        },
                        DOB = reader.GetDateTime(14)
                    };

                    while (reader.Read())
                    {
                        roles.Add(new Role_VM
                        {
                            RoleID = reader.GetString(0)
                        });
                    }

                    employee.Roles = roles;
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

            return employee;
        }

        /// <summary>
        ///     Authenticates given username and password hash and retrieves related security questions if authenticated to a client
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="Employee_VM">Employee_VM</see>: The authenticated employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password hash generated on the password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
       public string[] AuthenticateClientForSecurityQuestions(string username, string passwordHash)
        {
            string[] securityQuestions = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_client_for_security_questions";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    securityQuestions = new string[]
                    {
                        reader.GetStringNullable(0),
                        reader.GetStringNullable(1),
                        reader.GetStringNullable(2)
                    };
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

            return securityQuestions;
        }

        /// <summary>
        ///     Authenticates given username, password hash, and security responses if authenticated to a client
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <param name="securityResponse1">
        ///    The response to the first security question
        /// </param>
        /// <param name="securityResponse2">
        ///    The response to the second security question
        /// </param>
        /// <param name="securityResponse3">
        ///    The response to the third security question
        /// </param>
        /// <returns>
        ///    <see cref="Client_VM">Client_VM</see>: The authenticated client
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password hash generated on the password given by the user
        /// <br />
        ///    <see cref="string">string</see> securityResponse1: The response to the first security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse2: The response to the second security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse3: The response to the third security question
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
       public Client_VM AuthenticateClientWithSecurityResponses(
            string username,
            string passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3)
        {
            Client_VM client = null;
            List<ClientRole_VM> roles = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_client_with_security_responses";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_1", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_2", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_3", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;
            cmd.Parameters["@Security_Response_1"].Value = securityResponse1;
            cmd.Parameters["@Security_Response_2"].Value = securityResponse2;
            cmd.Parameters["@Security_Response_3"].Value = securityResponse3;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    if (!reader.IsDBNull(0))
                    {
                        roles.Add(new ClientRole_VM
                        {
                            ClientRoleID = reader.GetStringNullable(0)
                        });
                    }

                    client = new Client_VM
                    {
                        ClientID = reader.GetInt32(1),
                        GivenName = reader.GetString(2),
                        FamilyName = reader.GetString(3),
                        Address = reader.GetString(4),
                        City = reader.GetString(5),
                        VoiceNumber = reader.GetString(6),
                        Email = reader.GetString(7)
                    };

                    while (reader.Read())
                    {
                        roles.Add(new ClientRole_VM
                        {
                            ClientRoleID = reader.GetString(0)
                        });
                    }

                    client.Roles = roles;
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

            return client;
        }

        /// <summary>
        ///     Authenticates given username and password hash and retrieves related security questions if authenticated to an employee
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The password hash generated on the password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public string[] AuthenticateEmployeeForSecurityQuestions(string username, string passwordHash)
        {
            string[] securityQuestions = new string[3];

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_employee_for_security_questions";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    securityQuestions = new string[]
                    {
                        reader.GetStringNullable(0),
                        reader.GetStringNullable(1),
                        reader.GetStringNullable(2)
                    };
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

            return securityQuestions;
        }

        /// <summary>
        ///     Authenticates given username, password hash, and security responses if authenticated to an employee
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <param name="securityResponse1">
        ///    The response to the first security question
        /// </param>
        /// <param name="securityResponse2">
        ///    The response to the second security question
        /// </param>
        /// <param name="securityResponse3">
        ///    The response to the third security question
        /// </param>
        /// <returns>
        ///    <see cref="Employee_VM">Employee_VM</see>: The authenticated employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The password hash generated on the password given by the user
        /// <br />
        ///    <see cref="string">string</see> securityResponse1: The response to the first security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse2: The response to the second security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse3: The response to the third security question
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public Employee_VM AuthenticateEmployeeWithSecurityResponses(
            string username,
            string passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3)
        {
            Employee_VM employee = null;
            List<Role_VM> roles = new List<Role_VM>();

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_employee_with_security_responses";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_1", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_2", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Security_Response_3", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;
            cmd.Parameters["@Security_Response_1"].Value = securityResponse1;
            cmd.Parameters["@Security_Response_2"].Value = securityResponse2;
            cmd.Parameters["@Security_Response_3"].Value = securityResponse3;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    if (!reader.IsDBNull(0))
                    {
                        roles.Add(new Role_VM
                        {
                            RoleID = reader.GetString(0)
                        });
                    }

                    employee = new Employee_VM
                    {
                        Employee_ID = reader.GetInt32(1),
                        Given_Name = reader.GetString(2),
                        Family_Name = reader.GetString(3),
                        Address = reader.GetString(4),
                        Address2 = reader.GetStringNullable(5),
                        City = reader.GetString(6),
                        State = reader.GetString(7),
                        Country = reader.GetString(8),
                        Zip = reader.GetString(9),
                        Phone_Number = reader.GetString(10),
                        Email = reader.GetString(11),
                        Position = reader.GetString(12)
                    };

                    while (reader.Read())
                    {
                        roles.Add(new Role_VM
                        {
                            RoleID = reader.GetString(0)
                        });
                    }

                    employee.Roles = roles;
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

            return employee;
        }

        /// <summary>
        ///     Changes the associated user's password hash
        /// </summary>
        /// <param name="username">
        ///    The username of the user
        /// </param>
        /// <param name="passwordHash">
        ///    The user's new password hash
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows affected by the operation
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username of the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The user's new password hash
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public int UpdateLoginPasswordHash(string username, string passwordHash)
        {
            int rowsAffected = 0;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_update_login_password_hash";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
            cmd.Parameters.Add("@Password_Hash", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;
            cmd.Parameters["@Password_Hash"].Value = passwordHash;

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
        ///     Retrieves the email associated with a user
        /// </summary>
        /// <param name="username">
        ///    The username of the user
        /// </param>
        /// <returns>
        ///    <see cref="string">string</see>: The user's registered email
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username of the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public string GetLoginEmailByUsername(string username)
        {
            string email = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_get_login_email_by_username";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Username"].Value = username;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    email = reader.GetString(0);
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

            return email;
       }
    }
}
