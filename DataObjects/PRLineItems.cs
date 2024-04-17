using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Parker Svoboda
    /// <br />
    /// CREATED: 2024-04-09
    /// <br />
    ///     Represents a Parts Request Line Item
    /// </summary>
    public class PRLineItems
    {
        public int Parts_Request_ID { get; set; }
        public int Parts_Inventory_ID { get; set; }
        public int Qty_Requested { get; set; }
    }
    public class PRLineItemsVM:PRLineItems
    {
        Parts_Request Part_Request { get; set; }
        Parts_Inventory Part_Inventory { get; set; }
    }
}
