/// <summary>
/// Jonathan Beck
/// Created: 2024/01/31
/// 
/// Data Access Fakes for Parts_Inventory Objects
/// </summary>
///
/// <remarks>
/// Jonsthan Beck
/// Updated: 2024/02/06
/// </remarks>

using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessFakes
{
    public class Parts_Inventory_Fakes : IParts_InventoryAccessor
    {
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Iniailize Test Data
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Jonsthan Beck
        /// Updated: 2024/02/06
        /// <br /><br />
        /// 
        /// 
        /// Updater: Parker Svoboda
        /// <br />
        /// Updated 2024/04/09
        /// <br />
        ///     added parts that match with Parts request
        /// </remarks>

        private List<Parts_Inventory> _fakeparts = new List<Parts_Inventory>();
        private List<VehicleModelVM> _fakeModels = new List<VehicleModelVM>();
       
        public Parts_Inventory_Fakes()
        {
            Parts_Inventory part1 = new Parts_Inventory();
            part1.Parts_Inventory_ID = 1;
            part1.Part_Quantity = 1;
            part1.Part_Photo_URL = "www.google.com";
            part1.Part_Name = "Sprocket";
            part1.Ordered_Qty = 4;
            part1.Item_Description = "Mr. Spacely's";
            part1.Item_Specifications = "Tight as can be!";
            part1.Stock_Level = 20;
            part1.Is_Active = true;
            part1.CompatibleVehicleModelIds = new List<int> { 1 };
            _fakeparts.Add(part1);
            Parts_Inventory part2 = new Parts_Inventory();
            part2.Parts_Inventory_ID = 2;
            part2.Part_Quantity = 1;
            part2.Part_Photo_URL = "www.yahoo.com";
            part2.Part_Name = "widget";
            part2.Ordered_Qty = 5;
            part2.Item_Description = "Mr. Fred's";
            part2.Item_Specifications = "Loose as can be!";
            part2.Stock_Level = 20;
            part2.Is_Active = false;
            _fakeparts.Add(part2);
            Parts_Inventory part3 = new Parts_Inventory();
            part3.Parts_Inventory_ID = 3;
            part3.Part_Quantity = 1;
            part3.Part_Photo_URL = "www.kirkwood.edu";
            part3.Part_Name = "Wire Spool";
            part3.Ordered_Qty = 6;
            part3.Item_Description = "Mr. Beely's";
            part3.Item_Specifications = "Yep, that's wire!";
            part3.Stock_Level = 20;
            part3.Is_Active = true;
            part3.CompatibleVehicleModelIds = new List<int> { 1 };
            _fakeparts.Add(part3);

            Parts_Inventory part4 = new Parts_Inventory();
            part4.Parts_Inventory_ID = 4;
            part4.Part_Quantity = 1;
            part4.Part_Photo_URL = "www.kirkwood.edu";
            part4.Part_Name = "Spark Plug";
            part4.Ordered_Qty = 5;
            part4.Item_Description = "Mr. Fred's";
            part4.Item_Specifications = "That's Sparkn' alright!";
            part4.Stock_Level = 0;
            part4.Is_Active = true;
            _fakeparts.Add(part4);

            //created by James Williams
            _fakeModels.Add(new VehicleModelVM
            {
                VehicleModelID = 1,
                Name = "Chevy",
                Make = "Tercel",
                Year = 1994,
                Compatible_Parts = new List<Parts_Inventory>() {part1}
                
            });

            _fakeModels.Add(new VehicleModelVM
            {
                VehicleModelID = 2,
                Name = "Ford",
                Make = "F150",
                Year = 2022,
                Compatible_Parts = new List<Parts_Inventory>() { part2 }
            });

            _fakeModels.Add(new VehicleModelVM
            {
                VehicleModelID = 3,
                Name = "Camry",
                Make = "Toyota",
                Year = 2010,
                Compatible_Parts = new List<Parts_Inventory>() { part3 }
            });

        }

        public int AddModelCompatibility(int vehicleModelID, int partsInventoryID)
        {
            int rows = 0;
            VehicleModelVM vm = null;
            Parts_Inventory selectedPart = null;
            List<Parts_Inventory> compatibleParts = null;
            
            //grab the model from fakes
            vm = _fakeModels.FirstOrDefault(m => m.VehicleModelID == vehicleModelID);
            //grab the part from fakes
            selectedPart = _fakeparts.FirstOrDefault(p => p.Parts_Inventory_ID == partsInventoryID);
            
            //get original list of Compatible_Parts from modelVM
            compatibleParts = vm.Compatible_Parts.ToList();
            //redundancy check of parts
            Parts_Inventory partExists = vm.Compatible_Parts.FirstOrDefault(p => p.Parts_Inventory_ID == partsInventoryID);
            if(partExists != null) 
            {
                throw new ApplicationException("Part already compatible");
            }
            //add part if not already on list
            compatibleParts.Add(selectedPart);
            //overwrite modelVM Compatible_Parts with new list
            vm.Compatible_Parts = compatibleParts;
            return ++rows;
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024-02-23
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public int DeactivateParts_Inventory(Parts_Inventory part)
        {
            int result = 0;
            for (int i = 0; i < _fakeparts.Count; i++)
            {
                if (_fakeparts[i].Parts_Inventory_ID == part.Parts_Inventory_ID)
                {
                    _fakeparts[i].Is_Active = false;
                    result = 1;
                    break;
                }
            }
            if(result == 0)
            {
                throw new ArgumentException("No part found");
            }
            return result;
        }

        public int InsertParts_Inventory(Parts_Inventory newPart)
        {
            if (!String.IsNullOrEmpty(newPart.Part_Name)
                && !String.IsNullOrEmpty(newPart.Item_Description)
                && !String.IsNullOrEmpty(newPart.Item_Specifications)
                && !String.IsNullOrEmpty(newPart.Part_Photo_URL))
            {
                newPart.Parts_Inventory_ID = _fakeparts[_fakeparts.Count - 1].Parts_Inventory_ID + 1;
                _fakeparts.Add(newPart);
                return newPart.Parts_Inventory_ID;
            }
            else
            {
                throw new Exception("Incomplete Object cannot be added");
            }
        }
        //Jonathan Beck March 24 2024
        public int DeleteModelCompatibility(int vehicleModelId, int Parts_InventoryID)
        {
            int start = SelectPartsCompatibleWithVehicleModelId(vehicleModelId).Count();
            foreach (Parts_Inventory p in _fakeparts) {
                if (p.Parts_Inventory_ID == Parts_InventoryID && p.CompatibleVehicleModelIds.Contains(vehicleModelId)) { 
                
                    p.CompatibleVehicleModelIds.Remove(vehicleModelId);
                    break;
                }
            
            }
            int end = SelectPartsCompatibleWithVehicleModelId(vehicleModelId).Count();



            return start-end;
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// 
        /// Returns all parts_invnetory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>

        public List<Parts_Inventory> selectAllParts_Inventory()
        {
            return _fakeparts;
        }

        /// <summary>
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
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-22
        /// </remarks>
        public IEnumerable<Parts_Inventory> SelectPartsCompatibleWithVehicleModelId(int vehicleModelId)
        {
            return _fakeparts.Where(x => x.CompatibleVehicleModelIds != null &&  x.CompatibleVehicleModelIds.Contains(vehicleModelId));
        }

        /// <summary>
        /// Max Fare
        /// Created: 2024/02/25
        /// 
        /// Returns active parts_inventory records
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>

        public List<Parts_Inventory> selectParts_Inventory()
        {
            List<Parts_Inventory> result = new List<Parts_Inventory>();
            foreach(var p in _fakeparts)
            {
                if (p.Is_Active)
                {
                    result.Add(p);
                }
            }
            return result;
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/01/31
        /// 
        /// Retrieve Part_Inventory by primary Key. Throws argument exception if the Part_Inventory can not be found.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// <remarks>
        /// Update Name
        /// Updated: 2024/--/--
        /// 
        /// </remarks>
        /// <param name="Parts_InventoryID"></param>
        /// <throws Argument Exception >   </throws>
        public Parts_Inventory selectParts_InventoryByPrimaryKey(int Parts_InventoryID)
        {
            Parts_Inventory result = new Parts_Inventory();
            foreach (Parts_Inventory part in _fakeparts)
            {
                if (part.Parts_Inventory_ID == Parts_InventoryID)
                {
                    result.Parts_Inventory_ID = part.Parts_Inventory_ID;
                    result.Part_Quantity = part.Part_Quantity;
                    result.Part_Photo_URL = part.Part_Photo_URL;
                    result.Part_Name = part.Part_Name;
                    result.Ordered_Qty = part.Ordered_Qty;
                    result.Item_Description = part.Item_Description;
                    result.Item_Specifications = part.Item_Specifications;
                    result.Stock_Level = part.Stock_Level;
                    result.Is_Active = part.Is_Active;

                }
            }
            if (result.Item_Description == null) { throw new ArgumentException("Part not found"); }



            return result;
        }

        public int UpdateParts_Inventory(Parts_Inventory oldPart, Parts_Inventory newPart)
        {
            int result = 0;
            if (oldPart != null && newPart != null)
            {
                foreach(Parts_Inventory part in _fakeparts)
                {
                    if(part.Parts_Inventory_ID ==  oldPart.Parts_Inventory_ID)
                    {
                        part.Part_Quantity = newPart.Part_Quantity;
                        part.Part_Photo_URL = newPart.Part_Photo_URL;
                        part.Part_Name = newPart.Part_Name;
                        part.Ordered_Qty = newPart.Ordered_Qty;
                        part.Item_Description = newPart.Item_Description;
                        part.Item_Specifications = newPart.Item_Specifications;
                        part.Stock_Level = newPart.Stock_Level;
                        part.Is_Active = newPart.Is_Active;
                        result = 1;
                    }
                }
            }
            return result;
        }

        public int updateQuantity(int partsInventoryID, int quantity)
        {
            int result = 0;
            foreach (Parts_Inventory part in _fakeparts)
            {
                if (part.Parts_Inventory_ID == partsInventoryID)
                {
                    part.Ordered_Qty = quantity;
                    result++;
                    break;
                }
            }


            return result;
        }

        // Reviewed By: John Beck
    }
}
