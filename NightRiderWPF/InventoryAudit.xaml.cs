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

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Max Fare
    /// CREATED: 2024-02-04 (yyyy-MM-dd)
    /// 
    ///     A class to give functionality to the InventoryAudit.xaml window
    ///     
    /// </summary>
    /// 
    /// <remarks>
    
    /// Jonsthan Beck
    /// Updated: 2024/02/06
    /// </remarks>
    /// 
    ///     Update comments go here, include method or methods were changed or added 
    ///     (no other details necessary).
    ///     A new remark should be added for each update.
    /// </remarks>
    public partial class InventoryAudit : Window
    {
        private Parts_InventoryManager _parts_inventoryManager;
        public  Parts_Inventory _part;

        //test constuctor
        public InventoryAudit()
        {
            InitializeComponent();
        }

        public InventoryAudit(Parts_Inventory part)
        {
            InitializeComponent();
            _part = part;
            _parts_inventoryManager = new Parts_InventoryManager();
            FillPage();
            
        }
        /// <summary>
        /// CONTRIBUTOR: Max Fare
        /// CREATED: 2024-02-04
        /// 
        ///     A method that fills the page with correct values for a given part,
        ///     including returning the input field for the actual quantity on hand (QoH)
        ///     to an empty string
        ///     
        /// PARAMETERS:
        ///     <param name="a">
        ///         int: the value to be multiplied.
        ///     </param>
        /// 
        /// RETURNS:
        ///     <returns>int: the input value multiplied by x.</returns>
        ///     
        /// THROWS:
        ///     <exception cref="ArgumentOutOfRangeException">
        ///         a was not a positive number.         
        ///     </exception>
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Jonsthan Beck
        /// Updated: 2024/02/06
        /// </remarks>
        /// 
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        private void FillPage()
        {
            lblPartNumber.Content = "Part Number: " + _part.Parts_Inventory_ID.ToString();
            txtOnOrder.Text = _part.Ordered_Qty.ToString();
            txtStocklvl.Text = _part.Stock_Level.ToString();
            txtDescription.Text = _part.Item_Description;
            txtExpectedQoH.Text = _part.Part_Quantity.ToString();
            txtboxActualQoH.Text = "";
            //imgInvPart.Source =
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// CONTRIBUTOR: Max Fare
        /// CREATED: 2024-02-04
        /// 
        ///     A method that gets an updated quantity on hand value for the chosen part form the window,
        ///     and sends the value and part data to be updated in the database.
        ///     
        /// THROWS:
        ///     <exception cref="ArgumentException">
        ///         if the input value was not a number
        ///     </exception>
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Jonsthan Beck
        /// Updated: 2024/02/06
        /// </remarks>
        /// 
        ///
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(txtboxActualQoH.Text.ToString());
                if (x < 0) {
                    throw new ArgumentException();
                }
            }
            catch {
                MessageBox.Show("Please enter a postive number");
                return;
            }

            if(txtboxActualQoH.Text.Length >= 1)
            {
                try
                {
                    Parts_Inventory newPart = new Parts_Inventory();
                    newPart.Parts_Inventory_ID = _part.Parts_Inventory_ID;
                    newPart.Part_Name = _part.Part_Name;
                    newPart.Part_Quantity = Convert.ToInt32(txtboxActualQoH.Text.ToString().Trim());
                    newPart.Item_Description = _part.Item_Description;
                    newPart.Item_Specifications = _part.Item_Specifications;
                    newPart.Part_Photo_URL = _part.Part_Photo_URL;
                    newPart.Ordered_Qty = _part.Ordered_Qty;
                    newPart.Stock_Level = _part.Stock_Level;
                        

                   
                    if(1 == _parts_inventoryManager.EditParts_Inventory(_part, newPart))
                    {
                        MessageBox.Show("Audit Successful");
                        txtboxActualQoH.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Incorrect amount of rows returned from function, update failed");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong, inventory not changed.\n");
                }
            }
            else
            {
                MessageBox.Show("The 'Actual QoH' field cannot be empty.");
            }
        }

        // Reviewed By: John Beck
    }
}
