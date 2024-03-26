using DataObjects;
using LogicLayer;
using LogicLayer.Client;
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

namespace NightRiderWPF.Clients
{
    /// <summary>
    /// Interaction logic for GuardianViewEditDependent.xaml
    /// </summary>
    public partial class GuardianViewEditDependent : Page
    {
        private ClientManager _clientManager;
        private DependentManager _dependentManager;
        private ClientDependentRoleManager _cdrManager;
        private DependentVM _dependentVM;
        private DependentVM _updatedDependent;
        private Client_VM _client;
        private ClientDependentRole _clientDependentRole;


        public GuardianViewEditDependent(int clientID, int dependentID)
        {
            InitializeComponent();
            _clientManager = new ClientManager();
            _dependentManager = new DependentManager();
            _cdrManager = new ClientDependentRoleManager();
            _client = _clientManager.GetClientById(clientID);
            _dependentVM = _dependentManager.GetDependentByDependentId(dependentID);
            filterRoles(clientID, dependentID);
            _dependentVM.ClientDependentRoles = new List<ClientDependentRole> { _clientDependentRole };

        }


        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Load method populates boxes for views, sets settings to read-only
        /// as a 'View Detail' mode.
        /// </summary>
        ///   
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // object population


            // global settings
            lblTitle.Content = "View Dependent for: " + _client.FamilyName + ", " + _client.GivenName;
            btnSubmitDependent.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            btnDeleteDependent.Visibility = Visibility.Visible;
            btnEditDependent.Visibility = Visibility.Visible;
            // TODO update delete functionality when feature is added
            btnDeleteDependent.IsEnabled = false;

