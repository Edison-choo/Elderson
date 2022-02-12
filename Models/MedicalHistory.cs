using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class MedicalHistory
    {
        public string Id { get; set; }
        [Required, Range(1, 50, ErrorMessage = "Name needs to be within 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
