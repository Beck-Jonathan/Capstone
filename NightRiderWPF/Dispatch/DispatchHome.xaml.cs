using DataObjects;
using DataObjects.Assignment;
using DriverClass = DataObjects.Assignment.Driver;
using DataObjects.RouteObjects;
using LogicLayer;
using LogicLayer.PartsRequest;
using LogicLayer.RouteAssignment;
using LogicLayer.RouteStop;
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
using NightRiderWPF.RouteStop;



namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for DispatchHome.xaml
    /// </summary>
    public partial class DispatchHome : Page
    {
        string _selectedModel = null;
        IDriverScheduleManager _driverScheduleManager = null;
        List<Dispatch> _driverSchedule = null;
        private RouteAssignmentManager _assignmentManager;
        private VehicleModelManager _vehicleModelManager;
        private RouteManager _routeManager;
        List<RouteVM> _routes;
        List<VehicleAssignment> _availableVehicles;
        List<DriverClass> _availableDrivers;
        List<Route_Assignment> _currentAssignments;
        IEnumerable<VehicleModel> _allModels;
        int _selectedDriverID;
        int _selectedRouteID;
        string _selectedVIN;
        public DispatchHome()
        {
            _assignmentManager = new RouteAssignmentManager();
            _vehicleModelManager = new VehicleModelManager();
            _routes = new List<RouteVM>();
            _routeManager = new RouteManager();
            _selectedRouteID = 0;
            try
            {
                _allModels = _vehicleModelManager.GetVehicleModels();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n\n" + ex.InnerException.Message);
            }
            InitializeComponent();
            if (_allModels != null)
            {
                var capacities = _allModels.OrderBy(m => m.MaxPassengers).Select(m => m.MaxPassengers).Distinct();
                foreach (var item in capacities)
                {
                    cboVehicleAddSearchCapacity.Items.Add(item);
                }
            }
            cboVehicleAddSearchCapacity.SelectedIndex = -1;
            dateStart.DisplayDateStart = DateTime.Today;
            dateEnd.DisplayDateStart = DateTime.Today;
            btnSubmit.IsEnabled = false;
            hideUI();
        }

        private void cboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //collapse all grids when Model selection changed to avoid overlapping UI
            hideAllGrids();
            if (cboModel.SelectedItem is ComboBoxItem selectedItem)

            {
                _selectedModel = selectedItem.Content.ToString();
                //reset the Service combobox after Model selection change
                cboService.SelectedIndex = -1;
                if (_selectedModel == null)
                {
                    return;
                }

                switch (_selectedModel)
                {
                    case "Vehicle":
                        generateCBO(new[] { "Schedules", "Add To Route", "Maintenance" });
                        break;
                    case "Driver":
                        generateCBO(new[] { "Schedules", "Add To Route", "Availability" });
                        break;
                    case "Route":
                        generateCBO(new[] { "Schedules", "Add To Route" });
                        break;
                    case "Charter":
                        generateCBO(new[] { "Schedules" });
                        break;
                    case "Ride Service":
                        generateCBO(new[] { "Schedules" });
                        break;
                    default:
                        break;
                }
            }
        }


        //Service cbo selection change event
        private void cboService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboService.SelectedItem is string selectedItem)
            {
                string selectedService = selectedItem;
                if (selectedService == null)
                {
                    return;
                }
                switch (_selectedModel)
                {
                    // add your grid for your feature here.
                    case "Vehicle":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridVehicleSchedules);
                                break;
                            case "Maintenance":
                                showGrid(gridVehicleMaintenance);
                                break;
                            case "Add To Route":
                                showGrid(gridAddToRoute);
                                break;
                        }
                        break;
                    case "Driver":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridDriverSchedules);
                                break;
                            case "Availability":
                                showGrid(gridDriverAvailability);
                                break;
                            case "Add To Route":
                                showGrid(gridAddToRoute);
                                break;
                        }
                        break;
                    case "Route":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridRouteSchedules);
                                break;
                            case "Add To Route":
                                showGrid(gridAddToRoute);
                                break;
                        }
                        break;
                    case "Charter":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridCharterSchedules);
                                break;
                        }
                        break;
                    case "Ride Service":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridRideServiceSchedules);
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // Hides all grids except the grid provided as the parameter
        // Add any additional grids 
        private void showGrid(Grid grid)
        {
            gridVehicleSchedules.Visibility = Visibility.Collapsed;
            gridVehicleMaintenance.Visibility = Visibility.Collapsed;
            gridDriverSchedules.Visibility = Visibility.Collapsed;
            gridDriverAvailability.Visibility = Visibility.Collapsed;
            gridRouteSchedules.Visibility = Visibility.Collapsed;
            gridCharterSchedules.Visibility = Visibility.Collapsed;
            gridRideServiceSchedules.Visibility = Visibility.Collapsed;
            //add any additional grids here as Visibility.Collapsed


            grid.Visibility = Visibility.Visible;
        }

        // Collapses all grids. If you create a new grid, add it's reference here
        private void hideAllGrids()
        {
            gridVehicleSchedules.Visibility = Visibility.Collapsed;
            gridVehicleMaintenance.Visibility = Visibility.Collapsed;
            gridDriverSchedules.Visibility = Visibility.Collapsed;
            gridDriverAvailability.Visibility = Visibility.Collapsed;
            gridRouteSchedules.Visibility = Visibility.Collapsed;
            gridCharterSchedules.Visibility = Visibility.Collapsed;
            gridRideServiceSchedules.Visibility = Visibility.Collapsed;
            //add any additional grids here as Visibility.Collapsed
        }

        //generates combobox items from a list of strings
        private void generateCBO(string[] items)
        {
            cboService.ItemsSource = items;
        }

        private void gridDriverSchedules_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (gridDriverSchedules.Visibility == Visibility.Visible)
            {
                try
                {
                    _driverScheduleManager = new DriverScheduleManager();
                    _driverSchedule = _driverScheduleManager.GetDriverScheduleList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No driver schedules found. " + ex);
                }

                dataGridDriverSchedule.ItemsSource = _driverSchedule;
            }

        }

        private void gridAddToRoute_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            try
            {
                _routes = (List<RouteVM>)_routeManager.getRoutes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error. \n\n" + ex.InnerException.Message);
            }
            List<string> routeNames = _routes.Select(route => route.RouteName).ToList();
            cboVehicleAddRoutes.ItemsSource = routeNames;

        }



        private void cboVehicleAddRoutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showUI();
            resetUI();
            dataVehicleAddAvailableVehicles.ItemsSource = null;

            int routeID = 0;

            foreach (var route in _routes)
            {
                if (route.RouteName == cboVehicleAddRoutes.SelectedValue.ToString())
                {
                    routeID = route.RouteId;
                    _selectedRouteID = route.RouteId;
                    break;
                }
            }
        }




        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            resetUI();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)

        {
            // reason for why driver is unavailable
            string reason = "Assigned to route " + cboVehicleAddRoutes.SelectedItem.ToString();
            // ensure dates are entered correctly
            if (dateStart.SelectedDate == null && dateEnd.SelectedDate == null)
            {
                MessageBox.Show("Please choose start and end dates");
                return;
            }
            if (dateStart.SelectedDate > dateEnd.SelectedDate)
            {
                MessageBox.Show("Start day must be before or equal to the end date.");
                return;
            }

            DateTime start = (DateTime)dateStart.SelectedDate;
            DateTime endDate = (DateTime)dateEnd.SelectedDate;

            //Make sure driver and vehicle have been submitted
            if (txtSelectedDriver == null || txtSelectedDriver.Text == "" ||
                txtSelectedVehicle == null || txtSelectedVehicle.Text == "")
            {
                MessageBox.Show("Please select an available driver and vehicle");
                return;
            }
            try
            {
                //Check that both database calls are successfull
                bool result1 = _assignmentManager.AddRouteAssignment(_selectedDriverID, _selectedVIN, _selectedRouteID, start, endDate);
                bool result2 = _assignmentManager.AddVehicleAndDriverUnavailabilites(_selectedVIN, _selectedDriverID, start, endDate, reason);
                if (result1 == true && result2 == true)
                {
                    MessageBox.Show("Assignment added");
                    refreshData();
                    resetUI();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding assignment\n\n" + ex.Message);
                return;
            }
        }



        //Find button logic
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {

            clearDataGrids();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (_selectedRouteID == 0)
            {
                MessageBox.Show("No Routes Found");
            }

            try
            {
                startDate = (DateTime)dateStart.SelectedDate;
                endDate = (DateTime)dateEnd.SelectedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must choose a start and end date");
            }
            int capacity = 0;
            if (cboVehicleAddSearchCapacity.SelectedValue != null)
            {
                capacity = (int)cboVehicleAddSearchCapacity.SelectedValue;
            }
            if (startDate == null || endDate == null || capacity <= 0)
            {
                MessageBox.Show("Please define start and end dates and vehicle capacity");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Please select a start date greater than or equal to the end date");
                return;
            }

            _availableVehicles = new List<VehicleAssignment>();
            _availableDrivers = new List<DriverClass>();
            _currentAssignments = new List<Route_Assignment>();
            try
            {
                _availableDrivers = _assignmentManager.GetAvailableDriversByDateAndPassengerCount(startDate, endDate, capacity);
                _availableVehicles = _assignmentManager.GetAvailableVehiclesByDateAndPassengerCount(startDate, endDate, capacity);
                _currentAssignments = _assignmentManager.GetRouteAssignmentsByRouteIDAndDate(_selectedRouteID, startDate, endDate);

                if (_currentAssignments.Count > 0)
                {
                    _availableVehicles.RemoveAll(vehicle => _currentAssignments.Any(assignment => assignment.VIN_Number == vehicle.VIN));
                    _availableDrivers.RemoveAll(driver => _currentAssignments.Any(assignment => assignment.DriverID == driver.Employee_ID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing assignments\n\n" + ex.InnerException.Message);
            }
            if (_availableVehicles.Count > 0)
            {
                dataVehicleAddAvailableVehicles.ItemsSource = _availableVehicles;
            }
            if (_availableDrivers.Count > 0)
            {
                dataVehicleAddAssingedDriver.ItemsSource = _availableDrivers;
            }
            if (_currentAssignments.Count > 0)
            {
                dataVehicleAddAssingedVehicles.ItemsSource = _currentAssignments;
            }
        }

        //Double Click events for data grids
        private void dataVehicleAddAvailableVehicles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VehicleAssignment selection = dataVehicleAddAvailableVehicles.SelectedItem as VehicleAssignment;
            if (selection == null)
            {
                return;
            }

            txtSelectedVehicle.Text = selection.VIN;
            _selectedVIN = selection.VIN;
            if (!string.IsNullOrEmpty(txtSelectedDriver.Text) && !string.IsNullOrEmpty(txtSelectedVehicle.Text))
            {
                btnSubmit.IsEnabled = true;
            }
            else
            {
                btnSubmit.IsEnabled = false;
            }
        }
        //Driver data grid double-click event
        private void dataVehicleAddAssingedDriver_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DriverClass selection = dataVehicleAddAssingedDriver.SelectedItem as DriverClass;
            if (selection == null)
            {
                return;
            }
            txtSelectedDriver.Text = selection.Employee_ID.ToString();
            _selectedDriverID = selection.Employee_ID;
            if (!string.IsNullOrEmpty(txtSelectedDriver.Text) && !string.IsNullOrEmpty(txtSelectedVehicle.Text))
            {
                btnSubmit.IsEnabled = true;
            }
            else
            {
                btnSubmit.IsEnabled = false;
            }
        }

        //Helper Methods

        private void clearDataGrids()
        {
            if (_currentAssignments != null)
            {
                _currentAssignments.Clear();
                dataVehicleAddAssingedVehicles.ItemsSource = null;
                dataVehicleAddAssingedVehicles.ItemsSource = _currentAssignments;
            }


            if (_availableDrivers != null)
            {
                _availableDrivers.Clear();
                dataVehicleAddAssingedDriver.ItemsSource = null;
                dataVehicleAddAssingedDriver.ItemsSource = _availableDrivers;
            }


            if (_availableVehicles != null)
            {
                _availableVehicles.Clear();
                dataVehicleAddAvailableVehicles.ItemsSource = null;
                dataVehicleAddAvailableVehicles.ItemsSource = _availableVehicles;
            }
        }

        private void resetUI()
        {

            dateStart.SelectedDate = null;
            dateEnd.SelectedDate = null;
            cboVehicleAddSearchCapacity.SelectedIndex = -1;

            if (_currentAssignments != null)
            {
                _currentAssignments.Clear();
                dataVehicleAddAssingedVehicles.ItemsSource = null;
                dataVehicleAddAssingedVehicles.ItemsSource = _currentAssignments;
            }


            if (_availableDrivers != null)
            {
                _availableDrivers.Clear();
                dataVehicleAddAssingedDriver.ItemsSource = null;
                dataVehicleAddAssingedDriver.ItemsSource = _availableDrivers;
            }


            if (_availableVehicles != null)
            {
                _availableVehicles.Clear();
                dataVehicleAddAvailableVehicles.ItemsSource = null;
                dataVehicleAddAvailableVehicles.ItemsSource = _availableVehicles;
            }
            txtSelectedDriver.Clear();
            txtSelectedVehicle.Clear();
            btnSubmit.IsEnabled = false;
        }
        private void hideUI()
        {

            cboVehicleAddSearchCapacity.IsEnabled = false;
            dateStart.IsEnabled = false;
            dateEnd.IsEnabled = false;
            btnFind.IsEnabled = false;
            btnSubmit.IsEnabled = false;

        }
        private void showUI()
        {
            cboVehicleAddSearchCapacity.IsEnabled = true;
            dateStart.IsEnabled = true;
            dateEnd.IsEnabled = true;
            btnFind.IsEnabled = true;

        }
        private void refreshData()
        {

            try
            {

                _routes = (List<RouteVM>)_routeManager.getRoutes();
                dataVehicleAddAvailableVehicles.ItemsSource = _availableVehicles;
                dataVehicleAddAssingedDriver.ItemsSource = _availableDrivers;
                dataVehicleAddAssingedVehicles.ItemsSource = _currentAssignments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh data: \n\n" + ex.Message);
            }
        }

        private void btnVehicleAddViewRoutes_Click(object sender, RoutedEventArgs e)
        {
            RouteList routeList = new RouteList();
            this.NavigationService.Navigate(routeList);
        }
    }
}
