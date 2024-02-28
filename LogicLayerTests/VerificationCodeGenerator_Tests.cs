using LogicLayer.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class VerificationCodeGenerator_Tests
    {
        private IVerificationCodeGenerator _verificationCodeGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            _verificationCodeGenerator = new VerificationCodeGenerator();
        }

        [TestMethod]
        public void GenerateCode_GeneratesValidCodes()
        {
            // Arrange
            List<string> codes = new List<string>();
            int iterations = 100;

            // Act
            for (int it = 0; it < iterations; it++)
            {
                codes.Add(_verificationCodeGenerator.GenerateCode());
            }

            // Assert
            Assert.IsTrue(
                codes.All(code =>
                    code.Length == 6 && code.All(ch =>
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Contains(ch))) &&
                !codes.All(code => codes[0] == code));
        }
    }
}
