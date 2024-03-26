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
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
        /// Retrieves Part_Inventory by Part_InventoryID
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
        /// 
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
        /// Retrieves all part inventory records
        /// <throws> Argument Exception if item not found</throws>
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: 2024-02-25
        /// updated name to better indicate usage, and removed unnecesary exception thrown when no parts are found
        /// </remarks>
        public List<Parts_Inventory> GetAllParts_Inventory()
        {
            List<Parts_Inventory> result = null;
            try
            {
                result = _parts_inventoryaccessor.selectAllParts_Inventory();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-25
        /// 
        /// Retrieves active part inventory records
        /// <throws> Argument Exception if item not found</throws>
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public List<Parts_Inventory> GetActiveParts_Inventory()
        {
            List<Parts_Inventory> result = null;
            try
            {
                result = _parts_inventoryaccessor.selectParts_Inventory();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Max Fare
        /// 2024-02-23
        /// Removes the given part from the active inventory
        /// </summary>
        /// <param name="part">The Part to be removed</param>
        /// <returns>an integer representing the success of the removal</returns>
        public int RemoveParts_Inventory(Parts_Inventory part)
        {
            int result = 0;
            try
            {
                result = _parts_inventoryaccessor.DeactivateParts_Inventory(part);
                if (result == 0)
                {
                    throw new ArgumentException("No part with ID " + part.Parts_Inventory_ID + " found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-03-24
        /// Adds a new part to inventory
        /// </summary>
        /// <param name="newPart">The new part to add to inventory</param>
        /// <returns>The ID of the newly created part</returns>
        /// <exception cref="ApplicationException">If something goes wrong in the database</exception>
        /// <remarks>
        /// </remarks
        public int AddParts_Inventory(Parts_Inventory newPart)
        {
            int id = -1;
            try
            {
                id = _parts_inventoryaccessor.InsertParts_Inventory(newPart);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong", ex);
            }
            return id;
        }

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
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-22
        /// </remarks>
        public IEnumerable<Parts_Inventory> GetPartsCompatibleWithVehicleModelID(int vehicleModelId)
        {
            IEnumerable<Parts_Inventory> result = null;

            try
            {
                result = _parts_inventoryaccessor.SelectPartsCompatibleWithVehicleModelId(vehicleModelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }

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
        ///    <see cref="Int">int</see>: 1 if the part compatibility was removed
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> model_ID: The ID of the vehicle model
        ///   <see cref="int">int</see> part_ID: The ID of the part
        ///   /// <br /><br />
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>
        /// </remarks>

        public int PurgeModelPartCompatibility(int model_ID, int part_ID) {
            int result = 0;
            try
            {
                result= _parts_inventoryaccessor.DeleteModelCompatibility(model_ID, part_ID);
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
