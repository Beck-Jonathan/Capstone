using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.AppData
{
    public static class Authentication
    {
        public static Employee_VM AuthenticatedEmployee { get; set; } = null;
    }
}