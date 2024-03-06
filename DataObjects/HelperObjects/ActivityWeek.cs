using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.HelperObjects
{
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
