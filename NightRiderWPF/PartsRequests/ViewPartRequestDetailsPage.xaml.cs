using DataObjects;
using LogicLayer;
using LogicLayer.PartsRequest;
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

        /// <summary>
        /// AUTHOR: Everett DeVaux
        /// <br />
        /// CREATED 2024-03-02
        /// <br />
        /// Populates the parts request details on the parts request details page 
        /// </summary>
        public ViewPartRequestDetailsPage(Parts_Request selectedPartsRequestDetails)
        {
            try
            {
                InitializeComponent();
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
        /// Sends order to suppliers
        /// </summary>
        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to add this to order?", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
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
    }
}
