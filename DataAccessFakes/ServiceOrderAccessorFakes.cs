﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    ///     Fake ServiceOrder data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// Initial creation
    /// </remarks>
    public class ServiceOrderAccessorFakes : IServiceOrderAccessor
    {
        private List<ServiceOrder_VM> _fakeServiceOrders = new List<ServiceOrder_VM>();
        private List<ServiceOrder> _updatedServiceOrders = new List<ServiceOrder>();
        private Parts_Inventory_Fakes _fakePartsInventory = new Parts_Inventory_Fakes();

        public ServiceOrderAccessorFakes()
        {
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "2GNALDEK9C6340800",
                Service_Order_ID = 100000,
                Critical_Issue = true,
                Service_Type_ID = "Windshield Wiper Replacement",
                Service_Description = "Replace the windshield wipers with OEM wipers",
                Is_Active = true,
                Date_Started = new DateTime(2024, 02, 10),
                Date_Finished = new DateTime(2024, 02, 11)
            });            
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437",
                Service_Order_ID = 100001,
                Critical_Issue = false,
                Service_Type_ID = "Brake Pad Replacement",
                Service_Description = "Replace the brake pads with OEM pads",
                Is_Active = true,
                Date_Started = new DateTime(2024, 02, 10)
            });
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123456",
                Service_Order_ID = 100002,
                Critical_Issue = true,
                Service_Type_ID = "Windshield Wiper Replacement",
                Service_Description = "Replace the windshield wipers with OEM wipers",
                Is_Active = false
                //vehicle = _fakeVehicle.SelectVehicleForLookupList(),
                //partsInventory = _fakePartsInventory.selectAllParts_Inventory()
            });
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437",
                Service_Order_ID = 100003,
                Critical_Issue = false,
                Service_Type_ID = "Brake Pad ",
                Service_Description = "brake pads with OEM pads",
                Date_Started = new DateTime(2022, 02, 10),
                Date_Finished = new DateTime(2023, 01, 14)
            });
        }
        

        /// <summary>
        ///     Returns all fake ServiceOrder_VM records
        /// </summary>

        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> The list of all fake ServiceOrder_VM objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public List<ServiceOrder_VM> GetAllServiceOrders()
        {
            return _fakeServiceOrders;
        }

        /// <summary>
        /// Updates a fake service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the updated details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the update operation:
        /// </returns>
        /// <remarks>
        ///     If the provided <paramref name="serviceOrder"/> is null, an <see cref="ArgumentNullException"/> is thrown.
        ///     The method searches for the fake service order based on the provided Service_Order_ID.
        ///     If found, it updates the fake service order with the new values.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="serviceOrder"/> is null.</exception>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-02-18</created>
        /// <updated>yyyy-MM-dd</updated>
        /// <update>
        /// <summary>
        /// Update comments go here.
        /// </summary>
        /// <remarks>
        /// Explain what you changed in this method.
        /// A new remark should be added for each update to this method.
        /// </remarks>
        /// </update>

        public int UpdateServiceOrder(ServiceOrder serviceOrder)
        {
            if (serviceOrder == null)
            {
                throw new ArgumentNullException(nameof(serviceOrder), "Service order cannot be null.");
            }

            // Find the corresponding fake service order and update it
            var fakeServiceOrder = _fakeServiceOrders.Find(so => so.Service_Order_ID == serviceOrder.Service_Order_ID);
            if (fakeServiceOrder != null)
            {
                // Update the fake service order with the new values
                fakeServiceOrder.VIN = serviceOrder.VIN;
                fakeServiceOrder.Critical_Issue = serviceOrder.Critical_Issue;
                fakeServiceOrder.Service_Type_ID = serviceOrder.Service_Type_ID;
                fakeServiceOrder.Service_Description = serviceOrder.Service_Description;

                // Keep track of the updated service order
                _updatedServiceOrders.Add(serviceOrder);

                // Return 1 to indicate success (assuming the update was successful)
                return 1;
            }

            // Return 0 to indicate failure (service order not found)
            return 0;
        }

        /// <summary>
        /// Creates a fake service order with the provided details.
        /// </summary>
        /// <param name="serviceOrder">The service order object containing the details.</param>
        /// <returns>
        ///     Returns an integer indicating the outcome of the create operation:
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// <contributor>
        ///     Steven Sanchez
        /// </contributor>
        /// <created>2024-03-12</created>
        /// <updated>yyyy-MM-dd</updated>
        /// <update>
        /// <summary>
        /// Update comments go here.
        /// </summary>
        /// <remarks>
        /// Explain what you changed in this method.
        /// A new remark should be added for each update to this method.
        /// </remarks>
        public int CreateServiceOrder(ServiceOrder_VM serviceOrder)
        {

            if (_fakeServiceOrders.Any(so => so.Service_Order_ID == serviceOrder.Service_Order_ID))
            {
                return 0;
            }

            _fakeServiceOrders.Add(serviceOrder);
            return 1;
        }

        public List<ServiceOrder_VM> GetAllServiceTypes()
        {
            List<ServiceOrder_VM> fakeServiceTypes = new List<ServiceOrder_VM>
            {
                new ServiceOrder_VM
                {
                    Service_Type_ID = "Service_Type_1",
                    Service_Description = "Service Type 1 Description",
                    Is_Active = true
                },
                new ServiceOrder_VM
                {
                    Service_Type_ID = "Service_Type_2",
                    Service_Description = "Service Type 2 Description",
                    Is_Active = true
                },

            };

            return fakeServiceTypes;
        }

        /// <summary>
        ///     Returns all fake ServiceOrder_VM records
        /// </summary>

        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> The list of all fake ServiceOrder_VM objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public ServiceOrder_VM SelectServiceOrderByServiceOrderID(int serviceOrderID)
        {
            try
            {
                foreach (var order in _fakeServiceOrders)
                {
                    if (serviceOrderID == order.Service_Order_ID)
                    {
                        return order;
                    }
                }
                throw new ArgumentException("No record with that ID found.");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        /// <summary>
        /// Checks the active status of the given order, the deactivates it
        /// </summary>
        /// <param name="serviceOrder"></param>
        /// <returns>the number of records affected, should only be 1 or 0</returns>
        /// <exception cref="ApplicationException"></exception>
        /// <remarks>
        /// <br/>
        /// Created By: Max Fare
        /// Date: 2024-04-20
        /// </remarks>
        public int DeactivateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            for (int i = 0; i < _fakeServiceOrders.Count; i++)
            {
                if (serviceOrder.Service_Order_ID == _fakeServiceOrders[i].Service_Order_ID)
                {
                    if (_fakeServiceOrders[i].Is_Active)
                    {
                        _fakeServiceOrders[i].Is_Active = false;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            throw new ApplicationException("no service order matching the one given exists");
        }
        /// <summary>
        /// Checks the active status of the given order, the activates it
        /// </summary>
        /// <param name="serviceOrder"></param>
        /// <returns>the number of records affected, should only be 1 or 0</returns>
        /// <exception cref="ApplicationException"></exception>
        /// <remarks>
        /// <br/>
        /// Created By: Max Fare
        /// Date: 2024-04-20
        /// </remarks>
        public int ActivateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            for (int i = 0; i < _fakeServiceOrders.Count; i++)
            {
                if (serviceOrder.Service_Order_ID == _fakeServiceOrders[i].Service_Order_ID)
                {
                    if (_fakeServiceOrders[i].Is_Active)
                    {
                        return 0;
                    }
                    else
                    {
                        _fakeServiceOrders[i].Is_Active = true;
                        return 1;
                    }

                };
            }
            throw new ApplicationException("no service order matching the one given exists");
        }

        /// <summary>
        ///     A method that returns sevice orders that are complete
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see>: The list of all complete service orders.
        /// </returns>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-03-05
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<ServiceOrder_VM> GetAllCompleteServiceOrders()
        {
            List<ServiceOrder_VM> completeServiceOrders = _fakeServiceOrders.Where(serviceOrder => serviceOrder.Date_Finished > serviceOrder.Date_Started).ToList();
            return completeServiceOrders;
        }
        /// <summary>
        ///     A method that returns sevice orders that are incomplete
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see>: The list of all incomplete service orders.
        /// </returns>
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-03-05
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<ServiceOrder_VM> GetAllIncompleteServiceOrders()
        {
            List<ServiceOrder_VM> incompleteServiceOrders = _fakeServiceOrders.Where(serviceOrder => serviceOrder.Date_Finished < serviceOrder.Date_Started).ToList();
            return incompleteServiceOrders;
        }

        /// <summary>
        ///     A method that returns list of fake vehicles with pending service orders
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle_CM}">Vehicle_CM</see>: The list of all vehicles with pending service orders.
        /// </returns>
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-04-26
        /// <br />
        ///    Initial Creation
        /// </remarks>
        public List<Vehicle_CM> GetAllVehiclesWithPendingServiceOrders()
        {
            List<Vehicle_CM> vehiclesWithPendingServiceOrders = new List<Vehicle_CM>();

            Vehicle_CM vehicle1 = new Vehicle_CM
            {
                VIN = "2GNALDEK9C6340800",
                VehicleModelID = 1,
                VehicleMake = "Toyota",
                VehicleModel = "Corolla",
                VehicleMileage = 50000,
                VehicleType = "Sedan",
                ServiceOrders = new List<ServiceOrder_VM>
                {
                    new ServiceOrder_VM
                    {
                        Service_Order_ID = 100001,
                        Critical_Issue = false,
                        Service_Type_ID = "Oil Change",
                        Service_Description = "Perform routine oil change",
                        Date_Started = DateTime.Today.AddDays(-7),
                        Date_Finished = DateTime.MinValue
                    }
                }
            };

            Vehicle_CM vehicle2 = new Vehicle_CM
            {
                VIN = "JTLZE4FEXB1123437",
                VehicleModelID = 2,
                VehicleMake = "Honda",
                VehicleModel = "Accord",
                VehicleMileage = 60000,
                VehicleType = "Sedan",
                ServiceOrders = new List<ServiceOrder_VM>
                {
                    new ServiceOrder_VM
                    {
                        Service_Order_ID = 100002,
                        Critical_Issue = true,
                        Service_Type_ID = "Brake Inspection",
                        Service_Description = "Inspect and replace brake pads if necessary",
                        Date_Started = DateTime.Today.AddDays(-14),
                        Date_Finished = DateTime.MinValue
                    }
                }
            };

            vehiclesWithPendingServiceOrders.Add(vehicle1);
            vehiclesWithPendingServiceOrders.Add(vehicle2);

            return vehiclesWithPendingServiceOrders;
        }

        public int getNextID()
        {
            int max = 0;
            foreach (ServiceOrder s in _fakeServiceOrders)
            {
                if (s.Service_Order_ID > max)
                {
                    max = s.Service_Order_ID;
                }

            }


            return max;
        }
    }
}
