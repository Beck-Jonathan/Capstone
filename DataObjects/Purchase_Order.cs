using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Purchase_Order
    {
        public int Purchase_Order_ID { set; get; }
        public int Vendor_ID { set; get; }
        public DateTime Purchase_Order_Date { set; get; }
        public string Delivery_Address { set; get; }
        public string Delivery_Address2 { set; get; }
        public string Delivery_City { set; get; }
        public string Delivery_State { set; get; }
        public string Delivery_Country { set; get; }
        public string Delivery_Zip { set; get; }
        public bool Is_Active { set; get; }

    }
    public class Purchase_OrderVM : Purchase_Order
    {
        public Vendor vendor;
        public List<POLineItemVM> pOLineItems { set; get; }
    }
}
