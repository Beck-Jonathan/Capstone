﻿using DataAccessInterfaces;
using DataObjects;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
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
    ///     Provides access to test data stored in memory representing the login table
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
    ///  Add UpdateLoginPasswordHash and GetLoginEmailByUsername methods, change _fakeLoginData
    ///  from IEnumerable to List
    /// </remarks>
   public class LoginAccessorFake : ILoginAccessor
    {
        IEnumerable<Login_VM> _fakeLoginData;
        List<Login_VM> fakeLoginData;

        /// <summary>
        ///     Instantiates a fake login accessor. Accepts a collection of login objects mimicking a data source.
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public LoginAccessorFake(List<Login_VM> fakeLoginData)
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
            List<Employee_VM> fakeEmployees = (List<Employee_VM>)employeeFakes.GetAllEmployees();
            fakeLoginData = new List<Login_VM>();

            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                fakeLoginData.Add(new Login_VM()
                {
                    Username = (fakeEmployees[i].Given_Name + fakeEmployees[i].Family_Name + fakeEmployees[i].Zip).ToLower(),
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
		/// <see cref="string">string</see> username: The username of the user
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

            var retrievedLogin = _fakeLoginData.FirstOrDefault(login => login.Username == username);

            if (retrievedLogin != null)
            {
                retrievedLogin.PasswordHash = passwordHash;
                rowsAffected = 1;
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
            var retrievedLogin = _fakeLoginData.FirstOrDefault(login => login.Username == username);

            return retrievedLogin == null ? null : retrievedLogin.Employee != null ? retrievedLogin.Employee.Email : retrievedLogin.Client.Email;
		}

        /// <summary>
        ///     retrieves user's security questions using a given Email.
        /// </summary>
        /// <param name="email">
        ///    The email of the user who forgot their Username.
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
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

        /// <summary>
        ///     retrieves a list of all usernames
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{string}">string[]</see>: preexisting usernames
        /// <br /><br />
        ///    CONTRIBUTOR: Michael Springer
        /// <br />
        ///    CREATED: 2024-04-12
        /// </remarks>
        public IEnumerable<string> SelectAllUserNames()
        {
            List<string> results = new List<string>();
            // get the user names
            foreach(var login  in fakeLoginData) { results.Add(login.Username); }
            
            return results;
        }
        /// <summary>
        ///  Creates new entry for employee login data
        /// </summary>
        /// <param name="username"></param>
        /// <param name="employeeID"></param>
        /// <returns> int="rowsAffected" </returns>
        /// <remarks>
        /// <br />
        ///     CONTRIBUTOR: Michael Springer
        /// <br />
        ///     CREATED: 2024-04-13
        /// </remarks>
        public int InsertEmployeeLogin(string username, int employeeID)
        {
            bool original = true;
            foreach(var user in fakeLoginData)
            {
                if(user.Username.ToLower().Equals(username.ToLower())) {
                    original = false;
                    break;
                }
                if(user.EmployeeID == employeeID)
                {
                    original = false;
                    break;
                }
            }
            Login_VM newLogin = new Login_VM();
            newLogin.Username = username;
            newLogin.EmployeeID = employeeID;
            fakeLoginData.Add(newLogin);

            return original ? 1 : 0;
            
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
            List<int?> ids = new List<int?>();
            foreach (var row in _fakeLoginData)
            {
                ids.Add(row.ClientID);
            }
            return ids;
        }


        /// <summary>
        ///   retrieves a list of Employee ids
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
            List<int?> ids = new List<int?>();
            foreach(var row in _fakeLoginData)
            {
                ids.Add(row.EmployeeID);
            }
            return ids;
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
            foreach (var row in _fakeLoginData)
            {
                if (row.Employee.Email.Equals(email))
                {
                    userName = row.Username;
                    break;
                }
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
            foreach (var row in _fakeLoginData)
            {
                if (row.Client.Email.Equals(email))
                {
                    userName = row.Username;
                    break;
                }
            }
            return userName;
        }

        public Client_VM AuthenticateClient(string username, string passwordHash)
        {
            return
                _fakeLoginData
                .Where(login => login.Username == username && login.PasswordHash == passwordHash)
                .FirstOrDefault()
                .Client;
        }
    }
}
