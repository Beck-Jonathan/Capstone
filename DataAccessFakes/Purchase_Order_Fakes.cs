using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class Purchase_Order_Fakes : IPurchase_OrderAccessor
    {
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/09
        /// 
        /// Iniailize Test Data
        /// </summary>
        ///


        private List<Purchase_Order> fakeorders = new List<Purchase_Order>();
        public Purchase_Order_Fakes()
        {
            Purchase_OrderVM order1 = new Purchase_OrderVM();
            order1.Purchase_Order_ID = 1;
            order1.Vendor_ID = 1;
            order1.Purchase_Order_Date = new DateTime(2022, 06, 05);
            order1.Delivery_Address = "1001 Fake Street";
            order1.Delivery_Address2 = "Apartment 2";
            order1.Delivery_City = "Dubuque";
            order1.Delivery_State = "Iowa";
            order1.Delivery_Country = "USA";
            order1.Delivery_Zip = "52002";
            order1.Is_Active = true;
            fakeorders.Add(order1);
            Purchase_OrderVM order2 = new Purchase_OrderVM();
            order2.Purchase_Order_ID = 2;
            order2.Vendor_ID = 1;
            order2.Purchase_Order_Date = new DateTime(2022, 07, 10);
            order2.Delivery_Address = "2002 New Street";
            order2.Delivery_Address2 = "";
            order2.Delivery_City = "Ames";
            order2.Delivery_State = "Iowa";
            order2.Delivery_Country = "USA";
            order2.Delivery_Zip = "50424";
            order2.Is_Active = true;
            fakeorders.Add(order2);
            Purchase_OrderVM order3 = new Purchase_OrderVM();
            order3.Purchase_Order_ID = 3;
            order3.Vendor_ID = 1;
            order3.Purchase_Order_Date = new DateTime(2022, 10, 10);
            order3.Delivery_Address = "8816 Cheese Ave";
            order3.Delivery_Address2 = "Apartment 101";
            order3.Delivery_City = "La Crosse";
            order3.Delivery_State = "Wisconsin";
            order3.Delivery_Country = "USA";
            order3.Delivery_Zip = "67821";
            order3.Is_Active = true;
            fakeorders.Add(order3);


        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Get all purchase order within a date range
        /// </summary>
        public List<Purchase_OrderVM> GetPurchaseOrderByDateRange(DateTime startDate, DateTime endDate)
        {

            List<Purchase_OrderVM> results = new List<Purchase_OrderVM>();
            foreach (Purchase_OrderVM fake in fakeorders)
            {
                if (fake.Purchase_Order_Date > startDate && fake.Purchase_Order_Date < endDate)
                {
                    results.Add(fake);
                }
            }


            return results;
        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Get purchase order by id
        /// </summary>
        public Purchase_OrderVM GetPurchaseOrderByID(int Purchase_OrderID)
        {
            Purchase_OrderVM result = new Purchase_OrderVM();
            foreach (Purchase_OrderVM fake in fakeorders)
            {
                if (fake.Purchase_Order_ID == Purchase_OrderID)
                {
                    result = fake; break;
                }
            }
            if (result.Purchase_Order_ID == 0)
            {
                throw new ArgumentException("Purchase order not found");
            }

            return result;
        }
    }
}
