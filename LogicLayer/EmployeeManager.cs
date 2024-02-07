using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-04
    /// <br />
    /// 
    ///     Manager class for Employee that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-02-04
    /// <br />
    /// initial creation
    /// </remarks>
    public class EmployeeManager : IEmployeeManager
    {
        IEmployeeAccessor _employeeAccessor = null;
        //default constructor
        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        //parametized constructor to allow use of data fakes
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }


        /// <summary>
        ///     Inserts new employee record into the Employee table and Employee_Role table
        /// </summary>
        /// <param name="newEmployee">
        ///    Employee_VM object that contains the data of the new employee to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the email address is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Employee_VM">Employee_VM</see> a: Employee object to be inserted
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when error entering record
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public bool AddEmployee(Employee_VM newEmployee)
        {
            //new Employee_ID after insert
            int newID = 0;
            
            try
            {
                newID = _employeeAccessor.InsertEmployee(newEmployee);
             
                int rows = 0;
                // Creates a copy of Employe_Roles to be iterated
                var rolesCopy = newEmployee.Employee_Roles.ToList();
                foreach (var role in rolesCopy)
                {
                    string roleName = role.Role_ID;
                    //inserts new record(s) and returns number of rows inserted
                    rows = _employeeAccessor.InsertEmployeeRoles(newID, roleName);
                }
                // assumes if 0 rows returned that the insert failed
             
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error entering new employee record", ex);
            }
            return (0 != newID);
        }

        /// <summary>
        ///     Retrieves all Employee records from the database
        /// </summary>
        /// <returns>
        ///    List of Employee_VM objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<Employee_VM> GetAllEmployees()
        {
            List<Employee_VM> employees = null;

            try
            {
                employees = _employeeAccessor.GetAllEmployees();
             
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employees;
        }


    }
}
// Checked by Nathan Toothaker