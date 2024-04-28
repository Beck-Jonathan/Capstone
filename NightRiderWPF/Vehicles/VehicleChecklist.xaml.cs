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
    /// Interaction logic for VehicleChecklist.xaml
    /// </summary>
    public partial class VehicleChecklist : Page
    {
        private DataObjects.VehicleChecklist _checklist = null;
        private IVehicleManager _vehicleManager = null;
        private IEmployeeManager _employeeManager = null;

        public VehicleChecklist()
        {
            _vehicleManager = new VehicleManager();
            _employeeManager = new EmployeeManager();
            InitializeComponent();
        }

        public VehicleChecklist(IVehicleManager vehicleManager, IEmployeeManager employeeManager)
        {
            InitializeComponent();
            _vehicleManager = vehicleManager;
            _employeeManager = employeeManager;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vehicleManager == null)
            {
                _vehicleManager = new VehicleManager();
            }

            if (_employeeManager == null)
            {
                _employeeManager = new EmployeeManager();
            }

            cmbVehicle.Items.Clear();
            try
            {
                List<Vehicle> vehicles = _vehicleManager.VehicleLookupList();
                cmbVehicle.ItemsSource = vehicles.Select(s => s.VehicleMake).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to populate vehicle list.\n\n" + ex.InnerException.Message, "Vehicle List Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            cmbEmployee.Items.Clear();
            try
            {
                List<Employee_VM> employees = _employeeManager.GetEmployees();
                cmbEmployee.ItemsSource = employees.Select(s => s.Family_Name + ", " + s.Given_Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to populate employee list.\n\n" + ex.InnerException.Message, "Employee List Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var empID = 0;
                var vehID = "";
                List<Employee_VM> employees = _employeeManager.GetEmployees();
                if (cmbEmployee.SelectedItem != null)
                {
                    empID = employees.Where(s => s.Family_Name + ", " + s.Given_Name == cmbEmployee.SelectedItem.ToString()).Select(s => s.Employee_ID).Single();
                }
                
                List<Vehicle> vehicles = _vehicleManager.VehicleLookupList();
                if (cmbVehicle.SelectedItem != null)
                {
                    vehID = vehicles.Where(v => v.VehicleMake == cmbVehicle.SelectedItem.ToString()).Select(v => v.VIN).Single().ToString();
                }
                _checklist = new DataObjects.VehicleChecklist()
                {
                    EmployeeID = empID,
                    VIN = vehID,
                    ChecklistDate = DateTime.Now,
                    Clean = ckbClean.IsChecked.Value,
                    Pedals = ckbPedals.IsChecked.Value,
                    Dash = ckbDash.IsChecked.Value,
                    Steering = ckbSteering.IsChecked.Value,
                    AC_Heat = ckbClimate.IsChecked.Value,

                    MirrorDS = ckbMirrorDS.IsChecked.Value,
                    MirrorPS = ckbMirrorPS.IsChecked.Value,
                    MirrorRV = ckbMirrorRV.IsChecked.Value,

                    Cosmetic = txtCosmeticDamage.Text,

                    Tire_Pressure_DF = ckbDFTirePressure.IsChecked.Value,
                    Tire_Pressure_PF = ckbPFTirePressure.IsChecked.Value,
                    Tire_Pressure_DR = ckbDRTirePressure.IsChecked.Value,
                    Tire_Pressure_PR = ckbPRTirePressure.IsChecked.Value,

                    Blinker_DF = ckbDFTurnSignals.IsChecked.Value,
                    Blinker_PF = ckbPFTurnSignals.IsChecked.Value,
                    Blinker_DR = ckbDRTurnSignals.IsChecked.Value,
                    Blinker_PR = ckbPRTurnSignals.IsChecked.Value,

                    Breaklight_DR = ckbDriverBreaklights.IsChecked.Value,
                    Breaklight_PR = ckbPassengerBreaklights.IsChecked.Value,

                    Headlight_Driver = ckbDriverHeadlights.IsChecked.Value,
                    Headlight_Passenger = ckbPassengerHeadlights.IsChecked.Value,

                    TailLight_Driver = ckbDriverBreaklights.IsChecked.Value,
                    TailLight_Passenger = ckbPassengerBreaklights.IsChecked.Value,

                    Wiper_Driver = ckbDriverWipers.IsChecked.Value,
                    Wiper_Passenger = ckbPassengerWipers.IsChecked.Value,
                    Wiper_Rear = ckbRearWipers.IsChecked.Value,

                    SeatBelts = ckbSeatBelts.IsChecked.Value,
                    FireExtinguisher = ckbFireExtinguisher.IsChecked.Value,
                    Airbags = ckbAirbags.IsChecked.Value,
                    FirstAid = ckbFirstAidKit.IsChecked.Value,
                    EmergencyKit = ckbEmergencyKit.IsChecked.Value,
                    Mileage = Int32.Parse(txtMileage.Text),
                    FuelLevel = 5 - cmbFuelLevel.SelectedIndex,
                    Breaks = ckbBreaks.IsChecked.Value,
                    Accelerator = ckbAccelerator.IsChecked.Value,
                    Clutch = ckbClutch.IsChecked.Value,
                    Notes = txtNotes.Text
                };

                _checklist.ChecklistID = _vehicleManager.AddVehicleChecklist(_checklist);
                if (_checklist.ChecklistID != 0)
                {
                    MessageBox.Show("Checklist " + _checklist.ChecklistID.ToString() + " created.", "New checklist", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem creating the checklist.\n" +
                    "Make sure all needed fields have been entered.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Are you sure you want to cancel.\n" +
                "Enter information will be lost.",
                "Cancel Checklist", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (results == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}
