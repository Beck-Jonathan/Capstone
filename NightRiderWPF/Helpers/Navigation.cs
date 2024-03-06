using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NightRiderWPF.Helpers
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-24
    /// <br />
    ///     Provides static helper methods for page navigation in WPF
    /// </summary>
    public static class Navigation
    {
        /// <summary>
        ///     Navigates the main window to the provided page object
        /// </summary>
        /// <param name="page">
        ///    The page to navigate to
        /// </param>
        /// <returns>
        ///    <see cref="bool">Employee_VM</see>: true if navigation is not cancelled, otherwise false
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Page">page</see> page: The page to navigate to
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        public static bool Navigate(Page page)
        {
            return (Application.Current.MainWindow as MainWindow).PageViewer.Navigate(page);
        }
    }
}
