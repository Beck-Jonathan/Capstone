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
        private VehicleModelVM _newVehicleModel;
        private List<Parts_Inventory> _oldCompatibleParts;
        private List<Parts_Inventory> _newCompatibleParts;
        private List<Parts_Inventory> _allParts;

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
            if (_vehicleModel != null ) 
            {
                _newVehicleModel = new VehicleModelVM
                {
                    Make = _vehicleModel.Make,
                    MaxPassengers = _vehicleModel.MaxPassengers,

                    Name = _vehicleModel.Name,
                    VehicleModelID = _vehicleModel.VehicleModelID,
                    VehicleTypeID = _vehicleModel.VehicleTypeID,
                    Year = _vehicleModel.Year,

                    IsActive = _vehicleModel.IsActive
                };
                btnAddCompatiblePart.IsEnabled = true;
            }

            _allParts = _partsInventoryManager.GetAllParts_Inventory();
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
                btnCancel.Content = "Cancel";
                btnSave.Content = "Add New Model";
                _newCompatibleParts = new List<Parts_Inventory>();
            }
            else
            {
                btnSave.Content = "Save Changes";
                btnAddCompatiblePart.IsEnabled = true;

                txtName.Text = _vehicleModel.Name;
                txtMake.Text = _vehicleModel.Make;
                cmbYear.SelectedValue = _vehicleModel.Year;
                cmbType.SelectedValue = _vehicleModel.VehicleTypeID;
                txtMaxPassengers.Text = _vehicleModel.MaxPassengers.ToString();

                try
                {
                    _oldCompatibleParts = (List<Parts_Inventory>)_partsInventoryManager.GetPartsCompatibleWithVehicleModelID(_vehicleModel.VehicleModelID);
                    _newCompatibleParts = (List<Parts_Inventory>)_partsInventoryManager.GetPartsCompatibleWithVehicleModelID(_vehicleModel.VehicleModelID);

                    _vehicleModel.Compatible_Parts = _oldCompatibleParts;
                    _newVehicleModel.Compatible_Parts = _newCompatibleParts;

                    dat_compatiblePartsList.ItemsSource = _newCompatibleParts;
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while retrieving compatible parts");
                }
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
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    UPDATED: 2024-04-05
        /// <br />
        ///    Changed method name from Button_Click to removeCompatiblePartButton_Click
        /// </remarks>

        private void removeCompatiblePartButton_Click(object sender, RoutedEventArgs e)
        {
            Parts_Inventory part = dat_compatiblePartsList.SelectedItem as Parts_Inventory;
            if (part != null)
            {
                _newCompatibleParts.Remove(part);
               
            }
            dat_compatiblePartsList.ItemsSource = null;

            dat_compatiblePartsList.ItemsSource = _newCompatibleParts;
           
            btnSave.IsEnabled = true;
        }

        /// <summary>
        ///    saves the changes to the database
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>
        private void btnSave_Click(object sender, RoutedEventArgs e)
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

            _newVehicleModel = new VehicleModelVM
            {
                Name = name,
                Make = make,
                Year = year,
                VehicleTypeID = type,
                MaxPassengers = maxPassengers,
                Compatible_Parts = _newCompatibleParts
            };

            try
            {
                if (_vehicleModel == null)
                {
                    _vehicleModelManager.AddVehicleModel(_newVehicleModel);
                }
                else
                {
                    _vehicleModelManager.UpdateVehicleModel(_vehicleModel, _newVehicleModel);
                }

                NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred");
            }
        }

        /// <summary>
        ///     goes to the previous page, and does not save the changes
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

        /// <summary>
        ///     Event handler for clicking add compatible part button; opens the add compatible part dialog
        /// </summary>
        /// <remarks>
        ///     CONTRIBUTOR: Jared Hutton
        /// <br />
        ///     CREATED: 2024-04-05
        /// </remarks>
        private void btnAddCompatiblePart_Click(object sender, RoutedEventArgs e)
        {
            var availableParts =
                _vehicleModel == null ? _allParts :
                _allParts.Where(ap => !_newCompatibleParts.Any(oc => ap.Parts_Inventory_ID == oc.Parts_Inventory_ID));

            var dialog = new AddCompatiblePartWindow(availableParts);

            bool dialogResult = dialog.ShowDialog().Value;

            if (dialogResult)
            {
                _newCompatibleParts.Add(dialog.SelectedPart);
                dat_compatiblePartsList.ItemsSource = null;
                dat_compatiblePartsList.ItemsSource = _newCompatibleParts;
            }
        }
    }
}
