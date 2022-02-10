using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Form
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public string BookingId { get; set; }
        [Required]
        public string TemplateName { get; set; }
    }
}
