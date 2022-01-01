using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public string Nric { get; set; }
        public string HomeAddr { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyNum { get; set; }
        public string Relationship { get; set; }
        public string UserId { get; set; }
        public string CountryCode { get; set; }
    }
}
