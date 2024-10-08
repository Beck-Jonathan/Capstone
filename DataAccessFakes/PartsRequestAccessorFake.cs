﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Fake Parts Request data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-03-02
    /// <br />
    /// Initial creation
    /// </remarks>
    public class PartsRequestAccessorFake : IPartsRequestAccessor
    {
        private List<Parts_Request> _fakePartsRequest = new List<Parts_Request>();
        private List<Parts_Request> _inactivefakePartsRequest = new List<Parts_Request>();

        /// <summary>
        ///     Constructor to setup fake parts request object/s
        /// </summary>
        /// <remarks>
        /// <br /> <br />
        ///    CONTRIBUTOR: Ben Collins, Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        ///    UPDATER: Everett DeVaux
        /// <br />
        ///     UPDATED: 2024-03-27
        /// <br />
        ///     Updated Parts_Request_ID number to be 1 higher than before
        /// </remarks>
        public PartsRequestAccessorFake()
        {
            _fakePartsRequest.Add(new Parts_Request()
            {
                Parts_Request_ID = 100001,
                Date_Requested = DateTime.Parse("2024-01-19"),
                Quantity_Requested = 6,
                Part_Name = "Spark Plug"
            });
            _fakePartsRequest.Add(new Parts_Request()
            {
                Parts_Request_ID = 100002,
                Date_Requested = DateTime.Parse("2024-02-29"),
                Quantity_Requested = 1,
                Part_Name = "Water Pump"
            });

            _fakePartsRequest.Add(new Parts_Request()
            {
                Parts_Request_ID = 100003,
                Part_Name = "Part Name1",
                Quantity_Requested = 12,
                Vehicle_Year = "1972",
                Vehicle_Make = "Toyota",
                Vehicle_Model = "Super Decker",
                Parts_Request_Notes = "Need this part 1",
                Date_Requested = DateTime.Parse("2024-02-28"),
                Employee_ID = 100000
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
        public List<Parts_Request> GetAllActivePartsRequests()
        {
            return _fakePartsRequest;
        }


        /// <summary>
        ///     Returns all fake Parts Requesr Details
        /// </summary>
        /// 
        /// <remarks>
        /// <br />
        ///    <see cref="Parts_Request"></see>: 
        /// <br /> <br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        ///    UPDATER: updaterName
        /// <br />
        ///     UPDATED: yyyy-mm-dd
        /// <br />
        ///     Initial creation
        ///
        /// </remarks>
        public Parts_Request GetActivePartsRequestDetails(int partsRequestID)
        {
            // Iterate through the list to find the matching part request
            foreach (Parts_Request request in _fakePartsRequest)
            {
                if (request.Parts_Request_ID == partsRequestID)
                {
                    return request; // Return the matching request
                }
            }
            return null;
        }

        /// <summary>
        ///     Deactivates a Request by Id
        /// </summary>
        /// <returns>
        ///    <see cref="bool">Boolean</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-26
        /// </remarks>
        public int DeactivateRequestById(int id)
        {
            if (_fakePartsRequest
                .Where(partRequest => partRequest.Parts_Request_ID == id)
                .FirstOrDefault() == null)
            {
                throw new NullReferenceException("part request returned null, cannot be deactivated");
            }
            else
            {
                _inactivefakePartsRequest = _fakePartsRequest
                .Where(partRequest => partRequest.Parts_Request_ID == id)
                .ToList();
            }
            _fakePartsRequest.Remove(_fakePartsRequest.Where(partRequest => partRequest.Parts_Request_ID == id).FirstOrDefault());
            return _inactivefakePartsRequest.Count;
        }

        /// <summary>
        ///     sends request to POLineItems
        /// </summary>
        /// <returns>
        ///    <see cref="int">new Purchase order id</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: request was not sent
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-04-02
        /// </remarks>

        public int approveRequest(int partRequestID, int vendorid, int lineNumber)
        {
            //initializing variables to match the stored procedure
            Parts_Inventory_Fakes pinv = new Parts_Inventory_Fakes();
            PRLineItems_Fakes prli = new PRLineItems_Fakes();
            Purchase_Order_Fakes purchase_Order_Fakes = new Purchase_Order_Fakes();
            POLineItems_Fakes poli = new POLineItems_Fakes();
            VendorAccessorFakes vendfakes = new VendorAccessorFakes();

            Parts_Request request = GetActivePartsRequestDetails(partRequestID);
            Vendor vendor = vendfakes.selectVendorByVendorID(vendorid);

            // starting the real method


            // first query of transaction
            var query = from part in pinv.selectAllParts_Inventory() 
                        join therequestlineitem in prli.GetAllPRLineItems() on part.Parts_Inventory_ID equals therequestlineitem.Parts_Inventory_ID 
                        select new { partsinvID = therequestlineitem.Parts_Inventory_ID, Desc = part.Item_Description };

            // second query
            Parts_Inventory oldpart = pinv.selectParts_InventoryByPrimaryKey(query.FirstOrDefault().partsinvID);
            Parts_Inventory newpart = oldpart;
            newpart.Ordered_Qty += request.Quantity_Requested;
            pinv.UpdateParts_Inventory(oldpart, newpart);

            // third query
            Purchase_Order po = new Purchase_Order() { Vendor_ID = vendor.Vendor_ID };
            int poid = purchase_Order_Fakes.InsertPurchaseOrder(po);

            // forth query
            DeactivateRequestById(request.Parts_Request_ID);

            //final query
            POLineItem pOLineItem = new POLineItem() { PartsInventoryID = query.FirstOrDefault().partsinvID, LineItemDescription = query.FirstOrDefault().Desc, Price = (decimal)3.00*request.Quantity_Requested, LineNumber = lineNumber, LineItemName = request.Part_Name, Quantity = request.Quantity_Requested };
            poli.InsertPOLineItem(pOLineItem);

            return poid;
        }
    }
}
