using LogicLayer.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;

namespace NightRiderWPF.Login
{
    /// <summary>
    /// Author: Nathan Toothaker
    /// Creation Date: 2024-02-27
    /// Interaction logic for EmployeeLoginPage.xaml
    /// </summary>
    public partial class EmployeeLoginPage : Page
    {
        public EmployeeLoginPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Employee_VM loggedInEmployee = Authentication.AuthenticatedEmployee;
            //Employee_VM loggedInEmployee = new Employee_VM()
            //{
            //    Given_Name = "Michael Cera",
            //    Roles = new List<Role>()
            //    {
            //        new Role()
            //        {
            //            RoleID = "Mike"
            //        },
            //        new Role()
            //        {
            //            RoleID = "John"
            //        }
            //    }
            //};
            lblLoginGreeting.Content = $"Welcome, {loggedInEmployee.Given_Name}. Logged in as: ";
            lblLoginRoles.Content = String.Join("\n", loggedInEmployee.Roles.Select(role => role.RoleID));
        }
    }
}
