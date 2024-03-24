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

        // UPDATER: Steven Sanchez
        // UPDATE DATE: 2023-03-12
        // SUMMARY: Added the data objects below for Inserting records
        public int Service_Order_Version { get; set; }
        public int Service_Line_Item_ID { get; set; }
        public int Created_By_Employee_ID { get; set; }
        public int Serviced_By_Employee_ID { get; set; }
        public DateTime Date_Started { get; set; }
        public DateTime Date_Finished { get; set; }
        public bool Is_Active { get; set; }


    }

    public class ServiceOrder_VM : ServiceOrder
    {

    }
}
