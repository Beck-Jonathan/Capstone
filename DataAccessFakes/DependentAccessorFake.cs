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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class DependentAccessorFake : IDependentAccessor
    {
        private List<DependentVM> _fakeDependents = new List<DependentVM>();

        public DependentAccessorFake(IEnumerable<DependentVM> fakeDependents)
        {
            _fakeDependents = fakeDependents.ToList();
        }

        public DependentAccessorFake()
        {
            _fakeDependents.Add(new DependentVM()
            {


            });
            _fakeDependents.Add(new DependentVM()
            {
                DependentID = 100001,
                GivenName = "Ash",
                FamilyName = "Ketchum",
                MiddleName = "Pikachu",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Male",
                EmergencyContact = "Delia Ketchum",
                ContactRelationship = "Mom",
                EmergencyPhone = "3192221144",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            });
            _fakeDependents.Add(new DependentVM()
            {
                DependentID = 100002,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = false,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
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

        public IEnumerable<DependentVM> SelectDependentsByClientId(int id)
        {
            List<DependentVM> associatedDependents = new List<DependentVM>();
            foreach (var dependent in _fakeDependents)
            {
                foreach (var CDRole in dependent.ClientDependentRoles)
                {
                    if (CDRole.ClientID == id && dependent.IsActive == true)
                    {
                        associatedDependents.Add(dependent);
                    }
                }
            }
            return associatedDependents;
        }


        IEnumerable<DependentVM> IDependentAccessor.ListAllDependents()
        {
            return _fakeDependents;
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// returns only the selected dependent
        /// </summary>
        /// 

        public DependentVM SelectDependentByID(int id)
        {
            DependentVM dependent = null;
            foreach (var dep in _fakeDependents)
            {
                if (dep.DependentID == id && dep.IsActive == true)
                {
                    dependent = dep;
                }
            }

            if (dependent != null)
            {
                return dependent;
            }
            else
            {
                throw new ArgumentException("Id not found");
            }
        }

        public int UpdateDependent(DependentVM oldDependentInfo, DependentVM newDependentInfo, int clientId)
        {
            DependentVM fakeExistingData = new DependentVM
            {
                DependentID = 100004,
                GivenName = "Gem",
                FamilyName = "Amy",
                MiddleName = "Thyst",
                DOB = new DateTime(2004, 07, 14),
                Gender = "Female",
                EmergencyContact = "Mother Of Pearl",
                ContactRelationship = "Family Friend",
                EmergencyPhone = "3192221333",
                IsActive = true,
                ClientDependentRoles = new List<ClientDependentRole>()
                {
                    new ClientDependentRole()
                    {
                        ClientID = 3
                    }
                }
            };
            // Check for matching old data
            if (oldDependentInfo != null && oldDependentInfo.IsActive)
            {
                bool isMatch = true;
                if (oldDependentInfo.DependentID != fakeExistingData.DependentID) isMatch = false;
                if (!oldDependentInfo.GivenName.Equals(fakeExistingData.GivenName)) isMatch = false;
                if (!oldDependentInfo.FamilyName.Equals(fakeExistingData.FamilyName)) isMatch = false;
                if (!oldDependentInfo.MiddleName.Equals(fakeExistingData.MiddleName)) isMatch = false;
                if (DateTime.Compare(oldDependentInfo.DOB, fakeExistingData.DOB) != 0) isMatch = false;
                if (!oldDependentInfo.Gender.Equals(fakeExistingData.Gender)) isMatch = false;
                if (!oldDependentInfo.EmergencyContact.Equals(fakeExistingData.EmergencyContact)) isMatch = false;
                if (!oldDependentInfo.ContactRelationship.Equals(fakeExistingData.ContactRelationship)) isMatch = false;
                if (!oldDependentInfo.EmergencyPhone.Equals(fakeExistingData.EmergencyPhone)) isMatch = false;
                if (!oldDependentInfo.ClientDependentRoles.Any(oldRole => fakeExistingData
                        .ClientDependentRoles.Any(fakeRole => fakeRole.ClientID == oldRole.ClientID))) isMatch = false;


                // check for valid new data
                bool isValid = true;
                if (!newDependentInfo.FamilyName.isNotEmptyOrNull()) isValid = false;
                if (!newDependentInfo.GivenName.isNotEmptyOrNull()) isValid = false;
                if (!newDependentInfo.DOB.IsValidDate()) isValid = false;
                if (!newDependentInfo.Gender.isNotEmptyOrNull()) isValid = false;
                if (!newDependentInfo.EmergencyContact.isNotEmptyOrNull()) isValid = false;
                if (!newDependentInfo.ContactRelationship.isNotEmptyOrNull()) isValid = false;
                if (!newDependentInfo.EmergencyPhone.isNotEmptyOrNull()) isValid = false;

                if (isMatch && isValid)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}


