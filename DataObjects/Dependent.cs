/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Data Object represnting Dependent
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

namespace DataObjects
{
    public class Dependent
    {
        public int DependentID {  get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB {  get; set; }
        public string Gender { get; set; }
        public string EmergencyContact { get; set; }
        public string ContactRelationship { get; set; }
        public string EmergencyPhone { get; set; }
        public bool IsActive { get; set; }
    }

    public class DependentVM : Dependent
    {
        private IEnumerable<Accommodation> Accommodations { get; set; }
        // private IEnumerable<Client> Guardians { get; set; }
        
    }
}
