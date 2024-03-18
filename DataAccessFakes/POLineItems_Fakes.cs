using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class POLineItems_Fakes : IPOLineItemsAccessor
    {
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Iniailize Test Data
        /// </summary>
        ///
        /// <remarks>
        /// Jonsthan Beck
        /// Updated: 2024/02/06
        /// </remarks>

        private List<POLineItemVM> fakePOLines = new List<POLineItemVM>();
        Parts_Inventory_Fakes parts_Inventory_Fakes = new Parts_Inventory_Fakes();

        public POLineItems_Fakes()
        {
            POLineItemVM POLine1 = new POLineItemVM();
            POLine1.PurchaseOrderID = 1;
            POLine1.PartsInventoryID = 1;
            POLine1.LineNumber = 1;
            POLine1.LineItemName = "Sprocket";
            POLine1.Quantity = 5;
            POLine1.LineItemDescription = "Mr. Spacely's";
            POLine1.isActive = true;
            POLine1.Part = parts_Inventory_Fakes.selectParts_InventoryByPrimaryKey(1);
            fakePOLines.Add(POLine1);
            POLineItemVM POLine2 = new POLineItemVM();
            POLine2.PurchaseOrderID = 1;
            POLine2.PartsInventoryID = 2;
            POLine2.LineNumber = 2;
            POLine1.LineItemName = "Puck";
            POLine2.Quantity = 6;
            POLine2.LineItemDescription = "Mr. Pucks's";
            POLine2.isActive = true;
            POLine2.Part = parts_Inventory_Fakes.selectParts_InventoryByPrimaryKey(2);
            fakePOLines.Add(POLine2);
            POLineItemVM POLine3 = new POLineItemVM();
            POLine3.PurchaseOrderID = 2;
            POLine3.PartsInventoryID = 1;
            POLine3.LineNumber = 1;
            POLine3.LineItemName = "Rock";
            POLine3.Quantity = 10;
            POLine3.LineItemDescription = "That's a rock!";
            POLine3.isActive = true;
            POLine3.Part = parts_Inventory_Fakes.selectParts_InventoryByPrimaryKey(3);
            fakePOLines.Add(POLine3);


        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Get single PoLineItemVM
        /// </summary>
        public POLineItemVM GetPOLineItem(int purchaseOrderID, int lineNumber)
        {
            POLineItemVM polineItem = new POLineItemVM();
            foreach (POLineItemVM test in fakePOLines)
            {
                if (test.PurchaseOrderID == purchaseOrderID && test.LineNumber == lineNumber)
                {
                    polineItem = test; break;

                }

            }
            if (polineItem.PurchaseOrderID == 0) { throw new ArgumentException("PO Line Item not found"); }
            return polineItem;
        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Get all PoLineItemVM for a purchse order
        /// </summary>
        public List<POLineItemVM> GetPOLineItemsByPurchseOrder(int purchaseOrderID)
        {
            List<POLineItemVM> results = new List<POLineItemVM>();
            foreach (POLineItemVM test in fakePOLines)
            {
                if (test.PurchaseOrderID == purchaseOrderID)
                {
                    results.Add(test);

                }

            }
            if (results.Count == 0) { throw new ArgumentException("PO Line Items not found"); }
            return results;
        }
    }
}
