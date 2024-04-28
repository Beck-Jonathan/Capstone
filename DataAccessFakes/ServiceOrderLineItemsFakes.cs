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
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    ///     Fake ServiceOrder data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Max Fare
    /// <br />
    /// UPDATED: 2024-04-20
    /// <br />
    /// Added Insert method
    /// </remarks>
    public class ServiceOrderLineItemsFakes : IServiceOrderLineItemsAccessor
    {
        private List<ServiceOrderLineItems> _fakeLines = new List<ServiceOrderLineItems>();
        public ServiceOrderLineItemsFakes()
        {
            _fakeLines.Add(new ServiceOrderLineItems()
            {
                Service_Order_ID = 100000,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 1,
                Quantity = 1
            });
            _fakeLines.Add(new ServiceOrderLineItems()
            {
                Service_Order_ID = 100001,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 2,
                Quantity = 4
            });
            _fakeLines.Add(new ServiceOrderLineItems()
            {
                Service_Order_ID = 100000,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 2,
                Quantity = 2
            });
            _fakeLines.Add(new ServiceOrderLineItems()
            {
                Service_Order_ID = 100001,
                Service_Order_Version = 1,
                Parts_Inventory_ID = 1,
                Quantity = 6
            });
        }
        /// <summary>
        ///     Returns all fake ServiceOrderLineItems records
        /// </summary>

        /// <returns>
        ///    List of all fake <see cref="List{ServiceOrderLineItems}">ServiceOrderLineItems</see>  objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public List<ServiceOrderLineItems> GetAllServiceOrderLineItems()
        {
            try
            {
                return _fakeLines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     checks that the given line item is not a duplicate, then adds the line to the list
        /// </summary>
        /// <param name="item">the line item to attempt to add</param>
        /// <returns>1 if the addition is successful</returns>
        /// <exception cref="ApplicationException">If the given line item is a duplicate</exception>
        /// <remarks>
        /// <br/>
        /// Creator: Max Fare
        /// Date: 2024-04-20
        /// </remarks>
        public int InsertServiceOrderLineItem(ServiceOrderLineItems_VM item)
        {
            foreach (var line in _fakeLines)
            {
                if (line.Service_Order_Version == item.Service_Order_Version
                    && line.Service_Order_ID == item.Service_Order_ID
                    && line.Parts_Inventory_ID == item.Parts_Inventory_ID
                    && line.Quantity == item.Quantity)
                {
                    throw new ApplicationException("No duplicates please.");
                }
            }
            _fakeLines.Add(item);
            return 1;
        }

        
    }
}
