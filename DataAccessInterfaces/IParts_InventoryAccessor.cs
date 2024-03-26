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
        /// Retrieves Part_Inventory by Part_InventoryID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        Parts_Inventory selectParts_InventoryByPrimaryKey(int Parts_InventoryID);
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retrieves all parts_inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        List<Parts_Inventory> selectAllParts_Inventory();
        /// <summary>
        /// Max Fare
        /// Created: 2024/02/25
        /// 
        /// Retrieves active parts_inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        List<Parts_Inventory> selectParts_Inventory();
        /// <summary>
        /// Max Fare
        /// Created: 2024/02/04
        /// 
        /// updates the Part record with Parts_Inventory_ID equal to the ID of oldPart,
        /// changing it's values to that of newPart
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        int UpdateParts_Inventory(Parts_Inventory oldPart, Parts_Inventory newPart);

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-23
        /// Deactivates the given part
        /// </summary>
        /// <param name="part">The part to be deactivated</param>
        /// <remarks></remarks>
        int DeactivateParts_Inventory(Parts_Inventory part);

        /// <summary>
        /// Max Fare
        /// Created: 2024-03-24
        /// Attempts to create a new parts_inventory record in the database
        /// </summary>
        /// <param name="newPart">The part to add to the database</param>
        /// <returns>The ID of the newly created record</returns>
        int InsertParts_Inventory(Parts_Inventory newPart);

        ///<summary>
        ///     Selects all parts compatible with a given vehicle model
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
        IEnumerable<Parts_Inventory> SelectPartsCompatibleWithVehicleModelId(int vehicleModelId);

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
        ///    <see cref="Int">Int</see>: 1 if the part compatibility was removed
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> vehicleModelId: The ID of the vehicle model
        ///   <see cref="int">int</see> Parts_InventoryID: The ID of the part
        ///   /// <br /><br />
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>
        /// </remarks>

        int DeleteModelCompatibility(int vehicleModelId, int Parts_InventoryID);

        // Reviewed By: John Beck
    }

}
