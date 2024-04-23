using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Ben Collins, Everett DeVaux
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    /// 
    ///     Data access class for Parts Requests.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-03-02
    /// <br />
    ///     Initial creation
    ///     <br />
    ///     Added List<Parts_Request> GetPartsRequestDetails();
    /// </remarks>
    public interface IPartsRequestAccessor
    {
        /// <summary>
        ///     Retrieves all Parts Request records from the database.
        /// </summary>
        /// <returns>
        ///    List of <see cref="List{Parts_Request}">Parts_Request</see> objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
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
        List<Parts_Request> GetAllActivePartsRequests();


        /// <summary>
        ///     Retrieves Parts Request Details from the database.
        /// </summary>
        /// <returns>
        ///    <see cref="Parts_Request"></see> objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
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
        Parts_Request GetActivePartsRequestDetails(int partsRequestID);

        /// <summary>
        ///     Deactivates a Request by Id
        /// </summary>
        /// <returns>
        ///    <see cref="int">int</see>.
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
        int DeactivateRequestById(int id);

        /// <summary>
        ///     sends request to POLineItems
        /// </summary>
        /// <returns>
        ///    <see cref="int">Index of new POLineItem</see>.
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
        int approveRequest(int partRequestID, int vendorid, int lineNumber);
    }
}
