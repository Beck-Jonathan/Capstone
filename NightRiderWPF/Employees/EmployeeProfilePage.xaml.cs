/// <summary>
/// Steven Sanchez
/// Created: 2024/02/11
/// 
/// XAML to display records of  an Employee. 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 
using DataObjects;
using LogicLayer;
using LogicLayer.AppData;
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
using static System.Net.Mime.MediaTypeNames;
using static DataObjects.ValidationHelpers;
using NightRiderWPF.Login;
using NightRiderWPF.RouteStop;

namespace NightRiderWPF.Employees
{
    /// <summary>
    /// Interaction logic for EmployeeProfilePage.xaml
    /// </summary>
    public partial class EmployeeProfilePage : Page
    {
        private EmployeeManager _employeeManager;
        private Employee_VM _employee;
        public EmployeeProfilePage(Employee_VM employee)
        {
            _employee = employee;
            _employeeManager = new EmployeeManager();
            InitializeComponent();
        }
        /// <summary>
        ///   On Page load, use the manager and get records of an employee, then toss them on a text box.
        /// </summary>
        /// <param>
        ///    sender: The object that raised the event.
        ///    e: The event data.
        /// </param>
        /// <returns>
        ///     None
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions:Argument Exception
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>:Thrown when no employee is found in the database.
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-11 /// <br /><br />
        ///    UPDATER: Steven Sanchez
        /// <br />
        ///    UPDATED: 2024-02-27
        /// <br />
        ///     added username
        /// </remarks>


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            generateCBOStates(cboStates);
            try
            {

                if (_employee != null)
                {
                    GivenNametxt.Text = _employee.Given_Name;
                    FamilyNametxt.Text = _employee.Family_Name;
                    Phonetxt.Text = _employee.Phone_Number;
                    Citytxt.Text = _employee.City;
                    Emailtxt.Text = _employee.Email;
                    Addresstxt.Text = _employee.Address;
                    Ziptxt.Text = _employee.Zip;
                    txtCountry.Text = _employee.Country;
                    if (_employee.Country.ToUpper() == "US" || _employee.Country.ToUpper() == "USA")
                    {
                        Statetxt.Text = _employee.State;
                        Statetxt.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Statetxt.Visibility = Visibility.Hidden;
                    }
                    btnSubmit.Visibility = Visibility.Hidden;
                    updatebtn.Visibility = Visibility.Visible;
                    UpdatePasswordbtn.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Hidden;

                    UserNametxt.Text = _employee.Login.Username;
                }
                else
                {
                    MessageBox.Show("Employee not found.", "No data to show.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message,
                        "Employee Retrieval Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            updateProfileUI(true);
            //if not in USA, hide state field
            if (_employee.Country.ToUpper() == "US" || _employee.Country.ToUpper() == "USA")
            {
                Statetxt.Visibility = Visibility.Hidden;
                cboStates.Visibility = Visibility.Visible;
                cboStates.SelectedValue = _employee.State;
            }
            else
            {
                Statetxt.Visibility = Visibility.Hidden;
                cboStates.Visibility = Visibility.Hidden;
            }
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No previous page available.");
            }
        }

        private void ChangePasswordbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string state = "";
            if (txtCountry.Text.ToUpper() == "US" || txtCountry.Text.ToUpper() == "USA")
            {
                state = cboStates.SelectedValue.ToString().ToUpper();
            }


            if (!GivenNametxt.Text.IsValidGivenName())
            {
                MessageBox.Show("Please enter all required fields");
                errorHighlight(GivenNametxt);
                return;
            }
            if (!FamilyNametxt.Text.IsValidFamilyName())
            {
                MessageBox.Show("Please enter all required fields");
                errorHighlight(FamilyNametxt);
                return;
            }
            if (!Phonetxt.Text.isValidPhone())
            {
                MessageBox.Show("Please enter a valid phone number");
                errorHighlight(Phonetxt);
                return;
            }
            if (!Ziptxt.Text.isNotEmptyOrNull())
            {
                MessageBox.Show("Please enter all required fields");
                errorHighlight(Ziptxt);
                return;
            }
            if (!Citytxt.Text.isNotEmptyOrNull())
            {
                MessageBox.Show("Please enter all required fields");
                errorHighlight(Citytxt);
                return;
            }
            if (!Addresstxt.Text.isNotEmptyOrNull())
            {
                MessageBox.Show("Please enter all required fields");
                errorHighlight(Addresstxt);
                return;
            }

            else
            {
                Employee_VM updatedEmployee = new Employee_VM()
                {
                    Employee_ID = _employee.Employee_ID,
                    Given_Name = GivenNametxt.Text,
                    Family_Name = FamilyNametxt.Text,
                    Phone_Number = Phonetxt.Text,
                    DOB = _employee.DOB,
                    Country = txtCountry.Text.ToUpper(),
                    Zip = Ziptxt.Text,
                    Position = _employee.Position,
                    City = Citytxt.Text,
                    State = state.ToUpper(),
                    Address = Addresstxt.Text,
                    Address2 = Address2txt.Text.isNotEmptyOrNull() ? Address2txt.Text : "",
                    Email = Emailtxt.Text,
                    Roles = _employee.Roles,
                    Login = _employee.Login
                };
                try
                {
                    int rows = _employeeManager.EditEmployee(updatedEmployee, _employee);
                    if (rows > 0)
                    {

                        Authentication.AuthenticatedEmployee = updatedEmployee;
                        MessageBox.Show("Success!\n Please log out and back in to reflect changes");
                        updateProfileUI(false);
                        NavigationService.GoBack();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No employee information was updated");
                }
            }
        }

        //Handles UI between update view, and view view
        public void updateProfileUI(bool isBeingUpdated)
        {
            if (isBeingUpdated)
            {
                GivenNametxt.IsReadOnly = false;
                GivenNametxt.BorderThickness = new Thickness(3, 3, 3, 3);
                GivenNametxt.BorderBrush = Brushes.Black;
                FamilyNametxt.IsReadOnly = false;
                FamilyNametxt.BorderThickness = new Thickness(3, 3, 3, 3);
                FamilyNametxt.BorderBrush = Brushes.Black;
                Phonetxt.IsReadOnly = false;
                Phonetxt.BorderThickness = new Thickness(3, 3, 3, 3);
                Phonetxt.BorderBrush = Brushes.Black;
                Ziptxt.IsReadOnly = false;
                Ziptxt.BorderThickness = new Thickness(3, 3, 3, 3);
                Ziptxt.BorderBrush = Brushes.Black;
                Citytxt.IsReadOnly = false;
                Citytxt.BorderThickness = new Thickness(3, 3, 3, 3);
                Citytxt.BorderBrush = Brushes.Black;
                txtCountry.IsReadOnly = false;
                txtCountry.BorderThickness = new Thickness(3, 3, 3, 3);
                txtCountry.BorderBrush = Brushes.Black;
                Statetxt.BorderBrush = Brushes.Black;
                cboStates.IsEnabled = true;
                cboStates.BorderThickness = new Thickness(3, 3, 3, 3);
                cboStates.BorderBrush = Brushes.Black;
                Addresstxt.IsReadOnly = false;
                Addresstxt.BorderThickness = new Thickness(3, 3, 3, 3);
                Addresstxt.BorderBrush = Brushes.Black;
                Address2txt.IsReadOnly = false;
                updatebtn.Visibility = Visibility.Hidden;
                btnSubmit.Visibility = Visibility.Visible;
                UpdatePasswordbtn.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                GivenNametxt.IsReadOnly = true;
                GivenNametxt.BorderThickness = new Thickness(0, 0, 0, 0);
                FamilyNametxt.IsReadOnly = true;
                FamilyNametxt.BorderThickness = new Thickness(0, 0, 0, 0);
                Phonetxt.IsReadOnly = true;
                Phonetxt.BorderThickness = new Thickness(0, 0, 0, 0);
                Ziptxt.IsReadOnly = true;
                Ziptxt.BorderThickness = new Thickness(0, 0, 0, 0);
                Citytxt.IsReadOnly = true;
                Citytxt.BorderThickness = new Thickness(0, 0, 0, 0);
                txtCountry.IsReadOnly = true;
                txtCountry.BorderThickness = new Thickness(0, 0, 0, 0);
                cboStates.IsEnabled = false;
                cboStates.BorderThickness = new Thickness(0, 0, 0, 0);
                Statetxt.IsReadOnly = true;
                Statetxt.BorderThickness = new Thickness(0, 0, 0, 0);
                Addresstxt.IsReadOnly = true;
                Addresstxt.BorderThickness = new Thickness(0, 0, 0, 0);
                Address2txt.IsReadOnly = true;
                updatebtn.Visibility = Visibility.Visible;
                btnSubmit.Visibility = Visibility.Hidden;
                UpdatePasswordbtn.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Hidden;
            }
        }

        //highlight if error
        public void errorHighlight(TextBox field)
        {
            field.BorderThickness = new Thickness(3, 3, 3, 3);
            field.BorderBrush = Brushes.Red;
        }
        //reset error highlight
        public void resetErrorHighlight(TextBox field)
        {
            field.BorderThickness = new Thickness(3, 3, 3, 3);
            field.BorderBrush = Brushes.Black;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            updateProfileUI(false);
            GivenNametxt.Text = _employee.Given_Name;
            FamilyNametxt.Text = _employee.Family_Name;
            Phonetxt.Text = _employee.Phone_Number;
            Citytxt.Text = _employee.City;
            if (_employee.Country.ToUpper() == "US" || _employee.Country.ToUpper() == "USA")
            {
                Statetxt.Text = _employee.State;
                Statetxt.Visibility = Visibility.Visible;
            }
            else
            {
                Statetxt.Visibility = Visibility.Hidden;
            }
            Addresstxt.Text = _employee.Address;
            Ziptxt.Text = _employee.Zip;
        }

        public void generateCBOStates(ComboBox cbo)
        {
            List<string> states = new List<string>
            {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
                "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
            };
            foreach (string state in states)
            {
                cbo.Items.Add(state);
            }
        }


        private void txtCountry_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCountry.Text.ToUpper() == "US" || txtCountry.Text.ToUpper() == "USA")
            {
                cboStates.Visibility = Visibility.Visible;
                Statetxt.Visibility = Visibility.Hidden;
            }
            else
            {
                cboStates.Visibility = Visibility.Hidden;
                Statetxt.Visibility = Visibility.Hidden;
            }

        }
    }
}
