using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{

    public interface IEmployeeAccessor
    {
        List<Employee_VM> GetAllEmployees();
        int InsertEmployee(Employee_VM newEmployee);
        int InsertEmployeeRoles(int employee_ID, string role);
    }
}
// Checked by Nathan Toothaker