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
        /// Retrieves Part_Inventory by Part_InventoryID
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

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retrieves all Part_Inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Max Fare
        /// Updated: 2024-02-25
        /// changed name to GetAllParts_Inventory() to better reflect the intended usage
        /// </remarks>

        List<Parts_Inventory> GetAllParts_Inventory();

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-25
        /// 
        /// Retrieves active Part_Inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 

        List<Parts_Inventory> GetActiveParts_Inventory();

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-23
        /// Removes a part from the active inventory
        /// </summary>
        /// <param name="part">The part to remove from inventory</param>
        /// <remarks>
        /// </remarks>
        int RemoveParts_Inventory(Parts_Inventory part);

        /// <summary>
        /// Max Fare
        /// Created: 2024-03-24
        /// Adds a new part to inventory
        /// </summary>
        /// <param name="newPart">The new part to add to inventory</param>
        /// <returns>The ID of the newly created part</returns>
        /// <remarks>
        /// </remarks>
        int AddParts_Inventory(Parts_Inventory newPart);

        ///<summary>
        ///     Retrieves all parts compatible with a given vehicle model
        /// </summary>
        /// <param name="vehicleModelId">
        ///    The ID of the vehicle model
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{Parts_Inventory}">IEnumerable<Parts_Inventory></Parts_Inventory></see>: The parts compativle with the given vehicle model
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> vehicleModelId: The ID of the vehicle model
        /// </remarks>
        IEnumerable<Parts_Inventory> GetPartsCompatibleWithVehicleModelID(int vehicleModelId);

        /// <summary>
        ///     Retrieves all parts compatible with a given vehicle model
        /// </summary>
        /// <param name="model_ID">
        ///    The ID of the vehicle model
        /// </param>
        /// <param name="part_ID">
        ///    The ID of the part
        /// </param>
        /// <returns>
        ///    <see cref="Int">: 1 if the part compatibility was removed.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> model_ID: The ID of the vehicle model
        ///   <see cref="int">int</see> part_ID: The ID of the part
        /// </remarks>

        int PurgeModelPartCompatibility(int model_ID, int part_ID);

        // Reviewed By: John Beck
    }

}
