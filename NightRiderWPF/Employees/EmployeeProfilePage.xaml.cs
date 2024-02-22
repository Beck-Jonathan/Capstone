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

namespace NightRiderWPF.Employees
{
    /// <summary>
    /// Interaction logic for EmployeeProfilePage.xaml
    /// </summary>
    public partial class EmployeeProfilePage : Page
    {
        private EmployeeManager _employeeManager;
        private Employee_VM _employee;
        public EmployeeProfilePage(Employee_VM employeeVM)
        {
            _employee = employeeVM;
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
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_employee != null)
                {
                    GivenNametxt.Text = _employee.Given_Name;
                    FamilyNametxt.Text = _employee.Family_Name;
                    Phonetxt.Text = _employee.Phone_Number;
                    Citytxt.Text = _employee.City;
                    Statetxt.Text = _employee.State;
                    Emailtxt.Text = _employee.Email;
                    Addresstxt.Text = _employee.Address;
                    Ziptxt.Text = _employee.Zip;
                }
                else
                {
                    MessageBox.Show("Employee not found.", "No data to show.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Employee Retrieval Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            



        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //_employeeManager.Update   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
