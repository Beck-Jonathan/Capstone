using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    //created by Jonathan Beck
    //3/18/2024
    public class Source
    {
        [Display(Name = "Vendor_Id")]
        [Required(ErrorMessage = "Please enter Vendor_Id ")]
        public int Vendor_Id { set; get; }
        [Display(Name = "Parts_inventory_id")]
        [Required(ErrorMessage = "Please enter Parts_inventory_id ")]
        public int Parts_inventory_id { set; get; }
        [Display(Name = "Vendor_Part_Number")]
        [Required(ErrorMessage = "Please enter Vendor_Part_Number ")]
        [StringLength(100)]
        public string Vendor_Part_Number { set; get; }
        [Display(Name = "Estimated_delivery_time_days")]
        [Required(ErrorMessage = "Please enter Estimated_delivery_time_days ")]
        public int Estimated_delivery_time_days { set; get; }
        [Display(Name = "Part_Price")]
        [Required(ErrorMessage = "Please enter Part_Price ")]
        public Decimal Part_Price { set; get; }
        [Display(Name = "Minimum_order_Qty")]
        [Required(ErrorMessage = "Please enter Minimum_order_Qty ")]
        public int Minimum_order_Qty { set; get; }
        [Display(Name = "Active")]
        [Required(ErrorMessage = "Please enter Active ")]
        public bool Active { set; get; }

    }
}
