using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Administrator
    {
        public string Id { get; set; }
        public string Clinic { get; set; }
        public int BookingCount { get; set; }
        public float Earnings { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingHours { get; set; }
        public int TotalBookings { get; set; }
        public string UserId { get; set; }
    }
}
