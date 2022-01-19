using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class MedInventory
    {
        public string Id { get; set; }
        public string MedicationId { get; set; }

        [Required(ErrorMessage = "Please enter Medication's Current Amount")]
        public int CurrentAmt { get; set; }

        [Required(ErrorMessage = "Please enter Medication's Minimum Amount")]
        public int MinimumAmt { get; set; }

        [Required(ErrorMessage = "Please enter Medication's Price")]
        public double Price { get; set; }


        
    }

    
}
