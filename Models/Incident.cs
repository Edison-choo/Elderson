using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Timestamp { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Recommendation { get; set; }
        public string Report { get; set; }

        public string UserId { get; set; }
    }
}
