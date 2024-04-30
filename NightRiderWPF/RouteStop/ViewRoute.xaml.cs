
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
using Microsoft.Maps.MapControl.WPF;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// Author: Nathan Toothaker
    /// Date: 2024-03-06
    /// Interaction logic for ViewRoute.xaml
    /// </summary>
    public partial class ViewRoute : Page
    {
        RouteVM _route;
        IRouteManager _routeManager;
        IRouteStopManager _routeStopManager;
        public ViewRoute()
        {
            string BingMapsKey = "1QiIMAp08xaIF29LmivZ ~HfNjTuOpTOgAMPb8WVoong~An8kYuvnA_RTEYnNx3_csnKzrL_xvR-VYy7Q3Ri8bOiIu9tEj4rWndfsVMGwQgxV";
            _routeManager = new RouteManager();
            InitializeComponent();
            mapRoute.CredentialsProvider = new ApplicationIdCredentialsProvider(BingMapsKey);
            _route = new RouteVM()
            {
                RouteId = 100001,
                RouteName = "Weekend",
                StartTime = new Time(8, 0, 0),
                EndTime = new Time(16, 0, 0),
                RepeatTime = new TimeSpan(4, 0, 0),
                DaysOfService = new ActivityWeek(new char[] { '0', '0', '1', '0', '0', '1', '1' }),
                IsActive = true,
                RouteStops = new List<RouteStopVM>()
                {
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 0,
                        StopNumber = 1,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 0,
                            StreetAddress = "6301 Kirkwood Blvd SW, Cedar Rapids, IA",
                            ZIPCode = "52404",
                            Latitude = 41.917250m,
                            Longitude = -91.656470m,
                            IsActive = true
                        }
                    },
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 1,
                        StopNumber = 2,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 1,
                            StreetAddress = "5008, 1220 1st Ave NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 41.989670m,
                            Longitude = -91.649529m,
                            IsActive = true
                        }
                    },
                    new RouteStopVM()
                    {
                        RouteId = 100001,
                        StopId = 2,
                        StopNumber = 3,
                        OffsetFromRouteStart = new TimeSpan(0),
                        IsActive = true,
                        stop = new Stop()
                        {
                            StopId = 2,
                            StreetAddress = "1330 Elmhurst Dr NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 42.002548m,
                            Longitude = -91.652069m,
                            IsActive = true
                        }
                    }
                }
            };
            DisplayRouteData();

        }
        public ViewRoute(RouteVM route, IRouteManager routeManager)
        {
            InitializeComponent();
            string BingMapsKey = "1QiIMAp08xaIF29LmivZ ~HfNjTuOpTOgAMPb8WVoong~An8kYuvnA_RTEYnNx3_csnKzrL_xvR-VYy7Q3Ri8bOiIu9tEj4rWndfsVMGwQgxV";
            _route = route; _routeManager = routeManager; _routeStopManager = new RouteStopManager();
            mapRoute.CredentialsProvider = new ApplicationIdCredentialsProvider(BingMapsKey);
            if (_route.RouteStops == null || _route.RouteStops.Count() == 0)
            {
                try
                {
                    _route.RouteStops = _routeStopManager.GetRouteStopByRouteId(_route.RouteId);
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            DisplayRouteData();
        }

        private async void DisplayRouteData()
        {
            // display basic data
            lblRouteName.Content = _route.RouteName;
            lblStartTime.Content = _route.StartTime;
            lblEndTime.Content = _route.EndTime;
            lblDaysOfService.Content = _route.DaysOfService;
            lblCycleTime.Content = _route.RepeatTime.ToString(@"hh\:mm");

            // Set the stop pins on the map
            if (_route.RouteStops != null)
            {
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
                        if (_route.RouteStops.Count() > 1)
                        {
                            BingMapsResponse bingMapsResponse = await _routeManager.getRouteLine(_route);
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
                } else
                {
                    MessageBox.Show("No Stops on this route. Please add some.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); 
                                    }
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditRoute_Click(object sender, RoutedEventArgs e)
        {
            new AddEditRouteDetail(_route, _routeManager).ShowDialog();
        }

        private void btnBackToRouteList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RouteList(_routeManager));
        }

        private void btnEditStop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditRouteStops(_route, _routeStopManager, _routeManager));
        }

        private void btnAssignVehicleAndDriver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DispatchHome("Create Assignment"));
        }

        private void btnUdateAssignment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DispatchHome("Update Assignment"));
        }
    }
}
