using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Medication
    {
        public string Id { get; set; }
        [Required]
        public string MedName { get; set; }
        public string MedAbbreviation { get; set; }
        public string MedType { get; set; }
        public string MedDescription { get; set; }
        public string MedSupplierAbb { get; set; }
    }
}
