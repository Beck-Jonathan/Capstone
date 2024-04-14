using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Provides CRUD operations on the data source for login objects
    /// </summary>
    public interface ILoginAccessor
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
        /// </remarks>
        Employee_VM AuthenticateEmployee(string username, string passwordHash);

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
        ///    <see cref="Employee_VM">string[]</see>: The authenticated employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> passwordHash: The password hash generated on the password given by the user
        /// </remarks>
        string[] AuthenticateClientForSecurityQuestions(string username, string passwordHash);

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
        /// </remarks>
        Client_VM AuthenticateClientWithSecurityResponses(
            string username,
            string passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);

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
        /// </remarks>
        string[] AuthenticateEmployeeForSecurityQuestions(string username, string passwordHash);

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
        ///    <see cref="Client_VM">Client_VM</see>: The authenticated client
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
        /// </remarks>
        Employee_VM AuthenticateEmployeeWithSecurityResponses(
            string username,
            string passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);

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
        /// </remarks>
        int UpdateLoginPasswordHash(string username, string passwordHash);

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
        /// </remarks>
        string GetLoginEmailByUsername(string username);

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
        /// <br />
		///    <see cref="string">string</see> email: The email given by the user, which their account is registered with.
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-25
        /// </remarks>

        string[] VerifyUsernameRetrieval(string email);
        /// <summary>
        ///     retrieves username using a given security responses and email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user who forgot their Username.
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
		/// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
		///    <see cref="string">string</see> email: The email given by the user, which their account is registered with.
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-25
        /// </remarks>
        string RetrieveUsername(string email,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);
        /// <summary>
        ///     retrieves all client ids in the login table
        /// </summary>
        /// <returns>
        ///    List of Ints
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        ///    CREATED: 2024-04-10
        /// </remarks>
        List<int?> GetAllClientIdFromLogin();
        /// <summary>
        ///     retrieves all employee ids in the login table
        /// </summary>
        /// <returns>
        ///    List of Ints
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        ///    CREATED: 2024-04-10
        /// </remarks>
        List<int?> GetAllEmployeeIdFromLogin();

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
        String GetEmployeeUserNameByEmail(string email);

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
        String GetClientUserNameByEmail(string email);

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
        Client_VM AuthenticateClient(string username, string passwordHash);
    }
}
