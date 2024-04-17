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
    /// UPDATER: Ben Collins, Jonathan Beck
    /// <br />
    /// UPDATED: 2024-03-19, 2024-04-13
    /// <br />
    ///     Initial creation
    ///     Added a List of ServiceOrderLineItems's and Vehicle objects to the ServiceOrder_VM.
    ///     Added the rest of the ServiceOrder columns from the database
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
        public int Created_By_Employee_ID { get; set; }
        public int Serviced_By_Employee_ID { get; set; }
        public DateTime Date_Started { get; set; }
        public DateTime Date_Finished { get; set; }
        public bool Is_Active { get; set; }


    }

    public class ServiceOrder_VM : ServiceOrder
    {
        Employee_VM created_by_Employee_VM { get; set; }
        Employee_VM serviced_by_Employee_VM { get; set; }
        public List<ServiceOrderLineItems> serviceOrderLineItems { get; set; }
        public Vehicle vehicle { get; set; }
    }
}
