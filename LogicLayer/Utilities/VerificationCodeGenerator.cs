using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    /// <summary>
    ///     Generates verification codes for multi-factor login authentication and/or password resets
    /// </summary>
    public interface IVerificationCodeGenerator
    {
        /// <summary>
        ///     Generates a multi-factor login verification code
        /// </summary>
        /// <returns>
        ///    <see cref="string">string</see>: The generated verification code
        /// </returns>
       string GenerateCode();
    }
    
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Generates verification codes for multi-factor login authentication and/or password resets
    /// </summary>
    public class VerificationCodeGenerator : IVerificationCodeGenerator
    {
        private string _validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private int _codeLength = 6;
        private Random _random;

        /// <summary>
        ///     Instantiates a new VerificationCodeGenerator object
        /// </summary>
        /// <returns>
        ///    <see cref="VerificationCodeGenerator">VerificationCodeGenerator</see>:
        ///    The instantiated object
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public VerificationCodeGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        ///     Generates a multi-factor login verification code
        /// </summary>
        /// <returns>
        ///    <see cref="string">string</see>: The generated verification code
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public string GenerateCode()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int codeChIx = 0; codeChIx < _codeLength; codeChIx++)
            {
                int validChIx = _random.Next(0, _validCharacters.Length);

                stringBuilder.Append(_validCharacters[validChIx]);
            }

            string code = stringBuilder.ToString();

            return code;
        }
    }
}
