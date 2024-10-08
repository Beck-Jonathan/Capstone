﻿using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayer.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    ///     Manages and performs operations on login objects
    /// </summary>
    /// <remarks>
    /// UPDATED: 2024-04-09
    /// <br />
    /// UPDATER: Michael Springer
    /// Added functionality for retrieving a list of usernames for validation purposes
    /// Added functionality for adding employee login
    /// </remarks>
    ///     
    public interface ILoginManager
    {
        Employee_VM AuthenticateEmployee(string username, string password);
        string[] AuthenticateClientForSecurityQuestions(string username, string password);
        Client_VM AuthenticateClientWithSecurityResponses(
            string username,
            string password,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);
        string[] AuthenticateEmployeeForSecurityQuestions(string username, string password);
        Employee_VM AuthenticateEmployeeWithSecurityResponses(
            string username,
            string password,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);
        void EditLoginPassword(string username, string password);
        string GetLoginEmailByUsername(string username);
        string[] GetSecurityQuestionsforUsernameRetrieval(string email);
        string GetUsername(string email,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);

        IEnumerable<string> GetAllUserNames();
        int AddEmployeeLogin(string username, int employeeID);
        
        List<int?> GetAllClientIdFromLogin();
        List<int?> GetAllEmployeeIdFromLogin();

        String GetEmployeeUserNameByEmail(string email);
        String GetClientUserNameByEmail(string email);
        Client_VM AuthenticateClient(string username, string password);

    }

    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Manages and performs operations on login objects
    /// </summary>
    /// <remarks>
    /// UPDATER: Jared Hutton
    /// <br />
    /// UPDATED: 2024-02-16
    /// <br />
    ///  Add AuthenticateEmployee method
    /// </remarks>
    public class LoginManager : ILoginManager
    {
        private ILoginAccessor _loginAccessor;
        private IPasswordHasher _passwordHasher;


        /// <summary>
        ///     Instantiates a LoginManager
        /// </summary>
        /// <param name="passwordHasher">
        ///    The utility used to hash passwords
        /// </param>
        /// <returns>
        ///    <see cref="LoginManager">LoginManager</see>: A LoginManager object
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="IPasswordHasher">IPasswordHasher</see> passwordHasher: The utility used to hash passwords
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public LoginManager(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _loginAccessor = new LoginAccessor();
        }

        /// <summary>
        ///     Instantiates a LoginManager
        /// </summary>
        /// <param name="passwordHasher">
        ///    The utility used to hash passwords
        /// </param>
        /// <param name="loginAccessor">
        ///    The accessor used to send and retrieve login objects to/from the data source
        /// </param>
        /// <returns>
        ///    <see cref="LoginManager">LoginManager</see>: A LoginManager object
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="IPasswordHasher">IPasswordHasher</see> passwordHasher: The utility used to hash passwords
        /// <br />
        ///    <see cref="ILoginAccessor">ILoginAccessor</see> loginAccessor: The accessor used to send and retrieve login objects to/from the data source
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public LoginManager(IPasswordHasher passwordHasher, ILoginAccessor loginAccessor)
        {
            _passwordHasher = passwordHasher;
            _loginAccessor = loginAccessor;
        }

        /// <summary>
        ///     Authenticates given username and password and retrieves the employee data
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="Employee_VM">Employee_VM</see>: The authenticated employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-17
        /// </remarks>
        /// 


        /// <summary>
        ///     Instantiates a LoginManager
        /// </summary>
        /// <returns>
        ///    <see cref="LoginManager">LoginManager</see>: A LoginManager object
        /// </returns>
        /// <remarks>
        ///   Default constructor initializes a LoginAccessor
        /// <br />
        ///    CONTRIBUTOR: Michael Springer
        /// <br />
        ///    CREATED: 2024-04-12
        /// </remarks>
        public LoginManager()
        {
            _loginAccessor = new LoginAccessor();
        }


        public Employee_VM AuthenticateEmployee(string username, string password)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            var authenticatedEmployee = _loginAccessor.AuthenticateEmployee(username, passwordHash);

            if (authenticatedEmployee == null)
            {
                throw new ArgumentException("Could not authenticate employee");
            }

            return authenticatedEmployee;
        }

        /// <summary>
        ///     Authenticates given username and password and retrieves related security questions if authenticated to a client
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public string[] AuthenticateClientForSecurityQuestions(string username, string password)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            string[] securityQuestions = _loginAccessor.AuthenticateClientForSecurityQuestions(username, passwordHash);

            if (securityQuestions == null)
            {
                throw new ArgumentException("Could not authenticate client");
            }

            return securityQuestions;
        }

        /// <summary>
        ///     Authenticates given username, password, and security responses if authenticated to a client
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
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
        ///    <see cref="string">string</see> password: The password given by the user
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
        public Client_VM AuthenticateClientWithSecurityResponses(string username, string password, string securityResponse1, string securityResponse2, string securityResponse3)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            Client_VM client = _loginAccessor.AuthenticateClientWithSecurityResponses(
                username,
                passwordHash,
                securityResponse1,
                securityResponse2,
                securityResponse3);

            if (client == null)
            {
                throw new ArgumentException("Could not authenticate client");
            }

            return client;
        }

        /// <summary>
        ///     Authenticates given username and password and retrieves related security questions if authenticated to an employee
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
        /// </param>
        /// <returns>
        ///    <see cref="string[]">string[]</see>: The security questions
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password given by the user
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        public string[] AuthenticateEmployeeForSecurityQuestions(string username, string password)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            string[] securityQuestions = _loginAccessor.AuthenticateEmployeeForSecurityQuestions(username, passwordHash);

            if (securityQuestions == null)
            {
                throw new ArgumentException("Could not authenticate employee");
            }

            return securityQuestions;
        }

        /// <summary>
        ///     Authenticates given username, password, and security responses if authenticated to an employee
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
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
        ///    <see cref="string">string</see> password: The password given by the user
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
        public Employee_VM AuthenticateEmployeeWithSecurityResponses(string username, string password, string securityResponse1, string securityResponse2, string securityResponse3)
        {
            string passwordHash = _passwordHasher.HashPassword(password);
            
            Employee_VM employee = _loginAccessor.AuthenticateEmployeeWithSecurityResponses(
                username,
                passwordHash,
                securityResponse1,
                securityResponse2,
                securityResponse3);

            if (employee == null)
            {
                throw new ArgumentException("Could not authenticate employee");
            }

            return employee;
        }
		
		/// <summary>
        ///     Changes the associated user's password
        /// </summary>
        /// <param name="username">
        ///    The username of the user
        /// </param>
        /// <param name="password">
        ///    The user's new password
        /// </param>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username of the user
        /// <br />
        ///    <see cref="string">string</see> password: The user's new password
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public void EditLoginPassword(string username, string password)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            int rowsAffected = _loginAccessor.UpdateLoginPasswordHash(username, passwordHash);

            if (rowsAffected == 0)
            {
                throw new ArgumentException("Username not found");
            }
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
            string email = _loginAccessor.GetLoginEmailByUsername(username);

            if (email == null)
            {
                throw new ArgumentException("Username not found");
            }

            return email;
        }

        /// <summary>
        ///     Authenticates given username, password, and security responses if authenticated to an employee
        /// </summary>
        /// <param name="email">
        ///    The email of the user whose username was forgotten.
        /// </param>
        /// <returns>
        ///    <see cref="bool">boolean</see> success: The user's username was successfully retrieved (and will be emailed to the email given.)
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="string">string</see> email: The email given by the user
        /// <br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-26
        /// </remarks>
        public string[] GetSecurityQuestionsforUsernameRetrieval(string email)
        {
            try
            {
                string[] questions = _loginAccessor.VerifyUsernameRetrieval(email);
                return questions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Authenticates given username, password, and security responses if authenticated to an employee
        /// </summary>
        /// <param name="email">
        ///    The email of the user whose username was forgotten.
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
        ///    <see cref="bool">boolean</see> success: The user's username was successfully retrieved (and will be emailed to the email given.)
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="string">string</see> email: The email given by the user
        /// <br />
        ///    <see cref="string">string</see> securityResponse1: The response to the first security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse2: The response to the second security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse3: The response to the third security question
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        public string GetUsername(string email, string securityResponse1, string securityResponse2, string securityResponse3)
        {
            try
            {
                string username = _loginAccessor.RetrieveUsername(email, securityResponse1, securityResponse2, securityResponse3);
                return username;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves all active userNames for validation
        /// </summary>
        /// <returns>
        /// a list of all usernames
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <br />
        /// CONTRIBUTOR: Michael Springer
        /// <br />
        /// Created 2024-04-09
        /// 
        public IEnumerable<string> GetAllUserNames()
        {
            IEnumerable<string> usernames = new List<string>();
            try
            {
                usernames = _loginAccessor.SelectAllUserNames();
            }
            catch(Exception ex) {

                throw new ApplicationException(ex.Message);
            }
            return usernames;
        }

        public int AddEmployeeLogin(string username, int employeeID)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = _loginAccessor.InsertEmployeeLogin(username, employeeID);
            }catch(Exception ex)
            {
                throw ex;
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
            List<int?> clientIds = new List<int?>();
            try
            {
                clientIds = _loginAccessor.GetAllClientIdFromLogin();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return clientIds;
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
            List<int?> employeeIds = new List<int?>();
            try
            {
                employeeIds = _loginAccessor.GetAllEmployeeIdFromLogin();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return employeeIds;
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

        public Client_VM AuthenticateClient(string username, string password)
        {
            string passwordHash = _passwordHasher.HashPassword(password);

            var authenticatedClient = _loginAccessor.AuthenticateClient(username, passwordHash);

            if (authenticatedClient == null)
            {
                throw new ArgumentException("Could not authenticate client");
            }

            return authenticatedClient;
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
            try
            {
               userName = _loginAccessor.GetEmployeeUserNameByEmail(email);
            }catch(Exception ex)
            {
                throw ex;
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
            try
            {
                userName = _loginAccessor.GetClientUserNameByEmail(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userName;
        }
    }
}
