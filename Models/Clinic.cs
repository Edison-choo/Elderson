using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Clinic
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeSpan OpeningHours { get; set; }
        public TimeSpan ClosingHours { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        
    }
}
