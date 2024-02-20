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

    }
}
