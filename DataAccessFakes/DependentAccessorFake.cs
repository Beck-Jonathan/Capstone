/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Fake for Dependent Accessor
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class DependentAccessorFake : IDependentAccessor
    {
        private List<DependentVM> _fakeDependents = new List<DependentVM>();

        public DependentAccessorFake()
        {
            _fakeDependents.Add(new DependentVM()
            {
                DependentID = 100000,
                GivenName = "Jeremy",
                FamilyName = "Smith",
                MiddleName = "Lanklema",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Male",
                EmergencyContact = "Nancy Smith",
                ContactRelationship = "Mom",
                EmergencyPhone = "3192221111",
                IsActive = false,


            });
            _fakeDependents.Add(new DependentVM()
            {
                DependentID = 100000,
                GivenName = "Ash",
                FamilyName = "Ketchum",
                MiddleName = "Pikachu",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Male",
                EmergencyContact = "Delia Ketchum",
                ContactRelationship = "Mom",
                EmergencyPhone = "3192221144",
                IsActive = true,



            });
            _fakeDependents.Add(new DependentVM()
            {
                DependentID = 100000,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = false,


            });
        }

        public int InsertDependent(Dependent dependent)
        {
            if (dependent != null)
            {
                return 1;
            }
            else { return 0; }
        }

        IEnumerable<DependentVM> IDependentAccessor.ListAllDependents()
        {
            return _fakeDependents;
        }
    }
}


