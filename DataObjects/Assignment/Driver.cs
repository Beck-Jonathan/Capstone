using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Assignment
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-04-17
    /// <br />
    ///     Driver Model
    /// </summary>

    public class Driver
    {
        public int Employee_ID { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public string Driver_License_Class_ID { get; set; }
        public int Max_Passenger_Count { get; set; }
    }
}
