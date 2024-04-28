using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
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
    /// Author: Nathan Toothaker
    /// Creation Date: 2024-04-23
    /// Interaction logic for AddStopToRoute.xaml
    /// </summary>
    public partial class AddStopToRoute : Window
    {
        private IRouteStopManager _routeStopManager;
        private IStopManager _stopManager;
        private IEnumerable<Stop> _stops;
        private RouteVM _route;
        public AddStopToRoute(IRouteStopManager routeStopManager, RouteVM route)
        {
            _routeStopManager = routeStopManager;
            _stopManager = new StopManager();
            InitializeComponent();
            refreshStopList();
            _route = route;
        }
        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        /// <remarks>
        /// code borrowed from StopList.xaml.cs
        /// </remarks>
        private void refreshStopList()
        {
            try
            {
                _stops = _stopManager.GetStops();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

                this.DialogResult = false;
            }
            grdStopList.ItemsSource = _stops;
        }
        /// <summary>
        /// AUTHOR: Nathan Toothaker
        /// CREATED: 2024-04-23
        /// Logic for adding a stop to a route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStop_Click(object sender, RoutedEventArgs e)
        {
            if (grdStopList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please pick a stop to add");
            }
            else
            {
                Stop toAdd = grdStopList.SelectedItem as Stop;
                try
                {
                    RouteStopVM adding = new RouteStopVM()
                    {
                        RouteId = _route.RouteId,
                        StopId = toAdd.StopId,
                        StopNumber = _route.RouteStops.Count() + 1,
                        OffsetFromRouteStart = _route.RouteStops.Any() ? _route.RouteStops.Last().OffsetFromRouteStart : new TimeSpan(0, 30, 0),
                        stop = toAdd
                    };

                    _routeStopManager.AddRouteStop(adding);

                    List<RouteStopVM> routeStops = _route.RouteStops.ToList();
                    routeStops.Add(adding);
                    _route.RouteStops = routeStops;

                    this.DialogResult = true;

                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
