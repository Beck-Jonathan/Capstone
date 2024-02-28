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
        public ServiceOrder SelectedWorkOrder { get; private set; }
        public ServiceOrder_VM ServiceOrder_VM = null;
        private int serviceOrderID;


        public UpdateWorkOrderPage(ServiceOrder selectedWorkOrder)
        {
            InitializeComponent();
            SelectedWorkOrder = selectedWorkOrder;
            ServiceTypetxt.Text = selectedWorkOrder.Service_Type_ID;
            RequestDescriptiontxt.Text = selectedWorkOrder.Service_Description;
            serviceOrderID = selectedWorkOrder.Service_Order_ID;
        }


        private void Confirmbtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                IServiceOrderManager serviceOrderManager = new ServiceOrderManager();

                // Update the properties of the SelectedWorkOrder object based on UI inputs
                SelectedWorkOrder.Service_Order_ID = serviceOrderID;
                SelectedWorkOrder.Service_Type_ID = ServiceTypetxt.Text;
                SelectedWorkOrder.Service_Description = RequestDescriptiontxt.Text;


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
        //yes radio button
        private void Yesrbtn_Checked(object sender, RoutedEventArgs e)
        {

            SelectedWorkOrder.Critical_Issue = true;


        }
        //no radio button
        private void Norbtn_Checked(object sender, RoutedEventArgs e)
        {
            SelectedWorkOrder.Critical_Issue = false;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {




            // Set the initial state of the radio buttons based on the CriticalIssue property
            if (SelectedWorkOrder.Critical_Issue)
            {
                Yesrbtn.IsChecked = true;
            }
            else
            {
                Norbtn.IsChecked = true;
            }

        }
    }

}

