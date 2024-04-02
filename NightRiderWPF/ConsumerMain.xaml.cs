using NightRiderWPF.DeveloperView;
using NightRiderWPF.Inventory;
using NightRiderWPF.Resources;
using NightRiderWPF.WorkOrders;
using NightRiderWPF.Vehicles;
using NightRiderWPF.Employees;
using NightRiderWPF.Clients;
using NightRiderWPF.PartsRequests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LogicLayer.AppData;
using LogicLayer.Utilities;
using LogicLayer;
using DataObjects;
using NightRiderWPF.Login;
using NightRiderWPF.RouteStop;
using NightRiderWPF.VehicleModels;
using NightRiderWPF.Dispatch;

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for ConsumerMain.xaml
    /// Author: Nathan Toothaker
    /// Date: 2024-02-01
    /// </summary>
    public partial class ConsumerMain : Window
    {
        private ILoginManager _loginManager;
        private IPasswordHasher _passwordHasher;
        public ConsumerMain()
        {
            _passwordHasher = new PasswordHasher();
            _loginManager = new LoginManager(_passwordHasher);
            InitializeComponent();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnClients")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new AdminViewClientList());
            }
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnEmployees")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new AdminEmployeeListPage());
            }
        }

        private void btnVehicles_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnVehicles")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new VehicleLookupListPage());
            }
        }

        private void btnMaintenance_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnMaintenance")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new ViewWorkOrderList());
            }
        }
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnInventory")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new PartsInventoryPage());
            }
        }

        /// <summary>
        ///     Builds the user authenticated label content
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-17
        /// </remarks>
        /// <remarks>
        ///    CONTRIBUTOR: Nathan Toothaker
        /// <br />
        ///    UPDATED: 2024-02-21
        /// <br />
        ///    Split several lines into a separate method, UpdateUiForLogin()
        /// </remarks>
        /// <remarks>
        ///    CONTRIBUTOR: Nathan Toothaker
        /// <br />
        ///    UPDATED: 2024-02-27
        /// <br />
        ///    Added a navigate to a default page that just welcomes the logged in user more clearly.
        /// </remarks>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwdPassword.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a valid password.");
                return;
            }

            try
            {
                var authenticatedEmployee = _loginManager.AuthenticateEmployee(username, password);

                Authentication.AuthenticatedEmployee = authenticatedEmployee;
                UpdateUiForLogin();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("No employee account with that username and password could be found.");
            }
        }

        private void UpdateUiForLogin()
        {
            lblUsername.Visibility = Visibility.Hidden;
            txtUsername.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;
            pwdPassword.Visibility = Visibility.Hidden;
            btnLogin.Visibility = Visibility.Hidden;
            btnForgotPassword.Visibility = Visibility.Hidden;

            lbl_userAuthenticatedConfirmation.Content = BuildUserAuthenticatedConfirmationContent();
            lbl_userAuthenticatedConfirmation.Visibility = Visibility.Visible;

            foreach (var role in Authentication.AuthenticatedEmployee.Roles)
            {
                switch (role.RoleID)
                {
                    case "Admin":
                        btnClients.Visibility = Visibility.Visible;
                        btnEmployees.Visibility = Visibility.Visible;
                        btnVehicles.Visibility = Visibility.Visible;
                        btnVehicleModels.Visibility = Visibility.Visible;
                        btnMaintenance.Visibility = Visibility.Visible;
                        btnInventory.Visibility = Visibility.Visible;
                        btnDriverSchedules.Visibility = Visibility.Visible;
                        btnVehicleSchedules.Visibility = Visibility.Visible;
                        btnRoutes.Visibility = Visibility.Visible;
                        btnPartsRequests.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        btnDispatch.Visibility = Visibility.Visible;
                        break;
                    case "FleetAdmin":
                        btnVehicles.Visibility= Visibility.Visible;
                        btnVehicleModels.Visibility = Visibility.Visible;
                        btnVehicleSchedules.Visibility= Visibility.Visible;
                        btnMaintenance.Visibility= Visibility.Visible;
                        btnPartsRequests.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        btnDispatch.Visibility = Visibility.Visible;
                        break;
                    case "Mechanic":
                        btnMaintenance.Visibility = Visibility.Visible;
                        btnInventory.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        break;
                    case "Maintenance":
                        btnMaintenance.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        break;
                    case "PartsPerson":
                        btnInventory.Visibility = Visibility.Visible;
                        btnMaintenance.Visibility = Visibility.Visible;
                        btnPartsRequests.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        break;
                    case "Driver":
                    // btnMySchedule.Visibility= Visibility.Visible;
                    case "Operator":
                        btnRoutes.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        break;
                    case "Dispatch":
                        btnVehicles.Visibility = Visibility.Visible;
                        btnDriverSchedules.Visibility = Visibility.Visible;
                        btnVehicleSchedules.Visibility = Visibility.Visible;
                        btnRoutes.Visibility = Visibility.Visible;
                        btnPartsRequests.Visibility = Visibility.Visible;
                        btn_profile.Visibility = Visibility.Visible;
                        break;
                    default: break;
                }
            }
            bool isAdmin = Authentication.AuthenticatedEmployee.Roles.Where(r => String.Equals(r.RoleID, "Admin")).Count() >= 1;
            if (isAdmin)
            {
                PageViewer.Navigate(new AdminHome());
            }
            else
            {
                PageViewer.Navigate(new EmployeeLoginPage());
            }

            btn_logout.Visibility = Visibility.Visible;
        }

        private string BuildUserAuthenticatedConfirmationContent()
        {
            return $"Welcome, {Authentication.AuthenticatedEmployee.Given_Name}.";
        }

        /// <summary>
        ///     Handles click events for the logout button;
        ///     logout user and purges the active session for security
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton, Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-20
        /// </remarks>
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Authentication.AuthenticatedEmployee = null; // for security purposes
            Authentication.AuthenticatedClient = null; // more security
            UpdateUIforLogout();
            return;
        }
        private void UpdateUIforLogout()
        {
            btnLogin.IsDefault = true;

            txtUsername.Text = "";
            pwdPassword.Password = "";
            txtUsername.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblUsername.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            btnForgotPassword.Visibility = Visibility.Visible;
            btn_profile.Visibility = Visibility.Hidden;
            lbl_userAuthenticatedConfirmation.Visibility = Visibility.Hidden;
            btn_logout.Visibility = Visibility.Hidden;
            foreach (Button btn in stackMainNav.Children)
            {
                btn.Visibility = Visibility.Collapsed;
            }

            while (PageViewer.CanGoBack)
            {
                PageViewer.RemoveBackEntry();
            }
        }

        private void btnDriverSchedules_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnDriverSchedules")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                // PageViewer.Navigate(new DriverSchedulesPage());
            }
        }

        private void btnVehicleSchedules_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnVehicleSchedules")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                // PageViewer.Navigate(new VehicleSchedulesPage());
            }
        }

        private void btnRoutes_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnRoutes")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new RouteList());
            }
        }

        /// <summary>
        /// AUTHOR: Ben Collins
        /// <br />
        /// CREATED: 2024-03-05
        /// <br />
        ///     A page for displaying all of the Parts Requests.
        /// </summary>
        /// 
        /// <remarks>
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void btnPartsRequests_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnPartsRequests")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new ViewPartRequestsPage());
            }
        }
		
        private void btnForgotUsername_Click(object sender, RoutedEventArgs e)
        {
            PageViewer.Navigate(new UsernameRequestPage());
		}
		
        private void btn_profile_Click(object sender, RoutedEventArgs e)
        {
            PageViewer.Navigate(new EmployeeProfilePage(Authentication.AuthenticatedEmployee));
        }

        /// <summary>
        /// AUTHOR: Jared Hutton
        /// <br />
        /// CREATED: 2024-03-19
        /// <br />
        ///     A page for displaying all of the vehicle models
        /// </summary>
       private void btnVehicleModels_Click(object sender, RoutedEventArgs e)
       {
           if (sender is Button btn && btn.Name == "btnVehicleModels")
           {
               foreach (var child in stackMainNav.Children)
               {
                   if (child is Button button)
                   {
                       button.Background = Statics.SecondaryColor;
                   }
               }
               btn.Background = Statics.PrimaryColor;
               PageViewer.Navigate(new VehicleModelsListPage(
                   new VehicleModelManager(),
                   new VehicleManager(),
                   new Parts_InventoryManager()));
           }
       }
        private void btnDispatch_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnDispatch")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
                PageViewer.Navigate(new DispatchHome());
            }
        }
    }
}
// checked by James Williams