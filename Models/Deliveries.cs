using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Deliveries
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string PrescriptionId { get; set; }
        public string PackageId { get; set; }
        public string DeliveryCompany { get; set; }
        public string DeliveryStatus { get; set; }
        public string SentBy { get; set; }
    }
}
