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
    /// Updated by Chris Baenziger
    ///     2024-02-10
    ///     Added features for display and update vehicle

    public partial class AddUpdateDeleteVehicle : Page
    {
        private Vehicle _vehicle = null;
        private VehicleManager _vehicleManager = null;
        private string _pageType = null;

        // Default constructor, shows add button, submit disabled
        public AddUpdateDeleteVehicle()
        {
            _pageType = "add";
            InitializeComponent();
            UpdateDisplay();
        }

        // Vehicle view, shows vehicle information and has update button
        // when update selected shows submit button, add/update becomes cancel.
        public AddUpdateDeleteVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            _pageType = "display";
            InitializeComponent();
            UpdateDisplay();
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
                    txtVehicleYear.IsEnabled = false;
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

                    UpdateText();

                    break;

                case "display": // displays the selected vehicle, submit button is disabled, add/update button is update.
                    txtVehicleNumber.IsEnabled = false;
                    txtVIN.IsEnabled = false;
                    cmbVehicleMake.IsEnabled = false;
                    cmbVehicleModel.IsEnabled = false;
                    txtVehicleYear.IsEnabled = false;
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

                    UpdateText();

                    break;

                case "update": // submit updates to db, submit button is enabled, add/update button is cancel.
                    txtVehicleNumber.IsEnabled = true;
                    txtVIN.IsEnabled = true;
                    cmbVehicleMake.IsEnabled = true;
                    cmbVehicleModel.IsEnabled = true;
                    txtVehicleYear.IsEnabled = true;
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
                    break;

                case "add": // adds the vehicle to the db, submit button is enabled, add/update button is cancel.
                    txtVehicleNumber.IsEnabled = true;
                    txtVIN.IsEnabled = true;
                    cmbVehicleMake.IsEnabled = true;
                    cmbVehicleModel.IsEnabled = true;
                    txtVehicleYear.IsEnabled = true;
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
                    break;
            }
        }

        private void UpdateText()
        {
            if (_vehicle == null)
            {
                txtVehicleNumber.Text = "";
                txtVIN.Text = "";
                cmbVehicleMake.Text = "";
                cmbVehicleModel.Text = "";
                txtVehicleYear.Text = "";
                txtVehicleMileage.Text = "";
                txtVehicleLicensePlate.Text = "";
                txtVehicleDescription.Text = "";
                txtDateEntered.Text = "";
                txtSeatCount.Text = "";
                cmbVehicleType.Text = "";
                ckbRental.IsChecked = false;
            }
            else
            {
                txtVehicleNumber.Text = _vehicle.VehicleNumber.ToString();
                txtVIN.Text = _vehicle.VIN;
                cmbVehicleMake.Text = _vehicle.VehicleMake;
                cmbVehicleModel.Text = _vehicle.VehicleModel;
                txtVehicleYear.Text = _vehicle.VehicleYear.ToString();
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
                cmbVehicleType.ItemsSource = _vehicleManager.GetVehicleTypes();
                cmbVehicleModel.ItemsSource = _vehicleManager.GetVehicleModels();
                cmbVehicleMake.ItemsSource = _vehicleManager.GetVehicleMakes();
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
                        if (txtVehicleYear.Text.Equals("") || !ValidationHelpers.IsValidYear(int.Parse(txtVehicleYear.Text)))
                        {
                            MessageBox.Show("Please enter a valid year.", "Invalid Year", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleYear.Focus();
                            return;
                        }
                        if (txtVehicleDescription.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a description.", "Invalid Description", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }
                        if (cmbVehicleType.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a vehicle type.", "Invalid Type", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }

                        // convert input to new Vehicle object
                        _vehicle = new Vehicle()
                        {
                            VIN = txtVIN.Text,
                            VehicleNumber = txtVehicleNumber.Text,
                            VehicleMileage = int.Parse(txtVehicleMileage.Text),
                            VehicleLicensePlate = txtVehicleLicensePlate.Text,
                            VehicleMake = cmbVehicleMake.Text,
                            VehicleModel = cmbVehicleModel.Text,
                            VehicleYear = int.Parse(txtVehicleYear.Text),
                            DateEntered = DateTime.Now.Date,
                            MaxPassengers = int.Parse(txtSeatCount.Text),
                            VehicleDescription = txtVehicleDescription.Text,
                            VehicleType = cmbVehicleType.Text,
                            Rental = ckbRental.IsChecked.Value
                        };

                        // lookup ModelLookupID and see if it exsists.
                        _vehicle.ModelLookupID = _vehicleManager.GetModelLookupID(_vehicle).ModelLookupID;
                        if (_vehicle.ModelLookupID == 0)
                        {
                            // if Model ID wasn't found prompt use if they want to add it and update.
                            var update = MessageBox.Show("Unable to find a matching make and model.\nWould you like to add it?", "No Make/Model", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if ((int)update == 6)
                            {
                                // add new model if it didn't exsist and set ModelID to vehicle
                                var modelAdded = _vehicleManager.AddModelLookup(_vehicle);
                                if (modelAdded)
                                {
                                    // get new model lookup id that was added
                                    _vehicle.ModelLookupID = _vehicleManager.GetModelLookupID(_vehicle).ModelLookupID;
                                }
                            }
                        }

                        // Try to add the vehicle to the database
                        result = _vehicleManager.AddVehicle(_vehicle);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem adding the vehicle.\n" + ex.InnerException.Message, "Add Vehicle Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
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
                        if (txtVehicleYear.Text.Equals("") || !ValidationHelpers.IsValidYear(int.Parse(txtVehicleYear.Text)))
                        {
                            MessageBox.Show("Please enter a valid year.", "Invalid Year", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleYear.Focus();
                            return;
                        }
                        if (txtVehicleDescription.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a description.", "Invalid Description", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }
                        if (cmbVehicleType.Text.Equals(""))
                        {
                            MessageBox.Show("Please enter a vehicle type.", "Invalid Type", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtVehicleDescription.Focus();
                            return;
                        }

                        // convert changes to new Vehicle object
                        Vehicle newVehicle = new Vehicle()
                        {
                            VIN = txtVIN.Text,
                            VehicleNumber = txtVehicleNumber.Text,
                            VehicleMileage = int.Parse(txtVehicleMileage.Text),
                            VehicleLicensePlate = txtVehicleLicensePlate.Text,
                            VehicleMake = cmbVehicleMake.Text,
                            VehicleModel = cmbVehicleModel.Text,
                            VehicleYear = int.Parse(txtVehicleYear.Text),
                            DateEntered = DateTime.Parse(txtDateEntered.Text),
                            MaxPassengers = int.Parse(txtSeatCount.Text),
                            VehicleDescription = txtVehicleDescription.Text,
                            VehicleType = cmbVehicleType.Text,
                            Rental = ckbRental.IsChecked.Value
                        };

                        // lookup ModelLookupID in case user changed the make, model, year or passenger seat count.
                        newVehicle.ModelLookupID = _vehicleManager.GetModelLookupID(newVehicle).ModelLookupID;
                        if (newVehicle.ModelLookupID == 0)
                        {
                            // if Model ID wasn't found prompt use if they want to add it and update.
                            var update = MessageBox.Show("Unable to find a matching make and model.\nWould you like to add it?", "No Make/Model", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if ((int)update == 6)
                            {
                                // add new model if it didn't exsist
                                var modelAdded = _vehicleManager.AddModelLookup(newVehicle);
                                if (modelAdded)
                                {
                                    // get new model lookup id that was added
                                    newVehicle = _vehicleManager.GetModelLookupID(newVehicle);
                                }
                            }
                        }

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
                        MessageBox.Show("There was a problem updating the vehicle.\n" + ex.InnerException.Message, "Update Vehicle Error",
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
                if (_pageType.Equals("display")) {
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
    }
}