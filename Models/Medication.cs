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
        [Required (ErrorMessage ="Please enter Medication Name")]
        public string MedName { get; set; }
        [Required(ErrorMessage = "Please enter Medication Abbreviation")]
        public string MedAbbreviation { get; set; }
        [Required(ErrorMessage = "Please select Medication Type")]
        public string MedType { get; set; }
        [Required(ErrorMessage = "Please enter Medication Description")]
        public string MedDescription { get; set; }

        [Required(ErrorMessage = "Please enter Medication Supplier")]
        public string MedSupplierAbb { get; set; }

        [Required(ErrorMessage = "Please enter Medication Allergy Ingredients")]
        public string MedAllergyIngredients { get; set; }
    }
}
