using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayer.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Class for testing LoginManager functionality
    /// </summary>
    [TestClass]
    public class PasswordResetManager_Tests
    {
        private IVerificationCodeGenerator _verificationCodeGenerator;
        private long _secondsBeforeResetPasswordExpiry;

        /// <summary>
        ///     Initialize the required test objects
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestInitialize]
        public void TestInitialize()
        {
            _verificationCodeGenerator = new VerificationCodeGenerator();
            _secondsBeforeResetPasswordExpiry = 100;
        }

        /// <summary>
        ///     Test that beginning a password reset creates a new password object correctly
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        public void BeginPasswordReset_CreatesPasswordResetCorrectly()
        {
            // Arrange
            string username = "jackrussel49";

            var testPasswordResetData = new List<PasswordReset_VM>()
            {
                new PasswordReset_VM
                {
                    PasswordResetId = 100000,
                    Username = "wrong_username",
                    VerificationCode = "abcdef",
                    RequestDateTime = DateTime.Now,
                    Login = new Login_VM
                    {
                        Employee = new Employee_VM
                        {
                            Email = "wrong-email@wrong.com"
                        }
                    }
                }
            };

            var passwordResetAccessor = new PasswordResetAccessorFake(testPasswordResetData);

            var passwordResetManager = new PasswordResetManager(
                _verificationCodeGenerator,
                _secondsBeforeResetPasswordExpiry,
                passwordResetAccessor);

            // Act
            passwordResetManager.BeginPasswordReset(username);

            // Assert
            var createdPasswordReset =
                testPasswordResetData.Single(passwordReset => passwordReset.Username == username);

            Assert.IsNotNull(createdPasswordReset);
            Assert.IsNotNull(createdPasswordReset.VerificationCode);
        }

        /// <summary>
        ///     Test that a password reset successfully verifies when a password reset request meeting the criteria is
        ///     found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        public void VerifyPasswordReset_SucceedsInCorrectCase()
        {
            // Arrange
            string username = "jackrussel49";
            string email = "testemail@fake.com";
            string verificationCode = "123456";

            var testPasswordResetData = new List<PasswordReset_VM>()
            {
                new PasswordReset_VM
                {
                    PasswordResetId = 100000,
                    Username = "wrong_username",
                    VerificationCode = "abcdef",
                    RequestDateTime = DateTime.Now,
                    Login = new Login_VM
                    {
                        Employee = new Employee_VM
                        {
                            Email = "wrong-email@wrong.com"
                        }
                    }
                }, new PasswordReset_VM
                {
                    PasswordResetId = 100009,
                    Username = username,
                    VerificationCode = verificationCode,
                    RequestDateTime = DateTime.Now,
                    Login = new Login_VM
                    {
                        Employee = new Employee_VM
                        {
                            Email = email
                        }
                    }
                }

            };

            var passwordResetAccessor = new PasswordResetAccessorFake(testPasswordResetData);

            var passwordResetManager = new PasswordResetManager(
                _verificationCodeGenerator,
                _secondsBeforeResetPasswordExpiry,
                passwordResetAccessor);

            // Act
            bool passwordResetVerified = passwordResetManager.VerifyPasswordReset(username, email, verificationCode);

            Assert.IsTrue(passwordResetVerified);
        }

        /// <summary>
        ///     Test that a password reset accurately does not verify when a password reset request meeting the criteria is
        ///     not found
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        [TestMethod]
        public void VerifyPasswordReset_FailsInIncorrectCase()
        {
            // Arrange
            string username = "jackrussel49";
            string email = "testemail@fake.com";
            string verificationCode = "123456";

            var testPasswordResetData = new List<PasswordReset_VM>()
            {
                new PasswordReset_VM
                {
                    PasswordResetId = 100000,
                    Username = "wrong_username",
                    VerificationCode = "abcdef",
                    RequestDateTime = DateTime.Now,
                    Login = new Login_VM
                    {
                        Employee = new Employee_VM
                        {
                            Email = "wrong-email@wrong.com"
                        }
                    }
                }, new PasswordReset_VM
                {
                    PasswordResetId = 100009,
                    Username = username,
                    VerificationCode = "000000",
                    RequestDateTime = DateTime.Now,
                    Login = new Login_VM
                    {
                        Employee = new Employee_VM
                        {
                            Email = email
                        }
                    }
                }

            };

            var passwordResetAccessor = new PasswordResetAccessorFake(testPasswordResetData);

            var passwordResetManager = new PasswordResetManager(
                _verificationCodeGenerator,
                _secondsBeforeResetPasswordExpiry,
                passwordResetAccessor);

            // Act
            bool passwordResetVerified = passwordResetManager.VerifyPasswordReset(username, email, verificationCode);

            Assert.IsFalse(passwordResetVerified);
        }

    }
}
