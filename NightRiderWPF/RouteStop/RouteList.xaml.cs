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
using DataAccessFakes;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;


namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Interaction logic for RouteList.xaml
    /// </summary>
    /// <remarks>
    /// UPDATER: Chris Baenziger
    /// UDPATED: 2024-03-05
    /// Added route deactivate and activate functionality
    /// </remarks>
    public partial class RouteList : Page
    {
        private IEnumerable<RouteVM> _routes;
        private IRouteManager _routeManager;
        public RouteList()
        {
            InitializeComponent();
            _routeManager = new RouteManager();
            refreshRouteList();
        }

        private void refreshRouteList()
        {
            try
            {
                _routes = _routeManager.getRoutes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

                NavigationService.GoBack();
                return;
            }
            datRouteList.ItemsSource = _routes;
        }

        private void datRouteList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RouteVM selectedRoute = null;
            if(datRouteList.SelectedItem != null)
            {
                selectedRoute = datRouteList.SelectedItem as RouteVM;
                NavigationService.Navigate(new ViewRoute(selectedRoute, _routeManager));
            }
        }

        private void btnToggleRouteActive_Click(object sender, RoutedEventArgs e)
        {
            var route = datRouteList.SelectedItem as Route;
            if (route != null)
            {
                if (route.IsActive)
                {
                    var messageBoxResult = MessageBox.Show("Are you sure you want to deactivate " + route.RouteName,
                        "Deactivate Route", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        try
                        {
                            _routeManager.DeactivateRoute(route.RouteId);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("There was a problem deactivating the route.\n" + ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    try
                    {
                        _routeManager.ActivateRoute(route.RouteId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem activating the route.\n" + ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                refreshRouteList();
            }
            else
            {
                MessageBox.Show("No route selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnStopsList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StopList());
        }

        private void btnAddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddEditRouteDetail adder = new AddEditRouteDetail(_routeManager);
            adder.Closed += Adder_Closed;
            adder.ShowDialog();
        }

        private void Adder_Closed(object sender, EventArgs e)
        {
            refreshRouteList();
        }
    }
}
