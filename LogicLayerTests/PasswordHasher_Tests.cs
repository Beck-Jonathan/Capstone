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
    /// CREATED: 2024-02-01
    /// <br />
    ///     Class for testing PasswordHasher functionality
    /// </summary>
    [TestClass]
    public class PasswordHasher_Tests
    {
        private IPasswordHasher _passwordHasher;

        /// <summary>
        ///     Initialize the required test data
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
        ///     Test hashed password from plaintext password "galaxy"
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void HashPassword_1()
        {
            // Arrange
            string password = "galaxy";
            string expectedPasswordHash = "eba4ae33f54ae0f96bed25bfc13abd887ae157380330cd3fd3f0a4d054ce3a3f";

            // Act
            string actualPasswordHash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedPasswordHash, actualPasswordHash);
        }

        /// <summary>
        ///     Test hashed password from plaintext password "password"
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
       [TestMethod]
        public void HashPassword_2()
        {
            // Arrange
            string password = "password";
            string expectedPasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";

            // Act
            string actualPasswordHash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedPasswordHash, actualPasswordHash);
        }

        /// <summary>
        ///     Test hashed password from plaintext password "somethingsecret"
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void HashPassword_3()
        {
            // Arrange
            string password = "somethingsecret";
            string expectedPasswordHash = "3fb9b10921e37784b972f0bff82b9e3ea3f3745de5129e20839369f8c78602a0";

            // Act
            string actualPasswordHash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedPasswordHash, actualPasswordHash);
        }

        /// <summary>
        ///     Test hashed password from plaintext password "fakeentrance"
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void HashPassword_4()
        {
            // Arrange
            string password = "fakeentrance";
            string expectedPasswordHash = "8f82cb353f0905cccb18044ec70d12c4d03f6f86661bfd9312411e9d70130857";

            // Act
            string actualPasswordHash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedPasswordHash, actualPasswordHash);
        }

        /// <summary>
        ///     Test hashed password from plaintext password "dontusethis"
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        [TestMethod]
        public void HashPassword_5()
        {
            // Arrange
            string password = "dontusethis";
            string expectedPasswordHash = "9034ba76c0e6bf7d07b62034d958e18cd3effc34e4740a85183b9551d12c9efd";

            // Act
            string actualPasswordHash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.AreEqual(expectedPasswordHash, actualPasswordHash);
        }
    }
}
