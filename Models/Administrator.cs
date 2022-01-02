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
        public Double Earnings { get; set; }
        public TimeSpan OpeningHours { get; set; }
        public TimeSpan ClosingHours { get; set; }
        public int TotalBookings { get; set; }
        public string UserId { get; set; }
    }
}
