using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public string Clinic { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime BookDateTime { get; set; }
        [Required]
        public string Symptoms { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        public string PatientID { get; set; }
        [Required]
        public string DoctorID { get; set; }
    }
}
