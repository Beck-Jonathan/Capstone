using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.HelperObjects
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// 
    ///     a simple object used to take the char[7] that represents days of the week and interpret them smoothly
    /// </summary>
    /// 
    public class ActivityWeek
    {
        private char[] _days;
        public ActivityWeek(char[] days)
        {
            if(days.Length != 7)
            {
                throw new ArgumentException("There are 7 days in a week!");
            }
            _days = days;
        }
        /// <summary>
        ///     Gets the char[7] as a string, that represents the data.
        /// </summary>
        /// <returns>
        ///    <see cref="string">string</see>: a string representation to be stored in the database
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public object GetStorageString()
        {
            return new String(_days);
        }
        /// <summary>
        ///     Returns whether or not specific day is marked as active
        /// </summary>
        /// <param name="dayName">The string representation of the day to check.</param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether or not the day is marked as active
        /// </returns>
        /// <remarks>
        /// <br />
        /// PARAMETERS:
        /// <br />
        /// dayName: the string representation of the day to be checked.
        /// <br />
        /// Accept dayNames are "Monday", "Mon", "Tuesday", "Tue", etc. Capitalization does not matter.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public bool isActiveOnDay(string dayName)
        {
            bool result = false;
            switch(dayName.ToLower())
            {
                case "monday":
                case "mon":
                    result = _days[0] == '1';
                    break;
                case "tuesday":
                case "tue":
                    result = _days[1] == '1';
                    break;
                case "wednesday":
                case "wed":
                    result = _days[2] == '1';
                    break;
                case "thursday":
                case "thur":
                    result = _days[3] == '1';
                    break;
                case "friday":
                case "fri":
                    result = _days[4] == '1';
                    break;
                case "saturday":
                case "sat":
                    result = _days[5] == '1';
                    break;
                case "sunday":
                case "sun":
                    result = _days[6] == '1';
                    break;
            }
            return result;
        }
        /// <summary>
        ///     Gets the char[7] as a string that is more easily understood by readers.
        /// </summary>
        /// <returns>
        ///    <see cref="string">string</see>: a string representation to be stored in the database
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>
        public override string ToString()
        {
            string result = "";
            if (_days[0] == '1')
            {
                result += "Mon/";
            }
            if (_days[1] == '1')
            {
                result += "Tue/";
            }
            if (_days[2] == '1')
            {
                result += "Wed/";
            }
            if (_days[3] == '1')
            {
                result += "Thur/";
            }
            if (_days[4] == '1')
            {
                result += "Fri/";
            }
            if (_days[5] == '1')
            {
                result += "Sat/";
            }
            if (_days[6] == '1')
            {
                result += "Sun/";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

    }
}
