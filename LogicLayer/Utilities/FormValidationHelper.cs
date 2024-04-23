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
    /// <br />
    /// UPDATER: Michael Springer
    /// <br />
    /// UPDATED: 2024-04-09
    /// Added isValidUserName validation
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


        /// <summary>
        ///     Validates a time using a regular expression pattern.
        /// </summary>
        /// <param name="time">
        ///    The zipcode to validate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the time is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool IsValidTime(string time)
        {
            string timePattern = @"\b((1[0 - 2] | 0?[1 - 9]):([0 - 5][0 - 9])([AaPp][Mm]))";
            return Regex.IsMatch(time, timePattern);
        }

        /// <summary>
        ///     Converts a set of 7 booleans to a string representation,
        ///     <br />
        ///     for converting checkboxes into the char[] associated with ActivityWeek objects.
        /// </summary>
        /// <param name="isChecked1">
        ///    The first character to be added.
        /// </param>
        /// <param name="isChecked2">
        ///    The second character to be added.
        /// </param>
        /// <param name="isChecked3">
        ///    The third character to be added.
        /// </param>
        /// <param name="isChecked4">
        ///    The fourth character to be added.
        /// </param>
        /// <param name="isChecked5">
        ///    The fifth character to be added.
        /// </param>
        /// <param name="isChecked6">
        ///    The sixth character to be added.
        /// </param>
        /// <param name="isChecked7">
        ///    The seventh character to be added.
        /// </param>
        /// <returns>
        ///    <see cref="string">string</see>: the string representation of the 7 bools.
        /// </returns>
        public static string getActiveDays(bool isChecked1, 
            bool isChecked2, bool isChecked3, bool isChecked4, 
            bool isChecked5, bool isChecked6, bool isChecked7)
        {
            string result = "";

            result += isChecked1 ? "1" : "0";
            result += isChecked2 ? "1" : "0";
            result += isChecked3 ? "1" : "0";
            result += isChecked4 ? "1" : "0";
            result += isChecked5 ? "1" : "0";
            result += isChecked6 ? "1" : "0";
            result += isChecked7 ? "1" : "0";

            return result;
		}
        /// <summary>
        ///     Validates a userName to meet DB NVARCHAR(50) req
        /// </summary>
        /// <param name="userName">
        ///   The userName to Validate
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the userName is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        public static bool isValidUserName(string userName)
        {
            return (userName.Length <= 50 && userName.isNotEmptyOrNull());
        }
    }
}
