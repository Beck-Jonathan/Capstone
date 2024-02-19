/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Data object representing an accommodation
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
    public class Accommodation
    {
        private int AccommodationID {  get; set; }
        private string AccmodationDescription { get; set; }
        private bool IsActived { get; set; }
    }

    public class AccmmodationVM : Accommodation
    {

    }
}
