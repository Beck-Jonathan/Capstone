using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Vendor
    {
        public int Vendor_ID { set; get; }
        public string Vendor_Name { set; get; }
        public string Vendor_Contact_Given_Name { set; get; }
        public string Vendor_Contact_Family_Name { set; get; }
        public string Vendor_Contact_Phone_Number { set; get; }
        public string Vendor_Contact_Email { set; get; }
        public string Vendor_Phone_Number { set; get; }
        public string Vendor_Address { set; get; }
        public string Vendor_Address2 { set; get; }
        public string Vendor_City { set; get; }
        public string Vendor_State { set; get; }
        public string Vendor_Country { set; get; }
        public string Vendor_Zip { set; get; }
        public bool Preferred { set; get; }
        public bool Is_Active { set; get; }

    }
}
