using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Parker Svoboda
    /// Created: 2024/04/09
    /// 
    /// Class Description of PRLineItems_Fakes
    /// </summary>
    ///<remarks>
    ///
    ///</remarks>
    public class PRLineItems_Fakes
    {
        private List<PRLineItems> _PRLineItems = new List<PRLineItems>();
        public PRLineItems_Fakes()
        {
            PRLineItems item1 = new PRLineItems();
            item1.Parts_Request_ID = 100001;
            item1.Parts_Inventory_ID = 4;
            item1.Qty_Requested = 6;
            _PRLineItems.Add(item1);
        }

        public List<PRLineItems> GetAllPRLineItems()
        {
            return _PRLineItems;
        }
    }
}
