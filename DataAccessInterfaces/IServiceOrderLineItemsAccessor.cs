using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IServiceOrderLineItemsAccessor
    {
        List<ServiceOrderLineItems> GetAllServiceOrderLineItems();
        /// <summary>
        /// Inserts a service order line item record into the database
        /// <br />
        /// <br />
        ///    Max Fare
        /// <br />
        ///    CREATED: 2024-04-02
        /// </summary>
        /// <returns><see cref="int">An identifier of this line item</see></returns>
        int InsertServiceOrderLineItem(ServiceOrderLineItems_VM item);
    }
}
