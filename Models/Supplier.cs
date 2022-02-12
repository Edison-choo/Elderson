using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Supplier
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Name")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Abbreviation")]
        public string SupplierAbbreviation { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Phone")]
        public string SupplierPhone { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Email")]
        public string SupplierEmail { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Website")]
        public string SupplierWebsite { get; set; }
        [Required(ErrorMessage = "Please enter Supplier Address")]
        public string SupplierAddress { get; set; }

        public string SuppliedMeds { get; set; }
    }
}
