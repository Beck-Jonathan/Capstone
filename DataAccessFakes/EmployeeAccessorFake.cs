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
        private IEnumerable<Employee_VM> fakeEmployee = new List<Employee_VM>();
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
                Roles = new List<Role> { new Role { RoleID = "Admin", IsActive = true } }
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
                Roles = new List<Role> { new Role {RoleID = "Maintenance", IsActive = true } }
            });

            fakeEmployee = new List<Employee_VM>() {
                new Employee_VM() {
                    Employee_ID = 100000,
                    Given_Name = "John",
                    Family_Name = "Doe",
                    Address = "123 Main St",
                    Address2 = "",
                    City = "Anytown",
                    State = "CA",
                    Country = "USA",
                    Zip = "12345",
                    Phone_Number = "1234567890",
                    Email = "john@company.com",
                    Position = "Admin",
                    Is_Active = true
                },
                new Employee_VM() {

                    Employee_ID = 100001,
                    Given_Name = "Jane",
                    Family_Name = "Doe",
                    Address = "456 Main St",
                    Address2 = "",
                    City = "Anytown",
                    State = "CA",
                    Country = "USA",
                    Zip = "12345",
                    Phone_Number = "0987654321",
                    Email = "jane@company.com",
                    Position = "Mechanic",
                    Is_Active = true
                },
                new Employee_VM() {
                    Employee_ID = 100002,
                    Given_Name = "Jake",
                    Family_Name = "Doe",
                    Address = "789 Main St",
                    Address2 = "",
                    City = "Anytown",
                    State = "CA",
                    Country = "USA",
                    Zip = "12345",
                    Phone_Number = "1234567890",
                    Email = "jake@company.com",
                    Position = "Driver",
                    Is_Active = true
                }
            };
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

                    foreach (var employeeRole in employee.Roles)
                    {
                        employeeRoles.Add(new Role() { RoleID = employeeRole.RoleID, IsActive = true });
                        rows++;
                    }

                    employee.Roles = employeeRoles;
                }

            }
            if (rows == 0)
            {
                throw new ArgumentException("Error adding employee role");
            }

            return rows;
        }

        /// <summary>
        ///   returns List of employee fakes
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///    <see cref="List">List</see>: returns the list of fake employees.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-03
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        public IEnumerable<Employee_VM> GetEmployees()
        {
            return fakeEmployee;
        }


        public IEnumerable<Role> GetRolesByEmployeeID(int employee_ID)
        {
            IEnumerable<Role> roles = null;
            foreach(var employee in _fakeEmployees)
            {
                if(employee_ID == employee.Employee_ID)
                {
                    roles = employee.Roles;
                } 
               
            }
            if(roles == null)
            {
                throw new ArgumentException("No roles found");
            }
            return roles;
        }
        // Reviewed By Steven Sanchez

        /// <summary>
        ///   retrieve an employee by ID from fakeEmployee 
        /// </summary>
        /// <param>
        ///    Employee_ID
        /// </param>
        /// <returns>
        ///     <see cref="Employee_VM"/>: returns a fake employee.
        /// </returns>
        /// <remarks>
        ///    Parameters: id
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>

        public Employee_VM GetEmployee(int id)
        {
            return fakeEmployee.FirstOrDefault(e => e.Employee_ID == id);
        }
        // reviewed by james
    }
}
// Checked by Nathan Toothaker