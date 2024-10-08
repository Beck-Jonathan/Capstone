﻿using DataAccessInterfaces;
using DataAccessLayer.Helpers;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        ///     retrieves user's security questions using a given Email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user who forgot their Username.
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-27
        /// </remarks>
        public string[] VerifyUsernameRetrieval(string email)
        {
            string[] questions = new string[3];
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_get_security_questions_for_username_retrieval";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    reader.Read();
                    questions[0] = reader.GetString(0);
                    questions[1] = reader.GetString(1);
                    questions[2] = reader.GetString(2);
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
            return questions;
        }

        /// <summary>
        ///     retrieves username using given security responses and email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user who forgot their Username.
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
        ///    <see cref="string[]">string[]</see>: The security questions
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> email: The email given by the user, which their account is registered with.
        /// <br />
        ///    <see cref="string">string</see> securityResponse1: The response to the first security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse2: The response to the second security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse3: The response to the third security question
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-25
        /// </remarks>
        /// <br /><br />
        ///    UPDATER: Parker Svoboda
        /// <br />
        ///    UPDATED: 2024-03-04
        /// <br />
        /// Parameters changed to use Security Responses, method changed to use said Security Responses
        /// </remarks>
        public string RetrieveUsername(string email,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3)
        {
            string username = null;
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_get_username";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Security_Response_1", securityResponse1);
            cmd.Parameters.AddWithValue("@Security_Response_2", securityResponse2);
            cmd.Parameters.AddWithValue("@Security_Response_3", securityResponse3);

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    reader.Read();
                    username = reader.GetString(0);
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
            return username;
        }

        /// <summary>
        ///     retrieves a list of all user names
        /// <returns>
        ///    <see cref="IEnumerable{string}">string[]</see>: Usernames
        /// </returns>
        /// <remarks>
        /// <br /><br />
        ///    CONTRIBUTOR: Michael Springer
        /// <br />
        ///    CREATED: 2024-04-12
        /// </remarks>
        public IEnumerable<string> SelectAllUserNames()
        {
           List<string> usernames = new List<string>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_usernames";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string username = reader.GetString(0);
                    usernames.Add(username);
                }

                if (usernames.Count == 0)
                {
                    throw new ArgumentException("Username not found");
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
            return usernames;
        }
        /// <summary>
        ///    Adds employee login informationb
        /// <returns>
        ///    <see cref="int">string[]</see>: rowsaffected
        /// </returns>
        /// <remarks>
        /// <br /><br />
        ///    CONTRIBUTOR: Michael Springer
        /// <br />
        ///    CREATED: 2024-04-13
        /// </remarks>
        /// 
        public int InsertEmployeeLogin(string username, int employeeID)
        {
            // usernames are not case-sensitive
            username = username.ToLower();
            int rowsAffected = 0;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_employee_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_Username", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@P_Employee_ID", SqlDbType.Int);

            cmd.Parameters["@P_Username"].Value = username;
            cmd.Parameters["@P_Employee_ID"].Value = employeeID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected == 0)
                {
                    throw new ArgumentException("Error inserting login record");
                }
            }catch(Exception ex)
            {
                throw new ApplicationException("Error executing command to insert new login record", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        ///   retrieves a list of client ids
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="List{int}"/>: Returns the list of client ids
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    CREATOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-10
        /// </remarks>
        public List<int?> GetAllClientIdFromLogin()
        {
            List<int?> clientIDs = new List<int?>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_client_id_from_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    clientIDs.Add(reader.GetInt32Nullable(0));
                }

                if (clientIDs.Count == 0)
                {
                    throw new ArgumentException("No client records found");
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
            return clientIDs;
        }
        /// <summary>
        ///   retrieves a list of employee ids
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="List{int}"/>: Returns the list of employee ids
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    CREATOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-10
        /// </remarks>
        public List<int?> GetAllEmployeeIdFromLogin()
        {
            List<int?> employeeIDs = new List<int?>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_employee_id_from_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employeeIDs.Add(reader.GetInt32Nullable(0));
                }

                if (employeeIDs.Count == 0)
                {
                    throw new ArgumentException("No employee records found");
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
            return employeeIDs;
        }
        /// <summary>
        ///     retrieves employee username from email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user 
        /// </param>
        /// <returns>
        ///    string username
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        ///    CREATED: 2024-04-12
        /// </remarks>
        public string GetEmployeeUserNameByEmail(string email)
        {
            string userName = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_get_employee_username_MVC";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    userName = reader.GetString(0);
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

            return userName;
        }
        /// <summary>
        ///     retrieves client username from email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user 
        /// </param>
        /// <returns>
        ///    string username
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        ///    CREATED: 2024-04-12
        /// </remarks>
        public string GetClientUserNameByEmail(string email)
        {
            string userName = null;

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_get_client_username_MVC";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    userName = reader.GetString(0);
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

            return userName;
        }


        /// <summary>
        ///     Authenticates given username and password hash and retrieves the authenticated client data
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="passwordHash">
        ///    The password hash of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="Client_VM">Client_VM</see>: The authenticated client
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The password hash generated on the password given by the user
        /// </remarks>
        ///<remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        ///    CREATED: 2024-04-12
        /// </remarks>
        public Client_VM AuthenticateClient(string username, string passwordHash)
        {
            Client_VM client = null;
            List<ClientRole_VM> roles = new List<ClientRole_VM>();

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_authenticate_client";

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
                        roles.Add(new ClientRole_VM
                        {
                            ClientRoleID = reader.GetString(0)
                        });
                    }

                    client = new Client_VM
                    {
                        ClientID = reader.GetInt32(1),
                        GivenName = reader.GetString(2),
                        FamilyName = reader.GetString(3),
                        MiddleName = reader.GetString(4),
                        DOB = reader.GetDateTime(5),
                        Email = reader.GetString(6),
                        PostalCode = reader.GetString(7),
                        City = reader.GetString(8),
                        Region = reader.GetString(9),
                        Address = reader.GetString(10),
                        TextNumber = reader.GetString(11),
                        VoiceNumber = reader.GetString(12),
                        IsActive = reader.GetBoolean(13),
                        Login = new Login()
                        {
                            Username = reader.GetString(14),
                        }
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
    }
}
