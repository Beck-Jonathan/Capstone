using DataObjects;
using LogicLayer;
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

namespace NightRiderWPF.WorkOrders
{
    /// <summary>
    /// Interaction logic for UpdateWorkOrderPage.xaml
    /// </summary>
    public partial class UpdateWorkOrderPage : Page
    {
        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-02-18
        /// <br />
        ///     The presentation layer displaying a form for an employee to update a service order
        /// </summary>
        /// 
        /// <remarks>
        /// UPDATER: Steven Sanchez
        /// <br />
        /// UPDATED: 2024-02-18
        /// <br />
        ///     Initial Creation
        ///     
        ///     added the service_order_ID in order to update an existing service order
        /// </remarks>
        public ServiceOrder_VM SelectedWorkOrder { get; private set; }
        private int serviceOrderID;
        ServiceOrderManager _serviceOrderManager;

        public UpdateWorkOrderPage(ServiceOrder_VM selectedWorkOrder)
        {
            InitializeComponent();
            _serviceOrderManager = new ServiceOrderManager();
            List<ServiceOrder_VM> serviceType = _serviceOrderManager.GetAllServiceTypes();
            SelectedWorkOrder = selectedWorkOrder;
            var serviceTypeIds = serviceType.Select(st => st.Service_Type_ID);
            ServiceTypecbo.ItemsSource = serviceTypeIds;
            ServiceTypecbo.SelectedItem = selectedWorkOrder.Service_Type_ID;
            Descriptiontxt.Text = selectedWorkOrder.Service_Description;
            serviceOrderID = selectedWorkOrder.Service_Order_ID;
            if (SelectedWorkOrder != null && SelectedWorkOrder.Critical_Issue)
            {
                Yesrbtn.IsChecked = true;
                Norbtn.IsChecked = false;
            }
            else
            {
                Yesrbtn.IsChecked = false;
                Norbtn.IsChecked = true;
            }

        }

        private void Confirmbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IServiceOrderManager serviceOrderManager = new ServiceOrderManager();

                // Update the properties of the SelectedWorkOrder
                SelectedWorkOrder.Service_Order_ID = serviceOrderID;
                SelectedWorkOrder.Service_Type_ID = ServiceTypecbo.SelectedItem?.ToString();
                if (Yesrbtn.IsChecked == true)
                {
                    SelectedWorkOrder.Critical_Issue = true;
                }
                else
                {
                    SelectedWorkOrder.Critical_Issue = false;
                }

                // Perform the update operation
                serviceOrderManager.UpdateServiceOrder(SelectedWorkOrder);

                // Show a success message
                MessageBox.Show("Service order updated successfully!");

                ViewWorkOrderList viewPage = new ViewWorkOrderList();
                NavigationService.Navigate(viewPage);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                MessageBox.Show("Error updating service order: " + ex.Message);
            }
        }

        private void ServiceTypecbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected Service_Type_ID
            string selectedServiceTypeID = ServiceTypecbo.SelectedItem as string;


            ServiceOrder_VM selectedServiceType = _serviceOrderManager
                .GetAllServiceTypes()
                .FirstOrDefault(st => st.Service_Type_ID == selectedServiceTypeID);

            // Update the Descriptiontxt TextBox with the Service_Description
            if (selectedServiceType != null)
            {
                Descriptiontxt.Text = selectedServiceType.Service_Description;
            }
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel updating the Service Order?", "Cancel Service Order Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}