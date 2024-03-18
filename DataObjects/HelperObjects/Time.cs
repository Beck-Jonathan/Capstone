using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.HelperObjects
{

    public class Time
    {
        /// <summary>
         /// AUTHOR: Nathan Toothaker
         /// <br />
         /// CREATED: 2024-03-05
         /// <br />
         /// 
         ///     Extends functionality of the built-in DateTime object to represent a time of any day, instead of a specific one.
         /// </summary>
         ///
        private DateTime _time;
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// CREATED: 2024-03-05
        /// <br />
        /// For construction with database datetimes
        /// </summary>

        public Time(DateTime dateTime)
        {
            _time = dateTime;
        }
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// CREATED: 2024-03-05
        /// <br />
        /// For custom construction from the top end with database datetimes
        /// </summary>
        public Time(int Hours, int Minutes, int Seconds)
        {
            _time = new DateTime(1900, 1, 1, Hours, Minutes, Seconds);
        }
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// CREATED: 2024-03-05
        /// <br />
        /// Primary use is to store this data back into the database.
        /// </summary>
        /// <returns>
        ///    <see cref="DateTime">The DateTime object this object extends</see>
        /// </returns>
        public override string ToString()
        {
            return _time.ToString("h:mm tt");
        }
        public DateTime getStorageData() { return _time; }
    }
}
