using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-04
    /// <br />
    /// 
    ///     A static helper class for performing form validation on various input fields such as email, phone numbers, and ZIP codes.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm 
    /// <br />
    /// UPDATED: 2024-03-05
    /// <br />
    /// Added IsValidCity for checking if a city name is valid.
    /// </remarks>
    public static class FormValidationHelper
    {
        /// <summary>
        ///     Validates an email address using a regular expression pattern.
        /// </summary>
        /// <param name="email">
        ///    The email address to validate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the email address is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool IsValidEmail(string email)
        {
            // Regular expression pattern for validating email addresses
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        ///     Validates a phone number using a regular expression pattern.
        /// </summary>
        /// <param name="phoneNumber">
        ///    The phone number to validate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the phone number is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regular expression pattern for validating US phone numbers
            string phonePattern = @"^\d{10}$|^\d{3}-\d{3}-\d{4}$|^\(\d{3}\)\s?\d{3}-\d{4}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        /// <summary>
        ///     Validates a zipcode using a regular expression pattern.
        /// </summary>
        /// <param name="zipCode">
        ///    The zipcode to validate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the zipcode is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool IsValidZipCode(string zipCode)
        {
            // Regular expression pattern for validating US ZIP codes
            string zipPattern = @"^\d{5}(-\d{4})?$";
            return Regex.IsMatch(zipCode, zipPattern);
        }

        /// <summary>
        ///     Validates a city name using a regular expression pattern.
        /// </summary>
        /// <param name="city">
        ///    The city name to validate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the city is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool IsValidCity(string city)
        {
            // Regex from https://stackoverflow.com/a/25677072
            string cityPattern = @"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$";
            return Regex.IsMatch(city, cityPattern);
        }
    }
}
