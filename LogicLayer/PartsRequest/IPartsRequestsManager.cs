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
    /// UPDATED: 2024-03-02
    /// <br />
    ///     Initial creation
    ///     <br />
    ///     
    /// </remarks>
    public interface IPartsRequestsManager
    {
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
        List<Parts_Request> GetAllPartsRequests();

        /// <summary>
        ///     Retrieves all Parts_Request records from the database to get Request Details
        /// </summary>
        /// <returns>
        ///    <see cref="Parts_Request"></see> objects
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
        Parts_Request GetPartsRequestDetails(int partRequestID);

        /// <summary>
        ///     deactivates Parts_Request record in the database by id
        /// </summary>
        /// <returns>
        ///    List of <see cref="bool">Boolean</see>
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-26
        /// <br />
        bool DeactivatePartsRequest(int id);
    }
}
