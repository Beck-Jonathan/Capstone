/// <summary>
/// AUTHOR: Michael Springer
/// CREATED: 2024/02/04
/// 
///     Add Dependent Presentation page
///     
/// </summary>
/// 
/// <remarks>
/// 
/// </remarks>



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

namespace NightRiderWPF.Clients
{

    public partial class AddDependent : Page
    {
        private Dependent _dependent;
        private List<Accommodation> _accommodations;
        private Client _client;

        // TODO private User _user;

        public AddDependent()
        {
            _dependent = new Dependent();
            InitializeComponent();
        }

        public AddDependent(Client client)
        {

        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024/02/04
        /// Event Handler for form submit button -- calls
        /// validation methods for each field, prompts confirmation dialog,
        /// 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param

        private void btnSubmitDependent_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///
            /// Validates input fields and creates a dependent entry.
            /// 
            /// </summary>
            DependentManager _dependentManager = new DependentManager();
            bool givenNameisValid = false;
            bool familyNameisValid = false;
            bool middleNameisValid = false;
            bool dobIsValid = false;
            bool genderIsValid = false;
            bool emergencyContactIsValid = false;
            bool contactRelationIsValid = false;
            bool emergencyPhoneIsValid = false;

            // validate populate data object
            if (ValidationHelpers.IsValidGivenName(txtFirstName.Text))
            {
                _dependent.GivenName = txtFirstName.Text;
                errFirstName.Visibility = Visibility.Hidden;
                givenNameisValid = true;
            }
            else
            {
                errFirstName.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.IsValidFamilyName(txtLastName.Text))
            {
                _dependent.FamilyName = txtLastName.Text;
                errLastName.Visibility = Visibility.Hidden;
                familyNameisValid = true;
            }
            else
            {
                errLastName.Visibility = Visibility.Visible;
            }

            if (txtMiddleName.Text.Length == 0)
            {
                _dependent.MiddleName = null;
                errMiddleName.Visibility = Visibility.Hidden;
                middleNameisValid = true;
            }

            else if (ValidationHelpers.IsValidMiddleName(txtMiddleName.Text))
            {
                _dependent.MiddleName = txtMiddleName.Text;
                errMiddleName.Visibility = Visibility.Hidden;
                middleNameisValid = true;
            }
            else
            {
                errMiddleName.Visibility = Visibility.Visible;
            }

            if (dtpBirthDate.SelectedDate.HasValue && ValidationHelpers.IsValidDate(dtpBirthDate.SelectedDate.Value.Date))
            {
                _dependent.DOB = dtpBirthDate.SelectedDate.Value.Date;
                errDOB.Visibility = Visibility.Hidden;
                dobIsValid = true;
            }
            else
            {
                errDOB.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isNotEmptyOrNull(cboGender.Text))
            {
                _dependent.Gender = cboGender.Text;
                errRGender.Visibility = Visibility.Hidden;
                genderIsValid = true;
            }
            else
            {
                errRGender.Visibility = Visibility.Visible;
            }

            // ADD CONTENT FOR CLIENT IN NEXT FEATURE 
            // CLIENT DEPENDENT ROLE INFO:  cboRelationship.SelectedValue

            if (ValidationHelpers.IsValidCompoundName(txtEmergencyContact.Text))
            {
                _dependent.EmergencyContact = txtEmergencyContact.Text;
                errEmergencyContact.Visibility = Visibility.Hidden;
                emergencyContactIsValid = true;
            }
            else
            {
                errEmergencyContact.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isNotEmptyOrNull(txtContactRelationship.Text))
            {
                _dependent.ContactRelationship = txtContactRelationship.Text;
                errContactRelationship.Visibility = Visibility.Hidden;
                contactRelationIsValid = true;
            }
            else
            {
                errContactRelationship.Visibility = Visibility.Visible;
            }

            if (ValidationHelpers.isValidPhone(txtEmergencyPhone.Text))
            {
                _dependent.EmergencyPhone = txtEmergencyPhone.Text;
                errEmergencyPhone.Visibility = Visibility.Hidden;
                emergencyPhoneIsValid = true;
            }
            else
            {
                errEmergencyPhone.Visibility = Visibility.Visible;
            }

            // a little spaghetti until we can standardize validation
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
            if (validationPasses)
            {
                string messageBoxText = "Do you want to add this dependent?";
                string caption = "Confirm";
                MessageBoxButton btnConfirm = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, btnConfirm, icon, MessageBoxResult.Yes);

                if (result.ToString().Equals("Yes"))
                {
                    _dependentManager.AddDependent(_dependent);
                    string messageBoxText2 = "Dependent Added";
                    string caption2 = "Success";
                    MessageBoxButton btnSuccess = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.None;

                    MessageBox.Show(messageBoxText2, caption2, btnSuccess, icon, MessageBoxResult.OK);
                    resetForm();

                }
            }


            // ADD ACCOMMODATIONS IN NEXT FEATURE

            // cboAccommodations

        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024/12/12
        /// 
        /// Clears all fields on the form
        /// </summary>
        /// <remarks>
        /// Updated:
        /// </remarks>
        /// 

        public void resetForm()
        {
            cboRelationship.Text = String.Empty;
            cboRelationship.SelectedIndex = -1;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            if (txtMiddleName.Text != null)
            {
                txtMiddleName.Text = String.Empty;
            }
            dtpBirthDate.DisplayDate = DateTime.MaxValue;
            cboGender.Text = String.Empty;
            cboGender.SelectedIndex = -1;
            txtEmergencyContact.Text = String.Empty;
            txtContactRelationship.Text = String.Empty;
            txtEmergencyPhone.Text = String.Empty;
            if (cboAccommodations.Text != null)
            {
                cboAccommodations.Text = String.Empty;
                cboAccommodations.SelectedIndex = -1;
            }


        }

    }
}
