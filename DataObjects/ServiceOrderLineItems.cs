using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ServiceOrderLineItems
    {
        public int Service_Order_ID { get; set; }
        public int Service_Order_Version { get; set; }
        public int Parts_Inventory_ID { get; set; }
        public int Quantity { get; set; }
    }
}
