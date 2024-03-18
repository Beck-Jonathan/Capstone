using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class POLineItem
    {
        public int PurchaseOrderID { get; set; }
        public int PartsInventoryID { get; set; }
        public int LineNumber { get; set; }
        public string LineItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string LineItemDescription { get; set; }
        public bool isActive { get; set; }
    }
    public class POLineItemVM : POLineItem
    {
        public Parts_Inventory Part { get; set; }
    }
}
