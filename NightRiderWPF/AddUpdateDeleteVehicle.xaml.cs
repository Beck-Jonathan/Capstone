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

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Display page for vehicle add, update, and detail information.
    /// </summary>

    public partial class AddUpdateDeleteVehicle : Page
    {
        private Vehicle _vehicle = null;
        private VehicleManager _vehicleManager = null;

        public AddUpdateDeleteVehicle()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _vehicleManager = new VehicleManager();

            cmbVehicleType.ItemsSource = _vehicleManager.GetVehicleTypes();
            cmbVehicleModel.ItemsSource = _vehicleManager.GetVehicleModels();
            cmbVehicleMake.ItemsSource = _vehicleManager.GetVehicleMakes();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            try
            {
                // Validation inputs
                if (!ValidationHelpers.IsValidVehicleNumber(txtVehicleNumber.Text))
                {
                    MessageBox.Show("Please enter a vehicle number.", "Invalid Vehicle Number", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtVehicleNumber.Focus();
                    return;
                }
                if (!ValidationHelpers.IsValidVIN(txtVIN.Text)){
                    MessageBox.Show("Please enter a valid VIN.","Invalid VIN",MessageBoxButton.OK, MessageBoxImage.Error);
                    txtVIN.Focus();
                    return;
                }
                if (txtVehicleMileage.Text.Equals("") || !ValidationHelpers.IsValidMileage(int.Parse(txtVehicleMileage.Text))){
                    MessageBox.Show("Please enter a valid mileage.", "Invalid Mileage", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtVehicleMileage.Focus();
                    return;
                }
                if (txtSeatCount.Text.Equals("") || !ValidationHelpers.IsValidSeatCount(int.Parse(txtSeatCount.Text))){
                    MessageBox.Show("Please enter a valid seat count.", "Invalid Seat Count", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtSeatCount.Focus();
                    return;
                }
                if (!ValidationHelpers.IsValidLicensePlate(txtVehicleLicensePlate.Text)){
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

                // Try to add the vehicle to the database
                result = _vehicleManager.AddVehicle(
                new Vehicle()
                {
                    VIN = txtVIN.Text,
                    VehicleNumber = txtVehicleNumber.Text,
                    VehicleMileage = int.Parse(txtVehicleMileage.Text),
                    VehicleLicensePlate = txtVehicleLicensePlate.Text,
                    VehicleMake = cmbVehicleMake.Text,
                    VehicleModel = cmbVehicleModel.Text,
                    VehicleYear = int.Parse(txtVehicleYear.Text),
                    MaxPassengers = int.Parse(txtSeatCount.Text),
                    VehicleDescription = txtVehicleDescription.Text,
                    VehicleType = cmbVehicleType.Text
                }); ;
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem adding the vehicle.", "Add Vehicle Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if(result == true)
            {
                MessageBox.Show("Vehicle was added to the database.", "Vehicle Added",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
