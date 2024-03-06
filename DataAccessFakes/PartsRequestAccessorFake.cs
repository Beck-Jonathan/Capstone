using DataAccessInterfaces;
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
    ///     Fake Parts_Request data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// Initial creation
    /// </remarks>
    public class PartsRequestAccessorFake : IPartsRequestAccessor
    {
        private List<Parts_Request> _fakePartsRequest = new List<Parts_Request>();


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
        ///     UPDATED: 2024-03-02
        /// <br />
        ///     Initial creation
        /// </remarks>
        public PartsRequestAccessorFake()
        {
            _fakePartsRequest.Add(new Parts_Request()
            {
                Parts_Request_ID = 100000,
                Date_Requested = DateTime.Parse("2024-01-19"),
                Quantity_Requested = 6,
                Part_Name = "Spark Plug"
            });
            _fakePartsRequest.Add(new Parts_Request()
            {
                Parts_Request_ID = 100001,
                Date_Requested = DateTime.Parse("2024-02-29"),
                Quantity_Requested = 1,
                Part_Name = "Water Pump"
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
    }
}
