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
    /// CREATED: 2024-02-09
    /// <br />
    ///     Represents a maintenance service order
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-02-10
    /// <br />
    /// 
    ///     Initial creation
    /// </remarks>
    public class ServiceOrder
    {
        public string VIN { get; set; }
        public int Service_Order_ID { get; set; }
        public bool Critical_Issue { get; set; }
        public string Service_Type_ID { get; set; }
        public string Service_Description { get; set; }
    }

    public class ServiceOrder_VM : ServiceOrder
    {

    }
}
