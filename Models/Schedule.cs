using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Availability { get; set; }
    }
}
