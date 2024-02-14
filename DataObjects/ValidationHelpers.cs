using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-06
    ///     Data 
    /// </summary>
    public class ValidationHelpers
    {
        public static bool IsValidVehicleNumber(string vehicleNumber) 
        {
            /// <summary>
            ///     Checks a vehicle number is not blank and the right size.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="vehicleNumber">vehicleNumber</see> vehicleNumber: The vehicle number to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>

            return (!vehicleNumber.Equals("") && vehicleNumber.Length < 11 && vehicleNumber.Length > 0);
        }

        public static bool IsValidVIN(string vin)
        {
            /// <summary>
            ///     Checks a vehicle number is not blank and the right size.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="vehicleNumber">vehicleNumber</see> vehicleNumber: The vehicle number to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return vin.Length == 17;
        }

        public static bool IsValidYear(int year)
        {
            /// <summary>
            ///     Checks a year is not blank and the right size.
            /// </summary>
            /// <param name="year">
            ///    The vehicle number to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="year">Year</see> year: The year to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return (year >= 1900 && year <= 9999);
        }

        public static bool IsValidMileage(int mileage)
        {
            /// <summary>
            ///     Checks a mileage is entered and not negative.
            /// </summary>
            /// <param name="mileage">
            ///    The mileageto be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="mileage">mileage</see> mileage: The mileage to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return mileage >= 0;
        }

        public static bool IsValidSeatCount(int seatCount)
        {
            /// <summary>
            ///     Checks a seat count is entered and not negative.
            /// </summary>
            /// <param name="seatCount">
            ///    The seatCount to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="seatCount">seatCount</see> seatCount: The seatConut to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return seatCount >= 0;
        }

        public static bool IsValidLicensePlate(string licensePlate)
        {
            /// <summary>
            ///     Checks a licensePlate is not blank and the right size.
            /// </summary>
            /// <param name="licensePlate">
            ///    The licensePlate to be checked.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: True/False if the validation passes.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="licensePlate">licensePlate</see> licensePlate: The license plate to be checked.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
            return (!licensePlate.Equals("") && licensePlate.Length < 11 && licensePlate.Length > 0);
        }
    }
}
