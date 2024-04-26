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
    /// AUTHOR: Everett DeVaux
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    ///
    ///         Interaction logic for Presentation layer for the Vehicle Lookup List
    /// </summary>
    /// 
    /// <remarks>
    ///     Initial Creation
    ///     Genereated the function from the back end to the front end

    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-02-17
    ///     
    /// </remarks>
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added implementation for deactivate vehicle.
    /// </remarks>
    public partial class VehicleLookupListPage : Page
    {

        List<Vehicle> _vehicleLookupList = null;
        IVehicleManager _vehicleLookupListMgr = null;
        

        public VehicleLookupListPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message + " occurred");
                // throw ex; catch should not throw exception on the presentation layer. Chris Baenziger 2024-02-17
            }


        }

        private void viewVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayVehicle();
        }

        private void addVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUpdateDeleteVehicle(new VehicleModelManager()));
        }

        private void addRentalBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VehicleChecklist());
        }

        /// <summary>
        ///     Retrieves all Vehicle records from the database and presents them in the datagrid
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle}">Vehicle</see> List of Vehicle objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListContents();

        }

        private void RefreshListContents()
        {
            try
            {
                _vehicleLookupListMgr = new VehicleManager();
                _vehicleLookupList = new List<Vehicle>
                    (_vehicleLookupListMgr.VehicleLookupList());

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load the vehicle list", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error); // Chris Baenziger 2024-02-17
                // throw; catch should not throw exception on the presentation layer. Chris Baenziger 2024-02-17
            }
            List<dynamic> dataObjects = new List<dynamic>();
            foreach (Vehicle VehicleLookupList in _vehicleLookupList)
            {
                string VehicleMake = VehicleLookupList.VehicleMake;
                string VehicleNumber = VehicleLookupList.VehicleNumber;
                string VehicleModel = VehicleLookupList.VehicleModel;
                int MaxPassengers = VehicleLookupList.MaxPassengers;
                int VehicleMileage = VehicleLookupList.VehicleMileage;
                string VehicleDescription = VehicleLookupList.VehicleDescription;

                dynamic vehicleListDynamic = new
                {
                    PropertyOne = VehicleMake,
                    PropertyTwo = VehicleNumber,
                    PropertyThree = VehicleModel,
                    PropertyFour = MaxPassengers,
                    PropertyFive = VehicleMileage,
                    PropertySix = VehicleDescription,
                };
                dataObjects.Add(vehicleListDynamic);

            }

            //vehicleLookupDataGrid.Items.Insert(0, dataObjects.ToArray());
            //vehicleLookupDataGrid.Items.Add(dataObjects);
            //vehicleLookupDataGrid.Items.Add(dataObjects);
            vehicleLookupDataGrid.ItemsSource = dataObjects;
            vehicleLookupDataGrid.Columns[0].Header = "Vehicle #";
            vehicleLookupDataGrid.Columns[1].Header = "Make";
            vehicleLookupDataGrid.Columns[2].Header = "Model";
            vehicleLookupDataGrid.Columns[3].Header = "Seat Count";
            vehicleLookupDataGrid.Columns[4].Header = "Mileage";
            vehicleLookupDataGrid.Columns[5].Header = "Description";
        }

        private void vehicleLookupDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DisplayVehicle();
        }

        private void DisplayVehicle()
        {
            Vehicle vehicle = null;
            if (vehicleLookupDataGrid.SelectedItem != null)
            {
                var selectedVehicle = vehicleLookupDataGrid.SelectedItem;
                var nameOfProperty = "PropertyOne";
                var propertyInfo = selectedVehicle.GetType().GetProperty(nameOfProperty);
                string vehicleNumber = propertyInfo.GetValue(selectedVehicle, null).ToString();
                try
                {
                    vehicle = _vehicleLookupListMgr.GetVehicleByVehicleNumber(vehicleNumber);
                }
                catch (Exception oex)
                {
                    MessageBox.Show("Error displaying vehicle.\n"
                        + oex.InnerException.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (vehicle != null)
                {
                    NavigationService.Navigate(new AddUpdateDeleteVehicle(new VehicleModelManager(), vehicle));
                }
            }
        }
    }
}
