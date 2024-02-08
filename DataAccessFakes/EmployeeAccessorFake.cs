using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Fake employee data for unit testing
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    /// initial creation
    /// </remarks>
    public class EmployeeAccessorFake : IEmployeeAccessor
    {
        private List<Employee_VM> _fakeEmployees = new List<Employee_VM>();

        public EmployeeAccessorFake()
        {


            //Fake employee data set up
            _fakeEmployees.Add(new Employee_VM()
            {
                Employee_ID = 1,
                Given_Name = "Tess",
                Family_Name = "DaBest",
                Address = "103 Cherry Lane",
                Address2 = "108 Clinton St",
                City = "Atlanta",
                State = "Georgia",
                Country = "USA",
                Zip = "12345",
                Phone_Number = "5554541234",
                Email = "tess@company.com",
                Position = "Admin Manager",
                Is_Active = true,
                Employee_Roles = new List<Role> { new Role { Role_ID = "Admin", Is_Active = true } }
            });
            _fakeEmployees.Add(new Employee_VM()
            {
                Employee_ID = 2,
                Given_Name = "Fred",
                Family_Name = "Smith",
                Address = "87 State St",
                Address2 = "100 Fountain Lane",
                City = "Chicago",
                State = "Illinois",
                Country = "USA",
                Zip = "86753",
                Phone_Number = "3334541234",
                Email = "fred@company.com",
                Position = "Maintenance II",
                Is_Active = true,
                Employee_Roles = new List<Role> { new Role { Role_ID = "Maintenance", Is_Active = true } }
            });
        }

        /// <summary>
        ///     Test method to insert an Employee recored
        /// </summary>
        /// <param name="newEmployee">
        ///    Object representing the new employee.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The id of the new employee
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Employee_VM">Employee_VM</see> a: Employee object
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentOutOfRangeException">ArgumentException</see>: Employee already exists within system
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial creation
        /// </remarks>
        public int InsertEmployee(Employee_VM newEmployee)
        {
            int newID = 0;
            int originalCount = _fakeEmployees.Count;

            //checks for duplicate entry
            foreach (var employee in _fakeEmployees)
            {
                if (employee.Email == newEmployee.Email)
                {
                    throw new ArgumentException("Employee already exists within the system");
                }
            }
            _fakeEmployees.Add(newEmployee);

            if (originalCount < _fakeEmployees.Count)
            {
                newID = newEmployee.Employee_ID;
            }
            return newID;
        }

        /// <summary>
        ///     Returns all fake employee records
        /// </summary>
       
        /// <returns>
        ///    <see cref="List<Employee_VM> The list of all fake employee objects.
        /// </returns>
        /// <remarks>
        ///
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///    Initial Creation 
        /// </remarks>
        public List<Employee_VM> GetAllEmployees()
        {
            return _fakeEmployees;
        }

        /// <summary>
        ///     
        /// </summary>

        /// <returns>
        ///    <see cref="int">true</see> Number of "rows" added otherwise, <see cref="void">exeception</see>.
        /// </returns>
        /// 

        /// <summary>
        ///   Adds employee roles to fake employee objects
        /// </summary>
        /// <param name="employee_ID">
        ///    The Employee_ID of the employee that will have roles added to it
        /// </param>
        /// <param name="role">
        ///     Role to be added
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The input value multiplied by x.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> a: Value of rows inserted
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException> Error adding employee role
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
        public int InsertEmployeeRoles(int employee_ID, string role)
        {
            IEnumerable<Role> roles;
            List<Role> employeeRoles = new List<Role>();
            int rows = 0;
            foreach (var employee in _fakeEmployees)
            {
                if (employee.Employee_ID == employee_ID)
                {

                    foreach (var employeeRole in employee.Employee_Roles)
                    {
                        employeeRoles.Add(new Role() { Role_ID = employeeRole.Role_ID, Is_Active = true });
                        rows++;
                    }

                    employee.Employee_Roles = employeeRoles;
                }

            }
            if (rows == 0)
            {
                throw new ArgumentException("Error adding employee role");
            }

            return rows;
        }
    }
}
// Checked by Nathan Toothaker