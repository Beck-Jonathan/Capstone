using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayer.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Class for testing LoginManager functionality
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
    /// UPDATED: 2024-02-16
    /// <br />
    ///  Add AuthenticateEmployee method
    /// </remarks>
    [TestClass]
    public class LoginManager_Tests
    {
        private ILoginManager _loginManager;
        private IPasswordHasher _passwordHasher;

        /// <summary>
        ///     Initialize the required test objects
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestInitialize]
        public void TestInitialize()
        {
            _passwordHasher = new PasswordHasher();
        }

        /// <summary>
        ///     Test that authenticating an employee's username and password returns the correct employee when
        ///     the username and password are found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-16
        /// </remarks>
        [TestMethod]
        public void AuthenticateEmployee_ReturnsCorrectEmployee()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            int expectedEmployeeId = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = expectedEmployeeId,
                    Employee = new Employee_VM
                    {
                        Employee_ID = expectedEmployeeId
                    }
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    EmployeeID = 100000,
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100000
                    }
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            Employee_VM retrievedEmployee = _loginManager.AuthenticateEmployee(username, password);

            // Assert
            Assert.AreEqual(expectedEmployeeId, retrievedEmployee.Employee_ID);
        }

        /// <summary>
        ///     Test that authenticating an employee's username and password fails when the username and password are not found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-16
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateEmployee_FailsOnInvalidLogin()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            int expectedEmployeeId = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = expectedEmployeeId
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    EmployeeID = 100000
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Act
            _loginManager.AuthenticateEmployee(username, password);
        }

        /// <summary>
        ///     Test that authenticating a client's username and password returns the correct security questions
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void AuthenticateClientForSecurityQuestions_ReturnsCorrectSecurityQuestions()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            string[] expectedSecurityQuestions = new string[] {
                "What city did you have your first job in?",
                "Where did you go to school?",
                "What was the name of your first pet?"
            };

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f" ,
                    ClientID = 100082,
                    SecurityQuestion1 = expectedSecurityQuestions[0],
                    SecurityQuestion2 = expectedSecurityQuestions[1],
                    SecurityQuestion3 = expectedSecurityQuestions[2]
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000,
                    SecurityQuestion1 = "Wrong question 1",
                    SecurityQuestion2 = "Wrong question 2",
                    SecurityQuestion3 = "Wrong question 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            string[] securityQuestions = _loginManager.AuthenticateClientForSecurityQuestions(username, password);

            // Assert
            CollectionAssert.AreEquivalent(expectedSecurityQuestions, securityQuestions);
        }

        /// <summary>
        ///     Test that authenticating a client's username and password fails when username and password is invalid
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateClientForSecurityQuestions_ThrowsExceptionOnInvalidLogin()
        {
            // Arrange
            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            _loginManager.AuthenticateClientForSecurityQuestions("wrongusername", "wrongpassword");
        }

        /// <summary>
        ///     Test that authenticating a employee's username and password returns the correct security questions
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void AuthenticateEmployeeForSecurityQuestions_ReturnsCorrectSecurityQuestions()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            string[] expectedSecurityQuestions = new string[] {
                "What city did you have your first job in?",
                "Where did you go to school?",
                "What was the name of your first pet?"
            };

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100082,
                    SecurityQuestion1 = expectedSecurityQuestions[0],
                    SecurityQuestion2 = expectedSecurityQuestions[1],
                    SecurityQuestion3 = expectedSecurityQuestions[2]
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    EmployeeID = 100000,
                    SecurityQuestion1 = "Wrong question 1",
                    SecurityQuestion2 = "Wrong question 2",
                    SecurityQuestion3 = "Wrong question 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            string[] securityQuestions = _loginManager.AuthenticateEmployeeForSecurityQuestions(username, password);

            // Assert
            CollectionAssert.AreEquivalent(expectedSecurityQuestions, securityQuestions);
        }

        /// <summary>
        ///     Test that authenticating a employee's username and password fails when username and password is invalid
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateEmployeeForSecurityQuestions_ThrowsExceptionOnInvalidLogin()
        {
            // Arrange
            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            _loginManager.AuthenticateEmployeeForSecurityQuestions("wrongusername", "wrongpassword");
        }

        /// <summary>
        ///     Test that authenticating a client's username, password, and security responses returns correct client
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void AuthenticateClientWithSecurityResponses_ReturnsCorrectClient()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            string securityResponse1 = "Cedar Rapids";
            string securityResponse2 = "Kirkwood";
            string securityResponse3 = "Caroline";

            int expectedClientID = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = expectedClientID,
                    SecurityResponse1 = securityResponse1,
                    SecurityResponse2 = securityResponse2,
                    SecurityResponse3 = securityResponse3,
                    Client = new Client_VM
                    {
                        ClientID = expectedClientID
                    }
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000,
                    SecurityResponse1 = "Wrong response 1",
                    SecurityResponse2 = "Wrong response 2",
                    SecurityResponse3 = "Wrong response 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            Client_VM returnedClient = _loginManager.AuthenticateClientWithSecurityResponses(
                username,
                password,
                securityResponse1,
                securityResponse2,
                securityResponse3);

            // Assert
            Assert.AreEqual(expectedClientID, returnedClient.ClientID);
        }

        /// <summary>
        ///     Test that authenticating a client's username, password, and security responses fails when info is incorrect
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateClientWithSecurityResponses_ThrowsExceptionOnInvalidLogin()
        {
            string username = "jackrussel49";
            string password = "galaxy";

            // Arrange
            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082,
                    SecurityResponse1 = "Cedar Rapids",
                    SecurityResponse2 = "Kirkwood",
                    SecurityResponse3 = "Caroline"
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000,
                    SecurityResponse1 = "Wrong response 1",
                    SecurityResponse2 = "Wrong response 2",
                    SecurityResponse3 = "Wrong response 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            _loginManager.AuthenticateClientWithSecurityResponses(
                username,
                password,
                "wrong answer",
                "Kirkwood",
                "Caroline");
        }

        /// <summary>
        ///     Test that authenticating a employee's username, password, and security responses returns correct employee
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void AuthenticateEmployeeWithSecurityResponses_ReturnsCorrectEmployee()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            string securityResponse1 = "Cedar Rapids";
            string securityResponse2 = "Kirkwood";
            string securityResponse3 = "Caroline";

            int expectedEmployeeID = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = expectedEmployeeID,
                    SecurityResponse1 = securityResponse1,
                    SecurityResponse2 = securityResponse2,
                    SecurityResponse3 = securityResponse3,
                    Employee = new Employee_VM
                    {
                        Employee_ID = expectedEmployeeID
                    }
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    EmployeeID = 100000,
                    SecurityResponse1 = "Wrong response 1",
                    SecurityResponse2 = "Wrong response 2",
                    SecurityResponse3 = "Wrong response 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            Employee_VM returnedEmployee = _loginManager.AuthenticateEmployeeWithSecurityResponses(
                username,
                password,
                securityResponse1,
                securityResponse2,
                securityResponse3);

            // Assert
            Assert.AreEqual(expectedEmployeeID, returnedEmployee.Employee_ID);
        }

        /// <summary>
        ///     Test that authenticating a employee's username, password, and security responses fails when info is incorrect
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateEmployeeWithSecurityResponses_ThrowsExceptionOnInvalidLogin()
        {
            string username = "jackrussel49";
            string password = "galaxy";

            // Arrange
            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100082,
                    SecurityResponse1 = "Cedar Rapids",
                    SecurityResponse2 = "Kirkwood",
                    SecurityResponse3 = "Caroline"
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    EmployeeID = 100000,
                    SecurityResponse1 = "Wrong response 1",
                    SecurityResponse2 = "Wrong response 2",
                    SecurityResponse3 = "Wrong response 3"
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            _loginManager.AuthenticateEmployeeWithSecurityResponses(
                username,
                password,
                "wrong answer",
                "Kirkwood",
                "Caroline");
        }
		
		/// <summary>
        ///     Test that editing a user's password updates the password hash to the correct value
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        public void EditLoginPassword_CorrectlyEditsPassword()
        {
            // Arrange
            string username = "jackrussel49";
            string newPassword = "galaxy";
            string expectedPasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "oldPasswordHash"
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash"
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            _loginManager.EditLoginPassword(username, newPassword);

            // Assert
            var retrievedLogin = testLoginData.Single(login => login.Username == username);

            Assert.AreEqual(expectedPasswordHash, retrievedLogin.PasswordHash);
        }

        /// <summary>
        ///     Test that editing a user's password throws an error when the user is not found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditLoginPassword_FailsOnInvalidUsername()
        {
            // Arrange
            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f"
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            _loginManager.EditLoginPassword("wrongusername", "");
        }

        /// <summary>
        ///     Test that retrieving an email associated with a login returns the correct emil
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        public void GetLoginEmailByUsername_ReturnsCorrectEmail()
        {
            // Arrange
            string username = "jackrussel49";
            string expectedEmail = "correctemail@right.com";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    Employee = new Employee_VM
                    {
                        Email = expectedEmail
                    }
                }, new Login_VM
                {
                    Username = "incorrect",
                    Employee = new Employee_VM
                    {
                        Email = "wrongemail"
                    }
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string retrievedEmail = _loginManager.GetLoginEmailByUsername(username);

            // Assert
            Assert.AreEqual(expectedEmail, retrievedEmail);
        }

        /// <summary>
        ///     Test that retrieving an email fails when username is not found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLoginEmailByUsername_FailsOnInvalidUsername()
        {
            // Arrange
            string username = "invalid";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49"
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string retrievedEmail = _loginManager.GetLoginEmailByUsername(username);
        }

        /// <summary>
        ///     Test that when email gets a match, Security Questions are recieved
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-25
        /// </remarks>
        [TestMethod]
        public void GetSecurityQuestionsforUsernameRetrieval_Success()
        {
            // Arrange
            string email = "jake@company.com";
            string[] expectedArray = new string[] { "question1", "question2", "question3" };
            LoginAccessorFake loginAccessor = new LoginAccessorFake();
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Action

            string[] questions = _loginManager.GetSecurityQuestionsforUsernameRetrieval(email);

            //assert

            Assert.AreEqual(expectedArray[0], questions[0]);
            Assert.AreEqual(expectedArray[1], questions[1]);
            Assert.AreEqual(expectedArray[2], questions[2]);

        }

        /// <summary>
        ///     Test that when email doesn't match anything in the database, throws exception
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-25
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetSecurityQuestionsforUsernameRetrieval_FailureBadEmail()
        {
            // Arrange
            string email = "jake@achoo.com";
            LoginAccessorFake loginAccessor = new LoginAccessorFake();
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Action
            _loginManager.GetSecurityQuestionsforUsernameRetrieval(email);
        }

        /// <summary>
        ///     Test that when email doesn't match anything in the database, throws exception
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-27
        /// </remarks>
        /// <br /><br />
        ///    UPDATER: Parker Svoboda
        /// <br />
        ///    UPDATED: 2024-03-04
        /// <br />
        /// Parameters changed to use Security Responses, method changed to use said Security Responses
        /// </remarks>
        [TestMethod]
        public void GetUsername_Success()
        {
            // Arrange
            string email = "jake@company.com";
            string expectedUsername = "jakedoe12345";
            LoginAccessorFake loginAccessor = new LoginAccessorFake();
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Action
            string username = _loginManager.GetUsername(email, "response1", "response2", "response3");

            //Assert
            Assert.AreEqual(expectedUsername, username);
        }

        [TestMethod]
        public void GetAllUsername_Success()
        {
            // Arrange
            List<string> failTestNames = new List<string> { "Rando", "Brando", "Sando", "Mando", "Kevin" };
            List<string> actualFakesTestNames = new List<string>();
            EmployeeAccessorFake employeeAccessorFake = new EmployeeAccessorFake();
            List<Employee_VM> fakeEmployees = (List<Employee_VM>)employeeAccessorFake.GetAllEmployees();
            foreach (var employee in fakeEmployees)
            {
                // This concatination is how usernames were generated in the login accessor fake
                string username = (employee.Given_Name + employee.Family_Name + employee.Zip).ToLower();
                actualFakesTestNames.Add(username);
            }
            foreach(var name in actualFakesTestNames)
            {
                Console.WriteLine(name);
            }
            LoginAccessorFake loginAccessor = new LoginAccessorFake();
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // ACT
            List<string> results = (List<string>)_loginManager.GetAllUserNames();
            //foreach (var result in results)
            //{
            //    Console.WriteLine(result);
            //}

            // ASSERT
            Assert.AreEqual(results.Count, actualFakesTestNames.Count);
            foreach(var result in results)
            {
                Assert.IsTrue(actualFakesTestNames.Contains(result));
            }
        }
        /// <summary>
        /// tests if a valid data creates a new Login record
        /// </summary>
        /// <remarks>
        ///     CONTRIBUTOR: Michael Springer
        ///     <br>
        ///     UPDATED: 2024-04-12
        ///     Initial Creation
        ///     </br>
        /// </remarks>
        [TestMethod]
        public void AddUserLogin_Success()
        {
            // ARRANGE
            _loginManager = new LoginManager(new PasswordHasher(), new LoginAccessorFake());
            string unusedName = "bartleby";
            int unusedID = 22;
            int rowsAffectd = 0;

            // ACT
            rowsAffectd = _loginManager.AddEmployeeLogin(unusedName, unusedID);

            // ASSERT
            Assert.IsTrue(rowsAffectd == 1);

        }
        /// <summary>
        /// tests if invalid data (existing username-case insensitive and employee_id) fail to create
        /// a new record
        /// </summary>
        /// <remarks>
        ///     CONTRIBUTOR: Michael Springer
        ///     <br>
        ///     UPDATED: 2024-04-13
        ///     Initial Creation
        ///     </br>
        /// </remarks>
        [TestMethod] 
        public void InvalidData_AddUserLogin_Fail()
        {
            // ARRANGE
            _loginManager = new LoginManager(new PasswordHasher(), new LoginAccessorFake());
            string unusedName = "bartleby";
            int unusedID = 22;
            int rowsAffected = 0;

            string usedName = "bartleby";
            int unusedID2 = 23;

            string usedNameCase = "Bartleby";
            int unusedID3 = 24;

            string unusedName2 = "UltimateDriver";
            int usedID = 22;


            // ACT
            // add a valid entry to the list to check against.
            _loginManager.AddEmployeeLogin(unusedName, unusedID); // doesn't add to count
            // try to add each type of invalid case
            rowsAffected =  _loginManager.AddEmployeeLogin(usedName, unusedID2);
            rowsAffected += _loginManager.AddEmployeeLogin(usedNameCase, unusedID3);
            rowsAffected += _loginManager.AddEmployeeLogin(unusedName2, usedID);

            // ASSSERT -- is the count still 0?
            Assert.IsTrue(rowsAffected == 0);

 
        }


        [TestMethod]
        public void GetAllEmployeeIDPassesCorrectAmountOfValues()
        {
            // Arrange
            List<int?> expectedEmployeeIds = new List<int?>
            {
                100082,null,100083,100086
            };
            List<int?> actualEmployeeIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualEmployeeIds = _loginManager.GetAllEmployeeIdFromLogin();

            Assert.AreEqual(expectedEmployeeIds.Count, actualEmployeeIds.Count);
        }
        [TestMethod]
        public void GetAllEmployeeIDPassesSameValues()
        {
            // Arrange
            List<int?> expectedEmployeeIds = new List<int?>
            {
                100082,null,100083,100086
            };
            List<int?> actualEmployeeIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualEmployeeIds = _loginManager.GetAllEmployeeIdFromLogin();

            Assert.AreEqual(expectedEmployeeIds[0], actualEmployeeIds[0]);
            Assert.AreEqual(expectedEmployeeIds[1], actualEmployeeIds[1]);
            Assert.AreEqual(expectedEmployeeIds[2], actualEmployeeIds[2]);
            Assert.AreEqual(expectedEmployeeIds[3], actualEmployeeIds[3]);
        }

        [TestMethod]
        public void GetAllEmployeeIDReturnsNullOnClientIDEntry()
        {
            // Arrange
            List<int?> expectedEmployeeIds = new List<int?>
            {
                100082,null,100083,100086
            };
            List<int?> actualEmployeeIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualEmployeeIds = _loginManager.GetAllEmployeeIdFromLogin();

            Assert.AreEqual(null, actualEmployeeIds[1]);
        }
        [TestMethod]
        public void GetAllClientIDPassesCorrectAmountOfValues()
        {
            // Arrange
            List<int?> expectedClientIds = new List<int?>
            {
                100082,100031,null,null
            };
            List<int?> actualClientIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualClientIds = _loginManager.GetAllClientIdFromLogin();

            Assert.AreEqual(expectedClientIds.Count, actualClientIds.Count);
        }
        [TestMethod]
        public void GetAllClientIDPassesSameValues()
        {
            // Arrange
            List<int?> expectedClientIds = new List<int?>
            {
                100082,100031,null,null
            };
            List<int?> actualClientIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualClientIds = _loginManager.GetAllClientIdFromLogin();

            Assert.AreEqual(expectedClientIds[0], actualClientIds[0]);
            Assert.AreEqual(expectedClientIds[1], actualClientIds[1]);
            Assert.AreEqual(expectedClientIds[2], actualClientIds[2]);
            Assert.AreEqual(expectedClientIds[3], actualClientIds[3]);
        }

        [TestMethod]
        public void GetAllClientIDReturnsNullOnEmployeeIDEntry()
        {
            // Arrange
            List<int?> expectedClientIds = new List<int?>
            {
                100082,100031,null,null
            };
            List<int?> actualClientIds = new List<int?>();



            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "NA1",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = 100082,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100082
                    }
                },
                new Login_VM
                {
                    Username = "NA2",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = null,
                    ClientID = 100031,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Client = new Client_VM
                    {
                        ClientID = 100031,
                    }
                },
                new Login_VM
                {
                    Username = "NA3",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100083,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100083
                    }
                },
                new Login_VM
                {
                    Username = "NA4",
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    EmployeeID = 100086,
                    SecurityResponse1 = "NA",
                    SecurityResponse2 = "NA",
                    SecurityResponse3 = "NA",
                    Employee = new Employee_VM
                    {
                        Employee_ID = 100086
                    }
                }
            };
            var loginAccessor = new LoginAccessorFake(testLoginData);
            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            actualClientIds = _loginManager.GetAllClientIdFromLogin();

            Assert.AreEqual(null, actualClientIds[2]);
            Assert.AreEqual(null, actualClientIds[3]);
        }



        [TestMethod]
        public void GetEmployeeUsernameFromEmailReturnsCorrectUsername()
        {
            // Arrange
            string expectedUsername = "jackrussel49";
            string email = "correctemail@right.com";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    Employee = new Employee_VM
                    {
                        Email = "correctemail@right.com"
                    }
                }, new Login_VM
                {
                    Username = "wrong one hahaha",
                    Employee = new Employee_VM
                    {
                        Email = "wrongemail"
                    }
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string actualUsername = _loginManager.GetEmployeeUserNameByEmail(email);

            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
        }
        [TestMethod]
        public void GetEmployeeUsernameFromEmailReturnsNullWhenGivenBadEmail()
        {
            // Arrange
            string expectedUsername = null;
            string bademail = "blarglesnack@nonsense.com";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    Employee = new Employee_VM
                    {
                        Email = "correctemail@right.com"
                    }
                }, new Login_VM
                {
                    Username = "wrong one hahaha",
                    Employee = new Employee_VM
                    {
                        Email = "wrongemail"
                    }
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string actualUsername = _loginManager.GetEmployeeUserNameByEmail(bademail);

            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
        }




        [TestMethod]
        public void GetClientUsernameFromEmailReturnsCorrectUsername()
        {
            // Arrange
            string expectedUsername = "jackrussel49";
            string email = "correctemail@right.com";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    Client = new Client_VM
                    {
                        Email = "correctemail@right.com"
                    }
                }, new Login_VM
                {
                    Username = "wrong one hahaha",
                    Client = new Client_VM
                    {
                        Email = "wrongemail"
                    }
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string actualUsername = _loginManager.GetClientUserNameByEmail(email);

            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
        }
        [TestMethod]
        public void GetClientUsernameFromEmailReturnsNullWhenGivenBadEmail()
        {
            // Arrange
            string expectedUsername = null;
            string bademail = "blarglesnack@nonsense.com";

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = "jackrussel49",
                    Client = new Client_VM
                    {
                        Email = "correctemail@right.com"
                    }
                }, new Login_VM
                {
                    Username = "wrong one hahaha",
                    Client = new Client_VM
                    {
                        Email = "wrongemail"
                    }
                }
            };

            var loginAccessor = new LoginAccessorFake(testLoginData);

            _loginManager = new LoginManager(_passwordHasher, loginAccessor);

            // Act
            string actualUsername = _loginManager.GetClientUserNameByEmail(bademail);

            // Assert
            Assert.AreEqual(expectedUsername, actualUsername);
        }



        /// <summary>
        ///     Test that authenticating an client's username and password returns the correct employee when
        ///     the username and password are found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-12
        /// </remarks>
        [TestMethod]
        public void AuthenticateClientReturnsCorrectClient()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            int expectedClientId = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = expectedClientId,
                    Client = new Client_VM
                    {
                        ClientID = expectedClientId
                    }
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000,
                    Client = new Client_VM
                    {
                        ClientID = 100000
                    }
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Action
            Client_VM actualClient = _loginManager.AuthenticateClient(username, password);

            // Assert
            Assert.AreEqual(expectedClientId, actualClient.ClientID);
        }

        /// <summary>
        ///     Test that authenticating an Client's username and password fails when the username and password are not found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-12
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticateClient_FailsOnInvalidLogin()
        {
            // Arrange
            string username = "jackrussel49";
            string password = "galaxy";

            int expectedClientId = 100082;

            List<Login_VM> testLoginData = new List<Login_VM>
            {
                new Login_VM
                {
                    Username = username,
                    PasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f",
                    ClientID = expectedClientId
                }, new Login_VM
                {
                    Username = "incorrect",
                    PasswordHash = "invalidpasswordhash",
                    ClientID = 100000
                }
            };

            _loginManager = new LoginManager(_passwordHasher, new LoginAccessorFake(testLoginData));

            // Act
            _loginManager.AuthenticateClient(username, password);
        }

    }

}
