using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class PatientDetails
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string DetailsofVisit { get; set; }
        
        public DateTime DateofVisit { get; set; }

        public int PatientID { get; set; }
    }
}
