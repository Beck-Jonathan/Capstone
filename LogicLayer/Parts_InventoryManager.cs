/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// Manager class for Parts_Inventory Objects
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Parts_InventoryManager : IParts_InventoryManager
    {
        private IParts_InventoryAccessor _parts_inventoryaccessor = null;
        public Parts_InventoryManager()
        {

            _parts_inventoryaccessor = new Parts_InventoryAccessor(); //use the database
        }

        public Parts_InventoryManager(IParts_InventoryAccessor parts_inventoryaccessor)
        {
            _parts_inventoryaccessor = parts_inventoryaccessor; //use data access fakes
        }

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

        public Parts_Inventory GetParts_InventoryByID(int Parts_InventoryID)
        {
            Parts_Inventory result = null;
            try
            {
                result = _parts_inventoryaccessor.selectParts_InventoryByPrimaryKey(Parts_InventoryID);
                if (result.Item_Description == null) { throw new ArgumentException("Inventory not found"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-04
        /// 
        /// sends the data for oldPart and newPart the the data access layer to update part information
        /// 
        /// returns the number of rows affected
        /// 
        /// <param name="newPart"/> the new data for the part being edited
        /// <param name="oldPart"/> The original data for the part being edited
        /// 
        /// <para result/> the number of rows affected by the change (should be 1)
        /// 
        /// <throws> SQL exception if update fails</throws>
        /// </summary>
        ///
        /// <remarks>
        /// Jonsthan Beck
        /// Updated: 2024/02/06
        /// </remarks>

        public int EditParts_Inventory(Parts_Inventory oldPart, Parts_Inventory newPart)
        {
            int result = 0;
            try
            {
                if(oldPart != null && newPart != null)
                {
                    result = _parts_inventoryaccessor.UpdateParts_Inventory(oldPart, newPart);
                }
                else
                {
                    throw new ArgumentException("parameter was null.");
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// 
        /// Retreives all part inventory records
        /// <throws> Argument Exception if item not found</throws>
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: yyyy/mm/dd 
        public List<Parts_Inventory> GetActiveParts_Inventory()
        {
            List<Parts_Inventory> result = null;
            try
            {
                result = _parts_inventoryaccessor.selectAllParts_Inventory();
                if (result.Count == 0) { throw new ArgumentException("Inventory not found"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        // Reviewed By: John Beck
    }
}
