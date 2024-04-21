using LogicLayer.RouteAssignment;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
using System.Collections.Generic;
using DataObjects.HelperObjects;
using NightRiderWPF.WorkOrders;

namespace NightRiderWPF.Driver
{

    /// <summary>
    /// Interaction logic for RouteAssignmentPage.xaml
    /// </summary>
    public partial class RouteAssignmentPage : Page
    {
        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     The presentation layer displaying a map and list of routes for drivers
        /// </summary>
        /// Initial Creation
        /// <remarks>
        /// UPDATER: 
        /// <br />
        /// UPDATED: 
        /// <br />
        ///    
        /// </remarks>
        IEnumerable<Route_Assignment_VM> _routes;
        IRouteAssignmentManager _routeAssignmentManager = new RouteAssignmentManager();
        IRouteStopManager _routeStopManager = new RouteStopManager();
        Map _mapRoute;
        Route_Assignment_VM _selectedRoute;

        public RouteAssignmentPage()
        {
            InitializeComponent();
        }
        public RouteAssignmentPage(int id)
        {
            InitializeComponent();
            try
            {
                MessageBox.Show("Please have your locations settings on to view routes. To enable location services:\n\n1. " +
                    "Go to Settings on your device.\n2. Find and tap on 'Privacy & Security'.\n3." +
                    " Look for 'Location Services' or similar.\n4. Toggle the switch to turn it on.",
                    "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                _routes = _routeAssignmentManager.GetAllRouteAssignmentByDriverID(id);
                if (_routes.Any())
                {
                    _mapRoute = mapRoute;
                    _selectedRoute = _routes.FirstOrDefault();
                    ShowSelectedRouteOnMap(_selectedRoute);
                    UpdateDistanceText();
                    datRouteAssignmentList.ItemsSource = _routes;
                    mapRoute.PreviewMouseWheel += (sender, e) =>
                    {
                        mapRoute.ZoomLevel += e.Delta > 0 ? 1 : -1;
                        e.Handled = true;
                    };
                }
                else
                {
                    MessageBox.Show("No routes found for this driver.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     A method that sets up and displays route 
        ///     information on a map, including stops 
        ///     and the driver's current location.
        /// </summary>
        private void ShowSelectedRouteOnMap(Route_Assignment_VM route)
        {
            _mapRoute.Children.Clear();
            if (route != null)
            {
                var routeStops = _routeStopManager.GetRouteStopByRouteId(route.Route_ID);
                if (routeStops != null)
                {
                    try
                    {
                        LocationCollection locations = new LocationCollection();
                        foreach (var stop in routeStops)
                        {
                            // set up locations for Stop pushpins
                            double stopLatitude = (double)stop.stop.Latitude;
                            double stopLongitude = (double)stop.stop.Longitude;
                            Location stopLocation = new Location(stopLatitude, stopLongitude);
                            Pushpin stopPushpin = new Pushpin() { Location = stopLocation, Background = Brushes.Blue };
                            _mapRoute.Children.Add(stopPushpin);
                            locations.Add(stopLocation);
                            _mapRoute.Center = ((Pushpin)_mapRoute.Children[0]).Location;
                        }

                        Pushpin systemPushpin = new Pushpin() { Location = new Location(0, 0), Background = Brushes.Red };
                        _mapRoute.Children.Add(systemPushpin);

                        // get the systems location for pushpin
                        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
                        watcher.PositionChanged += async (sender, e) =>
                        {
                            var coord = e.Position.Location;
                            Location systemLocation = new Location(coord.Latitude, coord.Longitude);
                            systemPushpin.Location = systemLocation;
                            // Get the route line from Bing Maps API
                            BingMapsResponse bingMapsResponse = await _routeAssignmentManager.getRouteLineForRouteAssignmentVM(new List<Route_Assignment_VM> { route });
                            // Draw the route polyline on the map
                            MapPolyline line = new MapPolyline();
                            line.Locations = new LocationCollection();
                            line.Stroke = Brushes.Black;
                            line.StrokeThickness = 2;
                            // Add Bing Maps route coordinates to the polyline
                            foreach (var coordinateset in bingMapsResponse.ResourceSets[0].resources[0].routePath.line.coordinates)
                            {
                                line.Locations.Add(new Location(((List<double>)coordinateset)[0], ((List<double>)coordinateset)[1]));
                            }
                            mapRoute.Children.Add(line);
                        };
                        watcher.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error getting system's location: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     A method to update the Distance in the Distancetxt text box
        /// </summary>
        private void UpdateDistanceText()
        {
            if (_selectedRoute != null)
            {
                try
                {
                    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
                    watcher.PositionChanged += (sender, e) =>
                    {
                        var coord = e.Position.Location;
                        double distance = CalculateDistance(coord.Latitude, coord.Longitude, (double)_selectedRoute.stop.Latitude, (double)_selectedRoute.stop.Longitude);
                        Distancetxt.Text = $"{(int)Math.Round(distance)} miles";
                    };

                    // Check the status of the watcher
                    if (watcher.Status == GeoPositionStatus.Ready)
                    {
                        watcher.Start();
                    }
                    else
                    {
                        Distancetxt.Text = "Waiting for location...";
                        watcher.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting driver's location: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     A method to calculate the Distance between the 
        ///     systems location (Drivers location) and Stops location within a route
        /// </summary>
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            GeoCoordinate start = new GeoCoordinate(lat1, lon1);
            GeoCoordinate end = new GeoCoordinate(lat2, lon2);
            double distanceInMeters = start.GetDistanceTo(end);
            return distanceInMeters / 1609.34;
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     An event handler to update the map when a user double 
        ///     clicks a route on the list
        /// </summary>
        private void datRouteAssignmentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datRouteAssignmentList.SelectedItem is Route_Assignment_VM selectedRoute)
            {
                _selectedRoute = selectedRoute;
                ShowSelectedRouteOnMap(selectedRoute);
                UpdateDistanceText();
            }
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     An event handler to make zooming in easier
        /// </summary>
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            mapRoute.ZoomLevel += 1;
        }

        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-11
        /// <br />
        ///     An event handler to make zooming out easier
        /// </summary>
        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            mapRoute.ZoomLevel -= 1;
        }
    }
}