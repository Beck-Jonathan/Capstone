/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// IAccessor for Parts_Inventory
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

namespace DataAccessInterfaces
{
    //needs comment
    public interface IParts_InventoryAccessor
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
        Parts_Inventory selectParts_InventoryByPrimaryKey(int Parts_InventoryID);
        List<Parts_Inventory> selectAllParts_Inventory();

    }

}
