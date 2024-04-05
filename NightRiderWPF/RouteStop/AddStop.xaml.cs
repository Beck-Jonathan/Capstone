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
using DataObjects;
using System.Reflection.Emit;

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// Interaction logic for AddStop.xaml
    /// </summary>
    public partial class AddStop : Page
    {
        Stop _oldStop;
        private IStopManager _stopManager;
        public AddStop()
        {
            _stopManager = new StopManager();
            InitializeComponent();
            ckbIsActive.IsChecked = true;
            ckbIsActive.IsEnabled = false;
        }
        public AddStop(Stop oldStop) {
            _oldStop = oldStop;
            _stopManager = new StopManager();
            InitializeComponent();
            btnAddStop.Visibility = Visibility.Hidden;
            btnSaveStop.Visibility = Visibility.Visible;
            //street zip lat long active
            txtLatitude.Text = oldStop.Latitude.ToString();
            txtLongitude.Text = oldStop.Longitude.ToString();
            txtStreetAddress.Text = oldStop.StreetAddress;
            txtZipCode.Text = oldStop.ZIPCode;
            ckbIsActive.IsChecked = oldStop.IsActive;
            ckbIsActive.IsEnabled = true;
        }

        public AddStop(IStopManager stopManager)
        {
            _stopManager = stopManager;
            InitializeComponent();
            btnAddStop.Visibility = Visibility.Visible;
            btnSaveStop.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnAddStop_Click(object sender, RoutedEventArgs e)
        {
            int results = 0;
            try
            {
                // validation
                if (validate())
                {

                    // add stop
                    results = _stopManager.AddStop(new Stop()
                    {
                        StreetAddress = txtStreetAddress.Text,
                        ZIPCode = txtZipCode.Text,
                        Latitude = Decimal.Parse(txtLatitude.Text),
                        Longitude = Decimal.Parse(txtLongitude.Text),
                        IsActive = ckbIsActive.IsChecked.Value
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem adding the stop.\n" + ex.Message, "Add Stop Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (results != 0)
            {
                MessageBox.Show("Stop " + results + " was added.", "Stop Added", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?\nChanges will be lost!", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
        //Extracted Chris B's validation logic into a helper method so I could 
        //use it on my update function
        //Jonathan Beck - 4/2/2024
        private bool  validate() {

            if (!ValidationHelpers.isNotEmptyOrNull(txtStreetAddress.Text))
            {
                MessageBox.Show("Please enter a street address.", "Street Address", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStreetAddress.Focus();
                return false;
            }

            if (!ValidationHelpers.isValidZip(txtZipCode.Text))
            {
                MessageBox.Show("Please enter a valid zip code.", "Zip Code", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtZipCode.Focus();
                return false;
            }

            if (!ValidationHelpers.isValidLatitude(txtLatitude.Text))
            {
                MessageBox.Show("Please enter a valid Latitude.", "Latitdue", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLatitude.Focus();
                return false;
            }

            if (!ValidationHelpers.isValidLongitude(txtLongitude.Text))
            {
                MessageBox.Show("Please enter a valid Longitude.", "Longitude", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLongitude.Focus();
                return false;
            }
            return true;

        }
        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnSaveStop_Click(object sender, RoutedEventArgs e)
        {
            bool results = false;
            try
            {
                // validation
                if (validate()){

                    // create the new stop , grab the old stop, and update the stop
                    Stop newStop = new Stop {
                        StreetAddress = txtStreetAddress.Text,
                        ZIPCode = txtZipCode.Text,
                        Latitude = Decimal.Parse(txtLatitude.Text),
                        Longitude = Decimal.Parse(txtLongitude.Text),
                        IsActive = ckbIsActive.IsChecked.Value };
                    results = _stopManager.EditStop(_oldStop, newStop);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem adding the stop.\n" + ex.Message, "Edit Stop Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (results)
            {
                MessageBox.Show("Stop " + _oldStop.StopId + " was edited.", "Stop Edited", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
        }
    }
}
