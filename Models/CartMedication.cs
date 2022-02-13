using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class CartMedication
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public Double Price { get; set; }
        public int Quantity { get; set; }
        public string DoctorID { get; set; }
    }
}
