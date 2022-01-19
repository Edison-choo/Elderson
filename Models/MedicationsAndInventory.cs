using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class MedicationsAndInventory
    {
        public List<Medication> Medications { get; set; }
        public List<MedInventory> Inventories { get; set; }
    }
}
