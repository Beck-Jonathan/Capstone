using DataObjects;
using LogicLayer;
using LogicLayer.PartsRequest;
using LogicLayer.Vendor;
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

namespace NightRiderWPF.PartsRequests
{
    /// <summary>
    /// AUTHOR: Everett DeVaux
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///
    ///    Interaction logic for Presentation layer for the Parts Request Details page that 
    ///    is in the Parts Request
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// 
    ///     Initial Creation
    ///     
    /// </remarks>
    public partial class ViewPartRequestDetailsPage : Page
    {
        IPartsRequestsManager _partsRequestManager = null;
        Parts_Request parts_Request = null;
        IVendorManager _vendorManager = null;
        int i;

        /// <summary>
        /// AUTHOR: Everett DeVaux
        /// <br />
        /// CREATED 2024-03-02
        /// <br />
        /// Populates the parts request details on the parts request details page 
        /// </summary>
        /// <remarks>
        /// Updater: Parker Svoboda
        /// <br />
        /// Updated: 2024-04-13
        /// <br />
        /// added Vendor Select
        /// </remarks>
        public ViewPartRequestDetailsPage(Parts_Request selectedPartsRequestDetails)
        {
            try
            {
                InitializeComponent();
                _vendorManager = new VendorManager();
                _partsRequestManager = new PartsRequestManager();
                parts_Request = new Parts_Request();
                parts_Request = _partsRequestManager.GetPartsRequestDetails(selectedPartsRequestDetails.Parts_Request_ID);
                txtbxPartRequest.Text = parts_Request.Parts_Request_ID.ToString();
                txtbxQuantity.Text = parts_Request.Quantity_Requested.ToString();
                txtbxVehicle.Text = parts_Request.Vehicle_Year + " " +
                    parts_Request.Vehicle_Make + " " + parts_Request.Vehicle_Model;
                txtbxNotes.Text = parts_Request.Parts_Request_Notes;
                txtbxDateRequested.Text = parts_Request.Date_Requested.ToString();
                txtbxRequestFrom.Text = parts_Request.Employee_ID.ToString();
                cmbVendors.ItemsSource = _vendorManager.getAllVendors().Select(vendor => vendor.Vendor_Name);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message + " occurred.");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _partsRequestManager = new PartsRequestManager();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// CONTRIBUTOR: Parker Svoboda
        /// <br />
        /// CREATED: 2024-03-26
        /// <br />
        /// rejects order
        /// </summary>
        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to reject this request?", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                try
                {
                    _partsRequestManager.DeactivatePartsRequest(parts_Request.Parts_Request_ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.NavigationService.GoBack();
            }
        }


        /// <summary>
        /// CONTRIBUTOR: Parker Svoboda
        /// <br />
        /// CREATED: 2024-04-13
        /// <br />
        /// approves order
        /// </summary>
        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            
            if (cmbVendors.SelectedIndex == -1)
            {
                MessageBox.Show("no vendors are selected", "No Vendors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (txtLineNumber.Text == null)
            {
                MessageBox.Show("No Line Number Was Provided", "No Line Number", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtLineNumber.Text == "" || !int.TryParse(txtLineNumber.Text, out i))
            {
                MessageBox.Show("Line Number " + txtLineNumber.Text + " isn't a Number", "Invalid Line Number", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    MessageBox.Show("The Purchase Order ID is " + _partsRequestManager.PushToPOLine(parts_Request.Parts_Request_ID, cmbVendors.SelectedIndex + 100000, Convert.ToInt32(txtLineNumber.Text))
                    , "Order Sent.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("One Input was Invalid", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.NavigationService.GoBack();
            }
        }
    }
}
