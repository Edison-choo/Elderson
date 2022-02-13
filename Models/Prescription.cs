using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Prescription
    {
        public string Id { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string FormId { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Symptoms { get; set; }
        public string BookingId { get; set; }
        public string DoctorId { get; set; }
        public bool IsPurchased { get; set; }
    }
}
