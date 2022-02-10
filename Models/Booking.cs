using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Booking
    {
        public string Id { get; set; }
        [Required]
        public string Clinic { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime BookDateTime { get; set; }
        [Required]
        public string Symptoms { get; set; }
        [Required]
        public string PatientID { get; set; }
        [Required]
        public string DoctorID { get; set; }
        [Required]
        public string CallUUID { get; set; }
        public string FormId { get; set; }
    }
}
