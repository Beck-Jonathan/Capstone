/// <summary>
/// Steven Sanchez
/// Created: 2024/02/04
/// 
/// XAML to display all records of Employees. 
/// This will be updated to allow Adding and editing in a future sprint.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 
using DataObjects;
using LogicLayer;
using NightRiderWPF.DeveloperView;
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

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for AdminEmployeeListPage.xaml
    /// </summary>
    public partial class AdminEmployeeListPage : Page
    {
        EmployeeManager employeeManager = null;
        public AdminEmployeeListPage()
        {
            InitializeComponent();
        }


        /// <summary>
        ///   On Page load, use the manager and get all records, then toss them on a data grid.
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
        ///    <see cref="ArgumentException">ArgumentException</see>:Thrown when no employees are found in the database.
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-03
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
            employeeManager = new EmployeeManager();
            try
            {
                datEmployee_List.ItemsSource = employeeManager.GetEmployees();
                var columnsToRemove = new[] { "Employee_Roles", "Address2", "DOB" };
                foreach (var columnName in columnsToRemove)
                {
                    var columnToRemove = datEmployee_List.Columns.FirstOrDefault(c => c.Header.ToString() == columnName);
                    if (columnToRemove != null)
                    {
                        datEmployee_List.Columns.Remove(columnToRemove);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to load Employees" + ex.Message);
            }


        }

        private void datEmployee_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee_VM selected_employee = null;
            if(datEmployee_List.SelectedItem != null) 
            {
                selected_employee = datEmployee_List.SelectedItem as Employee_VM;
                NavigationService.Navigate(new AdminViewEmployee(employeeManager, selected_employee));

            }
            
            

        }

        private void AdminAddbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCreateNewEmployee());
        }
    }
}
// Checked by James Williams
