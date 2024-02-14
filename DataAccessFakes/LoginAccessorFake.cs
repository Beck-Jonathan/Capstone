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
    /// CREATED: 2024-02-01
    /// <br />
    ///     Provides access to test data stored in memory representing the client table
    /// </summary>
    public class LoginAccessorFake : ILoginAccessor
    {
        IEnumerable<Login_VM> _fakeLoginData;

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// CREATED: 2024-02-01
        /// <br />
        ///     Instantiates a fake login accessor. Accepts a collection of login objects mimicking a data source.
        /// </summary>
        public LoginAccessorFake(IEnumerable<Login_VM> fakeLoginData)
        {
            _fakeLoginData = fakeLoginData;
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
        ///    <see cref="string[]">string[]</see>: The security questions
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
            return _fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.ClientID != null
                    && login.Username == username
                    && login.PasswordHash == passwordHash)
                .Select(login =>
                    new string[] { login.SecurityQuestion1, login.SecurityQuestion2, login.SecurityQuestion3 })
                .FirstOrDefault();
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
            return _fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.ClientID != null
                    && login.Username == username
                    && login.PasswordHash == passwordHash
                    && login.SecurityResponse1 == securityResponse1
                    && login.SecurityResponse2 == securityResponse2
                    && login.SecurityResponse3 == securityResponse3)
                .Select(login => login.Client)
                .FirstOrDefault();
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
            return _fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Username == username
                    && login.PasswordHash == passwordHash)
                .Select(login =>
                    new string[] { login.SecurityQuestion1, login.SecurityQuestion2, login.SecurityQuestion3 })
                .FirstOrDefault();
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
            return _fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Username == username
                    && login.PasswordHash == passwordHash
                    && login.SecurityResponse1 == securityResponse1
                    && login.SecurityResponse2 == securityResponse2
                    && login.SecurityResponse3 == securityResponse3)
                .Select(login => login.Employee)
                .FirstOrDefault();
        }
    }
}
