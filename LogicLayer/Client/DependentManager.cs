/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Manager class for Dependent entity
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DependentManager : IDependentManager
    {
        IDependentAccessor _dependentAccessor = null;

        // Constructors
        public DependentManager()
        {
            _dependentAccessor = new DependentAccessor();
        }
        // can take a fake
        public DependentManager(IDependentAccessor dependentAccessor)
        {
            _dependentAccessor = dependentAccessor;
        }


        public int AddDependent(Dependent dependent)
        {
            try
            {
                return _dependentAccessor.InsertDependent(dependent);
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Dependent not added", ex);
            }
        }

        /// <summary>
        /// Author: Jacob Rohr
        /// CREATED: 2024-02-13
        /// 
        ///     Method to call accessor in order to retrieve dependents from database
        ///     
        ///     <returns> Returns: <see cref="IEnumerable{DependentVM}"> IEnumerable Of Dependent VM </see></returns>
        ///    
        /// 
        /// </summary>

        public IEnumerable<DependentVM> GetDependentList()
        {
            IEnumerable<DependentVM> dependentList = null;
            try
            {
                dependentList = _dependentAccessor.ListAllDependents();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return dependentList;
        }

    }
}
