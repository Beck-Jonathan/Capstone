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

namespace NightRiderWPF.VehicleModels
{
    /// <summary>
    /// Interaction logic for VehicleModelAddEditPage.xaml
    /// </summary>
    public partial class VehicleModelAddEditPage : Page
    {
        private IVehicleModelManager _vehicleModelManager;
        private IVehicleManager _vehicleManager;
        private IParts_InventoryManager _partsInventoryManager;

        private VehicleModel _vehicleModel;

        public VehicleModelAddEditPage(
            IVehicleModelManager vehicleModelManager, 
            IVehicleManager vehicleManager,
            IParts_InventoryManager partsInventoryManager,
            VehicleModel vehicleModel = null)
        {
            InitializeComponent();

            _vehicleModelManager = vehicleModelManager;
            _vehicleManager = vehicleManager;
            _partsInventoryManager = partsInventoryManager;
            _vehicleModel = vehicleModel;
        }

        /// <summary>
        ///     Handles load event for page; change page appearance
        ///     based on whether creating, editing, or viewing
        ///     a vehicle model; load supplementary data
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-22
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Populate year and type combo boxes
            List<int> years = new List<int>();

            for (int year = 1940; year < 2031; year++)
            {
                years.Add(year);
            }

            cmbYear.ItemsSource = years;
            cmbType.ItemsSource = _vehicleManager.GetVehicleTypes();
            
            // Code based on whether adding or editing vehicle model
            if (_vehicleModel == null)
            {
                btnSave.Content = "Add New Vehicle";
            }
            else
            {
                btnSave.Content = "Save Changes";

                txtName.Text = _vehicleModel.Name;
                txtMake.Text = _vehicleModel.Make;
                cmbYear.SelectedValue = _vehicleModel.Year;
                cmbType.SelectedValue = _vehicleModel.VehicleTypeID;
                txtMaxPassengers.Text = _vehicleModel.MaxPassengers.ToString();

                try
                {
                    dat_compatiblePartsList.ItemsSource =
                        _partsInventoryManager.GetPartsCompatibleWithVehicleModelID(_vehicleModel.VehicleModelID);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while retrieving compatible parts");
                }
            }
        }

        /// <summary>
        ///     Handles click events for the add button;
        ///     create a new vehicle entry in the data source
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-19
        /// </remarks>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string make = txtMake.Text;
            string type = cmbType.Text;
            int year;
            int maxPassengers;

            try
            {
                year = Int32.Parse(cmbYear.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select a valid year");
                return;
            }

            try
            {
                maxPassengers = Int32.Parse(txtMaxPassengers.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid value for max passengers");
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a model name");
                return;
            }

            if (string.IsNullOrEmpty(make))
            {
                MessageBox.Show("Please enter a make");
                return;
            }

            try
            {
                if (_vehicleModel == null)
                {
                    _vehicleModelManager.AddVehicleModel(new VehicleModel
                    {
                        Name = name,
                        Make = make,
                        Year = year,
                        VehicleTypeID = type,
                        MaxPassengers = maxPassengers
                    });
                }

                NavigationService.GoBack();
            }
            catch(Exception)
            {
                MessageBox.Show("An error occurred");
            }
        }
    }
}
