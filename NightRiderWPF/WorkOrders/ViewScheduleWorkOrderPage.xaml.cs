using DataObjects;
using LogicLayer;
using LogicLayer.ServiceOrder;
using NightRiderWPF.Maintenance;
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
    /// AUTHOR: Jared Roberts
    /// <br />
    /// CREATED: 2024-04-28
    /// <br />
    ///     The presentation layer displaying a list of all Scheduled Maintenance.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Jared Roberts
    /// <br />
    /// UPDATED: 2024-04-28
    /// <br />
    ///     Initial Creation
    ///     
    /// </remarks>
    public partial class ViewScheduleWorkOrderPage : Page
    {
        MaintenanceScheduleManager _maintenanceScheduleManager = null;
        List<MaintenanceScheduleVM> _scheduledOrders = null;
        List<MaintenanceScheduleVM> _searchedScheduledOrders = null;

        public ViewScheduleWorkOrderPage()
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

        public ViewScheduleWorkOrderPage(List<MaintenanceScheduleVM> scheduledOrders)
        {
            _scheduledOrders = scheduledOrders;
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        ///  Populates the list of scheduled work orders in the view scheduled work order page
        /// </summary>
        /// <br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void PopulateList()
        {
            _maintenanceScheduleManager = new MaintenanceScheduleManager();
            _scheduledOrders = _maintenanceScheduleManager.GetAllMaintenanceSchedules();
            if (_scheduledOrders.Count.Equals(0))
            {
                return;
            }
            mntcViewScheduledWorkOrderPendingDg.ItemsSource = _scheduledOrders;
            mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
            mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
            mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(6);
            mntcViewScheduledWorkOrderPendingDg.Columns[0].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.Columns[1].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.Columns[2].Header = "Service Type";
            mntcViewScheduledWorkOrderPendingDg.Columns[2].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.Columns[3].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.Columns[4].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.Columns[5].IsReadOnly = true;
            mntcViewScheduledWorkOrderPendingDg.CanUserAddRows = false;
        }

        /// <summary>
        ///  FIlters scheduled work orders that are complete, incomplete, or all
        /// </summary>
        /// <br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void mntcFilterCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (mntcFilterCbo.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (selectedValue == "All")
            {
                PopulateList();
            }
            else if (selectedValue == "Complete")
            {
                _maintenanceScheduleManager = new MaintenanceScheduleManager();
                _scheduledOrders = _maintenanceScheduleManager.GetAllCompleteMaintenanceSchedules();
                if (_scheduledOrders.Count.Equals(0))
                {
                    return;
                }
                mntcViewScheduledWorkOrderPendingDg.ItemsSource = _scheduledOrders;
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(6);
                mntcViewScheduledWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[2].Header = "Service Type";
                mntcViewScheduledWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[3].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[4].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[5].IsReadOnly = true;
            }
            else
            {
                _maintenanceScheduleManager = new MaintenanceScheduleManager();
                _scheduledOrders = _maintenanceScheduleManager.GetAllIncompleteMaintenanceSchedules();
                if (_scheduledOrders.Count.Equals(0))
                {
                    return;
                }
                mntcViewScheduledWorkOrderPendingDg.ItemsSource = _scheduledOrders;
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(6);
                mntcViewScheduledWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[2].Header = "Service Type";
                mntcViewScheduledWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[3].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[4].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[5].IsReadOnly = true;
            }
        }

        /// <summary>
        ///  Displays the searched scheduled work order in the view scheduled work order page
        /// </summary>
        /// <br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-04-28
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void mntcViewScheduledWorkOrdersSearchTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (mntcViewScheduledWorkOrdersSearchTxt.Text == "")
            {
                PopulateList();
            }
            else
            {

                _searchedScheduledOrders = new List<MaintenanceScheduleVM>();
                foreach (MaintenanceScheduleVM maintenanceScheduleVM in _scheduledOrders)
                {
                    if (maintenanceScheduleVM.ServiceTypeID.ToLower().Contains(mntcViewScheduledWorkOrdersSearchTxt.Text.ToLower()))
                    {
                        _searchedScheduledOrders.Add(maintenanceScheduleVM);
                    }
                }
                if (_searchedScheduledOrders.Count.Equals(0))
                {
                    return;
                }
                _maintenanceScheduleManager = new MaintenanceScheduleManager();
                mntcViewScheduledWorkOrderPendingDg.ItemsSource = _searchedScheduledOrders;
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(0);
                mntcViewScheduledWorkOrderPendingDg.Columns.RemoveAt(6);
                mntcViewScheduledWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[2].Header = "Service Type";
                mntcViewScheduledWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[3].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[4].IsReadOnly = true;
                mntcViewScheduledWorkOrderPendingDg.Columns[5].IsReadOnly = true;

            }
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddEditDeleteScheduledMaintenance());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something went wrong", ex.Message);
            }
        }
    }
}
