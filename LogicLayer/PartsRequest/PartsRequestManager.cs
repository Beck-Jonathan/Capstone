using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.PartsRequest
{
    /// <summary>
    /// AUTHOR: Ben Collins, Everett DeVaux
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    /// 
    ///     Manager class for Parts Request that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2023-03-02
    /// <br />
    ///     Initial creation
    ///     <br />
    ///     Created public List<Parts_Request> GetPartsRequestDetails()
    /// </remarks>
    public class PartsRequestManager : IPartsRequestsManager
    {
        IPartsRequestAccessor _partsRequestAccessor = null;

        // Default Constuctor
        public PartsRequestManager()
        {
            _partsRequestAccessor = new PartsRequestAccessor();
        }

        // Parametized constructor to allow use of fake data
        public PartsRequestManager(IPartsRequestAccessor partsRequestAccessor)
        {
            _partsRequestAccessor = partsRequestAccessor;
        }

        /// <summary>
        ///     Retrieves all Parts_Request records from the database
        /// </summary>
        /// <returns>
        ///    List of <see cref="List{Parts_Request}">Parts_Request</see> objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<Parts_Request> GetAllPartsRequests()
        {
            List<Parts_Request> partsRequests = null;

            try
            {
                partsRequests = _partsRequestAccessor.GetAllActivePartsRequests();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return partsRequests;
        }

        /// <summary>
        ///     Retrieves all Parts_Request records from the database for request details
        /// </summary>
        /// <returns>
        ///    List of <see cref="Parts_Request">Parts_Request</see> objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public Parts_Request GetPartsRequestDetails(int partRequestID)
        {
            Parts_Request partsRequestsdetails = null;

            try
            {
                partsRequestsdetails = _partsRequestAccessor.GetActivePartsRequestDetails(partRequestID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return partsRequestsdetails;
        }
    }
}
