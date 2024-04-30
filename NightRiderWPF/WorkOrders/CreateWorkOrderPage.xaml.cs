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
using System.Collections.Generic;
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
            PopulateServiceTypeComboBox();
        }

        private void PopulateTextBox()
        {
            try
            {
                // Populate Created_by_employee text box
                CreatedBytxt.Text = Authentication.AuthenticatedEmployee.Employee_ID.ToString();
                // Populate Service Order ID text box
                //var serviceOrders = _serviceOrderManager.GetALlServiceOrders();
                //var sortedServiceOrders = serviceOrders.OrderByDescending(o => o.Service_Order_ID).ToList();

                //Since this returns the max existing, we need to do +1 to get the next available.
                int nextServiceOrderID = _serviceOrderManager.getNextID() + 1;
                ServiceIDtxt.Text = nextServiceOrderID.ToString();

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
        private void PopulateServiceTypeComboBox()
        {
            try
            {
                List<ServiceOrder_VM> serviceType = _serviceOrderManager.GetAllServiceTypes();
                var serviceTypeIds = serviceType.Select(st => st.Service_Type_ID);
                ServiceTypeIDcbo.ItemsSource = serviceTypeIds;
                ServiceTypeIDcbo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service types: " + ex.Message);
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

            if (string.IsNullOrWhiteSpace(ServiceTypeIDcbo.SelectedValue?.ToString()))
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

            // Create a new ServiceOrder_VM object with data from the form
            ServiceOrder_VM serviceOrder = new ServiceOrder_VM()
            {
                Service_Order_ID = int.Parse(ServiceIDtxt.Text),
                Service_Order_Version = int.Parse(ServiceOrderVersiontxt.Text),
                VIN = VINcbo.SelectedValue.ToString(),
                Service_Type_ID = ServiceTypeIDcbo.SelectedValue.ToString(),
                Created_By_Employee_ID = int.Parse(CreatedBytxt.Text),
                Date_Started = DateStartedpkr.SelectedDate ?? DateTime.MinValue,
                Service_Description = ServiceDescriptiontxt.Text
            };
            try
            {
                bool result = _serviceOrderManager.CreateServiceOrder(serviceOrder);
                if (result)
                {
                    MessageBox.Show("Service Order created successfully.");
                    ClearForm();
                    ViewWorkOrderList viewPage = new ViewWorkOrderList();
                    NavigationService.Navigate(viewPage);
                }
                else
                {

                    int nextServiceOrderID = _serviceOrderManager.getNextID() + 1;
                    ServiceIDtxt.Text = nextServiceOrderID.ToString();
                    serviceOrder.Service_Order_ID = nextServiceOrderID;
                    MessageBox.Show("Unable to add order, please try again");

                }

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
            ServiceTypeIDcbo.SelectedIndex = -1;
            CreatedBytxt.Text = "";
            DateStartedpkr.SelectedDate = null;
            ServiceDescriptiontxt.Text = "";
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel creating the Service Order?", "Cancel Service Order Creation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void ServiceTypeIDcbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected Service_Type_ID
            string selectedServiceTypeID = ServiceTypeIDcbo.SelectedItem as string;


            ServiceOrder_VM selectedServiceType = _serviceOrderManager
                .GetAllServiceTypes()
                .FirstOrDefault(st => st.Service_Type_ID == selectedServiceTypeID);

            // Update the Descriptiontxt TextBox with the Service_Description
            if (selectedServiceType != null)
            {
                ServiceDescriptiontxt.Text = selectedServiceType.Service_Description;
            }
        }
    }
}
