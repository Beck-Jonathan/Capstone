using DataAccessFakes;
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
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    /// 
    ///     Manager class for ServiceOrder that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Steven Sanchez
    /// <br />
    /// UPDATED: 2024-02-18
    /// <br />
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-03-19
    /// <br />
    ///     Initial creation
    ///     Added UpdateServiceOrder(ServiceOrder serviceOrder)
    ///     Added SelectServiceOrderByServiceOrderID()
    /// </remarks>
    public class ServiceOrderManager : IServiceOrderManager
    {
        IServiceOrderAccessor _serviceOrderAccessor = null;
        IParts_InventoryManager _inventoryManager = null;
        IServiceOrderLineItemsManager _lineItemsManager = null;


        // Default Constuctor
        public ServiceOrderManager()
        {
            _serviceOrderAccessor = new ServiceOrderAccessor();
            _inventoryManager = new Parts_InventoryManager();
            _lineItemsManager = new ServiceOrderLineItemsManager();
        }

        // Parametized constructor to allow use of fake data
        public ServiceOrderManager(IServiceOrderAccessor serviceOrderAccessor)
        {
            _serviceOrderAccessor = serviceOrderAccessor;
            _inventoryManager = new Parts_InventoryManager(new Parts_Inventory_Fakes());
            _lineItemsManager = new ServiceOrderLineItemsManager(new ServiceOrderLineItemsFakes());

        }


        /// <summary>
        ///     Retrieves all ServiceOrder_VM records from the database
        /// </summary>
        /// <returns>
        ///    <see cref="List{ServiceOrder_VM}">ServiceOrder_VM</see> List of ServiceOrder_VM objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<ServiceOrder_VM> GetALlServiceOrders()
        {
            List<ServiceOrder_VM> serviceOrders = null;

            try
            {
                serviceOrders = _serviceOrderAccessor.GetAllServiceOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serviceOrders;
        }
        /// <summary>
        ///    Updates a service order record in the database
        /// </summary>
        /// <returns>
        ///   The number of rows affected by the update operation
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public int UpdateServiceOrder(ServiceOrder serviceOrder)
        {
            int rowsAffected = 0;

            try
            {
                rowsAffected = _serviceOrderAccessor.UpdateServiceOrder(serviceOrder);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Creates a service order with the provided details.
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
        /// </update>
        public bool CreateServiceOrder(ServiceOrder_VM serviceOrder)
        {
            bool result;

            try
            {
                result = (1 == _serviceOrderAccessor.CreateServiceOrder(serviceOrder));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Order not added", ex);
            }
            return result;
        }

        /// <summary>
        ///     Retrieves all ServiceOrder_VM records from the ServiceOrderAccessor and,
        ///     <br/>
        ///     the Vehicle, ServiceOrderLineItems, and Parts_Inventory from their 
        ///     <br/>
        ///     respective managers
        /// </summary>
        /// <returns>
        ///    <see cref="ServiceOrder_VM">ServiceOrder_VM</see> ServiceOrder_VM object
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public ServiceOrder_VM SelectServiceOrderByServiceOrderID(int serviceOrderID)
        {
            // use other objects managers existing Managers methods to populate the data.

            ServiceOrder_VM serviceOrder = new ServiceOrder_VM();
            IVehicleManager vehicleManager = new VehicleManager();
            IServiceOrderLineItemsManager LineItemManager = new ServiceOrderLineItemsManager();


            try
            {
                serviceOrder = _serviceOrderAccessor.SelectServiceOrderByServiceOrderID(serviceOrderID);
                serviceOrder.serviceOrderLineItems = LineItemManager.GetServiceOrderLineItems();
                serviceOrder.vehicle = vehicleManager.GetVehicleByVIN(serviceOrder.VIN);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return serviceOrder;
        }

        /// <summary>
        /// Marks a service order as completed, adding line 
        /// <br/>
        /// items to the order, and changing QoH for the parts used
        /// <br/>
        /// <br/>
        /// Author: Max Fare
        /// <br/>
        /// Created: 2024-04-01
        /// </summary>
        /// <param name="serviceOrder"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">If a step in the completion process fails</exception>
        public int CompleteServiceOrder(ServiceOrder_VM serviceOrder)
        {
            int result = 0;
            List<Parts_Inventory> inventory = null;
            //deactivate the service order
            try
            {
                if(1 != _serviceOrderAccessor.DeactivateServiceOrder(serviceOrder))
                {
                    throw new Exception("Deactivation failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order Failure", ex);
            }
            //add line items to the service order
            try
            {
                for(int i = 0; i < serviceOrder.serviceOrderLineItems.Count; i++)
                {
                    ServiceOrderLineItems_VM temp = new ServiceOrderLineItems_VM()
                    {
                        Service_Order_ID = serviceOrder.serviceOrderLineItems[i].Service_Order_ID,
                        Service_Order_Version = serviceOrder.serviceOrderLineItems[i].Service_Order_Version,
                        Quantity = serviceOrder.serviceOrderLineItems[i].Quantity,
                        Parts_Inventory_ID = serviceOrder.serviceOrderLineItems[i].Parts_Inventory_ID
                    };
                    foreach(var oldLine in _lineItemsManager.GetServiceOrderLineItems())
                    {
                        if(oldLine.Service_Order_ID == temp.Service_Order_ID
                            && oldLine.Service_Order_Version == temp.Service_Order_Version
                            && oldLine.Parts_Inventory_ID == temp.Parts_Inventory_ID)
                        {
                            serviceOrder.serviceOrderLineItems.Remove(serviceOrder.serviceOrderLineItems[i]);
                        }
                    }
                    foreach (var line in serviceOrder.serviceOrderLineItems)
                    {
                        if (line.Service_Order_ID == temp.Service_Order_ID
                            && line.Service_Order_Version == temp.Service_Order_Version
                            && line.Parts_Inventory_ID == temp.Parts_Inventory_ID)
                        {
                            _lineItemsManager.AddServiceOrderLineItem(temp);
                        }
                    }                      
                }
                result = 1;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("A line item already exists, or failed to complete.", ex);
            }
            //change inventory numbers to match what the service order used
            try
            {
                inventory = _inventoryManager.GetAllParts_Inventory();

                for (int i = 0; i < serviceOrder.serviceOrderLineItems.Count; i++)
                {
                    Parts_Inventory newQty = _inventoryManager.GetParts_InventoryByID(serviceOrder.serviceOrderLineItems[i].Parts_Inventory_ID);
                    int tempQty = newQty.Part_Quantity;
                    newQty.Part_Quantity -= serviceOrder.serviceOrderLineItems[i].Quantity;

                    if (0 == _inventoryManager.EditParts_Inventory(_inventoryManager.GetParts_InventoryByID(serviceOrder.serviceOrderLineItems[i].Parts_Inventory_ID), newQty))
                    {
                        //revert inventory back to what it was before updating
                        foreach (Parts_Inventory part in inventory)
                        {
                            _inventoryManager.EditParts_Inventory(_inventoryManager.GetParts_InventoryByID(part.Parts_Inventory_ID), part);
                        }
                        _serviceOrderAccessor.ActivateServiceOrder(serviceOrder);
                        throw new ArgumentException("A part failed to update", "Please contact IT");
                    }
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Inventory Update Failure", ex);
            }
            
            return result;
        }
    }
}
