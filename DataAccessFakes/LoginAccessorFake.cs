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
    /// <remarks>
    /// UPDATER: Jared Hutton
    /// <br />
    /// UPDATED: 2024-02-16
    /// <br />
    ///  Add AuthenticateEmployee method
    /// </remarks>
   public class LoginAccessorFake : ILoginAccessor
    {
        IEnumerable<Login_VM> _fakeLoginData;
        List<Login_VM> fakeLoginData;

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
        /// AUTHOR: Parker Svoboda
        /// <br />
        /// CREATED: 2024-02-26
        /// <br />
        ///     Instantiates a fake login accessor. 
        ///     <see cref="EmployeeAccessorFake">creates a collection of login objects using EmployeeAccessorFake.</see>
        /// </summary>
        public LoginAccessorFake()
        {
            EmployeeAccessorFake employeeFakes = new EmployeeAccessorFake();
            List<Employee_VM> fakeEmployees = employeeFakes.GetAllEmployees();
            fakeLoginData = new List<Login_VM>();
            
            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                fakeLoginData.Add(new Login_VM()
                {
                    Username = (fakeEmployees[i].Given_Name+fakeEmployees[i].Family_Name+ fakeEmployees[i].Zip).ToLower(),
                    PasswordHash = "e312910dfb28cc0c938c297f846ca61467023e47d68908e1900ab1b72d0ed7d3",
                    EmployeeID = fakeEmployees[i].Employee_ID,
                    ClientID = null,
                    SecurityQuestion1 = "question1",
                    SecurityQuestion2 = "question2",
                    SecurityQuestion3 = "question3",
                    SecurityResponse1 = "response1",
                    SecurityResponse2 = "response2",
                    SecurityResponse3 = "response3",
                    Employee = fakeEmployees[i],
                });
            }
        }

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
        ///    <see cref="string">string</see> password: The password hash generated on the password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-16
        /// </remarks>
        public Employee_VM AuthenticateEmployee(string username, string passwordHash)
        {
            return
                _fakeLoginData
                .Where(login => login.Username == username && login.PasswordHash == passwordHash)
                .FirstOrDefault()
                .Employee;
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
        public string[] VerifyUsernameRetrieval(string email)
        {
            return fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Employee.Email == email)
                .Select(login =>
                    new string[] { login.SecurityQuestion1, login.SecurityQuestion2, login.SecurityQuestion3 })
                .FirstOrDefault()[1] != null ? fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Employee.Email == email)
                .Select(login =>
                    new string[] { login.SecurityQuestion1, login.SecurityQuestion2, login.SecurityQuestion3 })
                .FirstOrDefault() : throw new ArgumentException("Invalid Email!");
        }

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
        public string RetrieveUsername(string email,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3)
        {
            string[] SecurityResponses = fakeLoginData
                .Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Employee.Email == email)
                .Select(login =>
                    new string[] { login.SecurityResponse1, login.SecurityResponse2, login.SecurityResponse3 })
                .FirstOrDefault();
            return (securityResponse1 == SecurityResponses[0] && securityResponse2 == SecurityResponses[1] && securityResponse3 == SecurityResponses[2]) ? 
                fakeLoginData.Where(login =>
                    login.IsActive
                    && login.EmployeeID != null
                    && login.Employee.Email == email)
                .Select(login => login.Username)
                .FirstOrDefault() :
                throw new ArgumentException("1 or more answers are wrong!"); 
        }
    }
}
