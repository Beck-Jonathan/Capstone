/// <summary>
/// Michael Springer
/// Created: 2024/02/06
/// 
/// Utility class consisting of validation helper methods
/// </summary>
///
/// <remarks>
/// Updater Michael Springer
/// Updated: 2024-02-19
/// Merged with Chris Baenziger's Version Created on 2024-02-06
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataObjects
{
    public static class ValidationHelpers
    {
        
        public static bool IsValidEmail(this string email)
        {
            int emailLength = 255;
            bool result = false;

            string pattern = @"\A[\w!#$%&'*+/=?`{|}~^-]+(?:\.[\w!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}\Z";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);

            result = (email.Length < emailLength && (rg.Match(email).Success));
            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            bool result = false;
            // Uibakery.io/regex-lbrary/password-regex-chsharp -- requires 8 chars, 1 upper, 1 lower, 1 number, 1 special
            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            Regex rg = new Regex(pattern);
            result = (password.Length < 255 && (rg.Match(password).Success));
            // TODO Temp Validation for easy access:
            result = password.Length > 5;
            return result;
        }
        public static bool IsValidGivenName(this string name)
        {
            int nameLength = 50;
            bool result = false;
            // stackoverflow.com/a/2385967 one or more letters or valid characters
            string pattern = @"^[A-z ,.'-]+$";
            Regex rg = new Regex(pattern);
            result = (name.Length <= nameLength && (rg.Match(name).Success));
            return result;
        }

        public static bool IsValidFamilyName(this string name)
        {
            int nameLength = 50;
            bool result = false;
            // stackoverflow.com/a/2385967 one or more letters or valid characters
            string pattern = @"^[A-z ,.'-]+$";
            Regex rg = new Regex(pattern);
            result = (name.Length <= nameLength && (rg.Match(name).Success));
            return result;
        }

        public static bool IsValidMiddleName(this string name)
        {
            int nameLength = 100;
            bool result = false;
            // stackoverflow.com/a/2385967 one or more letters or valid characters
            string pattern = @"^[a-z ,.'-]+$";
            Regex rg = new Regex(pattern);
            result = (name.Length <= nameLength && (rg.Match(name).Success));
            return result;
        }
        public static bool IsValidCompoundName(this string name)
        {
            int nameLength = 100;
            bool result = false;
            // stackoverflow.com/a/2385967 one or more letters or valid characters
            string pattern = @"^[a-z ,.'-]+$";
            Regex rg = new Regex(pattern);
            result = (name.Length <= nameLength && (rg.Match(name).Success));
            return result;
        }

        public static bool isValidPhone(this string phone)
        {
            bool result = false;
            // stackoverflow.com/a/16699507 comprehensive phone combinations and unformatted numbers
            string pattern = @"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
            Regex rg = new Regex(pattern);
            result = (rg.Match(phone).Success);
            return result;
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024/02/06
        /// 
        /// DOB Validator
        /// Accepts a required age argument and checks for validity
        /// </summary>
        ///
        /// <remarks>
        /// <param name="date">
        /// <param name="requiredAge"
        /// 
        public static bool IsValidDOB(this DateTime date, int requiredAge)
        {
            bool result = false;
            int yeardDifference = ((DateTime.Now - date).Days / 365);
            result = (date != null && yeardDifference >= requiredAge);
            return result;   
        }
        public static bool IsValidDate(this DateTime date)
        {
            return date != null;
        }

        public static bool isNotEmptyOrNull(this string value)
        {
            bool result = false;
            result = (value != null && value.Length > 0);
            return result;
        }

        public static bool IsValidVehicleNumber(string vehicleNumber) 
        {
            /// <summary>
            ///     Checks a vehicle number is not blank and the right size.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="vehicleNumber">vehicleNumber</see> vehicleNumber: The vehicle number to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>

            return (!vehicleNumber.Equals("") && vehicleNumber.Length < 11 && vehicleNumber.Length > 0);
        }

        public static bool IsValidVIN(string vin)
        {
            /// <summary>
            ///     Checks a vehicle number is not blank and the right size.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="vehicleNumber">vehicleNumber</see> vehicleNumber: The vehicle number to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            vin.Replace(" ", "");
            return vin.Length == 17;
        }

        public static bool IsValidYear(int year)
        {
            /// <summary>
            ///     Checks a year is not blank and the right size.
            /// </summary>
            /// <param name="year">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="year">Year</see> year: The year to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return (year >= 1900 && year <= 9999);
        }

        public static bool IsValidMileage(int mileage)
        {
            /// <summary>
            ///     Checks a mileage is entered and not negative.
            /// </summary>
            /// <param name="mileage">
            ///    The mileageto be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="mileage">mileage</see> mileage: The mileage to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return mileage >= 0;
        }

        public static bool IsValidSeatCount(int seatCount)
        {
            /// <summary>
            ///     Checks a seat count is entered and not negative.
            /// </summary>
            /// <param name="seatCount">
            ///    The seatCount to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="seatCount">seatCount</see> seatCount: The seatConut to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return seatCount >= 0;
        }

        public static bool IsValidLicensePlate(string licensePlate)
        {
            /// <summary>
            ///     Checks a licensePlate is not blank and the right size.
            /// </summary>
            /// <param name="licensePlate">
            ///    The licensePlate to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="licensePlate">licensePlate</see> licensePlate: The license plate to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return (!licensePlate.Equals("") && licensePlate.Length < 11 && licensePlate.Length > 0);
        }

        //Created By Jonathan Beck
        //3/19/2024
        public static List<String> generateCBOStates()
        {
            List<string> states = new List<string>
            {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
                "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
            };
            return states;
        }

        //Created By Jonathan Beck
        //3/19/2024
        public static Boolean isValidZip(this string zip)
        {
            Boolean result = true;
            if (zip.Length == 5 || zip.Length == 9)
            {
                char[] chars = zip.ToCharArray();
                foreach (char c in chars)
                {
                    int x = 0;
                    if (Int32.TryParse(c.ToString(), out x) == false)
                    {
                        result = false; break;
                    }
                }
            }
            return result;
        }

        // AUTHOR: Chris Baenziger
        // CREATED: 2024-03-24
        public static bool isValidLatitude(string latitude)
        {
            bool result = false;
            if(latitude != null || !latitude.Equals(""))
            {
                Decimal lat = Decimal.Parse(latitude);
                if(lat >= -90 && lat <= 90)
                {
                    result = true;
                }
            }
            return result;
        }

        // AUTHOR: Chris Baenziger
        // CREATED: 2024-03-24
        public static bool isValidLongitude(string longitude)
        {
            bool result = false;
            if (longitude != null || !longitude.Equals(""))
            {
                Decimal lon = Decimal.Parse(longitude);
                if (lon >= -180 && lon <= 180)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}

