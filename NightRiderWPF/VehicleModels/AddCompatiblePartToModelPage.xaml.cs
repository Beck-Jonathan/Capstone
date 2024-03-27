using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
    /// Interaction logic for AddCompatiblePartToModelPage.xaml
    /// </summary>
    public partial class AddCompatiblePartToModelPage : Page
    {
        IVehicleManager _vehicleManager = null;
        IVehicleModelManager _vehicleModelManager = null;
        IParts_InventoryManager _inventoryManager = null;
        List<Parts_Inventory> _allParts = null;
        List<Parts_Inventory> _oldParts = null;
        List<Parts_Inventory> _possibleParts = null;
        ObservableCollection<Parts_Inventory> _addedParts = null;
        VehicleModel _vehicleModel = null;
        public AddCompatiblePartToModelPage(IVehicleManager vehicleManager, IVehicleModelManager modelManager, IParts_InventoryManager partsInventoryManager,
            List<Parts_Inventory> oldCompatibleParts, VehicleModel vehicleModel)
        {
            _vehicleManager = vehicleManager;
            _vehicleModelManager = modelManager;
            _inventoryManager = partsInventoryManager;
            _oldParts = oldCompatibleParts;
            _vehicleModel = vehicleModel;
            _possibleParts = new List<Parts_Inventory>();
            
            try
            {
                _allParts = new List<Parts_Inventory>();
                _allParts = _inventoryManager.GetAllParts_Inventory();
            } catch(Exception ex)
            {
                MessageBox.Show("Error retreiving parts\n\n", ex.InnerException.Message);
            }
            //Observable collection used so that list can be modified live in UI
            _addedParts = new ObservableCollection<Parts_Inventory>();
           
            InitializeComponent();

            

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtMake.Text = _vehicleModel.Make;
            txtModel.Text = _vehicleModel.Name;
            txtYear.Text = _vehicleModel.Year.ToString();
            //Add only parts to _possibleParts that aren't already contained within the vehicle model compatiblity list
            foreach(var part in _allParts)
            {
                List<int> oldIDs = getPartIDs(_oldParts);
                if (oldIDs.Contains(part.Parts_Inventory_ID))
                {
                    continue;
                }
                _possibleParts.Add(part);
            }
            lstAvailableParts.ItemsSource = _possibleParts;
            
        }

        //helper method to return the part IDs from a list of parts
        private List<int> getPartIDs(List<Parts_Inventory> parts)
        {
            List<int> ids = new List<int>();
            foreach(var part in parts) 
            {
                ids.Add(part.Parts_Inventory_ID);
            }
            return ids;
        }

        private void lstAvailableParts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Parts_Inventory selectedItem = lstAvailableParts.SelectedItem as Parts_Inventory;
            if(lstAvailableParts != null && selectedItem != null ) 
            {
                //if _addedParts contains at least one element, check that the element being added isn't a duplicate
                if(_addedParts.Count > 0)
                {
                    foreach(var item in _addedParts)
                    {
                        if(item.Parts_Inventory_ID == selectedItem.Parts_Inventory_ID)
                        {
                            MessageBox.Show(selectedItem.Part_Name + " already added.");
                            return;
                        }
                    }
                }
                
                _addedParts.Add(selectedItem);
            }
            lstAdded.ItemsSource = _addedParts;
        }

        private void lstAdded_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Parts_Inventory selectedItem = lstAdded.SelectedItem as Parts_Inventory;
            if(selectedItem != null)
            {
                _addedParts.Remove(selectedItem);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_addedParts == null)
            {
                NavigationService.GoBack();
            }
            MessageBoxResult result = MessageBox.Show("Work not saved. Would you like to proceed?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                NavigationService.GoBack();
            }
            else
            {
                return;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(_addedParts.Count == 0)
            {
                MessageBox.Show("No parts to add");
                return;
            }
            try
            {
                foreach(var part in _addedParts)
                {
                    _inventoryManager.AddModelPartCompatibility(_vehicleModel.VehicleModelID, part.Parts_Inventory_ID);
                }
                MessageBox.Show("Parts Successfully Added");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error updating compatible parts\n\n", ex.InnerException.Message);
            }
        }
    }
}
