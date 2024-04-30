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

namespace NightRiderWPF.Vehicles
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Display page for vehicle add, update, and detail information.
    /// </summary>
    /// <remarks>
    /// Updated by Chris Baenziger
    ///     2024-02-10
    ///     Added features for display and update vehicle
    ///     </remarks>
    ///     <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added implementation for deactivate vehicle.
    /// UPDATER: Jared Hutton
    /// UPDATED: 2024-03-03
    /// Implement correct vehicle model functionality
    /// /// UPDATER: Jonathan Beck
    /// UPDATED: 2024-04-13
    /// Implement added btnWorkOrders and it's click event.
    /// </remarks>

    public partial class AddUpdateDeleteVehicle : Page
    {
        private Vehicle _vehicle = null;
        private VehicleManager _vehicleManager = null;
        private string _pageType = null;

        private IVehicleModelManager _vehicleModelManager;
        private IEnumerable<VehicleModel> _vehicleModels;

        // Default constructor, shows add button, submit disabled
        public AddUpdateDeleteVehicle(IVehicleModelManager vehicleModelManager)
        {
            _pageType = "add";
            InitializeComponent();
            UpdateDisplay();
            _vehicleModelManager = vehicleModelManager;
        }

        // Vehicle view, shows vehicle information and has update button
        // when update selected shows submit button, add/update becomes cancel.
        public AddUpdateDeleteVehicle(IVehicleModelManager vehicleModelManager, Vehicle vehicle)
        {
            _vehicle = vehicle;
            _pageType = "display";
            InitializeComponent();
            UpdateDisplay();
            _vehicleModelManager = vehicleModelManager;
        }

        private void UpdateDisplay()
        {
            switch (_pageType)
            {
                case "default": // disable submit button, add/update button is add
                    txtVehicleNumber.IsEnabled = false;
                    txtVIN.IsEnabled = false;
                    cmbVehicleMake.IsEnabled = false;
                    cmbVehicleModel.IsEnabled = false;
                    cmbVehicleYear.IsEnabled = false;
                    txtVehicleMileage.IsEnabled = false;
                    txtVehicleLicensePlate.IsEnabled = false;
                    txtVehicleDescription.IsEnabled = false;
                    txtDateEntered.IsEnabled = false;
                    txtSeatCount.IsEnabled = false;
                    cmbVehicleType.IsEnabled = false;
                    ckbRental.IsEnabled = false;

                    btnSubmit.IsEnabled = false;
                    btnSubmit.Visibility = Visibility.Hidden;
                    btnAddUpdate.IsEnabled = true;
                    btnAddUpdate.Content = "Add Vehicle"; // "Add Vehicle", "Update Vehicle", "Cancel"
                    btnDeactivate.Visibility = Visibility.Hidden;

                    UpdateText();

                    break;

                case "display": // displays the selected vehicle, submit button is disabled, add/update button is update.
                    txtVehicleNumber.IsEnabled = false;
                    txtVIN.IsEnabled = false;
                    cmbVehicleMake.IsEnabled = false;
                    cmbVehicleModel.IsEnabled = false;
                    cmbVehicleYear.IsEnabled = false;
                    txtVehicleMileage.IsEnabled = false;
                    txtVehicleLicensePlate.IsEnabled = false;
                    txtVehicleDescription.IsEnabled = false;
                    txtDateEntered.IsEnabled = false;
                    txtSeatCount.IsEnabled = false;
                    cmbVehicleType.IsEnabled = false;
                    ckbRental.IsEnabled = false;

                    btnSubmit.IsEnabled = false;
                    btnSubmit.Visibility = Visibility.Hidden;
                    btnAddUpdate.IsEnabled = true;
                    btnAddUpdate.Content = "Update Vehicle"; // "Add Vehicle", "Update Vehicle", "Cancel"
                    btnDeactivate.Visibility = Visibility.Hidden;

                    UpdateText();

                    break;

                case "update": // submit updates to db, submit button is enabled, add/update button is cancel.
                    txtVehicleNumber.IsEnabled = true;
                    txtVIN.IsEnabled = true;
                    cmbVehicleMake.IsEnabled = true;
                    cmbVehicleModel.IsEnabled = !string.IsNullOrWhiteSpace(_vehicle.VehicleMake) &&
                                                _vehicleModels.Count(x => x.Make == _vehicle.VehicleMake) > 1;
                    cmbVehicleYear.IsEnabled = !string.IsNullOrWhiteSpace(_vehicle.VehicleModel) &&
                                                _vehicleModels.Count(x => x.Name == _vehicle.VehicleMake && x.Name == _vehicle.VehicleModel) > 1;
                    txtVehicleMileage.IsEnabled = true;
                    txtVehicleLicensePlate.IsEnabled = true;
                    txtVehicleDescription.IsEnabled = true;
                    txtDateEntered.IsEnabled = false;
                    txtSeatCount.IsEnabled = true;
                    cmbVehicleType.IsEnabled = true;
                    ckbRental.IsEnabled = true;

                    btnSubmit.IsEnabled = true;
                    btnSubmit.Visibility = Visibility.Visible;
                    btnAddUpdate.IsEnabled = true;
                    btnAddUpdate.Content = "Cancel"; // "Add Vehicle", "Update Vehicle", "Cancel"
                    btnDeactivate.Visibility = Visibility.Visible;

                    break;

                case "add": // adds the vehicle to the db, submit button is enabled, add/update button is cancel.
                    txtVehicleNumber.IsEnabled = true;
                    txtVIN.IsEnabled = true;
                    cmbVehicleMake.IsEnabled = true;
                    txtVehicleMileage.IsEnabled = true;
                    txtVehicleLicensePlate.IsEnabled = true;
                    txtVehicleDescription.IsEnabled = true;
                    txtDateEntered.IsEnabled = false;
                    txtSeatCount.IsEnabled = true;
                    cmbVehicleType.IsEnabled = true;
                    ckbRental.IsEnabled = true;

                    btnSubmit.IsEnabled = true;
                    btnSubmit.Visibility = Visibility.Visible;
                    btnAddUpdate.IsEnabled = true;
                    btnAddUpdate.Content = "Cancel"; // "Add Vehicle", "Update Vehicle", "Cancel"
                    btnDeactivate.Visibility = Visibility.Hidden;

                    break;
            }
        }

        private void UpdateText()
        {
            if (_vehicle == null)
            {
                txtVehicleNumber.Text = "";
                txtVIN.Text = "";
                cmbVehicleMake.SelectedItem = null;
                cmbVehicleModel.SelectedItem = null;
                cmbVehicleYear.SelectedItem = null;
                txtVehicleMileage.Text = "";
                txtVehicleLicensePlate.Text = "";
                txtVehicleDescription.Text = "";
                txtDateEntered.Text = "";
                txtSeatCount.Text = "";
                cmbVehicleType.SelectedItem = null;
                ckbRental.IsChecked = false;
            }
            else
            {
                txtVehicleNumber.Text = _vehicle.VehicleNumber.ToString();
                txtVIN.Text = _vehicle.VIN;
                cmbVehicleMake.SelectedItem = _vehicle.VehicleMake;
                cmbVehicleModel.SelectedItem = _vehicle.VehicleModel;
                cmbVehicleYear.SelectedItem = _vehicle.VehicleYear.ToString();
                txtVehicleMileage.Text = _vehicle.VehicleMileage.ToString();
                txtVehicleLicensePlate.Text = _vehicle.VehicleLicensePlate;
                txtVehicleDescription.Text = _vehicle.VehicleDescription;
                txtDateEntered.Text = _vehicle.DateEntered.ToString();
                txtSeatCount.Text = _vehicle.MaxPassengers.ToString();
                cmbVehicleType.Text = _vehicle.VehicleType;
                ckbRental.IsChecked = _vehicle.Rental;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _vehicleManager = new VehicleManager();

            try
            {
                _vehicleModels = _vehicleModelManager.GetVehicleModels();
                cmbVehicleMake.ItemsSource = _vehicleModels.Select(vehicleModel => vehicleModel.Make);
                if (_vehicle != null && _vehicle.VehicleMake != null)
                {
                    cmbVehicleModel.ItemsSource = _vehicleModels.Where(x => x.Make == _vehicle.VehicleMake)
                                                                .Select(vehicleModel => vehicleModel.Name);

                    if (_vehicle.VehicleModel != null)
                    {
                        cmbVehicleYear.ItemsSource = _vehicleModels.Where(x => x.Name == _vehicle.VehicleModel)
                                                                   .Select(vehicleModel => vehicleModel.Year.ToString());
                    }
                }
                else
                {
                    cmbVehicleModel.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem loading the page.", "Page load error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            switch (_pageType)
            {
                case "display": // displays the selected vehicle, submit button is disabled, add/update button is update.

                    break;

                case "add": // adds the vehicle to the db, submit button is enabled, add/update button is cancel.

                    result = false;
                    try
                    {
                        // Validation inputs
                        if (!ValidationHelpers.IsValidVehicleNumber(txtVehicleNumber.Text))
                        {
                            MessageBox.Show("Please enter a vehicle number.", "Invalid Vehicle Number", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleNumber.Focus();
                            return;
                        }
                        if (!ValidationHelpers.IsValidVIN(txtVIN.Text))
                        {
                            MessageBox.Show("Please enter a valid VIN.", "Invalid VIN", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVIN.Focus();
                            return;
                        }
                        if (txtVehicleMileage.Text.Equals("") || !ValidationHelpers.IsValidMileage(int.Parse(txtVehicleMileage.Text)))
                        {
                            MessageBox.Show("Please enter a valid mileage.", "Invalid Mileage", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleMileage.Focus();
                            return;
                        }
                        if (txtSeatCount.Text.Equals("") || !ValidationHelpers.IsValidSeatCount(int.Parse(txtSeatCount.Text)))
                        {
                            MessageBox.Show("Please enter a valid seat count.", "Invalid Seat Count", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtSeatCount.Focus();
                            return;
                        }
                        if (!ValidationHelpers.IsValidLicensePlate(txtVehicleLicensePlate.Text))
                        {
                            MessageBox.Show("Please enter a valid license plate.", "Invalid License Plate", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleLicensePlate.Focus();
                            return;
                        }
                        if (cmbVehicleYear.Text.Equals("") || !ValidationHelpers.IsValidYear(int.Parse(cmbVehicleYear.Text)))
                        {
                            MessageBox.Show("Please enter a valid year.", "Invalid Year", MessageBoxButton.OK, MessageBoxImage.Error);
                            cmbVehicleYear.Focus();
                            return;
                        }
                        if (txtVehicleDescription.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a description.", "Invalid Description", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }

                        // convert input to new Vehicle object
                        _vehicle = new Vehicle()
                        {
                            VIN = txtVIN.Text,
                            VehicleNumber = txtVehicleNumber.Text,
                            VehicleMileage = int.Parse(txtVehicleMileage.Text),
                            VehicleModelID = _vehicleModels.Single(x => x.Make == cmbVehicleMake.Text &&
                                                                        x.Name == cmbVehicleModel.Text &&
                                                                        x.Year == int.Parse(cmbVehicleYear.Text))
                                                           .VehicleModelID,
                            VehicleLicensePlate = txtVehicleLicensePlate.Text,
                            VehicleYear = int.Parse(cmbVehicleYear.Text),
                            DateEntered = DateTime.Now.Date,
                            MaxPassengers = int.Parse(txtSeatCount.Text),
                            VehicleDescription = txtVehicleDescription.Text,
                            VehicleType = cmbVehicleType.Text,
                            Rental = ckbRental.IsChecked.Value
                        };

                        // Try to add the vehicle to the database
                        result = _vehicleManager.AddVehicle(_vehicle);
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            MessageBox.Show("There was a problem adding the vehicle.\n" + ex.InnerException.Message, "Add Vehicle Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("There was a problem adding the vehicle.", "Add Vehicle Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Vehicle was added to the database.", "Vehicle Added",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        _pageType = "display";
                        UpdateDisplay();
                    }
                    break;

                case "default": // disable submit button, add/update button is add

                    break;

                case "update": // submit updates to db, submit button is enabled, add/update button is cancel.

                    result = false;
                    try
                    {
                        // Validation inputs
                        if (!ValidationHelpers.IsValidVehicleNumber(txtVehicleNumber.Text))
                        {
                            MessageBox.Show("Please enter a vehicle number.", "Invalid Vehicle Number", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleNumber.Focus();
                            return;
                        }
                        if (!ValidationHelpers.IsValidVIN(txtVIN.Text))
                        {
                            MessageBox.Show("Please enter a valid VIN.", "Invalid VIN", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVIN.Focus();
                            return;
                        }
                        if (txtVehicleMileage.Text.Equals("") || !ValidationHelpers.IsValidMileage(int.Parse(txtVehicleMileage.Text)))
                        {
                            MessageBox.Show("Please enter a valid mileage.", "Invalid Mileage", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleMileage.Focus();
                            return;
                        }
                        if (txtSeatCount.Text.Equals("") || !ValidationHelpers.IsValidSeatCount(int.Parse(txtSeatCount.Text)))
                        {
                            MessageBox.Show("Please enter a valid seat count.", "Invalid Seat Count", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtSeatCount.Focus();
                            return;
                        }
                        if (!ValidationHelpers.IsValidLicensePlate(txtVehicleLicensePlate.Text))
                        {
                            MessageBox.Show("Please enter a valid license plate.", "Invalid License Plate", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleLicensePlate.Focus();
                            return;
                        }
                        if (cmbVehicleYear.Text.Equals("") || !ValidationHelpers.IsValidYear(int.Parse(cmbVehicleYear.Text)))
                        {
                            MessageBox.Show("Please enter a valid year.", "Invalid Year", MessageBoxButton.OK, MessageBoxImage.Error);
                            cmbVehicleYear.Focus();
                            return;
                        }
                        if (txtVehicleDescription.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a description.", "Invalid Description", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }

                        // convert changes to new Vehicle object
                        Vehicle newVehicle = new Vehicle()
                        {
                            VIN = txtVIN.Text,
                            VehicleModelID = _vehicleModels.Single(x => x.Make == cmbVehicleMake.Text &&
                                                                        x.Name == cmbVehicleModel.Text &&
                                                                        x.Year == int.Parse(cmbVehicleYear.Text))
                                                           .VehicleModelID,
                            VehicleNumber = txtVehicleNumber.Text,
                            VehicleMileage = int.Parse(txtVehicleMileage.Text),
                            VehicleLicensePlate = txtVehicleLicensePlate.Text,
                            VehicleMake = cmbVehicleMake.Text,
                            VehicleModel = cmbVehicleModel.Text,
                            VehicleYear = int.Parse(cmbVehicleYear.Text),
                            DateEntered = DateTime.Parse(txtDateEntered.Text),
                            MaxPassengers = int.Parse(txtSeatCount.Text),
                            VehicleDescription = txtVehicleDescription.Text,
                            VehicleType = cmbVehicleType.Text,
                            Rental = ckbRental.IsChecked.Value
                        };

                        // try to update the vehicle and save success to results
                        result = _vehicleManager.EditVehicle(_vehicle, newVehicle);
                        if (result)
                        {
                            // if successful update old vehicle with new vehicle to be displyed
                            _vehicle = newVehicle;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem updating the vehicle.\n" + ex.InnerException?.Message, "Update Vehicle Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Vehicle was updated in the database.", "Vehicle Updated",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        // update the page with the new vehicle
                        _pageType = "display";
                        UpdateDisplay();
                    }
                    break;

            }
        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (btnAddUpdate.Content.Equals("Add Vehicle"))
            {
                _pageType = "add";
                UpdateDisplay();
            }
            else if (btnAddUpdate.Content.Equals("Update Vehicle"))
            {
                _pageType = "update";
                UpdateDisplay();
            }
            else if (btnAddUpdate.Content.Equals("Cancel"))
            {
                // returns to 
                if (_pageType.Equals("display"))
                {
                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                }

                var result = MessageBox.Show("Are you sure you want to cancel? Changes will be lost!", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                // returns from add page to display page if vehicle isn't blank
                if (result == MessageBoxResult.Yes)
                {
                    if (_pageType.Equals("add") && _vehicle != null)
                    {
                        _pageType = "display";
                        UpdateDisplay();
                    }
                    else
                    {
                        if (NavigationService.CanGoBack)
                        {
                            NavigationService.GoBack();
                        }
                    }
                }
            }
        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to deactivate the vehicle?", "Deactivate Vehicle", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            // returns from add page to display page if vehicle isn't blank
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var deactivated = _vehicleManager.DeactivateVehicle(_vehicle);
                    if (deactivated)
                    {
                        MessageBox.Show("Vehicle was deactivated.", "Deactivated", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (NavigationService.CanGoBack)
                        {
                            NavigationService.GoBack();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to deactivate the vehicle.", "Deactivate Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void cmbVehicleMake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string make = cmbVehicleMake.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(make))
            {
                cmbVehicleYear.IsEnabled = false;
                cmbVehicleModel.SelectedItem = null;
                cmbVehicleModel.ItemsSource = null;

                cmbVehicleYear.IsEnabled = false;
                cmbVehicleYear.SelectedItem = null;
                cmbVehicleYear.ItemsSource = null;
            }
            else
            {
                FillVehicleModelOptions();
            }
        }

        private void cmbVehicleModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string model = cmbVehicleModel.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(model))
            {
                cmbVehicleYear.IsEnabled = false;
                cmbVehicleYear.SelectedItem = null;
                cmbVehicleYear.ItemsSource = null;
            }
            else
            {
                FillVehicleYearOptions();
            }
        }

        private void cmbVehicleYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string year = cmbVehicleYear.SelectedItem as string; // int.Parse(cmbVehicleYear.SelectedItem.ToString());

            if (string.IsNullOrWhiteSpace(year))
            {
                cmbVehicleType.IsEnabled = false;
                cmbVehicleType.SelectedItem = null;
                cmbVehicleType.ItemsSource = null;
            }
            else
            {
                FillVehicleTypeOptions();
            }
        }

        private void FillVehicleModelOptions()
        {
            var models = _vehicleModels.Where(x => x.Make == cmbVehicleMake.SelectedItem.ToString())
                                       .Select(x => x.Name);

            cmbVehicleModel.ItemsSource = models;

            if (models.Count() > 1)
            {
                cmbVehicleYear.SelectedItem = null;
                cmbVehicleYear.IsEnabled = true;
            }
            else
            {
                cmbVehicleModel.IsEnabled = false;
                cmbVehicleModel.SelectedIndex = 0;

                FillVehicleYearOptions();
            }
        }

        private void FillVehicleYearOptions()
        {
            var years = _vehicleModels.Where(x => x.Make == cmbVehicleMake.SelectedItem.ToString() && x.Name == cmbVehicleModel.SelectedItem.ToString())
                                      .Select(x => x.Year.ToString());

            cmbVehicleYear.ItemsSource = years;

            if (years.Count() > 1)
            {
                cmbVehicleYear.SelectedItem = null;
                cmbVehicleYear.IsEnabled = true;
            }
            else
            {
                //_vehicle.VehicleYear = Int32.Parse(years.Single());

                cmbVehicleYear.IsEnabled = false;
                cmbVehicleYear.SelectedIndex = 0;
            }
        }

        private void FillVehicleTypeOptions()
        {
            var type = _vehicleModels.Where(x => x.Make == cmbVehicleMake.SelectedItem.ToString() && x.Name == cmbVehicleModel.SelectedItem.ToString() && x.Year.ToString() == cmbVehicleYear.SelectedItem.ToString())
                                      .Select(x => x.VehicleTypeID);

            cmbVehicleType.ItemsSource = type;

            if (type.Count() > 1)
            {
                cmbVehicleType.SelectedItem = null;
                cmbVehicleType.IsEnabled = true;
            }
            else
            {
                cmbVehicleType.IsEnabled = false;
                cmbVehicleType.SelectedIndex = 0;
            }
        }

        //Jonathan Beck 2024-04-13
        //Get all service orders and load a new "ViewWorkOrderList" window
        private void btnWorkOrders_Click(object sender, RoutedEventArgs e)
        {
            List<ServiceOrder_VM> orders = new List<ServiceOrder_VM>();
            try
            {
                orders = _vehicleManager.getAllService_OrderByVIN(_vehicle.VIN);
            }
            catch (Exception)
            {

                throw;
            }
            NavigationService.Navigate(new WorkOrders.ViewWorkOrderList(orders));
        }


    }
}