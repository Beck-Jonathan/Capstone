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


namespace NightRiderWPF.Dispatch
{
    /// <summary>
    /// Interaction logic for DispatchHome.xaml
    /// </summary>
    public partial class DispatchHome : Page
    {
        string _selectedModel = null;
        public DispatchHome()
        {
            InitializeComponent();
        }

        private void cboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //collapse all grids when Model selection changed to avoid overlapping UI
            hideAllGrids();
            if (cboModel.SelectedItem is ComboBoxItem selectedItem)

            {
                _selectedModel = selectedItem.Content.ToString();
                //reset the Service combobox after Model selection change
                cboService.SelectedIndex = -1;
                if (_selectedModel == null)
                {
                    return;
                }

                switch (_selectedModel)
                {
                    //add any 'Service' cbo items here
                    case "Vehicle":
                        generateCBO(new[] { "Schedules", "Maintenance" });
                        break;
                    case "Driver":
                        generateCBO(new[] { "Schedules", "Availability" });
                        break;
                    case "Route":
                        generateCBO(new[] { "Schedules" });
                        break;
                    case "Charter":
                        generateCBO(new[] { "Schedules" });
                        break;
                    case "Ride Service":
                        generateCBO(new[] { "Schedules" });
                        break;
                    default:
                        break;
                }
            }
        }


        //Service cbo selection change event
        private void cboService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboService.SelectedItem is string selectedItem)
            {
                string selectedService = selectedItem;
                if (selectedService == null)
                {
                    return;
                }
                switch (_selectedModel)
                {
                    // add your grid for your feature here.
                    case "Vehicle":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridVehicleSchedules);
                                break;
                            case "Maintenance":
                                showGrid(gridVehicleMaintenance);
                                break;
                        }
                        break;
                    case "Driver":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridDriverSchedules);
                                break;
                            case "Availability":
                                showGrid(gridDriverAvailability);
                                break;
                        }
                        break;
                    case "Route":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridRouteSchedules);
                                break;
                        }
                        break;
                    case "Charter":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridCharterSchedules);
                                break;
                        }
                        break;
                    case "Ride Service":
                        switch (selectedService)
                        {
                            case "Schedules":
                                showGrid(gridRideServiceSchedules);
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // Hides all grids except the grid provided as the parameter
        // Add any additional grids 
        private void showGrid(Grid grid)
        {
            gridVehicleSchedules.Visibility = Visibility.Collapsed;
            gridVehicleMaintenance.Visibility = Visibility.Collapsed;
            gridDriverSchedules.Visibility = Visibility.Collapsed;
            gridDriverAvailability.Visibility = Visibility.Collapsed;
            gridRouteSchedules.Visibility = Visibility.Collapsed;
            gridCharterSchedules.Visibility = Visibility.Collapsed;
            gridRideServiceSchedules.Visibility = Visibility.Collapsed;
            //add any additional grids here as Visibility.Collapsed


            grid.Visibility = Visibility.Visible;
        }

        // Collapses all grids. If you create a new grid, add it's reference here
        private void hideAllGrids()
        {
            gridVehicleSchedules.Visibility = Visibility.Collapsed;
            gridVehicleMaintenance.Visibility = Visibility.Collapsed;
            gridDriverSchedules.Visibility = Visibility.Collapsed;
            gridDriverAvailability.Visibility = Visibility.Collapsed;
            gridRouteSchedules.Visibility = Visibility.Collapsed;
            gridCharterSchedules.Visibility = Visibility.Collapsed;
            gridRideServiceSchedules.Visibility = Visibility.Collapsed;
            //add any additional grids here as Visibility.Collapsed
        }

        //generates combobox items from a list of strings
        private void generateCBO(string[] items)
        {
            cboService.ItemsSource = items;
        }
    }

}
