using DataObjects;
using LogicLayer;
using LogicLayer.PartsRequest;
using NightRiderWPF.PartsRequests;
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
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     The presentation layer for displaying all of the Parts Requests.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: 
    /// <br />
    /// UPDATED: 
    /// <br />
    ///     Initial Creation
    /// </remarks>
    public partial class ViewPartRequestsPage : Page
    {
        IPartsRequestsManager _partsRequestsManager = null;
        List<Parts_Request> _partsRequests = null;

        public ViewPartRequestsPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message + " occurred.");
            }
        }

        // Populating the data grid with returned values
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _partsRequestsManager = new PartsRequestManager();
                _partsRequests = new List<Parts_Request>(_partsRequestsManager.GetAllPartsRequests());

                lblPartRequestsPageRequestCount.Content = "There are " + _partsRequests.Count + " requests";

            }
            catch (Exception ex)
            {
                MessageBox.Show("No parts requests found. " + ex);
            }
            
            datgrdPartsRequestsView.ItemsSource = _partsRequests;
            datgrdPartsRequestsView.Columns[0].DisplayIndex = 4;
            datgrdPartsRequestsView.Columns[0].Header = "";
            datgrdPartsRequestsView.Columns[0].Width = 125;
            datgrdPartsRequestsView.Columns[0].CanUserReorder = false;
            datgrdPartsRequestsView.Columns[0].IsReadOnly = true;
            datgrdPartsRequestsView.Columns[1].Header = "Request #";
            datgrdPartsRequestsView.Columns[1].Width = 100;
            datgrdPartsRequestsView.Columns[1].CanUserReorder = false;
            datgrdPartsRequestsView.Columns[1].IsReadOnly = true;
            datgrdPartsRequestsView.Columns[2].Header = "Part Requested";
            datgrdPartsRequestsView.Columns[2].Width = 175;
            datgrdPartsRequestsView.Columns[2].CanUserReorder = false;
            datgrdPartsRequestsView.Columns[2].IsReadOnly = true;
            datgrdPartsRequestsView.Columns[3].Header = "Quantity";
            datgrdPartsRequestsView.Columns[3].Width = 75;
            datgrdPartsRequestsView.Columns[3].CanUserReorder = false;
            datgrdPartsRequestsView.Columns[3].IsReadOnly = true;
            datgrdPartsRequestsView.Columns[4].Header = "Request Date";
            datgrdPartsRequestsView.Columns[4].Width = 225;
            datgrdPartsRequestsView.Columns[4].CanUserReorder = false;
            datgrdPartsRequestsView.Columns[4].IsReadOnly = true;
        }

        // Populating the data grid with search results
        private void txtPartRequestsPageRequestsSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<Parts_Request> dataObjects = new List<Parts_Request>();
            if (txtPartRequestsPageRequestsSearch.Text == "")
            {
                foreach (Parts_Request part in _partsRequests)
                {
                    dataObjects.Add(part);
                }

            }
            else
            {

                foreach (Parts_Request partsRequest in _partsRequests)
                {
                    if (partsRequest.Part_Name.ToLower().Contains(txtPartRequestsPageRequestsSearch.Text.ToLower()))
                    {

                        dataObjects.Add(partsRequest);
                    }
                }
            }
            datgrdPartsRequestsView.ItemsSource = dataObjects;
            if (dataObjects.Count > 0)
            {
                datgrdPartsRequestsView.Columns[0].DisplayIndex = 4;
                datgrdPartsRequestsView.Columns[0].Header = "";
                datgrdPartsRequestsView.Columns[0].Width = 125;
                datgrdPartsRequestsView.Columns[0].CanUserReorder = false;
                datgrdPartsRequestsView.Columns[0].IsReadOnly = true;
                datgrdPartsRequestsView.Columns[1].Header = "Request #";
                datgrdPartsRequestsView.Columns[1].Width = 100;
                datgrdPartsRequestsView.Columns[1].CanUserReorder = false;
                datgrdPartsRequestsView.Columns[1].IsReadOnly = true;
                datgrdPartsRequestsView.Columns[2].Header = "Part Requested";
                datgrdPartsRequestsView.Columns[2].Width = 175;
                datgrdPartsRequestsView.Columns[2].CanUserReorder = false;
                datgrdPartsRequestsView.Columns[2].IsReadOnly = true;
                datgrdPartsRequestsView.Columns[3].Header = "Quantity";
                datgrdPartsRequestsView.Columns[3].Width = 75;
                datgrdPartsRequestsView.Columns[3].CanUserReorder = false;
                datgrdPartsRequestsView.Columns[4].Header = "Request Date";
                datgrdPartsRequestsView.Columns[4].Width = 225;
                datgrdPartsRequestsView.Columns[4].CanUserReorder = false;
            }
        }

        // Navigation back
        private void btnPartRequestsPageBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("No page to navigate back too.");
            }
        }

        /// <summary>
        /// CONTRIBUTOR: Everett DeVaux
        /// <br />
        /// CREATED: 2024-03-07
        /// <br />
        /// navigation back button
        /// <br /> 
        /// UPDATED: 2024-03-27
        /// <br /> 
        /// UPDATER: Everett DeVaux
        /// <br />
        ///     Updated navigation on the back button
        /// </summary>
        private void btnPartRequestsPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parts_Request selectedRow = (Parts_Request)datgrdPartsRequestsView.SelectedItem;

                if (selectedRow != null)
                {
                    this.NavigationService.Navigate(new ViewPartRequestDetailsPage(selectedRow));
                }
                else
                {
                    MessageBox.Show("Part Request could not be found");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
