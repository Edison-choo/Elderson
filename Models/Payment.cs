using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateTime { get; set; }
        public string Amount { get; set; }
        public string PaymentContext { get; set; }
        public string Receipt { get; set; }
        public int PatientID { get; set; }
    }
}
