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


        public UpdateWorkOrderPage(ServiceOrder_VM selectedWorkOrder)
        {
            InitializeComponent();
            SelectedWorkOrder = selectedWorkOrder;
            CurrentServiceTypetxt.Text = selectedWorkOrder.Service_Type_ID;
            RequestDescriptiontxt.Text = selectedWorkOrder.Service_Description;
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

                // Get the old and new Service Type IDs
                string oldServiceTypeID = SelectedWorkOrder.Service_Type_ID;
                string newServiceTypeID = NewServiceTypetxt.Text;

                // Check if newServiceTypeID is null or empty
                if (string.IsNullOrWhiteSpace(newServiceTypeID))
                {
                    MessageBox.Show("New Service Type cannot be null or empty.");
                    return;
                }

                // Get the new Service Description
                string newServiceDescription = RequestDescriptiontxt.Text;

                // Check if newServiceDescription is null or empty
                if (string.IsNullOrWhiteSpace(newServiceDescription))
                {
                    MessageBox.Show("Service Description cannot be null or empty.");
                    return;
                }

                // Update the properties of the SelectedWorkOrder
                SelectedWorkOrder.Service_Order_ID = serviceOrderID;
                SelectedWorkOrder.Service_Type_ID = newServiceTypeID;
                SelectedWorkOrder.Service_Description = newServiceDescription;
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

                // Navigate back to the previous page 
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                MessageBox.Show("Error updating service order: " + ex.Message);
            }
        }


        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkOrderPage createPage = new CreateWorkOrderPage();
            NavigationService.Navigate(createPage);
        }
    }
}