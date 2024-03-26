/// <summary>
/// Steven Sanchez
/// Created: 2024/03/12
/// 
/// XAML to create a service/work order. 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 
using DataObjects;
using LogicLayer;
using LogicLayer.AppData;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NightRiderWPF.WorkOrders
{
    /// <summary>
    /// Interaction logic for CreateWorkOrderPage.xaml
    /// </summary>
    public partial class CreateWorkOrderPage : Page
    {
        ServiceOrderManager _serviceOrderManager = null;
        private EmployeeManager _employeeManager;
        private VehicleManager _vehicleManager;

        public CreateWorkOrderPage()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            _vehicleManager = new VehicleManager();
            _serviceOrderManager = new ServiceOrderManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateTextBox();
            PopulateVinComboBox();
        }

        private void PopulateTextBox()
        {
            try
            {
                // Populate Created_by_employee text box
                CreatedBytxt.Text = Authentication.AuthenticatedEmployee.Employee_ID.ToString();
                // Populate Service Order ID text box
                var serviceOrders = _serviceOrderManager.GetALlServiceOrders();
                var sortedServiceOrders = serviceOrders.OrderByDescending(o => o.Service_Order_ID).ToList();

                if (sortedServiceOrders.Count > 0)
                {
                    // Get the last Service Order ID
                    int lastServiceOrderID = sortedServiceOrders[0].Service_Order_ID;

                    // Increment by 1 to get the next Service Order ID
                    int nextServiceOrderID = lastServiceOrderID + 1;

                    // Set the next Service Order ID to the textbox
                    ServiceIDtxt.Text = nextServiceOrderID.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error loading SQL Data: " + ex.Message);
            }
        }

        private void PopulateVinComboBox()
        {
            try
            {
                var vehicles = _vehicleManager.VehicleLookupList();
                VINcbo.ItemsSource = vehicles;
                VINcbo.DisplayMemberPath = "VIN";
                VINcbo.SelectedValuePath = "VIN";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading VIN #'s: " + ex.Message);
            }
        }

        private void Createbtn_Click(object sender, RoutedEventArgs e)
        {

                // Check if form is null, empty, or whitespace
                if (string.IsNullOrWhiteSpace(ServiceIDtxt.Text))
                {
                    MessageBox.Show("Please enter a service ID.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ServiceOrderVersiontxt.Text))
                {
                    MessageBox.Show("Please enter a service version.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(VINcbo.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Please select a VIN.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ServiceTypeIDtxt.Text))
                {
                    MessageBox.Show("Please enter a service type ID.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ServiceDescriptiontxt.Text))
                {
                    MessageBox.Show("Please enter a service desctription.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CreatedBytxt.Text))
                {
                    MessageBox.Show("Please select an employee creating work order.");
                    return;
                }

                if (DateStartedpkr.SelectedDate == null)
                {
                    MessageBox.Show("Please select a date started.");
                    return;
                }

                if (DateFinishedpkr.SelectedDate == null)
                {
                    MessageBox.Show("Please select an anticipated date to finish.");
                    return;
                }

                // Create a new ServiceOrder_VM object with data from the form
                ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
                {
                    Service_Order_ID = int.Parse(ServiceIDtxt.Text),
                    Service_Order_Version = int.Parse(ServiceOrderVersiontxt.Text),
                    VIN = VINcbo.SelectedValue.ToString(),
                    Service_Type_ID = ServiceTypeIDtxt.Text,
                    Created_By_Employee_ID = int.Parse(CreatedBytxt.Text),
                    Date_Started = DateStartedpkr.SelectedDate ?? DateTime.MinValue,
                   Date_Finished = DateFinishedpkr.SelectedDate ?? DateTime.MinValue,
                    Service_Description = ServiceDescriptiontxt.Text
                };
            try
            {
                _serviceOrderManager.CreateServiceOrder(serviceOrder);
                MessageBox.Show("Service Order created successfully.");
                ClearForm();
                ViewWorkOrderList viewPage = new ViewWorkOrderList();
                NavigationService.Navigate(viewPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating Service Order: " + ex.Message);
            }
        }
        private void ClearForm()
        {
            // Clear all the form fields after successful creation
            ServiceIDtxt.Text = "";
            ServiceOrderVersiontxt.Text = "";
            VINcbo.SelectedIndex = -1;
            ServiceTypeIDtxt.Text = "";
            CreatedBytxt.Text="";
            DateStartedpkr.SelectedDate = null;
            DateFinishedpkr.SelectedDate = null;
            ServiceDescriptiontxt.Text = "";
        }
    }
}
