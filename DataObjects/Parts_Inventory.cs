/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// Class Description of Parts_Inventory items
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    public class Parts_Inventory
    {
        public int Parts_Inventory_ID { set; get; }
        public string Part_Name { set; get; }
        public int Part_Quantity { set; get; }
        public string Item_Description { set; get; }
        public string Item_Specifications { set; get; }
        public string Part_Photo_URL { set; get; }
        public int Ordered_Qty { set; get; }
        public int Stock_Level { set; get; }
        public bool Is_Active { set; get; }

    }
}
