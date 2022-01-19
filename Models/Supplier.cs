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
        [Required]
        public string SupplierName { get; set; }
        public string SupplierAbbreviation { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public string SuppplierWebsite { get; set; }
        public string SupplierAddress { get; set; }
        public string SuppliedMeds { get; set; }
    }
}
