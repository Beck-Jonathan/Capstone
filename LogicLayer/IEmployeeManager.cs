using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    public interface IEmployeeManager
    {
        List<Employee_VM> GetAllEmployees();
        bool AddEmployee(Employee_VM newEmployee);
    }
}
// Checked by Nathan Toothaker