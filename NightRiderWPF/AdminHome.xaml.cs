using DataObjects;
using LogicLayer;
using NightRiderWPF.DeveloperView;
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

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Max Fare
    /// DATE: 2024-02-14
    /// Interaction for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Page
    {
        private IRoleManager _roleManager = null;

        public AdminHome()
        {
            InitializeComponent();
            _roleManager = new RoleManager();
            SetupPage();
        }
        /// <summary>
        /// AUTHOR: Max Fare
        /// DATE: 2024-02-16
        /// Initializes the combobox for choosing to view from the perspective of different roles,
        /// although the page changes will need to be included in a future update in order to link to pages not yet made
        /// </summary>
        public void SetupPage()
        {
            cmbViews.Items.Clear();
            foreach (var r in _roleManager.GetAllRoles())
            {
                cmbViews.Items.Add(r.RoleID);
            }
        }
        /// <summary>
        /// AUTHOR: Max Fare
        /// DATE: 2024-02-18
        /// opens the client list page after the buton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            AdminViewClientList clientList = new AdminViewClientList();
            this.NavigationService.Navigate(clientList);
        }
        /// <summary>
        /// AUTHOR: Max Fare
        /// DATE: 2024-02-18
        /// opens the employee list page after the buton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            Employees.AdminEmployeeListPage employeeList = new Employees.AdminEmployeeListPage();
            this.NavigationService.Navigate(employeeList);
        }

        /// <summary>
        /// AUTHOR: Max Fare
        /// DATE: 2024-02-18
        /// When the selection is changed, opens the home page of the chosen role, to be navigated as if the admin is an employee with that role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbViews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string chosen = cmbViews.SelectedItem.ToString();
            switch (chosen)
            {
                //add role home pages here, run program to check role names
            }
        }
    }
}
