using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class FormMeds
    {
        public string Id { get; set; }
        public string FormId { get; set; }
        public string MedicationId { get; set; }
        public int Quantity { get; set; }
        public string MedName { get; set; }
        public string MedType { get; set; }
    }
}
