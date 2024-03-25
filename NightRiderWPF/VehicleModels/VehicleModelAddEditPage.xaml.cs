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

        private VehicleModelVM _vehicleModel;
        private VehicleModelVM _newVehcileModel;
        List<Parts_Inventory> oldCompatible;
        List<Parts_Inventory> newCompatible;

        public VehicleModelAddEditPage(
            IVehicleModelManager vehicleModelManager, 
            IVehicleManager vehicleManager,
            IParts_InventoryManager partsInventoryManager,
            VehicleModelVM vehicleModel = null)
        {
            InitializeComponent();

            _vehicleModelManager = vehicleModelManager;
            _vehicleManager = vehicleManager;
            _partsInventoryManager = partsInventoryManager;
            _vehicleModel = vehicleModel;
            _newVehcileModel = new VehicleModelVM {
                Make = _vehicleModel.Make,
                MaxPassengers = _vehicleModel.MaxPassengers,

                
                Name = _vehicleModel.Name,
                VehicleModelID = _vehicleModel.VehicleModelID,
                VehicleTypeID = _vehicleModel.VehicleTypeID,
                Year = _vehicleModel.Year,
                
                IsActive = _vehicleModel.IsActive       
 };
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
            try
            {
                cmbType.ItemsSource = _vehicleManager.GetVehicleTypes();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            
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
                    oldCompatible = (List<Parts_Inventory>)_partsInventoryManager.GetPartsCompatibleWithVehicleModelID(_vehicleModel.VehicleModelID);
                    newCompatible = (List<Parts_Inventory>)_partsInventoryManager.GetPartsCompatibleWithVehicleModelID(_vehicleModel.VehicleModelID);
                    _vehicleModel.Compatible_Parts = oldCompatible;
                    _newVehcileModel.Compatible_Parts = newCompatible;

                    dat_compatiblePartsList.ItemsSource = newCompatible;
                    
                 }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while retrieving compatible parts");
                }
            }
            
            btnAddCompatiblePart.IsEnabled = false;
            btnSave.IsEnabled = false;

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

        /// <summary>
        ///     Handles click events for the remove button;
        ///     Removes a part from the compatibility list, and updates the UI. Does not save to database at this time
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parts_Inventory part = dat_compatiblePartsList.SelectedItem as Parts_Inventory;
            if (part != null)
            {
                newCompatible.Remove(part);
               
            }
            dat_compatiblePartsList.ItemsSource = null;

            dat_compatiblePartsList.ItemsSource = newCompatible;
           
            btnSave.IsEnabled = true;
        }

        /// <summary>
        ///    saves the changes to teh database
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _vehicleModelManager.UpdateVehicleModel(_vehicleModel, _newVehcileModel);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Updated!");
            NavigationService.GoBack();
        }
        /// <summary>
        ///     goes to the previous page, and does not save the cnages
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) {
                NavigationService.GoBack();

            }
        }
    }
}
