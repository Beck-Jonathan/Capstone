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

namespace NightRiderWPF.RoleViews
{
    /// <summary>
    /// Interaction logic for CreateRole.xaml
    /// </summary>
    public partial class CreateRole : Page
    {
        private Role _role;
        private int charsLeft;
        private RoleManager _roleManager = null;
        public CreateRole()
        {
            _role = new Role();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            chkIsActive.IsEnabled = false;
            charsLeft = 255;
            

        }

        private void btnCreateRole_Click(object sender, RoutedEventArgs e)
        {
            _roleManager = new RoleManager();
            if (txtRoleName.Text.Length > 25)
            {
                MessageBox.Show("Please Input 25 or less characters for RoleName");
            }
            else if (txtRoleDescription.Text.Length > 255)
            {
                MessageBox.Show("Please Input 255 or less characters for RoleDescription");
            }
            else if (txtRoleName.Text == "")
            {
                MessageBox.Show("Please Input something in RoleName this is required");
            }
            else {
                try
                {
                    _role.RoleID = txtRoleName.Text;
                    _role.RoleDescription = txtRoleDescription.Text;

                    if (_roleManager.CreateRole(_role) == 1)
                    {
                        MessageBox.Show("Succesfully Inserted Role " + txtRoleName.Text, "Role Creation Wizard", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Couldn't Create Role, Please Try again", ex.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void txtRoleDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if(charsLeft - txtRoleDescription.Text.Length > 0)
            {
                lblCharsLeft.Content = "Characters Left: " + (charsLeft - txtRoleDescription.Text.Length);
            }
            else
            {
                lblCharsLeft.Content = "Max Characters";
            }
            

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No page to go back to.");
            }
        }
    }
}
