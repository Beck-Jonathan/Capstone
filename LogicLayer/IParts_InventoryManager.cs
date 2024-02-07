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
        /// <throws> Argument Exce[tion if item not found</throws>
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: yyyy/mm/dd 

        Parts_Inventory GetParts_InventoryByID(int Parts_InventoryID);
        

        /// <summary>
        /// Max Fare
        /// Created: 2024/01/31
        /// 
        /// changes the details of a part
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        int EditParts_Inventory(Parts_Inventory oldPart, Parts_Inventory newPart);

        // Reviewed By: John Beck
    }

}
