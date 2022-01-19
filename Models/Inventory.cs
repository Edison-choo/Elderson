using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Inventory
    {
        public string Id { get; set; }
        public string MedicationId { get; set; }
        public int CurrentAmt { get; set; }
        public int MinimumAmt { get; set; }
        public double Price { get; set; }
    }
}
