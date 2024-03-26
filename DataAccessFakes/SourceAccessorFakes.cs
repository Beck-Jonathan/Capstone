using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Jonathan Beck
    /// Created: 2024/01/31
    /// 
    /// Fake data for the source table
    /// </summary>
    public class SourceAccessorFakes : ISourceAccessor
    {
        List<Source> fakeSoources = new List<Source>();
        public SourceAccessorFakes()
        {
            Source FakeSource1 = new Source();
            FakeSource1.Vendor_Id = 1;
            FakeSource1.Parts_inventory_id = 1;
            FakeSource1.Vendor_Part_Number = "1x";
            FakeSource1.Estimated_delivery_time_days = 1;
            FakeSource1.Part_Price = 1;
            FakeSource1.Minimum_order_Qty = 1;
            FakeSource1.Active = true;
            fakeSoources.Add(FakeSource1);
            Source FakeSource2 = new Source();
            FakeSource2.Vendor_Id = 1;
            FakeSource2.Parts_inventory_id = 2;
            FakeSource2.Vendor_Part_Number = "2x";
            FakeSource2.Estimated_delivery_time_days = 1;
            FakeSource2.Part_Price = 3;
            FakeSource2.Minimum_order_Qty = 1;
            FakeSource2.Active = true;
            fakeSoources.Add(FakeSource2);
            Source FakeSource3 = new Source();
            FakeSource2.Vendor_Id = 2;
            FakeSource2.Parts_inventory_id = 3;
            FakeSource2.Vendor_Part_Number = "3x";
            FakeSource2.Estimated_delivery_time_days = 4;
            FakeSource2.Part_Price = 3;
            FakeSource2.Minimum_order_Qty = 1;
            FakeSource2.Active = true;
            fakeSoources.Add(FakeSource2);


        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// retreive source by vendor id and part inventory id
        /// </summary>
        public Source getSourceByVendorIDandPartsInventoryId(int Vendor_Id, int Parts_inventory_id)
        {
            Source result = new Source();
            foreach (Source s in fakeSoources)
            {
                if (s.Vendor_Id == Vendor_Id && s.Parts_inventory_id == Parts_inventory_id)
                {
                    result = s; break;
                }

            }

            return result;
        }
    }
}