            // set fields
            closeFields();
        }

        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// Edit button changes presentation layer properties for edit functionality
        /// </summary>
        ///   
        private void btnEditDependent_Click(object sender, RoutedEventArgs e)
        {   // presentation management
            openFields();
            btnDeleteDependent.Visibility = Visibility.Collapsed;
            btnEditDependent.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Visible;
            btnSubmitDependent.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// Submit button validates form, updates db with new info,
        /// updates local Dependent object based on the version stored in db
        /// </summary>
        /// 
        private void btnSubmitDependent_Click(object sender, RoutedEventArgs e)
        {
            //create new dependent object
            _updatedDependent = new DependentVM();
            _updatedDependent.DependentID = _dependentVM.DependentID;
            _updatedDependent.IsActive = _dependentVM.IsActive;
            _updatedDependent.ClientDependentRoles = new List<ClientDependentRole> {
                new ClientDependentRole
                {
                    DependentID = _dependentVM.DependentID,
                    ClientID = _client.ClientID,
                    Relationship = ((ComboBoxItem)cboRelationship.SelectedItem).Content.ToString(),
                    IsActive = true
                }
            };

            // use validate method to check 
            if (validateData())
            { // Dialog Box Confirmation
                string messageBoxText = "Do you want to update this dependent?";
                string caption = "Confirm";
                MessageBoxButton btnConfirm = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, btnConfirm, icon, MessageBoxResult.Yes);

                if (result.ToString().Equals("Yes"))
                {
                    try // Call the update method
                    {
                        _dependentManager.EditDependent(_dependentVM, _updatedDependent, _client.ClientID);
                        string messageBoxText2 = "Dependent Updated";
                        string caption2 = "Success";
                        MessageBoxButton btnSuccess = MessageBoxButton.OK;
                        MessageBoxImage icon2 = MessageBoxImage.None;

                        MessageBox.Show(messageBoxText2, caption2, btnSuccess, icon, MessageBoxResult.OK);
                        // update the page's object for consistent logic if updating again
                        _dependentVM = _dependentManager.GetDependentByDependentId(_dependentVM.DependentID);
                        filterRoles(_client.ClientID, _dependentVM.DependentID);

                        // Close the fields and change buttons
                        closeFields();
                        btnSubmitDependent.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnEditDependent.Visibility = Visibility.Visible;
                        btnDeleteDependent.Visibility = Visibility.Visible;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxButton btnFailure = MessageBoxButton.OK;
                        MessageBox.Show(ex.Message, "Update Failed", btnFailure, icon);
                    }
                    this.NavigationService.Navigate(new GuardianViewDependentList(_client));
                }
            }
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// Not yet implemented
        /// </summary>
        /// 
        private void btnDeleteDependent_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// Changes presentation elements frim "Update mode" to "View mode"
        /// </summary>
        /// 
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            closeFields();
            btnSubmitDependent.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            btnEditDependent.Visibility = Visibility.Visible;
            btnDeleteDependent.Visibility = Visibility.Visible;


        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// makes values editable
        /// </summary>
        ///
        private void openFields()
        {
            // set label
            lblTitle.Content = "Edit Dependent for: " + _client.FamilyName + ", " + _client.GivenName;
            // allow edit
            cboRelationship.IsEnabled = true;
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtMiddleName.IsReadOnly = false;
            dtpBirthDate.IsEnabled = true;
            cboGender.IsEnabled = true;
            txtEmergencyContact.IsReadOnly = false;
            txtContactRelationship.IsReadOnly = false;
            txtEmergencyPhone.IsReadOnly = false;
            cboAccommodations.IsEnabled = true;
        }

        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// changes presentation layer properties for read only and
        /// stored values
        /// </summary>
        /// 
        private void closeFields()
        {
            // Set label
            lblTitle.Content = "View Dependent for: " + _client.FamilyName + ", " + _client.GivenName;
            //populate detail
            cboRelationship.SelectedValue = _clientDependentRole.Relationship;
            cboRelationship.SelectedItem = _clientDependentRole.Relationship;
            cboRelationship.Text = _clientDependentRole.Relationship;
            cboRelationship.IsEnabled = false;
            txtFirstName.Text = _dependentVM.GivenName;
            txtFirstName.IsReadOnly = true;
            txtLastName.Text = _dependentVM.FamilyName;
            txtLastName.IsReadOnly = true;
            txtMiddleName.Text = _dependentVM.MiddleName ?? "";
            txtMiddleName.IsReadOnly = true;
            dtpBirthDate.SelectedDate = _dependentVM.DOB;
            dtpBirthDate.IsEnabled = false;
            cboGender.SelectedValue = _dependentVM.Gender ?? "Not Disclosed";
            cboGender.SelectedItem = _dependentVM.Gender ?? "Not Disclosed";
            cboGender.Text = _dependentVM.Gender ?? "Not Disclosed";
            cboGender.IsEnabled = false;
            txtEmergencyContact.Text = _dependentVM.EmergencyContact ?? "";
            txtEmergencyContact.IsReadOnly = true;
            txtContactRelationship.Text = _dependentVM.ContactRelationship ?? "";
            txtContactRelationship.IsReadOnly = true;
            txtEmergencyPhone.Text = _dependentVM.EmergencyPhone ?? "";
            txtEmergencyPhone.IsReadOnly = true;
            cboAccommodations.IsEnabled = false;
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Selects the matching client dependent role for the page's
        /// particular combination of client and dependent
        /// </summary>
        ///  
        private void filterRoles(int clientID, int dependentID)
        {
            IEnumerable<ClientDependentRole> roles = _cdrManager.GetClientDependentRolesByClient(clientID);
            foreach (ClientDependentRole role in roles)
            {
                if (role.DependentID == dependentID)
                {
                    _clientDependentRole = role;
                }
            }
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// form validation in its own method for improved readability
        /// </summary>
        ///  
        private bool validateData()
        {
            // Validate update data
            bool givenNameisValid = false;
            bool familyNameisValid = false;
            bool middleNameisValid = false;
            bool dobIsValid = false;
            bool genderIsValid = false;
            bool emergencyContactIsValid = false;
            bool contactRelationIsValid = false;
            bool emergencyPhoneIsValid = false;

            if (ValidationHelpers.IsValidGivenName(txtFirstName.Text))
            {
                _updatedDependent.GivenName = txtFirstName.Text;
                errFirstName.Visibility = Visibility.Hidden;
                givenNameisValid = true;
            }
            else
            {
                errFirstName.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.IsValidFamilyName(txtLastName.Text))
            {
                _updatedDependent.FamilyName = txtLastName.Text;
                errLastName.Visibility = Visibility.Hidden;
                familyNameisValid = true;
            }
            else
            {
                errLastName.Visibility = Visibility.Visible;
            }

            if (txtMiddleName.Text.Length == 0)
            {
                _updatedDependent.MiddleName = null;
                errMiddleName.Visibility = Visibility.Hidden;
                middleNameisValid = true;
            }

            else if (ValidationHelpers.IsValidMiddleName(txtMiddleName.Text))
            {
                _updatedDependent.MiddleName = txtMiddleName.Text;
                errMiddleName.Visibility = Visibility.Hidden;
                middleNameisValid = true;
            }
            else
            {
                errMiddleName.Visibility = Visibility.Visible;
            }

            if (dtpBirthDate.SelectedDate.HasValue && ValidationHelpers.IsValidDate(dtpBirthDate.SelectedDate.Value.Date))
            {
                _updatedDependent.DOB = dtpBirthDate.SelectedDate.Value.Date;
                errDOB.Visibility = Visibility.Hidden;
                dobIsValid = true;
            }
            else
            {
                errDOB.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isNotEmptyOrNull(cboGender.Text))
            {
                _updatedDependent.Gender = ((ComboBoxItem)cboGender.SelectedItem).Content.ToString();
                errRGender.Visibility = Visibility.Hidden;
                genderIsValid = true;
            }
            else
            {
                errRGender.Visibility = Visibility.Visible;
            }
            if (ValidationHelpers.IsValidCompoundName(txtEmergencyContact.Text))
            {
                _updatedDependent.EmergencyContact = txtEmergencyContact.Text;
                errEmergencyContact.Visibility = Visibility.Hidden;
                emergencyContactIsValid = true;
            }
            else
            {
                errEmergencyContact.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isNotEmptyOrNull(txtContactRelationship.Text))
            {
                _updatedDependent.ContactRelationship = txtContactRelationship.Text;
                errContactRelationship.Visibility = Visibility.Hidden;
                contactRelationIsValid = true;
            }
            else
            {
                errContactRelationship.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isValidPhone(txtEmergencyPhone.Text))
            {
                _updatedDependent.EmergencyPhone = txtEmergencyPhone.Text;
                errEmergencyPhone.Visibility = Visibility.Hidden;
                emergencyPhoneIsValid = true;
            }
            else
            {
                errEmergencyPhone.Visibility = Visibility.Visible;
            }

            // checking fields and confirmation
            bool[] validationBools =
            {
                givenNameisValid,
                familyNameisValid,
                middleNameisValid,
                dobIsValid,
                genderIsValid,
                emergencyContactIsValid,
                contactRelationIsValid,
                emergencyPhoneIsValid
            };
            bool validationPasses = true;
            foreach (var v in validationBools)
            {
                if (v == false) { validationPasses = false; break; }
            }
            return validationPasses;
        }

    }
}
