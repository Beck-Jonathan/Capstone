using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
using Microsoft.Maps.MapControl.WPF;
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

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// Interaction logic for EditRouteStops.xaml
    /// </summary>
    public partial class EditRouteStops : Page
    {
        private RouteVM _route;
        private IRouteStopManager _routeStopManager;
        private IRouteManager _routeManager;
        public EditRouteStops(RouteVM route, IRouteStopManager routeStopManager, IRouteManager routeManager)
        {
            _route = route;
            string BingMapsKey = "1QiIMAp08xaIF29LmivZ ~HfNjTuOpTOgAMPb8WVoong~An8kYuvnA_RTEYnNx3_csnKzrL_xvR-VYy7Q3Ri8bOiIu9tEj4rWndfsVMGwQgxV";
            _routeStopManager = routeStopManager;
            _routeManager = routeManager;
            InitializeComponent();
            mapRoute.CredentialsProvider = new ApplicationIdCredentialsProvider(BingMapsKey);
            DisplayRouteData();
        }

        private void btnAddStop_Click(object sender, RoutedEventArgs e)
        {
            bool? result = new AddStopToRoute(_routeStopManager, _route).ShowDialog();
            if(result == true)
            {
                MessageBox.Show("Successfully added stop. Data persisted.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                DisplayRouteData();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        private async void DisplayRouteData()
        {

            // Set the stop pins on the map
            if (_route.RouteStops != null)
            {
                mapRoute.Children.Clear();
                datStopList.DataContext = _route.RouteStops.ToList();
                foreach (RouteStopVM routeStop in _route.RouteStops)
                {
                    mapRoute.Children.Add(new Pushpin()
                    {
                        Location = new Location(Decimal.ToDouble(routeStop.stop.Latitude), Decimal.ToDouble(routeStop.stop.Longitude))
                    });
                }
                // get the most direct routes the bus will drive along, to make the route look like a nice circle.
                if (mapRoute.Children.Count > 0)
                {
                    try
                    {
                        BingMapsResponse bingMapsResponse = await _routeManager.getRouteLine(_route);
                        if (bingMapsResponse != null)
                        {
                            MapPolyline line = new MapPolyline();
                            line.Locations = new LocationCollection();
                            line.Stroke = Brushes.Black;
                            line.StrokeThickness = 2;
                            foreach (var coordinateset in bingMapsResponse.ResourceSets[0].resources[0].routePath.line.coordinates)
                            {
                                line.Locations.Add(new Location(((List<double>)coordinateset)[0], ((List<double>)coordinateset)[1]));
                            }
                            mapRoute.Children.Add(line);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\nWe will still show the stops, just not the path to get there.", "Something went wrong!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    // Focus the map on the start of the route
                    mapRoute.Center = ((Pushpin)mapRoute.Children[0]).Location;
                }
            }
        }

        private void datStopList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datStopList.SelectedItems.Count > 0)
            {
                try { 
                Stop selectedStop = ((RouteStopVM)datStopList.SelectedItems[0]).stop;
                mapRoute.Center = new Location(Decimal.ToDouble(selectedStop.Latitude), Decimal.ToDouble(selectedStop.Longitude));
                } catch
                {
                    // pass
                }
            }
        }

        private void btnMoveStopDown_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if (datStopList.SelectedItems.Count > 0)
                {
                    try
                    {
                        RouteStopVM current = (RouteStopVM)datStopList.SelectedItems[0];
                        List<RouteStopVM> stops = _route.RouteStops.ToList();
                        RouteStopVM temp = stops.Where(s => s.StopNumber == current.StopNumber + 1).First();
                        int tempNum = temp.StopNumber;
                        temp.StopNumber = current.StopNumber;
                        current.StopNumber = tempNum;
                        _route.RouteStops = stops.OrderBy(s => s.StopNumber).ToList();
                        recompileStopList();
                        DisplayRouteData();
                    } catch
                    {
                        // pass - don't move anything.
                    }
                }
            }
        }

        private void btnMoveStopUp_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (datStopList.SelectedItems.Count > 0)
                {
                    try
                    {
                        RouteStopVM current = (RouteStopVM)datStopList.SelectedItems[0];
                        List<RouteStopVM> stops = _route.RouteStops.ToList();
                        RouteStopVM temp = stops.Where(s => s.StopNumber == current.StopNumber - 1).First();
                        int tempNum = temp.StopNumber;
                        temp.StopNumber = current.StopNumber;
                        current.StopNumber = tempNum;
                        _route.RouteStops = stops.OrderBy(s => s.StopNumber).ToList();
                        recompileStopList();
                        DisplayRouteData();
                    } catch
                    {
                        // pass - don't move anything
                    }
                }
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            bool succeeded = true;
            foreach(RouteStopVM current in _route.RouteStops)
            {
                succeeded = succeeded && _routeStopManager.UpdateOrdinal(current);
            }
            if (!succeeded)
            {
                MessageBox.Show("Update incomplete!\nTry again later.", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else
            {
                NavigationService.Navigate(new ViewRoute(_route, _routeManager));
            }
        }

        private void btnDeactivateStopForRoute_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && datStopList.SelectedItems.Count > 0)
            {
                try
                {
                    RouteStopVM current = (RouteStopVM)datStopList.SelectedItems[0];
                    List<RouteStopVM> stops = _route.RouteStops.ToList();
                    _routeStopManager.DeleteRouteStop(current);
                    stops.Remove(current);
                    recompileStopList();
                    DisplayRouteData();
                }
                catch (Exception ex)
                {
                    // display error
                    MessageBox.Show("Unable to remove stop. Please try again.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void recompileStopList()
        {
            List<RouteStopVM> stops = _route.RouteStops.ToList();

            for(int i = 0; i < stops.Count;i++)
            {
                stops[i].StopNumber = i + 1;
                stops[i].OffsetFromRouteStart = new TimeSpan(0, 15 * i, 0);
            }

            _route.RouteStops = stops;
        }
    }
}
