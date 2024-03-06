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
    /// UPDATER: 
    /// <br />
    /// UPDATED: 
    /// <br />
    /// 
    ///     Initial creation
    /// </remarks>
    public class Parts_Request
    {
        public int Parts_Request_ID { get; set; }
        public DateTime Date_Requested { get; set; }
        public int Quantity_Requested { get; set; }
        public string Part_Name { get; set; }
    }
}
