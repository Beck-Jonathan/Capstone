/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// IManager for Parts_Inventory
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    //needs comment
    public interface IParts_InventoryManager
    {
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retreives Part_Inventory by Part_InventoryID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        Parts_Inventory getParts_InventoryByPrimaryKey(int Parts_InventoryID);
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retreives all Part_Inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 

        List<Parts_Inventory> getAllParts_Inventory();

    }

}
