/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Interface for the Dependent Data Access Classes
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IDependentAccessor
    {
        int InsertDependent(Dependent dependent);

        /// <summary>
        /// CONTRIBUTOR: Jacob Rohr
        /// CREATED: 2024-02-13
        /// 
        ///     A Method to retrieve a complete list of all dependents
        ///     
        /// <returns> Returns: <see cref = "IEnumerable{DependentVM}" > IEnumerable Of Dependent VM</see></returns>
        ///     
        /// </summary>
        IEnumerable<DependentVM> ListAllDependents();

        /// <summary>
        /// CONTRIBUTOR: Michael Springer
        /// CREATED: 2024-02-19
        /// 
        ///     Retrieving dependent by client ID
        ///     
        /// <returns> Returns: <see cref = "IEnumerable{DependentVM}" > IEnumerable Of Dependent VM</see></returns>
        ///     
        /// </summary>
        IEnumerable<DependentVM> SelectDependentsByClientId(int id);
        /// <summary>
        /// CONTRIBUTOR: Michael Springer
        /// CREATED: 2024-03-03
        /// 
        ///     Retrieves a single dependentVM from dependentID
        ///     
        /// <returns> Returns: <see cref = "DependentVM" >Dependent VM</see></returns>
        ///     
        /// </summary>
        DependentVM SelectDependentByID(int id);
        /// <summary>
        /// CONTRIBUTOR: Michael Springer
        /// CREATED: 2024-03-03
        /// 
        ///     update dependent return row affected
        ///     
        /// <returns> Returns: <see cref = "int" >rows affected</see></returns>
        ///     
        /// </summary>
        int UpdateDependent(DependentVM oldDependentInfo, DependentVM newDependentInfo, int clientId);
    }


}
