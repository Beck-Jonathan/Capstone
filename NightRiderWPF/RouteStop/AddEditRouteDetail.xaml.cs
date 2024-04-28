using DataObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// Interaction logic for AddEditRouteDetail.xaml
    /// Created By: Nathan Toothaker
    /// </summary>
    public partial class AddEditRouteDetail : Window
    {
        private RouteVM _route;
        IRouteManager _routeManager;

        public AddEditRouteDetail()
        {
            _routeManager = new RouteManager();
            InitializeComponent();
            this.Title = "Add a Route";
        }
        public AddEditRouteDetail(IRouteManager routeManager)
        {
            _routeManager = routeManager;
            InitializeComponent();
            this.Title = "Add a Route";
        }
        public AddEditRouteDetail(RouteVM route, IRouteManager routeManager)
        {
            _route = route;
            _routeManager = routeManager;
            InitializeComponent();
            this.Title = "Edit Route Details";
            txtRouteName.Text = _route.RouteName;
            tpStartTime.Value = _route.StartTime.getStorageData();
            tpEndTime.Value = _route.EndTime.getStorageData();
            txtRouteCycleTime.Text = _route.RepeatTime.ToString();
            chkMonday.IsChecked = _route.isActiveOnDay("Monday");
            chkTuesday.IsChecked = _route.isActiveOnDay("Tuesday");
            chkWednesday.IsChecked = _route.isActiveOnDay("Wednesday");
            chkThursday.IsChecked = _route.isActiveOnDay("Thursday");   
            chkFriday.IsChecked = _route.isActiveOnDay("Friday");
            chkSaturday.IsChecked = _route.isActiveOnDay("Saturday");
            chkSunday.IsChecked = _route.isActiveOnDay("Sunday");

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan repeatTime;
            if (TimeSpan.TryParse(txtRouteCycleTime.Text, out repeatTime)){
                if (repeatTime.Days > 0)
                {
                    MessageBox.Show("Cannot support a route that lasts longer than a single day.\n" +
                        "Please split this into multiple smaller routes.", "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        string activeDays = FormValidationHelper.getActiveDays(
                            (bool)chkMonday.IsChecked, (bool)chkTuesday.IsChecked, (bool)chkWednesday.IsChecked,
                            (bool)chkThursday.IsChecked, (bool)chkFriday.IsChecked, (bool)chkSaturday.IsChecked, (bool)chkSunday.IsChecked);

                        RouteVM newRouteVM = new RouteVM()
                        {
                            RouteId = (_route == null ? 0 : _route.RouteId),
                            RouteName = txtRouteName.Text,
                            StartTime = new DataObjects.HelperObjects.Time((DateTime)tpStartTime.Value),
                            EndTime = new DataObjects.HelperObjects.Time((DateTime)tpEndTime.Value),
                            RepeatTime = repeatTime,
                            DaysOfService = new DataObjects.HelperObjects.ActivityWeek(activeDays.ToCharArray())
                        };
                        if (_route == null)
                        {
                            _routeManager.AddRoute(newRouteVM);
                            MessageBox.Show("Route successfully added!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            _routeManager.EditRoute(_route, newRouteVM);
                            MessageBox.Show("Route successfully updated!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Route not stored", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    this.DialogResult = true;
                }
            } else
            {
                MessageBox.Show("Cycle tyme must be of format hh:mm", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                // or other formats, but trying to push the user into a certain way of thinking.
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((_route == null ? "Add" : "Edit") + " Cancelled.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = false;
        }
    }
}
