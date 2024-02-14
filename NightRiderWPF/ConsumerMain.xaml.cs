using NightRiderWPF.DeveloperView;
using NightRiderWPF.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConsumerMain : Window
    {
        public ConsumerMain()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is ListBoxItem item)
            {
                switch (item.Name)
                {
                    case "SamplePage":
                        PageViewer.Navigate(new SamplePage());
                        break;
                        // Your cases here
                }
            }
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnClients")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
            }
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnEmployees")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
            }
        }

        private void btnVehicles_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnVehicles")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
            }
        }

        private void btnMaintenance_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Name == "btnMaintenance")
            {
                foreach (var child in stackMainNav.Children)
                {
                    if (child is Button button)
                    {
                        button.Background = Statics.SecondaryColor;
                    }
                }
                btn.Background = Statics.PrimaryColor;
            }
        }
    }
}
// checked by James Williams