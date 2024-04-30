using DataObjects;
using DataObjects.Assignment;
using DataObjects.RouteObjects;
using LogicLayer;
using LogicLayer.RouteAssignment;
using LogicLayer.RouteStop;
using NightRiderWPF.RouteStop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DriverClass = DataObjects.Assignment.Driver;



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
        private EmployeeManager _employeeManager;
        private RouteManager _routeManager;
        List<RouteVM> _routes;
        List<VehicleAssignment> _availableVehicles;
        List<DriverClass> _availableDrivers;
        List<Route_Assignment> _currentAssignments;
        IEnumerable<VehicleModel> _allModels;
        int _selectedDriverID;
        int _selectedRouteID;
        string _selectedVIN;
        Route_Assignment _selectedAssignment;
        Employee_VM _selectedEmployee;
        VehicleModel _selectedVehicle;
        DriverClass _selectedDriver;
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
                _employeeManager = new EmployeeManager();
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
            dateAssignmnetStart.DisplayDateStart = DateTime.Today;
            dateAssignmnetEnd.DisplayDateStart = DateTime.Today;
            btnSubmit.IsEnabled = false;
            hideUI();
        }

        public DispatchHome(string gridToDisplay)
        {
            _assignmentManager = new RouteAssignmentManager();
            _vehicleModelManager = new VehicleModelManager();
            _routes = new List<RouteVM>();
            _routeManager = new RouteManager();
            _selectedRouteID = 0;
            try
            {
                _allModels = _vehicleModelManager.GetVehicleModels();
                _employeeManager = new EmployeeManager();
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
            dateAssignmnetStart.DisplayDateStart = DateTime.Today;
            dateAssignmnetEnd.DisplayDateStart = DateTime.Today;
            btnSubmit.IsEnabled = false;
            hideUI();
            if(gridToDisplay == "Update Assignment")
            {
                gridRouteAssignments.Visibility = Visibility.Visible;
            }
            if(gridToDisplay == "Create Assignment")
            {
                gridAddToRoute.Visibility = Visibility.Visible;
            }
        }

        private void cboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //collapse all grids when Model selection changed to avoid overlapping UI
            hideAllGrids();
            _selectedAssignment = null;
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
                        generateCBO(new[] { "Add To Route" });
                        break;
                    case "Driver":
                        generateCBO(new[] { "Schedules", "Add To Route" });
                        break;
                    case "Route":
                        generateCBO(new[] { "Schedules", "Add To Route" });
                        break;
                    default:
                        break;
                }
            }
        }


        //Service cbo selection change event
        private void cboService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAssignment = null;
            hideAllGrids();
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
                            case "Add To Route":
                                showGrid(gridAddToRoute);
                                break;
                        }
                        break;
                    case "Route":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridRouteAssignments);
                                break;
                            case "Add To Route":
                                showGrid(gridAddToRoute);
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
            gridRouteAssignments.Visibility = Visibility.Collapsed;
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
            gridRouteAssignments.Visibility = Visibility.Collapsed;
            gridCharterSchedules.Visibility = Visibility.Collapsed;
            gridRideServiceSchedules.Visibility = Visibility.Collapsed;
            gridAddToRoute.Visibility = Visibility.Collapsed;
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
        //button that navigates to Route List
        private void btnVehicleAddViewRoutes_Click(object sender, RoutedEventArgs e)
        {
            RouteList routeList = new RouteList();
            this.NavigationService.Navigate(routeList);
        }


        //Change SelectRoute cbo selection action
        private void cboSelectRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //reset RouteAssingmnet UI
            resetRouteAssignmentUI();

            //if the selected Index isn't -1, obtain the routeID from the selection
            int routeID = 0;
            if (cboSelectRoute.SelectedIndex != -1)
            {
                foreach (var route in _routes)
                {
                    if (route.RouteName == cboSelectRoute.SelectedValue.ToString())
                    {
                        routeID = route.RouteId;
                        _selectedRouteID = route.RouteId;
                        break;
                    }
                }
            }
            //Enable date searching UI
            dateAssignmnetStart.IsEnabled = true;
            dateAssignmnetEnd.IsEnabled = true;
            btnSearchRouteAssignments.IsEnabled = true;

        }

        //Events tp happen when the RouteAssignments grid visibility is changed
        private void gridRouteAssignments_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Reset UI when grid visibility changed
            resetRouteAssignmentUI();
            //Deselect all routes
            cboSelectRoute.SelectedIndex = -1;
            try
            {
                _routes = (List<RouteVM>)_routeManager.getRoutes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error. \n\n" + ex.InnerException.Message);
            }
            //Generate list of routes for cbo. Configure UI 
            List<string> routeNames = _routes.Select(route => route.RouteName).ToList();
            cboSelectRoute.ItemsSource = routeNames;
            dateAssignmnetStart.IsEnabled = false;
            dateAssignmnetEnd.IsEnabled = false;
            btnSearchRouteAssignments.IsEnabled = false;
            dataRouteAssignment.Visibility = Visibility.Collapsed;
        }

        //Event for Search btn in RouteAssignments
        private void btnSearchRouteAssignments_Click(object sender, RoutedEventArgs e)
        {
            //initialize date vars
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            //Set date vars
            try
            {
                startDate = (DateTime)dateAssignmnetStart.SelectedDate;
                endDate = (DateTime)dateAssignmnetEnd.SelectedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must choose a start and end date");
            }
            //Validation of dates       
            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please define start and end dates");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Please select a start date greater than or equal to the end date");
                return;
            }
            //Show Route Assignment data-grid if valid dates entered and attempt to populate it
            dataRouteAssignment.Visibility = Visibility.Visible;
            try
            {
                _currentAssignments = _assignmentManager.GetRouteAssignmentsByRouteIDAndDate(_selectedRouteID, startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing database assignments\n\n" + ex.InnerException.Message);
            }
            dataRouteAssignment.ItemsSource = _currentAssignments;

        }

        //double click event for dataRouteAssignment data-grid
        private void dataRouteAssignment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _selectedEmployee = null;
            _selectedVehicle = null;
            try
            {
                //attempt to retrieve assignment from double click and extract it's data
                _selectedAssignment = dataRouteAssignment.SelectedItem as Route_Assignment;
                if (_selectedAssignment != null)
                {
                    _selectedDriverID = _selectedAssignment.DriverID;
                    _selectedVIN = _selectedAssignment.VIN_Number;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing that assignment");
            }

            //query database for driver and vehicle model information
            try
            {
                _selectedDriver = _assignmentManager.GetRouteAssignmentDriverByAssignmentID(_selectedAssignment.Assignment_ID);
                _selectedVehicle = _vehicleModelManager.GetVehicleModelByVIN(_selectedVIN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing database\n\n" + ex.InnerException.Message);
            }

            //Display information
            if (_selectedDriver != null)
            {
                txtDriverName.Text = _selectedDriver.Family_Name + ", " + _selectedDriver.Given_Name;
                txtDriverCapacity.Text = _selectedDriver.Max_Passenger_Count.ToString();
                txtLicenseClass.Text = _selectedDriver.Driver_License_Class_ID;
            }

            if (_selectedVehicle != null)
            {
                txtAssignmentVIN.Text = _selectedVIN;
                txtAssignmentVehicleCapacity.Text = _selectedVehicle.MaxPassengers.ToString();

            }
            //Enable the update buttons
            btnUpdateDriverAssignment.IsEnabled = true;
            btnUpdateVehicleAssignment.IsEnabled = true;

        }

        //Button that redirects user to the add to route dispatch feature
        private void btnAddRouteAssignment_Click(object sender, RoutedEventArgs e)
        {
            hideAllGrids();
            gridAddToRoute.Visibility = Visibility.Visible;

        }

        //Click event for Driver update button
        private void btnUpdateDriverAssignment_Click(object sender, RoutedEventArgs e)
        {
            //Check text content of button and either peform actions or cancel actions
            if (btnUpdateDriverAssignment.Content.ToString() != "Cancel")
            {
                //When button does not == "Cancel"
                //Hides current datagrid and displays/populates DriverUpdate data grid
                DateTime start = (DateTime)dateAssignmnetStart.SelectedDate;
                DateTime end = (DateTime)dateAssignmnetEnd.SelectedDate;
                _availableDrivers = null;
                dataRouteAssignment.Visibility = Visibility.Collapsed;
                dataRouteAssignmentDriverUpdate.Visibility = Visibility.Visible;
                btnUpdateVehicleAssignment.IsEnabled = false;
                btnUpdateVehicleAssignment.Content = "Update Vehicle";
                try
                {
                    _availableDrivers = _assignmentManager.GetAvailableDriversByDate(start, end);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error accessing available drivers\n\n" + ex.InnerException.Message);
                }
                if (_availableDrivers == null)
                {
                    MessageBox.Show("Error accessing available drivers for update");
                    return;
                }
                dataRouteAssignmentDriverUpdate.ItemsSource = _availableDrivers;
                btnUpdateDriverAssignment.Content = "Cancel";
            }
            else
            {
                //Button press does == "cancel" just resets UI
                dataRouteAssignmentDriverUpdate.Visibility = Visibility.Collapsed;
                dataRouteAssignment.Visibility = Visibility.Visible;
                btnUpdateDriverAssignment.Content = "Update Driver";
                btnUpdateVehicleAssignment.IsEnabled = true;
            }
        }

        //Click event for Update Vehicle button
        private void btnUpdateVehicleAssignment_Click(object sender, RoutedEventArgs e)
        {
            if (btnUpdateVehicleAssignment.Content.ToString() != "Cancel")
            {
                //If button content does not == "Cancel"
                //Hide current datagrid, display VehicleUpdate data grid and populate it
                DateTime start = (DateTime)dateAssignmnetStart.SelectedDate;
                DateTime end = (DateTime)dateAssignmnetEnd.SelectedDate;
                _availableVehicles = null;
                dataRouteAssignment.Visibility = Visibility.Collapsed;
                dataRouteAssignmentVehicleUpdate.Visibility = Visibility.Visible;
                btnUpdateDriverAssignment.IsEnabled = false;
                btnUpdateDriverAssignment.Content = "Update Driver";
                try
                {
                    _availableVehicles = _assignmentManager.GetAvailableVehiclesByDate(start, end);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error accessing available vehicles\n\n" + ex.InnerException.Message);
                }
                if (_availableVehicles == null)
                {
                    MessageBox.Show("Error accessing available vehicles for update");
                    return;
                }
                dataRouteAssignmentVehicleUpdate.ItemsSource = _availableVehicles;
                btnUpdateVehicleAssignment.Content = "Cancel";
            }
            else
            {
                //Button content is "Cancel"
                //Reset UI
                dataRouteAssignmentVehicleUpdate.Visibility = Visibility.Collapsed;
                dataRouteAssignment.Visibility = Visibility.Visible;
                btnUpdateVehicleAssignment.Content = "Update Vehicle";
                btnUpdateDriverAssignment.IsEnabled = true;
            }
        }

        //Double click event on DriverUpdate grid
        private void dataRouteAssignmentDriverUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DriverClass selectedDriver = null;
            //Attempt to retrieve Driver object from grid double click
            try
            {
                selectedDriver = dataRouteAssignmentDriverUpdate.SelectedItem as DriverClass;
                //If selected driver's license prevents them from driving the currently assigned vehicle, prompt user
                if (selectedDriver.Max_Passenger_Count < _selectedVehicle.MaxPassengers)
                {
                    MessageBox.Show("This driver can not operate the currently assigned vehicle due to license restrictions.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select driver");
                return;
            }
            //Prompt user if they would like to update driver with the currently selected
            if (selectedDriver != null)
            {
                string message = "Would you like to update driver to: " + selectedDriver.Family_Name + " ," + selectedDriver.Given_Name + "?";
                var result = MessageBox.Show(message, "Update Driver", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //If they choose MessageBox prompt "Yes" attempt to update database
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _assignmentManager.UpdateRouteAssignmentDriver(_selectedAssignment.Assignment_ID, selectedDriver.Employee_ID);
                        MessageBox.Show("Driver has been successfully updated");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update failed\n\n" + ex.InnerException.Message);
                    }

                }
                resetRouteAssignmentUI();

            }
        }

        //reset RouteAssignment UI elements helper method
        public void resetRouteAssignmentUI()
        {

            txtDriverName.Clear();
            txtLicenseClass.Clear();
            txtDriverCapacity.Clear();
            txtAssignmentVIN.Clear();
            txtAssignmentVehicleCapacity.Clear();
            dateAssignmnetEnd.SelectedDate = DateTime.Now;
            dateAssignmnetStart.SelectedDate = DateTime.Now;
            dataRouteAssignmentDriverUpdate.Visibility = Visibility.Collapsed;
            dataRouteAssignmentVehicleUpdate.Visibility = Visibility.Collapsed;
            dataRouteAssignment.Visibility = Visibility.Collapsed;
            btnUpdateDriverAssignment.Content = "Update Driver";
            btnUpdateVehicleAssignment.Content = "Update Vehicle";
            btnUpdateVehicleAssignment.IsEnabled = false;
            btnUpdateDriverAssignment.IsEnabled = false;
        }

        //Double click even for VehicleUpdate datagrid
        private void dataRouteAssignmentVehicleUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VehicleAssignment vehicleAssignment = null;
            //Attempt to get VehicleAssignment object from selected datagrid item
            try
            {
                vehicleAssignment = dataRouteAssignmentVehicleUpdate.SelectedItem as VehicleAssignment;
                //if the selected vehicle can not be driven by driver due to license class limitations, prompt user
                if (vehicleAssignment.Max_Passengers > _selectedDriver.Max_Passenger_Count)
                {
                    MessageBox.Show("Assigned driver does not have the license priviledges to operate this vehicle.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a vehicle");
                return;
            }
            //Prompt user if they would like to update vehicle with the currently selected
            if (vehicleAssignment != null)
            {
                string message = "Would you like to update driver to: "
                    + vehicleAssignment.VIN + " " + vehicleAssignment.Name + " "
                    + vehicleAssignment.Make + "?";
                var result = MessageBox.Show(message, "Update Vehicle", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _assignmentManager.UpdateRouteAssignmentVehicle(_selectedAssignment.Assignment_ID, vehicleAssignment.VIN);
                        MessageBox.Show("Vehicle has been successfully updated");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update failed\n\n" + ex.InnerException.Message);
                    }

                }
                resetRouteAssignmentUI();

            }
        }

    }
}
