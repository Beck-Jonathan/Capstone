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

namespace NightRiderWPF.DeveloperView
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
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// 
    ///     Initial Creation
    ///     
    ///     Genereated the function from the back end to the front end
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
                throw ex;
            }


        }

        private void viewVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void addVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void addRentalBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
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

            try
            {
                _vehicleLookupListMgr = new VehicleManager();
                _vehicleLookupList = new List<Vehicle>
                    (_vehicleLookupListMgr.VehicleLookupList());

            }
            catch (Exception)
            {

                throw;
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
        //Checked by James Williams

        private void vehicleLookupDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }
    }
}
