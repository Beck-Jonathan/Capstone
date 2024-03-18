using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Represents a Parts request
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 
    /// <br />
    /// 
    ///     Initial creation
    ///     <br />
    ///     Added values: Vehicle_Year, Vehicle_Make, Vehicle_Model, Parts_Request_Notes, Employee_ID
    ///         
    /// </remarks>
    public class Parts_Request
    {
        public int Parts_Request_ID { get; set; }
        public DateTime Date_Requested { get; set; }
        public int Quantity_Requested { get; set; }
        public string Part_Name { get; set; }
        public string Vehicle_Year { get; set; }
        public string Vehicle_Make { get; set; }
        public string Vehicle_Model { get; set; }
        public string Parts_Request_Notes { get; set; }
        public int Employee_ID { get; set; }
    }
}
