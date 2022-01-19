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
        public int CurrentAmt { get; set; }
        public int MinimumAmt { get; set; }
        public double Price { get; set; }

        
    }

    
}
